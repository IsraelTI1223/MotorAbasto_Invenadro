using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using OfficeOpenXml;
using PortalGRFP.Business.AlmacenVirtual;
using PortalGRFP.Data.GeneralesAbasto;
using PortalGRFP.Entities.Common;
using PortalGRFP.Entities.Common.Capacity;
using PortalGRFP.Entities.Response;

namespace PortalGRFP.Business.GeneralesAbasto
{
    public class GeneralesAbastoBLL
    {
        private readonly GeneralesAbastoData gAbasto = new GeneralesAbastoData();

        public NegadosViewModel GetNegadosBLL()
        {
            var lst = gAbasto.GetNegadosData();

            return lst;
        }
        public int UINegadosBLL(NegadosViewModel model)
        {
            return gAbasto.UINegadosData(model);
        }


        public List<PedidosCompEspecialesViewModel> GetPedidosComprasEspecialesBLL(int flag, int id)
        {
            var listUsrs = gAbasto.GetPedidosComprasEspecialesData(flag, id).ToList();

            return listUsrs;
        }

        public Response UIPedidosComprasEspecialesBLL(PedidosCompEspecialesViewModel model)
        {
            var response = new Response();

            try
            {
                response = gAbasto.UIPedidosComprasEspecialesData(model);

            }
            catch (Exception e)
            {

                response.Message = e.Message.ToString();
                response.Success = false;

                Console.WriteLine(e.Message.ToString());

            }
            return response;
        }


        public AlgoritmoCompraViewModel GetAlgoritmoCompraBLL()
        {
            var lst = gAbasto.GetAlgoritmoCompraData();

            return lst;
        }

        public int UIAlgoritmoCompraBLL(AlgoritmoCompraViewModel model)
        {
            return gAbasto.UIAlgoritmoCompraData(model);
        }
        public List<ComboSucursalesProveedor> GetTiendasBLL(int flag)
        {
            var lista = gAbasto.GetTiendasList(flag).ToList();

            return lista;
        }
        public Response CargaExcepcionesInvenadroBll(ExcepcionesInvenadroModel consulta, int usuario)
        {
            var response = new Response();
            try
            {
                response = gAbasto.CargaExcepcionesData(consulta,usuario);

            }
            catch (Exception e)
            {

                response.Message = e.Message.ToString();
                response.Success = false;

            }
            return response;
        }

        public ResponseList<ExcepcionesInvenadroModel> ConsultaRExecpcionesBLL(ExcepcionesInvenadroModel consulting)
        {
            var response = new ResponseList<ExcepcionesInvenadroModel>();
            try
            {
                response = gAbasto.ExcepcionesoData(consulting);
            }
            catch (Exception ex)
            {
                response.Message = "Error al realizar la consulta de información. " + ex.Message;
                response.Success = false;
            }
            return response;
        }

        public ParametrosPVDpredictivo GetParamsPVD()
        {
            var lst = gAbasto.GetParamsPVDdata();

            return lst;
        }
        public int UIParamsPVDBLL(ParametrosPVDpredictivo model)
        {
            return gAbasto.UIParamsPVDData(model);
        }



    }
}
