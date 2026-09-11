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
    public class ReporteSucursalesNuevasController : Controller
    {
        // GET: ReporteSucursalesNuevas
        public ActionResult Index()
        {
            var usuario = this.GetUsuario();
            if (usuario == null)
            {
                return RedirectToAction("Login", "Account");
            }


            Reportes reportes = new Reportes();
            var listData = reportes.GetSucursalesNuevas();

            return View(listData);
        }

        public void DescargarExcel(string flag)
        {
            Reportes reportes = new Reportes();

            var excel = reportes.GetExcelSucursalesNuevas();

            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment: filename=report.xlsx");
            Response.BinaryWrite(excel);
            Response.End();
        }
    }
}