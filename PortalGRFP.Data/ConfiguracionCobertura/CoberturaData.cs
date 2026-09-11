using Microsoft.Practices.EnterpriseLibrary.Data;
using PortalGRFP.Data.Extensions;
using PortalGRFP.Entities.Common;
using PortalGRFP.Entities.Response;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalGRFP.Data.ConfiguracionCobertura
{
    public class CoberturaData
    {
        public ResponseList<CoberturaMontoPzaExt> ObtenerCoberturas(CoberturaMontoPza cobertura)//int idCarga
        {
            var response = new ResponseList<CoberturaMontoPzaExt>();

            var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");

            var command = db.GetStoredProcCommand("mta.sp_consulta_cobertura");
            command.CommandTimeout = 0;

            db.AddInParameter(command, "@idForma", DbType.Int32, cobertura.Forma);
            db.AddInParameter(command, "@tipoid", DbType.Int32, cobertura.Tipo);
            db.AddInParameter(command, "@monto_clasif", DbType.Int32, cobertura.ClasficacionMonto);
            db.AddInParameter(command, "@pza_clasif", DbType.Int32, cobertura.ClasficacionPieza);

            var read = db.ExecuteReader(command);
            response.Result = read.Reader(x => x.ToCoberturaMontoPzaExt());
            if (response.Result.Count == 0)
            {
                response.Message = "No hay Registros";
            }
            response.Success = response.Result.Any();

            return response;
        }

        public ResponseList<CatalogoGenerico_Conf> GetFormas(int catalogo)//1=formas 2=tipos 3=clasificacion 4=periodicidad
        {
            var response = new ResponseList<CatalogoGenerico_Conf>();

            var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");
            var command = db.GetStoredProcCommand("mta.sp_get_catalogosCobertura");//solo para inicializar

            db.AddInParameter(command, "@catalogo", DbType.Int32, catalogo);

            command.CommandTimeout = 0;

            var read = db.ExecuteReader(command);
            response.Result = read.Reader(x => x.ToCatalogoGenerico_Conf());
            response.Success = response.Result.Any();

            return response;
        }

        public Response GuardarCobertura(DataTable cobertura)//int idCarga
        {
            var response = new Response();

            var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");

            var command = db.GetStoredProcCommand("MTA.SP_GUARDACOBERTURA");
            command.CommandTimeout = 0;
            command.Parameters.Add(new SqlParameter("@Tipo_COBERTURAMONTOPZA", SqlDbType.Structured) { Value = cobertura });

            var exito = db.ExecuteNonQuery(command);
            response.Success = exito != default;

            return response;
        }

        public Response EliminarCobertura(int forma, int tipo, int monto, int pieza)//int idCarga
        {
            var response = new Response();

            var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");

            var command = db.GetStoredProcCommand("MTA.sp_EliminarCobertura");
            command.CommandTimeout = 0;
            db.AddInParameter(command, "@forma", DbType.Int32, forma);
            db.AddInParameter(command, "@tipo", DbType.Int32, tipo);
            db.AddInParameter(command, "@claMonto", DbType.Int32, monto);
            db.AddInParameter(command, "@claPza", DbType.Int32, pieza);

            var exito = db.ExecuteNonQuery(command);
            response.Success = exito != default;

            return response;
        } 
        public Response ValidarFormaData(int Idforma)
        {
            var response = new Response();

            var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");

            var command = db.GetStoredProcCommand("mta.SP_GRFP_GET_FORMA_CAT_COBERTURA");
            command.CommandTimeout = 0;

            using (IDataReader dr = db.ExecuteReader(command))
            {
                while (dr.Read())
                {
                    response.Message = dr.Get<string>("IDFORMA");
                }
            }
            response.Success = true;
            

            return response;
        }  
        
        public Response EliminarCambioFormaData(int Idforma, int Idusuario)
        {
            var response = new Response();

            var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");

            var command = db.GetStoredProcCommand("mta.SP_GRFP_CAMBIO_FORMA_CAT_COBERTURA");
            command.CommandTimeout = 0;
            db.AddInParameter(command, "@IdForma", DbType.Int32, Idforma);
            db.AddInParameter(command, "@IdUsuario", DbType.Int32, Idusuario);

            var exito = db.ExecuteNonQuery(command);
            response.Success = true;

            return response;
        }
    }
}
