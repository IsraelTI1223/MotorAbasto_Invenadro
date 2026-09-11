using PortalGRFP.Business.Reportes;
using PortalGRFP.Data.ConsultaSucursales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PortalGRFP.Controllers
{
    public class ConsultaSucursalController : Controller
    {
        // GET: ConsultaSucursal
        public ActionResult Index()
        {

            ComboList cmbDatos = new ComboList();
            var cadena = cmbDatos.ComboCadena();
            TempData["Cadena"] = cadena;

           
            return View();
         
        }


        [HttpPost]
        public ActionResult Index(int Cadena)//int Cadena
        {


            ComboList cmbDatos = new ComboList();
            var cadena = cmbDatos.ComboCadena();
            TempData["Cadena"] = cadena;


            var listreporte = new Reportes().GetConsultaSucursales(Cadena);


            return View(listreporte);
        }

        [HttpPost]
        public ActionResult Obtenermarca(FormCollection datos)
        {
            int _cadena = int.Parse(datos["Cadena"].ToString());
            //var lista = (List<ComboGenerico>)Session["Id"];
            //var Id = (from x in lista where x.Id == _proveedor select new { x.Id, x.Valor }).ToList();

            ComboList cmbIdProveedor = new ComboList();
            var marca = cmbIdProveedor.ComboIdMarca(_cadena);
            var json = Json(new { data = marca }, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = 500000000;
            return json;
        }

        public void DescargarExcel()
        {
            Reportes reportes = new Reportes();

            var excel = reportes.GetExcelConsultaSucursales();

            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment: filename=report.xlsx");
            Response.BinaryWrite(excel);
            Response.End();
        }


    }
}