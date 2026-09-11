using PortalGRFP.Entities.Attributes;
using PortalGRFP.Entities.Enums;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
/// <summary>
/// Autor: William Gordillo Palomera
/// Ultimo cambio: 25-09-2020
/// </summary>
namespace PortalGRFP.Entities.Models.DatLayouts
{
    /// <summary>
    /// Clase abstracta que implementa suma validada de cadena mapeada
    /// </summary>
    public abstract class LayoutBase : IDatLayout
    {
        public TipoCargas TipoCargas { get; set; }
        public LayoutTypes TipoLayout { get; set; }

        private int _checkSum;
        /// <summary>
        /// Propiedad para manejo de errores
        /// </summary>
        public List<string> ErrorMessages { get; set; }
        /// <summary>
        /// Propiedad para conteo de linea
        /// </summary>
        public long RowNum { get; set; }
        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public LayoutBase() {
            ErrorMessages = new List<string>();
        }
        /// <summary>
        /// Metodo para obtener el mapeo de propiedades
        /// </summary>
        /// <typeparam name="TModel">Layout</typeparam>
        /// <returns>Mapeo substr({Inicio:NumeroCaracteres})</returns>
        public Dictionary<string, string> GetMetadataLayout<TModel>()
            where TModel : IDatLayout
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            PropertyInfo[] propiedades = typeof(TModel).GetProperties();
            var filter = propiedades.ToList()
                .Where(f => !f.Name.Equals("ErrorMessages"))
                .Where(f => !f.Name.Equals("TipoCargas"))
                .Where(f => !f.Name.Equals("TipoLayout"))
                .Where(f => !f.Name.Equals("RowNum"));
            foreach (PropertyInfo info in filter)
            {
                var metadata = info.GetCustomAttribute<DatLayoutAttribute>(true);
                result.Add(info.Name, $"{metadata.Start}:{metadata.Length}:{metadata.DateFormat}");
                _checkSum += metadata.Length;
            }
            return result;
        }
        /// <summary>
        /// Metodo para validar el tamaño de la cadena
        /// </summary>
        /// <returns>Tamaño de la cadena mapeada en el Layout</returns>
        public int GetCheckSum() => _checkSum;
        /// <summary>
        /// Metodo para obtener el tipo de carga destino en base de datos
        /// </summary>
        /// <returns>Tipo de carga destino</returns>
        public TipoCargas GetTipoCarga() => TipoCargas;
        /// <summary>
        /// Metodo para obtener el tipo de mapeo
        /// </summary>
        /// <returns>Tipo de layout mapeado</returns>
        public LayoutTypes GetTipoLayout() => TipoLayout;
        /// <summary>
        /// Metodo para obtener errores en lectura de layout
        /// </summary>
        /// <returns>Lista de errores</returns>
        public List<string> GetErrorMessages() => ErrorMessages;
    }
}