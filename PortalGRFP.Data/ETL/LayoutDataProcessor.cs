using PortalGRFP.Data.Extensions;
using PortalGRFP.Entities.Models;
using PortalGRFP.Entities.Models.DatLayouts;
using PortalGRFP.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace PortalGRFP.Data.ETL
{
    public class LayoutDataProcessor<TModel> : DBContext
        where TModel : IDatLayout
    {
        /// <summary>
        /// Metodo para procesar layout en DB
        /// </summary>
        /// <param name="datos">Datos transformados</param>
        /// <param name="nombreArchivo">nombre del archivo</param>
        /// <param name="idUsuario">Identificador unico del usuario que procesa el archivo</param>
        public void ProcessData(IEnumerable<TModel> datos, string nombreArchivo, int idUsuario)
        {
            var ok = datos.Where(f => f.GetErrorMessages().Count == 0).ToList();
            var error = datos.Where(f => f.GetErrorMessages().Count > 0).ToList();

            var muestra = datos.FirstOrDefault();
            var xmlOK = Serializer.Serialize(ok);
            var xmlError = Serializer.Serialize(error);

            var command = Context.GetStoredProcCommand("SP_GRFP_LOAD_LAYOUT");
            command.Parameters.Add(new SqlParameter("@IdTipoCarga", (int)muestra.GetTipoCarga()));
            command.Parameters.Add(new SqlParameter("@NombreArchivo", nombreArchivo));
            command.Parameters.Add(new SqlParameter("@TotalRegistros", datos.Count()));
            command.Parameters.Add(new SqlParameter("@TotalCargados", ok.Count));
            command.Parameters.Add(new SqlParameter("@TotalErrores", error.Count));
            command.Parameters.Add(new SqlParameter("@UsuarioRegistro", idUsuario));
            command.Parameters.Add(new SqlParameter("@dataOK", xmlOK));
            command.Parameters.Add(new SqlParameter("@dataERROR", xmlError));
            command.Parameters.Add(new SqlParameter("@TipoLayout", (int)muestra.GetTipoLayout()));

            var dr = Context.ExecuteReader(command);
        }
        /// <summary>
        /// Metodo para obtener la configuracion de carga de archivos
        /// </summary>
        /// <param name="idTipoCarga">Tipo de carga</param>
        /// <returns>Configuracion del dia</returns>
        public CtrlCarga GetLoadConfig(int idTipoCarga)
        {
            CtrlCarga response = null;
            var command = Context.GetStoredProcCommand("GRFP_CTRL_CARGA_GET");
            command.Parameters.Add(new SqlParameter("@IdTipoCarga", idTipoCarga));
            using (IDataReader dr = Context.ExecuteReader(command))
            {
                while (dr.Read())
                {
                    response = new CtrlCarga();
                    response.ArchivosPorDia = dr.Get<int>("ArchivosPorDia");
                    response.NumeroCargas = dr.Get<int>("NumeroCargas");
                    response.HoraFin = dr.Get<TimeSpan>("HoraFin");
                    response.HoraInicio = dr.Get<TimeSpan>("HoraInicio");
                    response.FechaUltimaCarga = dr.Get<DateTime>("FechaUltimaCarga");
                }
            }
            return response;
        }
    }
}