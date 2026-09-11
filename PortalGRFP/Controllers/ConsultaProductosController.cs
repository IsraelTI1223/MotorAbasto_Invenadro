using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PortalGRFP.Business.Reportes;
using PortalGRFP.Extensions;

namespace PortalGRFP.Controllers
{
    public class ConsultaProductosController : Controller
    {
        // GET: ConsultaProductos
        public ActionResult Index()
        {
            Reportes reportes = new Reportes();
            //var listData = reportes.GetConsultaArticulos();

            //return View(listData);
            return View();
        }


        public void DescargarExcel(string flag)
        {
            Reportes reportes = new Reportes();

            var excel = reportes.GetExcelConsultaArticulos();

            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment: filename=report.xlsx");
            Response.BinaryWrite(excel);
            Response.End();
        }

        public ActionResult ConsultarProductos(string search)
        {

            var tbPedidos = new Reportes().GetProductosListBLL(search, this.GetUsuario().IdUsuario);
            
            var json = Json(tbPedidos.Result, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = 500000000;
            return json;
        }
    }
}