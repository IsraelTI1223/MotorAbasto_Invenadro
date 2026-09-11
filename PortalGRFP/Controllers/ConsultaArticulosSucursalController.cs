using PortalGRFP.Business.Reportes;
using PortalGRFP.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PortalGRFP.Controllers
{
    public class ConsultaArticulosSucursalController : Controller
    {
        // GET: ConsultaArticulosSucursal
        public ActionResult Index()
        {
            //var listreporte = new Reportes().GetConsultaArticulosSucursal();
            //return View(listreporte);
            var grupos = new Reportes().GetFiltrosArticuloSucursalBLL(this.GetUsuario().IdUsuario, 1,"");
            TempData["gruposArticuloSuc"] = grupos.Result;

            return View();
        }


        public void DescargarExcel()
        {
            Reportes reportes = new Reportes();

            var excel = reportes.GetExcelConsultaArticulosSucursal();

            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment: filename=report.xlsx");
            Response.BinaryWrite(excel);
            Response.End();
        }

        public ActionResult GetSucursalesXGrupo(string search)
        {

            var sucursales = new Reportes().GetFiltrosArticuloSucursalBLL(this.GetUsuario().IdUsuario, 2, search).Result;

            var json = Json(new { data = sucursales }, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = 500000000;
            return json;
        }
        public ActionResult ConsultarProductosXSuc(string storeId,string estatus)
        {

            var productos = new Reportes().GetProductosSucursalListBLL(this.GetUsuario().IdUsuario,storeId,estatus);

            var json = Json(productos.Result, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = 500000000;
            return json;
        }
    }
}