using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PortalGRFP.Entities.Common;
using PortalGRFP.Entities.Common.Capacity;
using PortalGRFP.Business.GeneralesAbasto;
using PortalGRFP.Business.Capacity;
using System.Dynamic;
using PortalGRFP.Extensions;

namespace PortalGRFP.Controllers
{
    public class ReporteCapacityController : Controller
    {
        public ActionResult Index()
        {

            var Flag = 1;

            var Resp = new CapacityBusiness().SelectedCapacity(Flag);
            TempData["lstTipoCapacity"] = Resp;
            return View(Resp);
        }

        [HttpPost]
        public JsonResult RepCapacity(FormCollection formulario)
        {

            var flag = Convert.ToInt16(formulario["Flags"]);
            var tipoCapacity = formulario["tipoCapacity"];
            var feini = formulario["txtFini"];
            var fefin = formulario["txtFfin"];

            var Resp = new CapacityBusiness().ReporteCapacity(flag, tipoCapacity, feini, fefin);

            var json = Json(JsonRequestBehavior.AllowGet);
            if (Resp.Count == 0)
            {
                json = Json(new { Resp, Success = false, Message = "No hay Registros en la consulta realizada" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                json = Json(new { Resp, Success = true, Message = "Consulta Exitosa" }, JsonRequestBehavior.AllowGet);
            }
                        
            return json;
        }


    }
}

