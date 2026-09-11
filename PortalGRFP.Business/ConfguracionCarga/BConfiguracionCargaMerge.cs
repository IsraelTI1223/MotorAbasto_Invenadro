namespace PortalGRFP.Business.ConfguracionCarga
{
    using PortalGRFP.Data.ConfiguracionCarga;
    using PortalGRFP.Entities.Common;
    using PortalGRFP.Entities.Request;
    using PortalGRFP.Entities.Response;
    using System;

    public class BConfiguracionCargaMerge
    {
        private readonly DConfiguracionCargaMerge dConfiguracionCargaMerge;

        public BConfiguracionCargaMerge()
        {
            dConfiguracionCargaMerge = new DConfiguracionCargaMerge();
        }

        public Response Execute(Request<ConfiguracionCarga> request)
        {
            var response = new Response();
            try
            {
                if (request.Parameters.HoraInicio < request.Parameters.HoraFin)
                {
                    response = dConfiguracionCargaMerge.Execute(request);
                    response.Message = response.Success ? "La operación se realizo con exito." : "No se pudo completar la operación";
                }
                else
                {
                    response.Message = "La hora inicio no puede ser mayor a la hora fin";
                }
            }
            catch (Exception ex)
            {
                response.Message = "Error al realizar la consulta de información. " + ex.Message;
            }
            return response;
        }
    }
}
