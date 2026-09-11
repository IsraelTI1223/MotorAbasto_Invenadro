using PortalGRFP.Business.Reportes;
using PortalGRFP.Data.ConsultaSucursalProveedor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PortalGRFP.Controllers
{
    public class ConsultaSucursalProveedorController : Controller
    {
        // GET: ConsultaSucursalProveedor
        public ActionResult Index()
        {

            ComboList cmbDatos = new ComboList();
            var cadena = cmbDatos.ComboGrupo();
            TempData["Grupo"] = cadena;


            return View();
        }

        [HttpPost]
        public ActionResult Index(int Sucursal)//int Cadena
        {


            ComboList cmbDatos = new ComboList();
            var cadena = cmbDatos.ComboGrupo();
            TempData["Grupo"] = cadena;


            var listreporte = new Reportes().GetConsultaSucursalesProveedor(Sucursal);


            return View(listreporte);
        }


        [HttpPost]
        public ActionResult Obtenersucursal(FormCollection datos)
        {
            int _grupo = int.Parse(datos["Grupo"].ToString());
           
            ComboList cmbIdProveedor = new ComboList();
            var sucursal = cmbIdProveedor.ComboIdSucursal(_grupo);
            var json = Json(new { data = sucursal }, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = 500000000;
            return json;
        }

        public void DescargarExcel()
        {
            Reportes reportes = new Reportes();

            var excel = reportes.GetExcelConsultaSucursalesProveedor();

            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment: filename=report.xlsx");
            Response.BinaryWrite(excel);
            Response.End();
        }

    }
}