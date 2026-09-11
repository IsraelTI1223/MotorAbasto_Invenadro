using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PortalGRFP.Business.ComprasEspeciales;
using PortalGRFP.Business.ComprasEspecialesBLL;
using PortalGRFP.Business.Configuracion_Invenadro;
using PortalGRFP.Entities.Common.ComprasEspeciales;
using PortalGRFP.Extensions;
using static PortalGRFP.Entities.Common.ComprasEspeciales.ConsultaPedidosModel;
using PortalGRFP.Entities.Common;


namespace PortalGRFP.Controllers
{
    public class ComprasEspecialesController : Controller
    {
        // GET: ComprasEspeciales
        public ActionResult ConsultaPedido()
        {

            var cbGrupos = new ComprasEspecialesBLL().GetGrupoListCompraBLL(this.GetUsuario().IdUsuario);


            TempData["listGrupos"] = cbGrupos.Result;


            return View();
        }


        //[HttpPost]
        //public ActionResult GetSucursalesXGrupo(FormCollection data)
        //{
        //    int grupo = int.Parse(data["grupo"].ToString());

        //    var sucursales = new ComprasEspecialesBLL().GetGrupoSucursalBLL(grupo, this.GetUsuario().IdUsuario).Result;

        //    //var tbPedidos = new ComprasEspecialesBLL().GetPedidosListCompraBLL();
        //    //Session["tbPedidos"] = tbPedidos.Result;

        //    var json = Json(new { data = sucursales }, JsonRequestBehavior.AllowGet);
        //    json.MaxJsonLength = 500000000;
        //    return json;
        //}

        public ActionResult ConsultarPedidos(string search)
        {

            var tbPedidos = new ComprasEspecialesBLL().GetPedidosListCompraBLL(search, this.GetUsuario().IdUsuario);
            //Session["tbPedidos"] = tbPedidos.Result;

            var json = Json(tbPedidos.Result, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = 500000000;
            return json;
        }

        public ActionResult EliminarPedidos(string folio, string id_tipo, string id_proveedor, string id_suc, string search)
        {

            var resp = new ComprasEspecialesBLL().DeletePedidosListCompraBLL(folio,id_tipo,id_proveedor,id_suc, search, this.GetUsuario().IdUsuario);
            //Session["tbPedidos"] = tbPedidos.Result;

            var json = Json(resp, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = 500000000;
            return json;
        }
        public ActionResult FiltrarPedidos(string texto)
        {
            if (texto != null)
            {

                List<PedidosConsultaModel> listPedidos = (List<PedidosConsultaModel>)Session["tbPedidos"];

                var jsonArticulo = (from N in listPedidos
                                    where N.IdSucursal.ToString().Contains(texto.ToLower())
                                    select N);

                return Json(jsonArticulo, JsonRequestBehavior.AllowGet);
            }

            return null;

        }


        public ActionResult GeneracionPedido()
        {
            //var sugTipoPedido = new SugeridoBusiness().Filtros(1);
            //TempData["TipoPedido"] = sugTipoPedido.Result;


            //var SugProveedor = new SugeridoBusiness().Filtros(2);
            //TempData["TipoProveedor"] = SugProveedor.Result;

            return View();
        }


        [HttpPost]
        //public JsonResult CargaSugerido(FormCollection formulario)
        //{
        //    int tipoPedido = int.Parse(formulario["tipoPedido2"].ToString());
        //    int tipoProveedor = int.Parse(formulario["tipoProveedor2"].ToString());
        //    string fecha = formulario["txtGenOC2"].ToString();

        //    var user = this.GetUsuario();
        //    var cargaMasiva = new SugeridoBusiness().CargaSugerido(Request.Files, tipoPedido, tipoProveedor, fecha, this.GetUsuario().IdUsuario);

        //    var json = Json(cargaMasiva, JsonRequestBehavior.AllowGet);
        //    json.MaxJsonLength = 500000000;
        //    return json;
        //}
        public JsonResult CargaSugerido(FormCollection formulario)
        {
            
            //int tipoProveedor = int.Parse(formulario["tipoProveedor2"].ToString());
            string fecha = formulario["txtGenOC2"].ToString();

            var user = this.GetUsuario();
            var cargaMasiva = new SugeridoBusiness().CargaSugerido(Request.Files, fecha, this.GetUsuario().IdUsuario);

            var json = Json(cargaMasiva, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = 500000000;
            return json;
        }

        public JsonResult ConsultaMasiva(FormCollection sugerido)
        {
            Int64 folio = Int64.Parse(sugerido["Folio"].ToString());
            string fecha = sugerido["Fecha"].ToString();
            var cargaMasiva = new SugeridoBusiness().ConsultaPedidosCargados(folio, this.GetUsuario().IdUsuario,fecha);

            var json = Json(cargaMasiva, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = 500000000;
            return json;
        }

        public JsonResult BorrarCarag(FormCollection sugerido)
        {
            Int64 folio = Int64.Parse(sugerido["Folio"].ToString());
            string fecha = sugerido["Fecha"].ToString();

            var cargaMasiva = new SugeridoBusiness().BorrarPedido(folio, this.GetUsuario().IdUsuario, fecha);

            var json = Json(cargaMasiva, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = 500000000;
            return json;
        }

        public JsonResult EnviarSugerido(FormCollection sugerido)
        {
            
            string fecha = sugerido["Fecha"].ToString();

            var sugeridoEnviar = new SugeridoBusiness().EnviarSugeridoBLL(this.GetUsuario().IdUsuario, fecha);

            var json = Json(sugeridoEnviar, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = 500000000;
            return json;
        }

        [HttpPost]
        public JsonResult EliminarPedidosList(List<PedidosEspecialesDeleteModel> model)
        {
            int user = this.GetUsuario().IdUsuario;
            var resp = new ComprasEspecialesBLL().DeletePedidosListGroupCompraBLL(model, user);

            var json = Json(resp , JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = 500000000;
            return json;
        }

        public ActionResult GetSucursalesXGrupo(string search)
        {

            var sucursales = new ComprasEspecialesBLL().GetGrupoSucursalBLL(search, this.GetUsuario().IdUsuario).Result;

            var json = Json(new { data = sucursales }, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = 500000000;
            return json;
        }


    }
}