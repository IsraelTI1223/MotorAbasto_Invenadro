using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PortalGRFP.Business.Mantenimiento;
using PortalGRFP.Entities.Common;
using PortalGRFP.Entities.Common.MantenimientoArticulo;
using PortalGRFP.Entities.Response;
using PortalGRFP.Extensions;
using static PortalGRFP.Entities.Common.MantenimientoArticulo.ArticuloPermisoCompraModel;

namespace PortalGRFP.Controllers
{
    public class MantenimientoArticuloController : Controller
    {
        // GET: MantenimientoArticulo
       

        public ActionResult ArticuloTipoCompra()
        {
            var autoSku = new MantenimientoArticuloBLL().GetAArticuloTipoListCompraBLL(1);
            Session["ListSKU"] = autoSku.Result;

            var autoDesc = new MantenimientoArticuloBLL().GetAArticuloTipoListCompraBLL(2);
            Session["ListDescrip"] = autoDesc.Result;

            return View();  
        }

        public ActionResult AutocommpleteSKU(string texto)
        {
            List<Autocomplete> autocompletes = (List<Autocomplete>)Session["ListSKU"];
            var jsonArticulo = (from N in autocompletes
                                 where N.Valor.ToLower().Contains(texto.ToLower())
                                 select N).Take(15);
            return Json(jsonArticulo, JsonRequestBehavior.AllowGet);
        }
        public ActionResult AutocommpleteDESC(string texto)
        {
            List<Autocomplete> autocompletes = (List<Autocomplete>)Session["ListDescrip"];
            var jsonArticulo = (from N in autocompletes
                                 where N.Valor.ToLower().Contains(texto.ToLower())
                                 select N).Take(15);
            return Json(jsonArticulo, JsonRequestBehavior.AllowGet);
        }


