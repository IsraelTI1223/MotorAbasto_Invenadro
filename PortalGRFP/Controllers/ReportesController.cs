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
using PortalGRFP.Entities.Common;
using PortalGRFP.Business.ComprasEspeciales;
using PortalGRFP.Business.Mantenimiento;

namespace PortalGRFP.Controllers
{
    public class ReportesController : Controller
    {
        // GET: Reportes
        public ActionResult Index()
        {


            var usuario = this.GetUsuario();
            if (usuario == null)
            {
                return RedirectToAction("Login", "Account");
            }


            Reportes reportes = new Reportes();
            var listReport = reportes.GetProductsReport();

            return View(listReport);
        }

        public ActionResult ConsultDataReport(string flag)
        {
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

            //return new FileStreamResult(excel, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
            //{
            //    FileDownloadName = "conciliacion"+mes.ToString()+anio.ToString()+".xlsx"
            //};
        }


        public void downLoadReport(int anio, int mes)
        {

        }



        public ActionResult ReporteProveedoresAgencias()
        {
            TempData["ListProv"] = new Reportes().GetListProvAgenciaBLL(1, 0).Result;

            return View();
        }

        [HttpPost]
        public ActionResult GetAgencias(FormCollection data)
        {
            int idProv = int.Parse(data["idProv"].ToString());

            var sucursales = new Reportes().GetListProvAgenciaBLL(2, idProv).Result;

            var json = Json(new { data = sucursales }, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = 500000000;
            return json;
        }

        public ActionResult GetProveedorAgenciaProd(int idprov, int idagencia)
        {
            //int idprov, idagencia;
            //if (data["IdProveedor"] == null || data["IdAgencia"] == null)
            //{
            //    idprov = 0;
            //    idagencia = 0;

            //}
            //else
            //{
            //    idprov = int.Parse(data["IdProveedor"].ToString());
            //    idagencia = int.Parse(data["IdAgencia"].ToString());
            //}

            var reporte = new Reportes().GetProveedorAgenciaProdBLL(idprov, idagencia);

            var json = Json(reporte.Result, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = 500000000;
            return json;
        }

        public ActionResult ReporteEstadisticaCompras()
        {
            var user = this.GetUsuario();

            var Grupos = new SugeridoBusiness().Filtros(3).Result;
            var gruposUsuario = (List<Combo3>)Session["Grupos_Usuario"];

            var usuarioGrupos = (from g in gruposUsuario where g.IdFiltro == user.IdUsuario && g.Valor == "True" select g.Id).ToList();

            var grupos2 = (from gr in Grupos where usuarioGrupos.Contains(gr.Id) select gr).ToList();

            TempData["grupo"] = grupos2;

            return View();
        }

        public ActionResult AutocommpleteSKU(string texto)
        {
            var autocompletar = new Reportes().AutocompletarArticulo(texto);

            return Json(autocompletar, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetReporteEstadistica(FormCollection formData)
        {
            string articulo = formData["articulo"].ToString();
            string grupo = formData["grupo"];

            var reporte = new Reportes().ReporteEstadisticaCompras(articulo, int.Parse(grupo));

            var json = Json(reporte, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = 500000000;
            return json;
        }

        public ActionResult VistaReporteInvenadroAut()
        {
            var user = this.GetUsuario();
            if (user == null)
                return RedirectToAction("Login", "Account");

            var Grupos = new SugeridoBusiness().Filtros(3);
            Session["Grupos_Usuario"] = new GruposBLL().GetGrupos_Usuario(4).Result;
            List<Combo3> gruposUsuario = (List<Combo3>)Session["Grupos_Usuario"];
            var usuarioGrupos = (from g in gruposUsuario where g.IdFiltro == user.IdUsuario && g.Valor == "True" select g.Id).ToList();
            TempData["GrupoTMP"] = (from gr in Grupos.Result where usuarioGrupos.Contains(gr.Id) select gr).ToList();

            var Sucursales = new SugeridoBusiness().Filtros2(4);
            Session["SucursalTMP"] = Sucursales.Result;

            return View();
        }

        [HttpPost]
        public ActionResult GetSucursalReporteInv(FormCollection datos)
        {
            var _idGrupo = datos["idGrupo"];
            var buscar = _idGrupo.Split(',');
            List<Combo3> sucursales = (List<Combo3>)Session["SucursalTMP"];
            var jsonfarmacia = (from N in sucursales
                                where buscar.Contains(N.IdFiltro.ToString())
                                select new { N.Id, N.Valor });
            return Json(jsonfarmacia, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public void DescargarExcelInvenadroAut(FormCollection datos)
        {
            try
            {
                string sucursales = datos["Sucursal"];
                string sku = datos["SKU"];

                Reportes reportes = new Reportes();
                var excel = reportes.GetExcelInvenadroAut(sucursales, sku);

                Response.Clear();
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment; filename=ReporteInvenadroAut.xlsx");
                Response.BinaryWrite(excel);
                Response.End();
            }
            catch (Exception ex)
            {
                Response.Clear();
                Response.ContentType = "text/plain";
                Response.Write("Error al generar el reporte: " + ex.Message);
                Response.End();
            }
        }

        [HttpPost]
        public ActionResult BuscarReporteInvenadroAut(FormCollection datos)
        {
            try
            {
                string sucursales = datos["Sucursal"];
                string sku = datos["SKU"];

                var resultado = new Reportes().GetReporteInvenadroAutBLL(sucursales, sku);

                var json = Json(new { success = resultado.Success, message = resultado.Message, data = resultado.Result }, JsonRequestBehavior.AllowGet);
                json.MaxJsonLength = 500000000;
                return json;
            }
            catch (Exception ex)
            {
                var json = Json(new { success = false, message = "Error al generar el reporte: " + ex.Message, data = new List<ReporteInvenadroAutModel>() }, JsonRequestBehavior.AllowGet);
                json.MaxJsonLength = 500000000;
                return json;
            }
        }

    }
}