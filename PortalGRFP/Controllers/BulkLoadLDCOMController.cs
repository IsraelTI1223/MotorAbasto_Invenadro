using PortalGRFP.Business.BulkLoads;
using PortalGRFP.Data.TipoPerfil;
using PortalGRFP.Entities.Enums;
using PortalGRFP.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PortalGRFP.Controllers
{
    public class BulkLoadLDCOMController : Controller
    {
        // GET: BulkLoadLDCOM
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult FileUpload()
        {

        
            var idTipo = Request.Form[0];
            var user = this.GetUsuario();
            var dataLogic = new BBulkLoadDiscount();
            var layoutType = Request.Form["LayoutType"];
            var file = Request.Files[0];
            var idCliente = Request.Form[1];
            var tipoPerfil = Request.Form[2];
            var idTipo2 = Request.Form[3];
            var idSubCliente = Request.Form[4];
            var IDTIPO2 = (user.Perfil == "Administrador") ? Request.Form[3] : Convert.ToString(1);
            var NombreCliente = Request.Form[5];
            var NombreSubcliente = Request.Form[6];
            var response = dataLogic.FileUpload(1,file, idCliente, user.IdPerfil, tipoPerfil, Convert.ToInt32(IDTIPO2), Convert.ToInt32(idSubCliente), NombreCliente, NombreSubcliente);
            return Json(response, JsonRequestBehavior.AllowGet);
        }
    }
}
