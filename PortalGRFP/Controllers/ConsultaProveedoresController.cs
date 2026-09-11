using PortalGRFP.Data.ConsultaProvedores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PortalGRFP.Controllers
{
    public class ConsultaProveedoresController : Controller
    {
        // GET: ConsultaProveedores
        public ActionResult Index()
        {
            ComboList cmbDatos = new ComboList();
            var proveedor = cmbDatos.ComboProveedor();
            TempData["Proveedor"] = proveedor;

          

            return View();
        }



        [HttpPost]
        public ActionResult Obteneragencia(FormCollection datos)
        {
            int _proveedor = int.Parse(datos["Proveedor"].ToString());
            //var lista = (List<ComboGenerico>)Session["Id"];
            //var Id = (from x in lista where x.Id == _proveedor select new { x.Id, x.Valor }).ToList();

            ComboList cmbIdProveedor = new ComboList();
            var agencia = cmbIdProveedor.ComboIdProv(_proveedor);
            var json = Json(new { data = agencia }, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = 500000000;
            return json;
        }
    }
}