using PortalGRFP.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PortalGRFP.Entities.Common;
using PortalGRFP.Business.ComprasEspeciales;
using PortalGRFP.Business.Mantenimiento;


namespace PortalGRFP.Controllers
{
    public class ReporteNeg2PedController : Controller
    {
        // GET: ReporteNeg2Ped
        public ActionResult Index()
        {
            var user = this.GetUsuario();

            var Grupos = new SugeridoBusiness().Filtros(3);
            //var gruposUsuario = (List<Combo3>)Session["Grupos_Usuario"];
            Session["Grupos_Usuario"] = new GruposBLL().GetGrupos_Usuario(4).Result;

            List<Combo3> gruposUsuario = (List<Combo3>)Session["Grupos_Usuario"];

            var usuarioGrupos = (from g in gruposUsuario where g.IdFiltro == user.IdUsuario && g.Valor == "True" select g.Id).ToList();


            TempData["GrupoTMP"] = (from gr in Grupos.Result where usuarioGrupos.Contains(gr.Id) select gr).ToList();

            var Sucursales = new SugeridoBusiness().Filtros2(4);
            Session["SucursalTMP"] = Sucursales.Result;

            return View();
        }

        public ActionResult GetSucursal(FormCollection datos)
        {
            var _idGrupo = datos["idGrupo"];
            var buscar = _idGrupo.Split(',');
            List<Combo3> sucursales = (List<Combo3>)Session["SucursalTMP"];
            var jsonfarmacia = (from N in sucursales
                                where buscar.Contains(N.IdFiltro.ToString())
                                select new { N.Id, N.Valor });
            return Json(jsonfarmacia, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetDTCuenta(FormCollection form)
        {
            //Int64 NuevoFolio = (Int64)Session["FolioSugerido"];
            //string incidencia = data["value"].ToString();
            int usuario = this.GetUsuario().IdUsuario;
            var Grupo = form["Grupo"];
            
            var Sucursales = form["Sucursal"];
            var fechaini = form["txtFini"];
            var fechafin = form["txtFfin"];

            var log = new SugeridoBusiness().GetDTReporteNegados(Sucursales, fechaini, fechafin);
            var json = Json(log, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = 500000000;
            return json;
        }
    }
}