using PortalGRFP.Entities.Enums;
using System.Collections.Generic;

namespace PortalGRFP.Entities.Models.DatLayouts
{
    /// <summary>
    /// Interfaz para validar tamaño de cadena permitida para procesar
    /// </summary>
    public interface IDatLayout
    {
        /// <summary>
        /// Metodo de suma validada
        /// </summary>
        /// <returns>Tamaño total de la cadena mapeada</returns>
        int GetCheckSum();
        /// <summary>
        /// Metodo para obtener el tipo de carga destino en base de datos
        /// </summary>
        /// <returns>Tipo de carga destino</returns>
        TipoCargas GetTipoCarga();
        /// <summary>
        /// Metodo para obtener el tipo de mapeo
        /// </summary>
        /// <returns>Tipo de layout mapeado</returns>
        LayoutTypes GetTipoLayout();
        /// <summary>
        /// Metodo para obtener errores en lectura de layout
        /// </summary>
        /// <returns>Lista de errores</returns>
        List<string> GetErrorMessages();
    }
}