namespace PortalGRFP.Entities.Enums
{
    /// <summary>
    /// Catalogo de layouts mapeados
    /// </summary>
    public enum LayoutTypes
    {
        /// <summary>
        /// Layout de archivo .txt o .dat con cadena de 49 caracteres, 
        /// para LDCOM
        /// </summary>
        LDCOM49 = 49,
        /// <summary>
        /// Layout de archivo .DAT con cadena de 61 caracteres,
        /// para POPSAE
        /// </summary>
        POPSAE61 = 61,
        /// <summary>
        /// Layout de archivo .txt o .dat con cadena de 142 caracteres, 
        /// para FR -> NANDRO
        /// Layout del catálogo Productos
        /// </summary>
        NANDRO114 = 114,
        /// <summary>
        /// Layout de archivo .txt o .dat con cadena de 142 caracteres, 
        /// para FR -> FARMACOS | FANASA
        /// </summary>
        FARMACOS142 = 142,
        /// <summary>
        /// Layout de archivo .txt o .dat con cadena de 277 caracteres, 
        /// para FR -> MARZAM
        /// </summary>
        MARZAM277 = 277
    }
}