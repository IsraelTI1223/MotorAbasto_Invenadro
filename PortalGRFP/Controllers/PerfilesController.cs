using PortalGRFP.Business.Perfiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PortalGRFP.Models;
using PortalGRFP.Entities.Common;
using PortalGRFP.Business.Modulos;
using PortalGRFP.Entities.Response;
using PortalGRFP.Business.Acciones;
using PortalGRFP.Extensions;
using PortalGRFP.Entities.Models;

namespace PortalGRFP.Controllers
{
    public class PerfilesController : Controller
    {
        // GET: Perfiles
        public ActionResult Index()
        {
            var usuario = this.GetUsuario();
            if (usuario == null)
            {
                return RedirectToAction("Login", "Account");
            }
            var pp = usuario.Permisos.ToList().Where(x => x.Modulo == "Configuraciones");

            //var per = pp.FirstOrDefault().SubModuloHijos.ToList();
            var per = pp.FirstOrDefault().SubModuloHijos.ToList().Where(x => x.Modulo == "Seguridad");

            //var smh = per.
            var action = per.FirstOrDefault().SubModuloHijos.ToList().Where(x => x.Modulo == "Generar Perfil").FirstOrDefault().Acciones.ToList();

            /*//var action = per
            var per = pp.FirstOrDefault().SubModulos.ToList();
           var action = per.Where(subm =>
            subm.SubModulo == "Generar Perfil"
            ).FirstOrDefault().Acciones.ToList();*/

            foreach (var item in action)
            {
                if (item.Value.Equals("Actualizar") && Session["PerfilActualizar"] == null)
                {
                    Session["PerfilActualizar"] = item.Value;

                }
                if (item.Value.Equals("Registrar") && Session["PerfilRegistrar"] == null)
                {
                    Session["PerfilRegistrar"] = item.Value;
                }
                if (item.Value.Equals("Consultar") && ViewBag.PermisoConsultaPerfil == null)
                {
                    ViewBag.PermisoConsultaPerfil = "Consultar";
                }
                if (Session["PerfilActualizar"] != null && Session["PerfilRegistrar"] != null && ViewBag.PermisoConsultaPerfil != null)
                {
                    break;
                }
            }

            if (Session["PerfilActualizar"] == null)
            {
                Session["PerfilActualizar"] = "Sin permisos";
            }

            if (Session["PerfilRegistrar"] == null)
            {
                Session["PerfilRegistrar"] = "Sin permisos";
            }
            if (ViewBag.PermisoConsultaPerfil == null)
            {
                ViewBag.PermisoConsultaPerfil = "Sin Permisos";
            }

            List<Models.Perfil> lstPerfil = new List<Models.Perfil> { };
            // obtenemos los perfiles activos
            PerfilBusiness perfilBussiness = new PerfilBusiness();
            var lstPerfilbd = perfilBussiness.GetAllPerfil(2);
            if (lstPerfilbd.Result != null)
            {
                lstPerfil = lstPerfilbd.Result.Select(perf => new Models.Perfil { IdPerfil = perf.IdPerfil, Nombre = perf.Nombre, Activo = perf.Activo }).ToList();
            }
            return View(lstPerfil);
        }

        [HttpGet]
        public ActionResult NuevoPerfil()
        {
            string arbolModuloAcciones = "";
            string tablaModuloAcciones = "";
            PerfilViewModel model = new PerfilViewModel { };

            PerfilBusiness perfilBusiness = new PerfilBusiness();
            ResponseList<PerfilModuloModel> lstPerfilModulo = perfilBusiness.PerfilModulos(0, ref arbolModuloAcciones, ref tablaModuloAcciones);
            
            //ViewBag.TreePerfilModulo = arbolModuloAcciones;
            ViewBag.TablaModuloAcciones = tablaModuloAcciones;
            return View(model);
        }

