using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PortalGRFP.Business.AlmacenVirtual;
using PortalGRFP.Extensions;

namespace PortalGRFP.Controllers
{
    public class AlmacenVirtualController : Controller
    {
        // GET: AlmacenVirtual
        public ActionResult AlmacenVirtualUp()
        {
            return View();
        }

        [HttpPost]
        public JsonResult CargaAlmacen(FormCollection formulario)
        {
            var files = Request.Files;
            var archivo = files["fileExcel"];
            string fecha = DateTime.Now.ToString("dd/MM/yyyy");
            AlamacenVirtualBusiness cargaAlamacen = new AlamacenVirtualBusiness();
            var cargaMasiva = cargaAlamacen.CargaAlmacen(files, fecha, this.GetUsuario().IdUsuario);
            //var user = this.GetUsuario();
            //var cargaMasiva = new SugeridoBusiness().CargaSugerido(Request.Files, tipoPedido, tipoProveedor, fecha, this.GetUsuario().IdUsuario);
            var Usuario = Session["dtoUsuario"];
            cargaMasiva.tblCargaAV.ToList().ForEach(j => j.usuario = ((PortalGRFP.Entities.Models.UsuarioModel)Usuario).Correo);
            //    myList.Where(w => w.Name == "dTomi").ToList().ForEach(i => i.Marks = 35);
            var json = Json(cargaMasiva, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = 500000000;
            return json;
        }

        [HttpPost]
        public JsonResult updateAlmacen(string Serializado)
        {
            var files = Request.Files;
            var archivo = files["fileExcel"];
            string fecha = DateTime.Now.ToString("dd/MM/yyyy");
            AlamacenVirtualBusiness cargaAlamacen = new AlamacenVirtualBusiness();
            var cargaMasiva = cargaAlamacen.procesaAlmacenV(Serializado, 2);
            //var user = this.GetUsuario();
            //var cargaMasiva = new SugeridoBusiness().CargaSugerido(Request.Files, tipoPedido, tipoProveedor, fecha, this.GetUsuario().IdUsuario);

            var json = Json(cargaMasiva, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = 500000000;
            return json;
        }


        /*seccion para la alta de almacenes en mantenimiento*/
        public ActionResult AlmacenVirtualManto()
        {
            return View();
        }

        public JsonResult AlmacenLoad(int opcion, string Nombre="", string Cadena="")
        {
            AlamacenVirtualBusiness cargaAlamacen = new AlamacenVirtualBusiness();
            var cargaMasiva = cargaAlamacen.procesaAlmacenLoad(opcion, this.GetUsuario().IdUsuario,Nombre,Cadena);
            var json = Json(cargaMasiva, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = 500000000;
            return json;
        }
    }
}
