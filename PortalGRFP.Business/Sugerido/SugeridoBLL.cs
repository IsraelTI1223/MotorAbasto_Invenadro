using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PortalGRFP.Entities.Response;
using PortalGRFP.Data.Sugerido;
using PortalGRFP.Entities.Common.Sugerido;
using PortalGRFP.Entities.Common;

namespace PortalGRFP.Business.Sugerido
{
    public class SugeridoBLL
    {
        private readonly SugeridoData oSugerido = new SugeridoData();


        public ResponseList<ListaSugeridoModel> GetListaSugeridoBLL(int usuario)
        {
            var response = new ResponseList<ListaSugeridoModel>();
            try
            {
                response = oSugerido.GetListaSugeridoData(usuario);
            }
            catch (Exception ex)
            {
                response.Message = "Error al realizar la consulta de información. " + ex.Message;
            }
            return response;
        }

        public ResponseList<ValidacionMontoSugeridoModel> GetListMontosValidarBLL(string idsugerido, int user)
        {
            var response = new ResponseList<ValidacionMontoSugeridoModel>();
            try
            {
                response = oSugerido.GetListMontosValidarData(idsugerido, user);
            }
            catch (Exception ex)
            {
                response.Message = "Error al realizar la consulta de información. " + ex.Message;
            }
            return response;
        }

        public Response DeleteSugeridoListBLL(string idsugerido, int user)
        {
            var response = new Response();
            try
            {
                response = oSugerido.DeleteSugeridoListData(idsugerido, user);
                response.Message = response.Success ? "La operación se realizo con exito." : "No se pudo completar la operación";
            }
            catch (Exception ex)
            {
                response.Message = "Error al realizar la consulta de información. " + ex.Message;
            }
            return response;
        }

        public Response UpdateSugeridoListBLL(List<ValidarMontosModel> model, int user)
        {
            var response = new Response();

            try
            {
                response = oSugerido.UpdateSugeridoListData(model, user);
            }
            catch (Exception ex)
            {
                response.Message = "Error al autorizar los montos" + ex.Message;
            }
            return response;
        }

        public Response AplicarSugeridoBLL(string idsugerido, int user, int estatus)
        {
            var response = new Response();

            try
            {
                response = oSugerido.AplicarSugeridoData(idsugerido, user, estatus);
            }
            catch (Exception ex)
            {
                response.Message = "Error al autorizar los montos" + ex.Message;
            }
            return response;
        }

        public Response GetValidarPermisoBLL(string Correo, int IdModulo, int usuario)
        {
            var response = new Response();

            try
            {
                response = oSugerido.GetValidarPermisoData(Correo, IdModulo, usuario);
            }
            catch (Exception ex)
            {
                response.Message = "Error al autorizar los montos" + ex.Message;
            }
            return response;
        }

        public ResponseList<DetalladoSugerido> GetDetalladoSugerido(Int64 idSugerido, int usuario)
        {
            var response = new ResponseList<DetalladoSugerido>();
            try
            {
                response = oSugerido.GetDetalleSugerido(idSugerido, usuario);
            }
            catch (Exception ex)
            {
                response.Message = "Error al realizar la consulta de información. " + ex.Message;
            }
            return response;
        }

        public int GetAccessValidarMontosBLL(long idsugerido, int user)
        {
            var response = 0;
            try
            {
                response = oSugerido.GetAccessValidarMontosData(idsugerido,user);
            }
            catch (Exception ex)
            {
                response = 0;
            }
            return response;
        }
    }
}
