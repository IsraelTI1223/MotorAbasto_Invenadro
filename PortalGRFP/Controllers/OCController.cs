
using PortalGRFP.Business.OC;
using PortalGRFP.Data.OrdenCompra;
using PortalGRFP.Entities.Common.Sugerido;
using PortalGRFP.Extensions;
using PortalGRFP.Extensions.FTP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PortalGRFP.Controllers
{
    public class OCController : Controller
    {
        // GET: OC
        public ActionResult Index()
        {

            ComboList cmbDatos = new ComboList();
            var idsugerido = cmbDatos.ComboSugeridoId();
            TempData["IdSugerido"] = idsugerido;


            return View();
        }


        [HttpPost]
        public ActionResult Index(string id_sugerido)
        {
            Session["id_sugerido"] = id_sugerido;

            var user = this.GetUsuario();

            if (id_sugerido == null)
            {

                ComboList cmbDatos = new ComboList();
                var idsugerido = cmbDatos.ComboSugeridoId();
                TempData["IdSugerido"] = idsugerido;
            }
            else
            {
                var idsugerido = id_sugerido;
            }

            GetListaRHBusiness CREACIONOC = new GetListaRHBusiness();
            var idUs = CREACIONOC.InsertOC(id_sugerido, user.IdUsuario.ToString());


            var listreporte = new GetListaRHBusiness().GetConsultaOrdenesCompra(id_sugerido);


            return View(listreporte);

        }


        public ActionResult OrdenesCompra()
        {
            //llenar combos
            //int usuario = this.GetUsuario().IdUsuario;

            //var proveedores = new OrdenCompraBLL().GetListProveedoresBLL(usuario);
            //TempData["ProvSugeridoAplicado"] = proveedores.Result;

            return View();
        }

        public ActionResult GetSucursalesXProveedor(string proveedores)
        {
            int usuario = this.GetUsuario().IdUsuario;

            var sucursales = new OrdenCompraBLL().GetListSucursalesBLL(usuario, proveedores).Result;

            var json = Json(sucursales, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = 500000000;
            return json;
        }

        public ActionResult GetListaSugeridoAplicado(string sucursales)
        {
            int usuario = this.GetUsuario().IdUsuario;

            //var sugerido = new OrdenCompraBLL().GetListaSugeridoAplicadoBLL(usuario, sucursales);
            var sugerido = new OrdenCompraBLL().GetListaSugeridoAplicadoBLL(usuario);
            var json = Json(sugerido, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = 500000000;
            return json;
        }

        [HttpPost]
        public JsonResult GenerarOrdenCompra(List<GenerarOrdenModel> model)
        {
            int user = this.GetUsuario().IdUsuario;
            var resp = new OrdenCompraBLL().GenerarOrdenCompraBLL(model, user);

            var json = Json(resp, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = 500000000;
            return json;
        }

        public ActionResult GetProveedoresOC()
        {
            int usuario = this.GetUsuario().IdUsuario;

            var proveedores = new OrdenCompraBLL().GetListProveedoresBLL(usuario);
            //TempData["ProvSugeridoAplicado"] = proveedores.Result;

            var json = Json(proveedores, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = 500000000;
            return json;

        }

        public ActionResult GetListOrdenesCompra(string sucursales)
        {
            int usuario = this.GetUsuario().IdUsuario;

            var ordenes = new OrdenCompraBLL().GetListOrdenesCompraBLL(usuario, sucursales);

            var json = Json(ordenes, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = 500000000;
            return json;
        }

        [HttpPost]
        public JsonResult EliminarOrdenCompra(List<EliminarOrdenModel> model)
        {
            int user = this.GetUsuario().IdUsuario;
            var resp = new OrdenCompraBLL().EliminarOrdenCompraBLL(model, user);

            var json = Json(resp, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = 500000000;
            return json;
        }

        [HttpPost]
        public JsonResult EnviarOrdenesCompra(List<EliminarOrdenModel> model)
        {
            int user = this.GetUsuario().IdUsuario;

            var resp = new OrdenCompraBLL().EnviarOrdenesCompraBLL(model, user);

            var json = Json(resp, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = 500000000;
            return json;
        }

    }
}
