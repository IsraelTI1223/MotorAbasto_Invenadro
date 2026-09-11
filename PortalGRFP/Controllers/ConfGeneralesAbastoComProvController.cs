using PortalGRFP.Data.ConfGeneralesAbastoComProv;
using PortalGRFP.Entities.Common;
using PortalGRFP.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PortalGRFP.Business.ConfGeneralesAbastoComProv;

namespace PortalGRFP.Controllers
{
    public class ConfGeneralesAbastoComProvController : Controller
    {
        // GET: ConfGeneralesAbastoComProv
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult PrioridadCompraProveedor()
        {
            ComboList cmbDatos = new ComboList();

            //Combo Nivel de Escolaridad
            var proveedor = cmbDatos.ComboProveedor();
            var Id = (from x in proveedor select new { x.Id, x.Descr }).ToList();
            Session["Id"] = Id;


            TempData["Proveedor"] = proveedor;

            var consultamta = new GetListaProvBusiness().ObtenerConsultaPRV();

            return View(consultamta);
            //return View();
        }



        [HttpPost]
        public ActionResult Obteneridproveedor(FormCollection datos)
        {
            int _proveedor = int.Parse(datos["Proveedor"].ToString());
            //var lista = (List<ComboGenerico>)Session["Id"];
            //var Id = (from x in lista where x.Id == _proveedor select new { x.Id, x.Valor }).ToList();

            ComboList cmbIdProveedor = new ComboList();
            var idproveedor = cmbIdProveedor.ComboIdProv(_proveedor);
            var json = Json(new { data = idproveedor }, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = 500000000;
            return json;
        }

        [HttpPost]
        public ActionResult Obtenerdescrproveedor(FormCollection datos)
        {
            int _proveedor = int.Parse(datos["Proveedor"].ToString());
            //var lista = (List<ComboGenerico>)Session["Id"];
            //var Id = (from x in lista where x.Id == _proveedor select new { x.Id, x.Valor }).ToList();

            ComboList cmbIdProveedor = new ComboList();
            var descrproveedor = cmbIdProveedor.ComboDescrProv(_proveedor);
            var json = Json(new { data = descrproveedor }, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = 500000000;
            return json;
        }


        [HttpPost]

        public ActionResult PrioridadCompraProveedor(InsertProveedor model)
        {

            var user = this.GetUsuario();

            GetListaProvBusiness Datos = new GetListaProvBusiness();
            var idUs = Datos.InsertData(model, user.IdUsuario.ToString());

            return RedirectToAction("PrioridadCompraProveedor", "ConfGeneralesAbastoComProv");
            //var json = Json("ok", JsonRequestBehavior.AllowGet);
            //json.MaxJsonLength = 500000000;
            //return json;

        }
   
        public JsonResult Guardar(PorcentajeProv model)
        {

            var user = this.GetUsuario();

               GetListaProvBusiness Datos = new GetListaProvBusiness();
            var idUs = Datos.UpdateData(model, user.IdUsuario.ToString());


            //return RedirectToAction("PrioridadCompraProveedor", "ConfGeneralesAbastoComProv");
            var json = Json("ok", JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = 500000000;
            return json;

        }


        public ActionResult Eliminar(PorcentajeProv model)
        {

            var user = this.GetUsuario();


            GetListaProvBusiness Datos = new GetListaProvBusiness();
            var idUs = Datos.DeleteData(model, user.IdUsuario.ToString());


           // return RedirectToAction("PrioridadCompraProveedor", "ConfGeneralesAbastoComProv");

            var json = Json("ok", JsonRequestBehavior.AllowGet);

            json.MaxJsonLength = 500000000;
            return json;

        }

        public ActionResult AltaProveedor()
        {

                
            return View();
        }


    }
}


