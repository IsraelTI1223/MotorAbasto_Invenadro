using PortalGRFP.Business.ConfiguracionProveedor;
using PortalGRFP.Business.Mantenimiento;
using PortalGRFP.Entities.Common;
using PortalGRFP.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PortalGRFP.Controllers
{
    public class MantenimientoSucursalController : Controller
    {
        // GET: MantenimientoSucursal
        public ActionResult MantenimientoSuc()
        {
            var manttoGru = new SucursalesBusiness().Filtros(1);
            TempData["Grupos"] = manttoGru.Result;


            var manttoSuc = new SucursalesBusiness().Filtros(2);
            TempData["Sucursales"] = manttoSuc.Result;


            return View();
        }

        public ActionResult GuardarManttoSuc(FormCollection formulario)
        {
            MantenimientoSucursal mantenimiento = new MantenimientoSucursal();
            mantenimiento.IdSucursal = int.Parse(formulario["IdSucursal"].ToString());
            mantenimiento.Grupo = int.Parse(formulario["Grupo"].ToString());
            mantenimiento.Invenadro = formulario["invenadro"] == null ? false : bool.Parse(formulario["invenadro"].ToString());
            mantenimiento.PVD = formulario["PVD"] == null ? false : bool.Parse(formulario["PVD"].ToString());
            mantenimiento.Negados = formulario["negados"] == null ? false : bool.Parse(formulario["negados"].ToString());


            var user = this.GetUsuario();

            var aplica = new SucursalesBusiness().GuardarManttoSuc(mantenimiento, user.IdUsuario);
            var json = Json(aplica, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = 500000000;
            return json;
        }

        public ActionResult ConsultarManttoSuc(FormCollection formulario)
        {
            MantenimientoSucursal mantenimiento = new MantenimientoSucursal();
            mantenimiento.IdSucursal = int.Parse(formulario["IdSucursal"].ToString());

            var aplica = new SucursalesBusiness().GetManttoSuc(mantenimiento);
            var json = Json(aplica, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = 500000000;
            return json;
        }


        public ActionResult SucursalProveedor()
        {
            TempData["Proveedores"] = new ConfiguracionProveedorBusiness().Autocompletar(1).Result;

            Session["Agencia"] = new ConfiguracionProveedorBusiness().ProveedorAgencia(3).Result;

            var manttoSuc = new SucursalesBusiness().Filtros(2);
            TempData["Sucursales"] = manttoSuc.Result;

            return View();
        }


        public ActionResult ObtenerAgencias(FormCollection datos)
        {

            int _idProv = int.Parse(datos["idProveedor"].ToString());

            List<ProveedorAgencia> proveedorAgencias = (List<ProveedorAgencia>)Session["Agencia"];
            var jsonProveedor = (from N in proveedorAgencias
                                 where N.IdProveedor == _idProv
                                 select N).Distinct();
            return Json(jsonProveedor, JsonRequestBehavior.AllowGet);
        }


        public ActionResult GuardarSucursalProveedor(FormCollection sucProv)
        {
            SucursalProveedor sucEnvio = new SucursalProveedor();
            sucEnvio.IdSucursal = int.Parse(sucProv["IdSucursal"].ToString());
            sucEnvio.IdProveedor = int.Parse(sucProv["IdProveedor"].ToString());
            sucEnvio.LeadTime = int.Parse(sucProv["leadTime"].ToString());
            sucEnvio.DiasCobertura = int.Parse(sucProv["DiasCobertura"].ToString());
            sucEnvio.IdAgencia = int.Parse(sucProv["IdAgencia"].ToString());
            sucEnvio.ClienteProveedor = int.Parse(sucProv["ClienteProveedor"].ToString());
            sucEnvio.Lunes = sucProv["Lunes"] == null ? 0 : 1;
            sucEnvio.Martes = sucProv["Martes"] == null ? 0 : 1;
            sucEnvio.Miercoles = sucProv["Miercoles"] == null ? 0 : 1;
            sucEnvio.Jueves = sucProv["Jueves"] == null ? 0 : 1;
            sucEnvio.Viernes = sucProv["Viernes"] == null ? 0 : 1;
            sucEnvio.Sabado = sucProv["Sabado"] == null ? 0 : 1;
            sucEnvio.Domingo = sucProv["Domingo"] == null ? 0 : 1;

            var user = this.GetUsuario();
            var coberturaResp = new SucursalesBusiness().GuardarSucursalProveedor(sucEnvio, user.IdUsuario);

            var json = Json(coberturaResp, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = 500000000;
            return json;
        }

        public ActionResult ConsultarSucursalProv(FormCollection sucProv)
        {
            SucursalProveedor sucEnvio = new SucursalProveedor();
            sucEnvio.IdSucursal = int.Parse(sucProv["IdSucursal"].ToString());
            //sucEnvio.IdProveedor = int.Parse(sucProv["IdProveedor"].ToString());
            //sucEnvio.IdAgencia = int.Parse(sucProv["IdAgencia"].ToString());

            var coberturaResp = new SucursalesBusiness().ConsultaSucursalProv(sucEnvio);
            var json = Json(coberturaResp, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = 500000000;
            return json;
        }


        [HttpPost]
        public JsonResult CargaMasiva()
        {
            Session["CargaMasiva"] = Request.Files;
            var cargaMasiva = new SucursalesBusiness().CargaMasiva(Request.Files, this.GetUsuario().IdUsuario);

            var json = Json(cargaMasiva, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = 500000000;
            return json;
        }

     
        public JsonResult ConsultaMasiva()
        {
            HttpFileCollectionBase filesCM = (HttpFileCollectionBase)Session["CargaMasiva"];
            var cargaMasiva = new SucursalesBusiness().ConsultaMasiva(filesCM, this.GetUsuario().IdUsuario);

            var json = Json(cargaMasiva.Result, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = 500000000;
            return json;
        }
    }
}