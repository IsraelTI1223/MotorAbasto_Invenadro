using PortalGRFP.Data.OC;
using PortalGRFP.Entities.Common;
using PortalGRFP.Entities.Common.Sugerido;
using PortalGRFP.Entities.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalGRFP.Business.OC
{
    public class OrdenCompraBLL
    {
        private readonly OrdenCompraData oOC = new OrdenCompraData();



        public ResponseList<ComboGenerico> GetListProveedoresBLL(int usuario)
        {
            var response = new ResponseList<ComboGenerico>();
            try
            {
                response = oOC.GetListProveedoresData(usuario);
            }
            catch (Exception ex)
            {
                response.Message = "Error al realizar la consulta de información. " + ex.Message;
            }
            return response;
        }

        public ResponseList<ComboGenerico> GetListSucursalesBLL(int usuario, string proveedores)
        {
            var response = new ResponseList<ComboGenerico>();
            try
            {
                response = oOC.GetListSucursalesData(usuario, proveedores);
            }
            catch (Exception ex)
            {
                response.Message = "Error al realizar la consulta de información. " + ex.Message;
            }
            return response;
        }


        //public ResponseList<SugeridoAplicadoModel> GetListaSugeridoAplicadoBLL(int usuario, string sucursales)
        //{
        //    var response = new ResponseList<SugeridoAplicadoModel>();
        //    try
        //    {
        //        response = oOC.GetListaSugeridoAplicadoData(usuario, sucursales);
        //    }
        //    catch (Exception ex)
        //    {
        //        response.Message = "Error al realizar la consulta de información. " + ex.Message;
        //        response.Success = false;
        //    }
        //    return response;
        //} 
        public ResponseList<SugeridoAplicadoModel> GetListaSugeridoAplicadoBLL(int usuario)
        {
            var response = new ResponseList<SugeridoAplicadoModel>();
            try
            {
                response = oOC.GetListaSugeridoAplicadoData(usuario);
            }
            catch (Exception ex)
            {
                response.Message = "Error al realizar la consulta de información. " + ex.Message;
                response.Success = false;
            }
            return response;
        }

        public Response GenerarOrdenCompraBLL(List<GenerarOrdenModel> model, int usuario)
        {
            var response = new Response();
            try
            {
                response = oOC.GenerarOrdenCompraData(model, usuario);
            }
            catch (Exception ex)
            {
                response.Message = "Error al realizar la consulta de información. " + ex.Message;
                response.Success = false;
            }
            return response;
        }



        public ResponseList<OrdenCompraModel> GetListOrdenesCompraBLL(int usuario, string sucursales)
        {
            var response = new ResponseList<OrdenCompraModel>();
            try
            {
                response = oOC.GetListOrdenesCompraData(usuario, sucursales);
            }
            catch (Exception ex)
            {
                response.Message = "Error al realizar la consulta de información. " + ex.Message;
                response.Success = false;
            }
            return response;
        }

        public Response EliminarOrdenCompraBLL(List<EliminarOrdenModel> model, int usuario)
        {
            var response = new Response();
            try
            {
                response = oOC.EliminarOrdenCompraData(model, usuario);
            }
            catch (Exception ex)
            {
                response.Message = "Error al realizar la consulta de información. " + ex.Message;
                response.Success = false;
            }
            return response;
        }

        public Response EnviarOrdenesCompraBLL(List<EliminarOrdenModel> model, int usuario)
        {
            var response = new Response();
            try
            {
                response = oOC.EnviarOrdenesCompraData(model, usuario);
            }
            catch (Exception ex)
            {
                response.Message = "Error al realizar la consulta de información. " + ex.Message;
                response.Success = false;
            }
            return response;
        }

        //public Response EnviarOrdenesCompraFTP(List<EliminarOrdenModel> model, int usuario)
        //{
        //    var response = new Response();
        //    try
        //    {
        //        response = oOC.EnviarOrdenesCompraData(model, usuario);
        //    }
        //    catch (Exception ex)
        //    {
        //        response.Message = "Error al realizar la consulta de información. " + ex.Message;
        //        response.Success = false;
        //    }
        //    return response;
        //}
    }
}
