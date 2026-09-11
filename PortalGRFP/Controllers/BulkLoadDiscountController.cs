using Microsoft.Ajax.Utilities;
using PortalGRFP.Business.BulkLoads;
using PortalGRFP.Business.Descuentos;
using PortalGRFP.Data.GetSubClientesPopsae;
using PortalGRFP.Data.SubCliente;
using PortalGRFP.Data.TipoPerfil;
using PortalGRFP.Entities.Common;
using PortalGRFP.Entities.Enums;
using PortalGRFP.Entities.Models;
using PortalGRFP.Entities.Parameters;
using PortalGRFP.Entities.Response;
using PortalGRFP.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PortalGRFP.Controllers
{
    public class BulkLoadDiscountController : Controller
    {
        private readonly BDiscountHdrGetList discountHdrGetList;

        public BulkLoadDiscountController()
        {
            discountHdrGetList = new BDiscountHdrGetList();
        }

        // GET: BulkLoadDiscount
        public ActionResult Index(TipoCarga tipoCarga = TipoCarga.TODOS)
        {

            TipoPerfilData model = new TipoPerfilData();
            SubclientesPopsae modelP = new SubclientesPopsae();
            SubClienteData modelS = new SubClienteData();
            var user = this.GetUsuario();
            var opcion = (user.Perfil == "Administrador" || user.Perfil == "POPSAE") ? 2 : 1;
          

            
            ViewBag.Actions = this.GetUsuario().Permisos.GetActions(20) ?? new Dictionary<int, string>();
            TempData["TipoCarga"] = tipoCarga;
            var discounts = discountHdrGetList.Execute(new Entities.Request.Request<DiscountGetListParameter>
            {
                Parameters = new DiscountGetListParameter
                {
                    IdTipoCarga = (int)tipoCarga
                }
            });

            ViewBag.IdPerfil = user.IdPerfil;
            ViewBag.Clientes = (user.Perfil == "Administrador" || user.Perfil== "POPSAE") ? null : model.GetClientesByTipo(user.IdPerfil,opcion).Result;
            ViewBag.SubClientes = (user.Perfil == "Administrador" || user.Perfil == "POPSAE") ? modelP.GetSubClientesByTipo(user.IdPerfil).Result : null;

            return View(discounts);
        }

        [HttpPost]
        public JsonResult FileUpload()
        {                  
            var user = this.GetUsuario();
            var dataLogic = new BBulkLoadDiscount();
            var file = Request.Files[0];
            var idCliente = Request.Form[1];
            var tipoPerfil = Request.Form[2];
            var idTiposub = Request.Form[3];
            var idSubCliente = Request.Form[4];
            var IDTIPO2 = (user.Perfil == "Administrador") ? Request.Form[3] : idTiposub;
            IDTIPO2 = user.IdPerfil == 2 ? "1" : IDTIPO2;
            IDTIPO2 = user.IdPerfil == 4 ? "2" : IDTIPO2;
            var NombreCliente = Request.Form[5];
            var NombreSubcliente = Request.Form[6];
            var response = dataLogic.FileUpload(user.IdUsuario, file, idCliente, user.IdPerfil, tipoPerfil, Convert.ToInt32(IDTIPO2), Convert.ToInt32(idSubCliente), NombreCliente, NombreSubcliente);
            return Json(response, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult GetClientesBySubTipo()
        {
            TipoPerfilData model = new TipoPerfilData();
           
            var idTipo = Request.Form[0];         
            var clientes = model.GetClientesByTipo(Convert.ToInt32(idTipo),2); 
            return Json(clientes, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult GetClientesBySubCliente()
        {
            SubClienteData model = new SubClienteData();
            var IdCliente = Request.Form[0];      
            var SubClientes = model.GetSubClientesBy(Convert.ToInt32(IdCliente));
            return Json(SubClientes, JsonRequestBehavior.AllowGet);
        }


    }
}
