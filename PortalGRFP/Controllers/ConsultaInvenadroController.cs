using PortalGRFP.Business.ComprasEspeciales;
using PortalGRFP.Entities.Common;
using PortalGRFP.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PortalGRFP.Controllers
{
    public class ConsultaInvenadroController : Controller
    {
        #region "Consulta invenadro cargado"
        // GET: ConsultaInvenadro
        public ActionResult Index()
        {
            var user = this.GetUsuario().IdUsuario;

            var Cadena = new SugeridoBusiness().FiltrosInvenadro(1,user);

            TempData["Cadena"] = (from gr in Cadena.Result  select gr).ToList();

            var Sucursales = new SugeridoBusiness().FiltrosInvenadro2(2,user);
            Session["SucursalTMP"] = Sucursales.Result;

            var Estatus = new SugeridoBusiness().FiltrosInvenadro(4, user);

            TempData["Estatus"] = (from gr in Estatus.Result select gr).ToList();

            var SKU = new SugeridoBusiness().FiltrosInvenadro2(3, user);
            Session["SKU"] = SKU.Result;

            return View();
        }

        public ActionResult GetSucursal(FormCollection datos)
        {
            var _Cadena = datos["idGrupo"];
            var buscar = _Cadena.Split(',');
            List<Combo4> sucursales = (List<Combo4>)Session["SucursalTMP"];
            var jsonfarmacia = (from N in sucursales
                                where buscar.Contains(N.Filtro.ToString())
                                select new { N.Id, N.Valor });
            return Json(jsonfarmacia, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetSKU(FormCollection datos)
        {
            var _Estatus = datos["idEstatus"];
            var buscar = _Estatus.Split(',');
            List<Combo4> sku = (List<Combo4>)Session["SKU"];
            var jsonfarmacia = (from N in sku
                                where buscar.Contains(N.Filtro.ToString())
                                select new { N.Id, N.Valor });
            return Json(jsonfarmacia, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ConsultarProductoInvenadro(FormCollection datos)
        {
            var Sucursal = datos["Sucursal"];
            var SKU = datos["SKU"];
            var Concepto = datos["Conceptos"];

            var productos = new SugeridoBusiness().GetProductoInvenadroListBLL(Sucursal, SKU, Concepto);

            var json = Json(productos.Result, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = 500000000;
            return json;
        }
        #endregion

        #region Consulta bitacora invenadro automatico
        public ActionResult ConsultaInvenadroAutomatico()
        {
            
            return View();
        }

        #endregion
    }
}