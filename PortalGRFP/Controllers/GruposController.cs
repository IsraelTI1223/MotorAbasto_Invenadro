using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PortalGRFP.Business.Mantenimiento;
using PortalGRFP.Entities.Common;
using PortalGRFP.Extensions;

namespace PortalGRFP.Controllers
{
    public class GruposController : Controller
    {
        // GET: Grupos
        public ActionResult Grupos()
        {
            var lstGrupos = new GruposBLL().GetGruposBLL(1,0).ToList();
            
            return View(lstGrupos);
        }

       [HttpPost]
        public ActionResult Grupos(GruposModel model)
        {

            if (!ModelState.IsValid)
            {
                return View(model);
            }
            
            model.IdUsuario = this.GetUsuario().IdUsuario;
            var resp = new GruposBLL().UIGruposBLL(model);

            return Content(resp.ToString());
            

        }

    }
}