using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PortalGRFP.Data.DConsultaMTA;
using PortalGRFP.Entities.Common;
using PortalGRFP.Entities.Response;

namespace PortalGRFP.Business.BConsultaMTA
{
   public class GetListaConsultaMTABusiness
    {
        private readonly GetListaConsultarMTAData getListaConsultaMTAData;
        public GetListaConsultaMTABusiness()
        {
            getListaConsultaMTAData = new GetListaConsultarMTAData();
        }

        public ResponseList<SincronizacionInput> ObtenerConsultaMTA()
        {
            var response = new ResponseList<SincronizacionInput>();
            try
            {
                response = getListaConsultaMTAData.ObtenerConsultaMTA();
            }
            catch (Exception ex)
            {
                response.Message = "Error al realizar la consulta de información. " + ex.Message;
            }
            return response;
        }


        public ResponseList<DetalleInput> ObtenerConsultaDetalleMTA(string id_input)
        {
            var response = new ResponseList<DetalleInput>();
            try
            {
                response = getListaConsultaMTAData.ObtenerConsultaDetalleMTA(id_input);
            }
            catch (Exception ex)
            {
                response.Message = "Error al realizar la consulta de información. " + ex.Message;
            }
            return response;
        }

        public ResponseList<DetalleInputVenta> ObtenerConsultaCifrasMTA(string id_input, int user) //Me quede aqui
        {

            var response = new ResponseList<DetalleInputVenta>();              
            try
            {
                response = getListaConsultaMTAData.GetVentaDetalleMTA(id_input,user);
            }
            catch (Exception ex)
            {
                response.Message = "Error al realizar la consulta de información. " + ex.Message;
            }
            return response;
        }

        public ResponseList<DetalleInputInvenadro> ObtenerCifrasInvenadroMTA(string id_input,int user) //Me quede aqui
        {

            var response = new ResponseList<DetalleInputInvenadro>();
            try
            {
                response = getListaConsultaMTAData.GetInvenadroDetalleMTA(id_input,user);
            }
            catch (Exception ex)
            {
                response.Message = "Error al realizar la consulta de información. " + ex.Message;
            }
            return response;
        }

        public ResponseList<DetalleInputNegado> ObtenerCifrasNegadoMTA(string id_input, int user) //Me quede aqui
        {

            var response = new ResponseList<DetalleInputNegado>();
            try
            {
                response = getListaConsultaMTAData.GetNegadoDetalleMTA(id_input, user);
            }
            catch (Exception ex)
            {
                response.Message = "Error al realizar la consulta de información. " + ex.Message;
            }
            return response;
        }

        public ResponseList<DetalleInputLista> ObtenerCifrasListaMTA(string id_input, int user) //Me quede aqui
        {

            var response = new ResponseList<DetalleInputLista>();
            try
            {
                response = getListaConsultaMTAData.GetListaDetalleMTA(id_input, user);
            }
            catch (Exception ex)
            {
                response.Message = "Error al realizar la consulta de información. " + ex.Message;
            }
            return response;
        }

        public ResponseList<DetalleInputTransitt> ObtenerCifrasTransitoMTA(string id_input, int user) //Me quede aqui
        {

            var response = new ResponseList<DetalleInputTransitt>();
            try
            {
                response = getListaConsultaMTAData.GetTransitoDetalleMTA(id_input, user);
            }
            catch (Exception ex)
            {
                response.Message = "Error al realizar la consulta de información. " + ex.Message;
            }
            return response;
        }


    }
}
