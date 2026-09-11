    using PortalGRFP.Business.Mantenimiento;
using PortalGRFP.Entities.Common;
using PortalGRFP.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PortalGRFP.Controllers
{
    public class TopedeCompraController : Controller
    {
        // GET: TopedeCompra
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CargaTopedeCompra(FormCollection mant)
        {

            var cargaMasiva = new TopedeCompraBLL().CargaTopeBll(Request.Files, this.GetUsuario().IdUsuario);

            var json = Json(cargaMasiva, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = 500000000;
            return json;
        }
        public ActionResult ConsultaTope()
        {
            var ordenes = new TopedeCompraBLL().ConsultaTopeBLL();

            var json = Json(ordenes, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = 500000000;
            return json;
        }
        public ActionResult DescargarExcel()
        {
            TopedeCompraBLL tope = new TopedeCompraBLL();

            TempData["file"] = null;

            TempData["file"] = tope.GetExcelTope();

            return null;
        }
        public virtual ActionResult Descargar()
        {
            byte[] data = TempData["file"] as byte[];

            var filename = "Configuración Tope de Compra" + DateTime.Now.ToString() + ".xlsx";

            return File(data, "excel/xlsx", filename);
        }
    }
}
