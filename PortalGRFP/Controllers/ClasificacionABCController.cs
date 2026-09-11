using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PortalGRFP.Business.ABCBusiness;

namespace PortalGRFP.Controllers
{
    public class ClasificacionABCController : Controller
    {
        // GET: ClasificacionABC
        public ActionResult Index()
        {

            var consultaABC = new GetLIstaConsultaABC().ObtenerConsultaABC();


            return View(consultaABC);
        }

        [HttpPost]
        public JsonResult udateClasificacion(int idABC, string clasifica)
        {
            var consultaABC = new GetLIstaConsultaABC().updateABC(idABC, clasifica);
            var json = Json(consultaABC, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = 500000000;
            return json;
        }

    }
}