using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PortalGRFP.Extensions;
using System.IO;
using PortalGRFP.Business.Reportes;
using System.Security.Cryptography;
using System.Web;

namespace PortalGRFP.Controllers
{
    public class ProductosNuevosController : Controller
    {
        // GET: ProductosNuevos
        public ActionResult Index()
        {
            var usuario = this.GetUsuario();
            if (usuario == null)
            {
                return RedirectToAction("Login", "Account");
            }


            Reportes reportes = new Reportes();
            var listReport = reportes.GetProductosNuevos();

            return View(listReport);
        }
        public void DescargarExcel(string flag)
        {
            Reportes reportes = new Reportes();

            var excel = reportes.getExcelProductosNuevos();

            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment: filename=report.xlsx");
            Response.BinaryWrite(excel);
            Response.End();
        }
    }
}