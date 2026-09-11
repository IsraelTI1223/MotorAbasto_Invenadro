using PortalGRFP.Utilities.Core.Responses;
using System;
using System.Diagnostics;

namespace PortalGRFP.Utilities.Core.Interceptors
{
    /// <summary>
    /// Clase estatica que encapsula metodos con trazado y manejo de errores
    /// </summary>
    public static class CoreInterceptor
    {
        /// <summary>
        /// Metodo que realiza una traza en el procesamiento de un evento
        /// </summary>
        /// <typeparam name="TRequest">Tipo de parametros de entrada</typeparam>
        /// <typeparam name="TResponseModel">Tipo de modelo a retornar</typeparam>
        /// <param name="callBack">Evento delegado</param>
        /// <param name="request">Parametros de entrada</param>
        /// <returns>Respuesta simple trazada</returns>
        public static ResponseSimple<TResponseModel> Trace<TRequest, TResponseModel>(Func<TRequest, TResponseModel> callBack,
            TRequest request)
        {
            Stopwatch stopwatch = new Stopwatch();
            var response = new ResponseSimple<TResponseModel>();
            stopwatch.Start();
            try
            {
                response.Result = callBack(request);
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                if (ex.InnerException != null)
                {
                    response.Messages.Add(ex.InnerException.Message);
                }
                response.Messages.Add(ex.Message);
            }
            stopwatch.Stop();
            response.Elapsed = stopwatch.Elapsed.ToString();
            response.ElapsedMilliseconds = stopwatch.ElapsedMilliseconds;
            return response;
        }
    }
}