using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PortalGRFP.Entities.Common;
using PortalGRFP.Business.ConfProveedoresG;
using PortalGRFP.Extensions;
using PortalGRFP.Business.ConfiguracionProveedor;
using PortalGRFP.Data.ConfiguracionProveerdores;

namespace PortalGRFP.Controllers
{
    public class ConfiguracionesProveedoresController : Controller
    {
        // GET: ConfiguracionesProveedores
        //public ActionResult General()
        //{


        //    var provedor = new ConfiguracionProveedorGeneralModel();

        //    var lst = new ConfProveedoresGBLL().GetProveedoresListBLL();
        //    provedor.lstProveedores = lst;


        //    return View(provedor);
        //}

        [HttpPost]
        public ActionResult Obteneragencia(FormCollection datos)
        {
            int _proveedor = int.Parse(datos["Proveedor"].ToString());
            //var lista = (List<ComboGenerico>)Session["Id"];
            //var Id = (from x in lista where x.Id == _proveedor select new { x.Id, x.Valor }).ToList();

            ConfiguracionProveedorData cmbAgencia = new ConfiguracionProveedorData();
            var agencia = cmbAgencia.ComboAgencia(_proveedor);
            var json = Json(new { data = agencia }, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = 500000000;
            return json;
        }

        [HttpPost]
        public ActionResult ObtenerRazonSocial(FormCollection datos)
        {
            int _proveedor = int.Parse(datos["Proveedor"].ToString());
            //var lista = (List<ComboGenerico>)Session["Id"];
            //var Id = (from x in lista where x.Id == _proveedor select new { x.Id, x.Valor }).ToList();

            ConfiguracionProveedorData cmbAgencia = new ConfiguracionProveedorData();
            var agencia = cmbAgencia.ComboRazonSocial(_proveedor);
            var json = Json(new { data = agencia }, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = 500000000;
            return json;
        }


        public ActionResult ModificarFTP(string Id)
        {

            ConfiguracionProveedorBusiness datos = new ConfiguracionProveedorBusiness();
            ProveedoresAgenciaFTPCompleto idUs = datos.SelectDataFTP(Id);
            ViewBag.Cabecera = idUs;
            Session["Modificar"] = idUs;

            ConfiguracionProveedorData cmbDatos = new ConfiguracionProveedorData();

            //Combo Nivel de Escolaridad
            var frecuencia = cmbDatos.ComboFrecuenciaProveedor();
            TempData["Frecuencia"] = frecuencia;

            //ComboList cmbDatosmovc = new ComboList();
            //var Estatus = cmbDatosmovc.ComboEstatus();
            //TempData["Estatus"] = Estatus;

            return View();
        }

        public ActionResult InformacionComplementariaSet()
        {
            ProveedoresAgenciaFTPCompleto idUs = (ProveedoresAgenciaFTPCompleto)Session["Modificar"];
            return Json(idUs, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult ModificarFTP(ProveedoresAgenciaFTPCompleto model)
        {
            var user = this.GetUsuario();

            ConfiguracionProveedorBusiness Mod = new ConfiguracionProveedorBusiness();
            var idMod = Mod.UpdateFTP(model, user.IdUsuario.ToString());

            //return RedirectToAction("FTP", "ConfiguracionesProveedores");
            var json = Json("ok", JsonRequestBehavior.AllowGet);

            json.MaxJsonLength = 500000000;
            return json;
        }

        public ActionResult Eliminar(ProveedoresAgenciaFTPCompleto model)
        {
            var user = this.GetUsuario();


            ConfiguracionProveedorBusiness Mod = new ConfiguracionProveedorBusiness();
            var idMod = Mod.DeleteFTP(model, user.IdUsuario.ToString());


            return RedirectToAction("FTP", "ConfiguracionesProveedores");
        }

        public ActionResult Ftp()
        {

            ConfiguracionProveedorData cmbDatos = new ConfiguracionProveedorData();

            //Combo Nivel de Escolaridad
            var formato = cmbDatos.ComboFormatoProveedor();
            TempData["Formato"] = formato;

            var frecuencia = cmbDatos.ComboFrecuenciaProveedor();
            TempData["Frecuencia"] = frecuencia;


            var prov = cmbDatos.ComboProv();
            TempData["Proveedor"] = prov;


            var consultaftp = new ConfiguracionProveedorBusiness().ObtenerConsultaFTP();

            return View(consultaftp);

        }

        [HttpPost]
        public ActionResult Ftp(InsertFTP model)
        {

            var user = this.GetUsuario();

            ConfiguracionProveedorBusiness Datos = new ConfiguracionProveedorBusiness();
            var idUs = Datos.InsertData(model, user.IdUsuario.ToString());


            //return RedirectToAction("Ftp", "ConfiguracionesProveedores");
            var json = Json("ok", JsonRequestBehavior.AllowGet);

            json.MaxJsonLength = 500000000;
            return json;
        }

        public ActionResult EditarProveedores()
        {

            var lstProv = new ConfProveedoresGBLL().GetProveedoresBLL(1);

            return View(lstProv);
        }
        [HttpPost]
        public ActionResult GuardarProveedores(ConfProveedoresModel model)
        {
            model.IdUsuarioAlta = this.GetUsuario().IdUsuario;

            var resp = new ConfProveedoresGBLL().UIProveedorBLL(model);
            return Content(resp.ToString());


            //return View(model);

        }

        public ActionResult RegistrarProveedores()
        {
            var lstProv = new ConfProveedoresGBLL().GetProveedoresBLL(2);
            return View(lstProv);
        }

        [HttpPost]
        public JsonResult EliminarProveedor(ConfProveedoresModel model)
        {

            model.IdUsuarioAlta = this.GetUsuario().IdUsuario;

            var resp = new ConfProveedoresGBLL().UIProveedorBLL(model);

            var json = Json("ok", JsonRequestBehavior.AllowGet);

            json.MaxJsonLength = 500000000;
            return json;
        }

        public ActionResult Agencias()
        {
            var autocompletar = new ConfiguracionProveedorBusiness().Autocompletar(1);
            Session["AutocompletarProvedor"] = autocompletar.Result;

            TempData["Agencia"] = new ConfiguracionProveedorBusiness().Autocompletar(2).Result;

            return View();
        }

        public ActionResult Autocommplete(string texto)
        {
            List<Autocomplete> autocompletes = (List<Autocomplete>)Session["AutocompletarProvedor"];
            var jsonProveedor = (from N in autocompletes
                                 where N.Valor.ToLower().Contains(texto.ToLower())
                                 select N).Take(5);
            return Json(jsonProveedor, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AgregarAgencia(FormCollection regla)
        {

            AgenciaProveedor agencia = new AgenciaProveedor();
            agencia.IdProveedor = regla["IdProveedor"] == null ? 0 : int.Parse(regla["IdProveedor"].ToString().Split(',')[0]);
            agencia.IdProveedor = regla["hProveedor"] == null ? 0 : int.Parse(regla["hProveedor"].ToString());
            agencia.IdAgencia = regla["IdAgencia"] == null ? 0 : int.Parse(regla["IdAgencia"].ToString());
            agencia.OrdenCompraAutomatica = regla["OrdenCompraAutomatica"] == null ? false : regla["OrdenCompraAutomatica"].ToString() == "true" ? true : false;
            agencia.FacturaAutomatica = regla["FacturaAutomatica"] == null ? false : regla["FacturaAutomatica"].ToString() == "true" ? true : false;
            agencia.CatalogoAutomatico = regla["CatalogoAutomatico"] == null ? false : regla["CatalogoAutomatico"].ToString() == "true" ? true : false;
            agencia.PermiteRemisiones = regla["PermiteRemisiones"] == null ? false : regla["PermiteRemisiones"].ToString() == "true" ? true : false;
            agencia.RespuestaFaltante = regla["RespuestaFaltante"] == null ? false : regla["RespuestaFaltante"].ToString() == "true" ? true : false;

            var user = this.GetUsuario();
            var agenciaAdd = new ConfiguracionProveedorBusiness().AgregarAgencia(agencia, user.IdUsuario);
            var json = Json(agenciaAdd, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = 500000000;
            return json;
        }

        public ActionResult ConsultarAgencias()
        {
            var agenciaAdd = new ConfiguracionProveedorBusiness().ConsultarAgencias();
            var json = Json(agenciaAdd.Result, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = 500000000;
            return json;
        }

        public ActionResult Exclusiones()
        {
            var autocompletar = new ConfiguracionProveedorBusiness().Autocompletar(1);
            Session["AutocompletarProvedor"] = autocompletar.Result;

            return View();
        }


        [HttpPost]
        public JsonResult CargarExclusiones(FormCollection formulario   )
        {
            string proveedor = formulario["hProveedor"].ToString();//"13213,4645,87989"
            string eliminar = formulario["chkBorrar"] == null ? "false" : "true";
            string agregar = formulario["chkcargar"] == null ? "false" : "true";


            var cargaMasiva = new ConfiguracionProveedorBusiness().CargaExclusiones(Request.Files, this.GetUsuario().IdUsuario, int.Parse(proveedor), bool.Parse(eliminar), bool.Parse(agregar));

            var json = Json(cargaMasiva, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = 500000000;
            return json;
        }

        public ActionResult GetExclusiones(string idProveedor)
        {
            var bitacora = new ConfiguracionProveedorBusiness().GetLogExclusiones(int.Parse(idProveedor));
            var json = Json(bitacora.Result, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = 500000000;
            return json;
        }
    }
}