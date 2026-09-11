using PortalGRFP.Business.ConfguracionCarga;
using PortalGRFP.Entities.Common;
using PortalGRFP.Entities.Request;
using PortalGRFP.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PortalGRFP.Controllers
{
    public class FileUploadConfigurationController : Controller
    {      

        // GET: FileUploadConfiguration
        public ActionResult Index()
        {
            var configurations = new BConfiguracionCargaGetList().Execute();
            return View(configurations);
        }

        [HttpPost]
        public ActionResult Merge(PortalGRFP.Entities.Common.ConfiguracionCarga configuration)
        {
            var response = new BConfiguracionCargaMerge().Execute(new Request<ConfiguracionCarga>
            {
                IdUsuario = this.GetUsuario().IdUsuario,
                Parameters = configuration
            });
            return Json(response, JsonRequestBehavior.AllowGet);
        }
    }
}
