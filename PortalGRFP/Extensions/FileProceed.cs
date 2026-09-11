using PortalGRFP.Business.Common.Strategies;
using PortalGRFP.Business.ETL;
using PortalGRFP.Entities.Common;
using PortalGRFP.Entities.Models.DatLayouts;
using PortalGRFP.Utilities.Core.Responses;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace PortalGRFP.Extensions
{
    public static class FileProceed
    {
        /// <summary>
        /// Metodo para guardar archivo en el servidor
        /// </summary>
        /// <param name="file">Archivo</param>
        /// <returns>Nombre Archivo: Ruta destino</returns>
        public static Dictionary<string,string> SaveFile(this HttpPostedFileBase file) {
            string path = Path.Combine(Directory.GetDirectoryRoot(Directory.GetCurrentDirectory()), "origen");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string dat = Path.Combine(path, file.FileName);
            file.SaveAs(dat);
            return new Dictionary<string, string> { { file.FileName, dat } };
        }
        /// <summary>
        /// Metodo para obtener el Historial de archivos cargados
        /// </summary>
        /// <param name="context">Controlador</param>
        /// <returns>Lista de archivos cargados</returns>
        public static ResponseSimple<List<DescuentoHdr>> GetHistory(this Controller context) {
            var user = context.GetUsuario();
            return new HistoryBusiness().GetHistory(user.IdUsuario, Entities.Enums.TipoCargas.PMP_LDCOM);
        }
        /// <summary>
        /// Extension para leer datos de archivos .dat, txt
        /// </summary>
        /// <param name="context">Controlador</param>
        /// <param name="path">Ruta del archivo</param>
        /// <returns>Datos procesados</returns>
        public static ResponseSimple<IEnumerable<IDatLayout>> ReadDatFile(this Controller context, string path)
            => DatProcessingStrategy.ReadData(path);
        /// <summary>
        /// Extension para carga de datos en DB
        /// </summary>
        /// <param name="context"></param>
        /// <param name="datos">Datos procesados</param>
        /// <param name="nombreArchivo">Nombre del archivo</param>
        /// <param name="idUsuario">Usuario que carga el archivo</param>
        public static ResponseSimple<DateTime> LoadDatFile(this Controller context, IEnumerable<IDatLayout> datos, string nombreArchivo)
        {
            var user = context.GetUsuario();
            if (user != null)
            {
                return DatProcessingStrategy.LoadData(datos, nombreArchivo, user.IdUsuario);
            }
            else {
                return null;
            }
        }
    }
}