using System;

namespace PortalGRFP.Entities.Attributes
{
    /// <summary>
    /// Atributo personalizado para mapedo de archivos .DAT
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class DatLayoutAttribute : Attribute
    {
        /// <summary>
        /// Inicio de la cadena
        /// </summary>
        public short Start { get; set; }
        /// <summary>
        /// Total de caractares a recuperar
        /// </summary>
        public short Length { get; set; }
        /// <summary>
        /// Formato de fecha
        /// </summary>
        public string DateFormat { get; set; }
        /// <summary>
        /// Constructor comun
        /// </summary>
        /// <param name="start">Inicio de la cadena</param>
        /// <param name="length">Tamaño de la cadena</param>
        public DatLayoutAttribute(short start = 0, short length = 0, 
            string dateFormat = "dd/MM/yyyy")
        {
            Start = start;
            Length = length;
            DateFormat = dateFormat;
        }
    }
}