        public ActionResult GetProductoBySku(string texto)
        {

            var jsonArticulo = new MantenimientoArticuloBLL().GetAArticuloTipoCompBLL(1, texto);

            return Json(jsonArticulo, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetProductoByDesc(string texto)
        {

            //List<Autocomplete> autocompletes = (List<Autocomplete>)Session["ListDescrip"];
            //var jsonArticulo = (from N in autocompletes
            //                     where N.Valor.ToLower().Contains(texto.ToLower())
            //                     select N).Take(15);

            //  List<Autocomplete> autocompletes = (List<Autocomplete>)Session["ListDescrip"];

            var jsonArticulo = new MantenimientoArticuloBLL().GetAArticuloTipoCompBLL(2, texto);


            return Json(jsonArticulo, JsonRequestBehavior.AllowGet);

            //return Json(jsonArticulo, JsonRequestBehavior.AllowGet);
        }




        public ActionResult ArticuloPermisoCompra()
        {
            TempData["CadenasList"] = new MantenimientoArticuloBLL().GetCadenaSucursalBLL(1,"").Result;

            var autoSku = new MantenimientoArticuloBLL().GetAArticuloTipoListCompraBLL(1);
            Session["ListSKU"] = autoSku.Result;

            var autoDesc = new MantenimientoArticuloBLL().GetAArticuloTipoListCompraBLL(2);
            Session["ListDescrip"] = autoDesc.Result;





            return View();
        }

        [HttpPost]
        public JsonResult CargarExcel()
        {
            var lstExcel = new MantenimientoArticuloBLL().CargarExcelBLL(Request.Files);
            Session["ListExcel"] = lstExcel;
            var response = new Response();
            response.Success = true;
            response.Message = "OK";

            //HttpFileCollectionBase httpFile = Request.Files;


            //var cargaMensual = new GetListaConciliacionesBusiness().ConciliacionMensual(Request.Files);

            //var json = Json(cargaMensual, JsonRequestBehavior.AllowGet);
            //json.MaxJsonLength = 500000000;

            var json = Json(response, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = 500000000;
            return json;

        }



        [HttpPost]
        public JsonResult GuardarPermisoCompra(CargaArticuloMViewModel model)
        {
            model.IdUsuario = this.GetUsuario().IdUsuario;

            List<ArticuloExcel> lstExcel = (List<ArticuloExcel>)Session["ListExcel"];

            var resp = new MantenimientoArticuloBLL().UIPermisoCompraArticuloBLL(model, lstExcel);

            var json = Json(resp, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = 500000000;
            return json;
        }



        [HttpPost]
        public ActionResult GetSucursalesXCadena(FormCollection data)
        {
            string cadena = data["cadena"].ToString();

            var sucursales = new MantenimientoArticuloBLL().GetCadenaSucursalBLL(2, cadena).Result;

            var json = Json(new { data = sucursales }, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = 500000000;
            return json;
        }


        [HttpPost]
        public JsonResult GuardarPermisoCompraPorSucursal(ArticuloXSucursalModel model)
        {

            model.IdUsuario = this.GetUsuario().IdUsuario;

            var resp = new MantenimientoArticuloBLL().UIPermisoCompraArticuloPorSucursalBLL(model);

            var json = Json(resp, JsonRequestBehavior.AllowGet);

            json.MaxJsonLength = 500000000;
            return json;
        }

        public ActionResult ConsultarLogError()
        {
            var log = new MantenimientoArticuloBLL().GetLogErrorBLL(this.GetUsuario().IdUsuario);
            var json = Json(log.Result, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = 500000000;
            return json;
        }


        public ActionResult ArticulosPorSucursal()
        {

            TempData["CadenasListAPXS"] = new MantenimientoArticuloBLL().GetCadenasSucursalesBLL(1, this.GetUsuario().IdUsuario, "").Result;

            var autoSku = new MantenimientoArticuloBLL().GetAutoCompleteProductListBLL(1);
            Session["ListSKUAPXS"] = autoSku.Result;

            var autoDesc = new MantenimientoArticuloBLL().GetAutoCompleteProductListBLL(2);
            Session["ListDescripAPXS"] = autoDesc.Result;

            TempData["CatSub"] = new CatSubEstatusBLL().GetCatalogoSubEstBLL(1, 0).OrderByDescending(d => d.Id).ToList();

            return View();
        }
        public ActionResult AutocommpleteSKUAPXS(string texto)
        {
            List<Autocomplete> autocompletes = (List<Autocomplete>)Session["ListSKUAPXS"];
            var jsonArticulo = (from N in autocompletes
                                where N.Valor.ToLower().Contains(texto.ToLower())
                                select N).Take(15);
            return Json(jsonArticulo, JsonRequestBehavior.AllowGet);
        }
        public ActionResult AutocommpleteDESCAPXS(string texto)
        {
            List<Autocomplete> autocompletes = (List<Autocomplete>)Session["ListDescripAPXS"];
            var jsonArticulo = (from N in autocompletes
                                where N.Valor.ToLower().Contains(texto.ToLower())
                                select N).Take(15);
            return Json(jsonArticulo, JsonRequestBehavior.AllowGet);
        }


        public ActionResult GetSucursalesXCadenas(string cadenas)
        {

            var sucursales = new MantenimientoArticuloBLL().GetCadenasSucursalesBLL(2, this.GetUsuario().IdUsuario, cadenas).Result;

            var json = Json(new { data = sucursales }, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = 500000000;
            return json;
        }
       
        public ActionResult GetPermisosArticuloSucursal(string sku,string sucursales)
        {
            var tbArticulos = new MantenimientoArticuloBLL().GetPermisosArticuloSucursalBLL(3, this.GetUsuario().IdUsuario, sku, sucursales);

            var json = Json( tbArticulos , JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = 500000000;
            return json;
        }

        [HttpPost]
        public JsonResult PermisosPorSucursal(ArticulosPorSucursalModel model)
        {

            model.IdUsuario = this.GetUsuario().IdUsuario;

            var resp = new MantenimientoArticuloBLL().PermisosPorSucursalBLL(model);

            var json = Json(resp, JsonRequestBehavior.AllowGet);

            json.MaxJsonLength = 500000000;
            return json;
        }


    }
}