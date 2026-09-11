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
    public class CatSubEstatusController : Controller
    {
        // GET
        public ActionResult CatSubEstatus()
        {
            var cat = new CatSubEstatusBLL().GetCatalogoSubEstBLL(1, 0).OrderByDescending(d => d.Id).ToList();

            return View(cat);
        }


        [HttpPost]
        public ActionResult CrearSubEstatus(CatSubEstatusModel model)
        {

            if (!ModelState.IsValid)
            {
                return View(model);
            }
            model.IdUsuario = this.GetUsuario().IdUsuario;
            var resp = new CatSubEstatusBLL().UICatSubEstatusBLL(model);

            var json = Json(resp, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = 500000000;
            return json;
        }

        [HttpPost]
        public JsonResult EliminarSubEstatus(CatSubEstatusModel model)
        {

            model.IdUsuario = this.GetUsuario().IdUsuario;

            var resp = new CatSubEstatusBLL().UICatSubEstatusBLL(model);

            var json = Json("ok", JsonRequestBehavior.AllowGet);

            json.MaxJsonLength = 500000000;
            return json;
        }

    }
}