        [HttpPost]
        public ActionResult NuevoPerfil(PerfilViewModel perfil)
        {
            Response response = new Entities.Response.Response();
            response.Success = false;
            if (ModelState.IsValid)
            {
                PerfilBusiness perfilBusiness = new PerfilBusiness();
                List<string> modulos = new List<string> { };
                if (perfil.ModulosSeleccionados != null)
                {
                    if (perfil.ModulosSeleccionados.Trim().Length > 0)
                    {
                        modulos = perfil.ModulosSeleccionados.Split('|').ToList();
                    }
                }
                response = perfilBusiness.InsertCTRLPERFIL(perfil.Nombre, this.GetUsuario().IdUsuario, modulos);
                return Json(response, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(response, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult ModulosHijo(int IdPadre, int TipoOperacion, int IdPerfil)
        {
            ModuloBusiness moduloBusiness = new ModuloBusiness();
            var lstModulobd = moduloBusiness.GetListByPadre(IdPadre, TipoOperacion, IdPerfil);
            List<Models.Modulo> lstViewModel = new List<Models.Modulo> { };
            if (lstModulobd.Result != null)
            {
                lstViewModel = lstModulobd.Result.Select(mod => new Models.Modulo { IdModulo = mod.IdModulo, NombreModulo = mod.NombreModulo, Actualizar = mod.Actualizar, Consultar = mod.Consultar, Cargar = mod.Cargar, Eliminar = mod.Eliminar, Insertar = mod.Insertar, Descargar = mod.Descargar, Opciones = mod.Opciones }).ToList();
            }
            return PartialView(lstViewModel);
        }

        [HttpGet]
        public ActionResult ActualizaPerfil(int IdPerfil)
        {
            string arbolModuloAcciones = "";
            string tablaModuloAcciones = "";

            PerfilViewModel model = new PerfilViewModel { };
            PerfilBusiness perfilBusiness = new PerfilBusiness();
            ResponseList<Entities.Common.Perfil> lstPerfil = perfilBusiness.GetPerfilById(IdPerfil);
            if (lstPerfil != null)
            {
                if (lstPerfil.Result.Count() > 0)
                {
                    model.IdPerfil = lstPerfil.Result[0].IdPerfil;
                    model.Nombre = lstPerfil.Result[0].Nombre;
                    model.Activo = lstPerfil.Result[0].Activo;
                }
            }
            ResponseList<PerfilModuloModel> lstPerfilModulo = perfilBusiness.PerfilModulos(IdPerfil, ref arbolModuloAcciones, ref tablaModuloAcciones);
            ViewBag.TablaModuloAcciones = tablaModuloAcciones;
            return View(model);
        }

        [HttpPost]
        public ActionResult ActualizaPerfil(PerfilViewModel perfil)
        {
            Response response = new Entities.Response.Response();
            response.Success = false;
            if (perfil.Activo == false && perfil.Nombre == null)
            {
                perfil.Nombre = "-";
            }
            if (ModelState.IsValid)
            {
                PerfilBusiness perfilBusiness = new PerfilBusiness();
                List<string> modulos = new List<string> { };
                if (perfil.ModulosSeleccionados != null)
                {
                    if (perfil.ModulosSeleccionados.Trim().Length > 0)
                    {
                        modulos = perfil.ModulosSeleccionados.Split('|').ToList();
                    }
                }
                response = perfilBusiness.UpdateCTRLPERFIL(perfil.IdPerfil, perfil.Activo, perfil.Nombre, this.GetUsuario().IdUsuario, modulos);
                return Json(response, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(response, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult AddPerfil()
        {
            string arbolModuloAcciones = "";
            string tablaModuloAcciones = "";
            PerfilViewModel model = new PerfilViewModel { };

            PerfilBusiness perfilBusiness = new PerfilBusiness();
            ResponseList<PerfilModuloModel> lstPerfilModulo = perfilBusiness.PerfilModulos(0, ref arbolModuloAcciones, ref tablaModuloAcciones);

            //ViewBag.TreePerfilModulo = arbolModuloAcciones;
            ViewBag.TablaModuloAcciones = tablaModuloAcciones;
            return View(model);
        }

        [HttpPost]
        public ActionResult AddPerfil(PerfilViewModel perfil)
        {
            Response response = new Entities.Response.Response();
            response.Success = false;
            if (ModelState.IsValid)
            {
                PerfilBusiness perfilBusiness = new PerfilBusiness();
                List<string> modulos = new List<string> { };
                if (perfil.ModulosSeleccionados != null)
                {
                    if (perfil.ModulosSeleccionados.Trim().Length > 0)
                    {
                        modulos = perfil.ModulosSeleccionados.Split('|').ToList();
                    }
                }
                response = perfilBusiness.InsertCTRLPERFIL(perfil.Nombre, this.GetUsuario().IdUsuario, modulos);
                return Json(response, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(response, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult UpdatePerfil(int IdPerfil)
        {
            string arbolModuloAcciones = "";
            string tablaModuloAcciones = "";

            PerfilViewModel model = new PerfilViewModel { };
            PerfilBusiness perfilBusiness = new PerfilBusiness();
            ResponseList<Entities.Common.Perfil> lstPerfil = perfilBusiness.GetPerfilById(IdPerfil);
            if (lstPerfil != null)
            {
                if (lstPerfil.Result.Count() > 0)
                {
                    model.IdPerfil = lstPerfil.Result[0].IdPerfil;
                    model.Nombre = lstPerfil.Result[0].Nombre;
                    model.Activo = lstPerfil.Result[0].Activo;
                }
            }
            ResponseList<PerfilModuloModel> lstPerfilModulo = perfilBusiness.PerfilModulos(IdPerfil, ref arbolModuloAcciones, ref tablaModuloAcciones);
            ViewBag.TablaModuloAcciones = tablaModuloAcciones;
            return View(model);
        }

        [HttpPost]
        public ActionResult UpdatePerfil(PerfilViewModel perfil)
        {
            Response response = new Entities.Response.Response();
            response.Success = false;
            if (perfil.Activo == false && perfil.Nombre == null)
            {
                perfil.Nombre = "-";
            }
            if (ModelState.IsValid)
            {
                PerfilBusiness perfilBusiness = new PerfilBusiness();
                List<string> modulos = new List<string> { };
                if (perfil.ModulosSeleccionados != null)
                {
                    if (perfil.ModulosSeleccionados.Trim().Length > 0)
                    {
                        modulos = perfil.ModulosSeleccionados.Split('|').ToList();
                    }
                }
                response = perfilBusiness.UpdateCTRLPERFIL(perfil.IdPerfil, perfil.Activo, perfil.Nombre, this.GetUsuario().IdUsuario, modulos);
                return Json(response, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(response, JsonRequestBehavior.AllowGet);
            }
        }

    }
}