using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PortalGRFP.Entities.Common;
using PortalGRFP.Entities.Response;
using static PortalGRFP.Entities.Common.ComprasEspeciales.ConsultaPedidosModel;
using PortalGRFP.Data.ComprasEspecialesData;
using static PortalGRFP.Entities.Common.MantenimientoArticulo.ArticuloPermisoCompraModel;
using PortalGRFP.Entities.Common.ComprasEspeciales;

namespace PortalGRFP.Business.ComprasEspecialesBLL
{
    public class ComprasEspecialesBLL
    {
        private readonly ComprasEspecialesData oCompData = new ComprasEspecialesData();
        public ResponseList<GruposListViewsModel> GetGrupoListCompraBLL(int user)
        {
            var response = new ResponseList<GruposListViewsModel>();
            try
            {
                response = oCompData.GetGrupoListCompraData(user);
            }
            catch (Exception ex)
            {
                response.Message = "Error al realizar la consulta de información. " + ex.Message;
            }
            return response;
        }
        public ResponseList<CadenasSucursalesModel> GetGrupoSucursalBLL(/*int grupo*/string grupos, int user)
        {
            var response = new ResponseList<CadenasSucursalesModel>();
            try
            {
                response = oCompData.GetGrupoSucursalData(grupos,user);
            }
            catch (Exception ex)
            {
                response.Message = "Error al realizar la consulta de información. " + ex.Message;
            }
            return response;
        }
        public ResponseList<PedidosConsultaModel> GetPedidosListCompraBLL(string search, int user)
        {
            var response = new ResponseList<PedidosConsultaModel>();
            try
            {
                response = oCompData.GetPedidosListCompraData(search,user);
            }
            catch (Exception ex)
            {
                response.Message = "Error al realizar la consulta de información. " + ex.Message;
            }
            return response;
        }

        public ResponseList<PedidosConsultaModel> DeletePedidosListCompraBLL(string folio, string id_tipo, string id_proveedor, string id_suc, string search, int user)
        {
            var response = new ResponseList<PedidosConsultaModel>();
            try
            {
                response = oCompData.DeletePedidosListCompraData(folio, id_tipo, id_proveedor, id_suc, search,user);
            }
            catch (Exception ex)
            {
                response.Message = "Error al realizar la consulta de información. " + ex.Message;
            }
            return response;
        }

        public ResponseList<PedidosConsultaModel> DeletePedidosListGroupCompraBLL(List<PedidosEspecialesDeleteModel> model, int user)
        {
            var response = new ResponseList<PedidosConsultaModel>();
            try
            {
                response = oCompData.DeletePedidosListGroupCompraData(model, user);
            }
            catch (Exception ex)
            {
                response.Message = "Error al realizar la consulta de información. " + ex.Message;
                response.Success = false;

            }
            return response;
        }
    }
}
