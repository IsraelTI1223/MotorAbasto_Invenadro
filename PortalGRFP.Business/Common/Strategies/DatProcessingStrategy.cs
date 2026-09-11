using PortalGRFP.Business.Common.Factories;
using PortalGRFP.Business.Common.FileProcessor.DAT;
using PortalGRFP.Business.ETL;
using PortalGRFP.Entities.Enums;
using PortalGRFP.Entities.Models.DatLayouts;
using PortalGRFP.Entities.Models.DatLayouts.LDCOM;
using PortalGRFP.Utilities.Core.Interceptors;
using PortalGRFP.Utilities.Core.Responses;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PortalGRFP.Business.Common.Strategies
{
    public class DatProcessingStrategy
    {
        private static ILoaderLayout _loaderLayout;

        /// <summary>
        /// Metodo para encontrar un procesador de layout
        /// </summary>
        /// <param name="path">Archivo layout .dat, .txt</param>
        /// <param name="validarSegundaLinea">Bandera para forsar busqueda avanzada (Opcional)</param>
        /// <returns>Archivo con lectura transformada y trasada</returns>
        public static ResponseSimple<IEnumerable<IDatLayout>> ReadData(string path)
        {
            string[] lines = File.ReadAllLines(path);
            var chesum = lines.Select(s => (LayoutTypes)s.Length).Distinct().ToArray();
            INormalizer tem = DatLayoutFactory.GetReadProcessors(chesum).FirstOrDefault();
            if (tem != null)
            {
                return CoreInterceptor.Trace<IEnumerable<string>, IEnumerable<IDatLayout>>(tem.Process, lines);
            }
            else
            {
                return CoreInterceptor.Trace<IEnumerable<string>, IEnumerable<IDatLayout>>(ErrorProcess, lines);
            }
        }
        /// <summary>
        /// Metodo para carga de layout en base de datos
        /// </summary>
        /// <param name="datos">Layout procesado</param>
        /// <param name="nombreArchivo">Nombre del archivo procesado</param>
        /// <param name="idUsuario">Identificador unico del usuario que procesa el archivo</param>
        public static ResponseSimple<DateTime> LoadData(IEnumerable<IDatLayout> datos, string nombreArchivo, int idUsuario)
        {
            var request = new RequestLoadData { NombreArchivo = nombreArchivo, IdUsuario = idUsuario, Datos = datos };
            if (datos != null && datos.Count() > 0)
            {
                var muestra = datos.FirstOrDefault();
                _loaderLayout = DatLayoutFactory.GetLoadRocessor(muestra.GetTipoCarga(), muestra.GetTipoLayout());
                return CoreInterceptor.Trace(TraceLoadData, request);
            }
            else
            {
                return CoreInterceptor.Trace(ErrorProcess, request);
            }
        }
        private static DateTime TraceLoadData(RequestLoadData param) {
            if (_loaderLayout != null)
            {
                _loaderLayout.ProcessData(param.Datos, param.NombreArchivo, param.IdUsuario);
            }
            else {
                throw new Exception($"No se encontró un tipo de carga, para este archivo {param.NombreArchivo}");
            }
            return DateTime.Now;
        }

        /// <summary>
        /// Trasado de error
        /// </summary>
        /// <param name="firma">Firma de metodo</param>
        /// <returns>Mensaje de error trasado</returns>
        private static IEnumerable<IDatLayout> ErrorProcess(IEnumerable<string> firma) 
            => throw new Exception("No se encontró una estrategia para procesar el archivo");
        /// <summary>
        /// Trasado de error
        /// </summary>
        /// <param name="firma">Firma de metodo</param>
        /// <returns>Mensaje de error trasado</returns>
        private static DateTime ErrorProcess(RequestLoadData firma)
            => throw new Exception("No se encontraron datos para guardar en la DB");       
    }
    /// <summary>
    /// Clase local para encapsular parametros
    /// </summary>
    class RequestLoadData
    {
        public string NombreArchivo { get; set; }
        public int IdUsuario { get; set; }
        public IEnumerable<IDatLayout> Datos { get; set; }
    }
}