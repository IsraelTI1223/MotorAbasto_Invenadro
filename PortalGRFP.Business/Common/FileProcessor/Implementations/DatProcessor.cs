using PortalGRFP.Business.Common.FileProcessor.DAT;
using PortalGRFP.Entities.Enums;
using PortalGRFP.Entities.Models.DatLayouts;
using System.Collections.Generic;

namespace PortalGRFP.Business.Common.FileProcessor.Implementations
{
    /// <summary>
    /// Clase strategy, que encapsula el procesamientos de archivos .dat
    /// </summary>
    /// <typeparam name="TDatLayout">Layout generico con mapeo de archivo .DAT</typeparam>
    public class DatProcessor<TDatLayout> : INormalizer
        where TDatLayout : LayoutBase
    {
        /// <summary>
        /// Tipo de mapeo
        /// </summary>
        private LayoutTypes _type;
        /// <summary>
        /// Contructor de clase
        /// </summary>
        /// <param name="type">Tipo de mapeo</param>
        public DatProcessor(LayoutTypes type) { _type = type; }
        /// <summary>
        /// Metodo que retorna el tipo de mapeo implementado en el strategy
        /// </summary>
        /// <returns>Enum con tipo de layout</returns>
        public LayoutTypes GetLayoutType() => _type;
        /// <summary>
        /// Metodo que procesa el layout implementado en el strategy
        /// </summary>
        /// <param name="datos">Lista de cadenas para aplicar el mapeo</param>
        /// <returns>Lista mapeada</returns>
        public IEnumerable<IDatLayout> Process(IEnumerable<string> datos)
        => Normalizer.NormalizeLayout<TDatLayout>(datos);
    }
}