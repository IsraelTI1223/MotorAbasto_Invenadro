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
    public class CatArticulosController : Controller
    {
        public string draw = "";
        public string start = "";
        public string length = "";
        public string sortColumn = "";
        public string sortColumnDir = "";
        public string searchValue = "";
        public int pageSize, skip, recordsTotal;
        // GET: CatArticulos
        public ActionResult Index()
        {
            var usuario = this.GetUsuario();
            if (usuario == null)
            {
                return RedirectToAction("Login", "Account");
            }

           // Reportes reportes = new Reportes();
           // var listReport = reportes.GetProductsReport();

            return View();
        }

        public void DescargarExcel(string flag)
        {
            Reportes reportes = new Reportes();

            var excel = reportes.getExcelCatProduct();

            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment: filename=report.xlsx");
            Response.BinaryWrite(excel);
            Response.End();
        }


        [HttpPost]

        public ActionResult Json()
        {
            List<CatArticulosController> lst = new List<CatArticulosController>();

            var draw = Request.Form.GetValues("draw").FirstOrDefault();
            var start = Request.Form.GetValues("start").FirstOrDefault();
            var length = Request.Form.GetValues("length").FirstOrDefault();
            var sortColumn = Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();
            var sortColumnDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();
            var searchValue = Request.Form.GetValues("search[value]").FirstOrDefault();


            pageSize = length != null ? Convert.ToInt32(length) : 0;
            skip = start != null ? Convert.ToInt32(start) : 0;
            recordsTotal = 0;


            Reportes reportes = new Reportes();
            var listReport = reportes.GetProductsReport();

            if (searchValue != "")
            {
                listReport = listReport.Where(x => x.Sku.Contains(searchValue)).ToList();
            }

            if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
            {
                listReport = listReport.OrderBy(x => x.Sku).ToList();
            }

            recordsTotal = listReport.Count();

            //listReport.Skip(skip).Take(pageSize).ToList();

            return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = listReport.Skip(skip).Take(pageSize).ToList() });
        }





    }
}