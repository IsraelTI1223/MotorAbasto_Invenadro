using PortalGRFP.Entities.Models;
using PortalGRFP.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PortalGRFP.Controllers
{
    public class MenuController : Controller
    {
        // GET: Menu
        public ActionResult Index()
        {
            var usuario = this.GetUsuario() ?? new UsuarioModel { Permisos = new List<ModuloModel>() };
            Session["dtoUsuario"] = usuario;
            return PartialView(usuario);
        }
    }
}