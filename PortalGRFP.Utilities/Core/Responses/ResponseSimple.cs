namespace PortalGRFP.Utilities.Core.Responses
{
    /// <summary>
    /// Clase que implementa una respuesta generica simple
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ResponseSimple<T> : ResponseBase
    {
        /// <summary>
        /// Datos resultado
        /// </summary>
        public T Result { get; set; }
    }
}