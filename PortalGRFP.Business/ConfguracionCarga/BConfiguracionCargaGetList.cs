namespace PortalGRFP.Business.ConfguracionCarga
{
    using PortalGRFP.Data.ConfiguracionCarga;
    using PortalGRFP.Entities.Common;
    using PortalGRFP.Entities.Response;
    using System;

    public class BConfiguracionCargaGetList
    {
        private readonly DConfiguracionCargaGetList dConfiguracionCargaGetList;

        public BConfiguracionCargaGetList()
        {
            dConfiguracionCargaGetList = new DConfiguracionCargaGetList();
        }

        public ResponseList<ConfiguracionCarga> Execute()
        {
            var response = new ResponseList<ConfiguracionCarga>();
            try
            {
                response = dConfiguracionCargaGetList.Execute(0);
            }
            catch (Exception ex)
            {
                response.Message = "Error al realizar la consulta de información. " + ex.Message;
            }
            return response;
        }
    }
}
