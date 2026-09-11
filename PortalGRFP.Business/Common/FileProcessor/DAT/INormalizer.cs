using PortalGRFP.Entities.Enums;
using PortalGRFP.Entities.Models.DatLayouts;
using System.Collections.Generic;

namespace PortalGRFP.Business.Common.FileProcessor.DAT
{
    /// <summary>
    /// Interfaz que define el comportamiento de 
    /// un procesador de archivos
    /// </summary>
    public interface INormalizer
    {
        /// <summary>
        /// Metodo que indica el tipo de layout a procesar
        /// </summary>
        /// <returns>Tipo de layout para procesar</returns>
        LayoutTypes GetLayoutType();
        /// <summary>
        /// Metodo que define el procesamiento del layout
        /// </summary>
        /// <param name="datos">Lista de cadenas para procesar mapeo</param>
        /// <returns>Lista mapeada</returns>
        IEnumerable<IDatLayout> Process(IEnumerable<string> datos);
    }
}