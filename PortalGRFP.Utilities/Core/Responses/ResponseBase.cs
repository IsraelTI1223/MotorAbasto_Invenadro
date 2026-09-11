using System;
using System.Collections.Generic;

namespace PortalGRFP.Utilities.Core.Responses
{
    /// <summary>
    /// Clase abstracta que encapsula una respuesta de procesamiento
    /// </summary>
    public abstract class ResponseBase
    {
        /// <summary>
        /// Estado del proceso
        /// </summary>
        public bool Success { get; set; }
        /// <summary>
        /// Servidor que ejecuta el ensamblado
        /// </summary>
        public string Server { get; set; }
        /// <summary>
        /// Tiempom total de procesamiento
        /// </summary>
        public string Elapsed { get; set; }
        /// <summary>
        /// Tiempo total de procesamiento en milisegundos
        /// </summary>
        public long ElapsedMilliseconds { get; set; }
        /// <summary>
        /// Mensajes 
        /// </summary>
        public List<string> Messages { get; set; }
        /// <summary>
        /// Contrstructos por defecto
        /// </summary>
        public ResponseBase()
        {
            Messages = new List<string>();
            Server = Environment.MachineName;
        }
    }
}