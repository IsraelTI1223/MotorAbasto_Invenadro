using PortalGRFP.Business.Common.FileProcessor.DAT;
using PortalGRFP.Business.Common.FileProcessor.Implementations;
using PortalGRFP.Business.ETL;
using PortalGRFP.Entities.Enums;
using PortalGRFP.Entities.Models.DatLayouts.FR.FARMACOS;
using PortalGRFP.Entities.Models.DatLayouts.FR.MARZAM;
using PortalGRFP.Entities.Models.DatLayouts.FR.NANDRO;
using PortalGRFP.Entities.Models.DatLayouts.LDCOM;
using PortalGRFP.Entities.Models.DatLayouts.POPSAE;
using System.Collections.Generic;
using System.Linq;

namespace PortalGRFP.Business.Common.Factories
{
    /// <summary>
    /// Clase statica que encapsula una Fablica de implementaciones 
    /// para procesamiento de layout de archivos .DAT
    /// </summary>
    public static class DatLayoutFactory
    {
        /// <summary>
        /// Repositorio privado de instancias
        /// </summary>
        private static readonly List<INormalizer> _repository = new List<INormalizer>
        {
            new DatProcessor<POPSAE1>(LayoutTypes.POPSAE61),
            new DatProcessor<LDCOM1>(LayoutTypes.LDCOM49),
#region [FR]
            new DatProcessor<MARZAM1>(LayoutTypes.MARZAM277),
            new DatProcessor<NANDRO1>(LayoutTypes.NANDRO114),
            new DatProcessor<FARMACOS1>(LayoutTypes.FARMACOS142)
#endregion
        };
        /// <summary>
        /// Repositorio privado de instancias para carga de datos en DB
        /// </summary>
        private static readonly List<ILoaderLayout> _loaderLayouts = new List<ILoaderLayout> {
            new LayoutLoader<LDCOM1>(TipoCargas.PMP_LDCOM,LayoutTypes.LDCOM49),
#region [FR]
            new LayoutLoader<NANDRO1>(TipoCargas.PMP_FR,LayoutTypes.NANDRO114),
            new LayoutLoader<MARZAM1>(TipoCargas.PMP_FR,LayoutTypes.MARZAM277),
            new LayoutLoader<FARMACOS1>(TipoCargas.PMP_FR,LayoutTypes.FARMACOS142),
#endregion
            new LayoutLoader<POPSAE1>(TipoCargas.PMP_POPSAE,LayoutTypes.POPSAE61)
        };
        /// <summary>
        /// Metodo que retorna una implementacion capas de procesar 
        /// un layout mapeado de archivo .DAT
        /// </summary>
        /// <param name="type">Tipo de layout a procesar</param>
        /// <returns>Instancia de procesador de archivo .DAT</returns>
        public static INormalizer GetReadProcessor(LayoutTypes type)
            => _repository.Find(f => f.GetLayoutType().Equals(type));
        /// <summary>
        /// Metodo que retorna una implementacion capas de procesar 
        /// un layout mapeado de archivo DAT
        /// </summary>
        /// <param name="types">Tipos de layout a procesar</param>
        /// <returns>Lista de instancias de procesador de archivo DAT</returns>
        public static List<INormalizer> GetReadProcessors(IEnumerable<LayoutTypes> types)
            => _repository.Where(f => types.Contains(f.GetLayoutType())).ToList();
        /// <summary>
        /// Metodo que retorna una implementacion capas de insertar 
        /// un layout en DB
        /// </summary>
        /// <param name="type">Tipo de carga DB</param>
        /// <returns>Instancia de procesador de archivos .dat, txt en DB</returns>
        public static ILoaderLayout GetLoadRocessor(TipoCargas type, LayoutTypes layout)
            => _loaderLayouts.Find(f => f.GetLoadLayoutType().Equals(type) && f.GetLayoutType().Equals(layout));
    }
}