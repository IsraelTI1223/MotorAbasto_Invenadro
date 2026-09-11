using PortalGRFP.Business.ConfiguracionCobertura;
using PortalGRFP.Entities.Common;
using PortalGRFP.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PortalGRFP.Controllers
{
    public class ConfiguracionCoberturaController : Controller
    {
        // GET: ConfiguracionCobertura
        public ActionResult Index()
        {
            TempData["Formas"] = new CoberturaBusiness().CargaCatalogos(1).Result;
            TempData["Tipos"] = new CoberturaBusiness().CargaCatalogos(2).Result;
            TempData["Clasificaciones"] = new CoberturaBusiness().CargaCatalogos(3).Result;
            TempData["Periodicidad"] = new CoberturaBusiness().CargaCatalogos(4).Result;
            return View();
        }

        public ActionResult AgregarCobertura(FormCollection regla)
        {
            CoberturaMontoPza cobertura = new CoberturaMontoPza();

            cobertura.Forma = int.Parse(regla["forma"].ToString());
            cobertura.Tipo = int.Parse(regla["Tipo"].ToString());
            if (cobertura.Tipo == 1)//rango montos
            {
                cobertura.ClasficacionMonto = regla["ClasficacionMonto"] == null ? 0 : int.Parse(regla["ClasficacionMonto"].ToString());
                cobertura.MontoDe = regla["MontoDe"] == null ? 0 : decimal.Parse(regla["MontoDe"].ToString());
                cobertura.MontoHasta = regla["MontoHasta"] == null ? 0 : decimal.Parse(regla["MontoHasta"].ToString());
                cobertura.ClasficacionPieza = 0;
                cobertura.PiezasDe = 0;
                cobertura.PiezasHasta = 0;
            }
            else if (cobertura.Tipo == 2)//rango piezas
            {
                cobertura.ClasficacionPieza = regla["ClasficacionPieza"] == null ? 0 : int.Parse(regla["ClasficacionPieza"].ToString());
                cobertura.PiezasDe = regla["PiezasDe"] == null ? 0 : int.Parse(regla["PiezasDe"].ToString());
                cobertura.PiezasHasta = regla["PiezasHasta"] == null ? 0 : int.Parse(regla["PiezasHasta"].ToString());
                cobertura.ClasficacionMonto = 0;
                cobertura.MontoDe = 0;
                cobertura.MontoHasta = 0;
            }
            else if (cobertura.Tipo == 3)
            {
                cobertura.ClasficacionMonto = regla["ClasficacionMonto"] == null ? 0 : int.Parse(regla["ClasficacionMonto"].ToString());
                cobertura.MontoDe = regla["MontoDe"] == null ? 0 : decimal.Parse(regla["MontoDe"].ToString());
                cobertura.MontoHasta = regla["MontoHasta"] == null ? 0 : decimal.Parse(regla["MontoHasta"].ToString());
                cobertura.ClasficacionPieza = regla["ClasficacionPieza"] == null ? 0 : int.Parse(regla["ClasficacionPieza"].ToString());
                cobertura.PiezasDe = regla["PiezasDe"] == null ? 0 : int.Parse(regla["PiezasDe"].ToString());
                cobertura.PiezasHasta = regla["PiezasHasta"] == null ? 0 : int.Parse(regla["PiezasHasta"].ToString());
            }
            if (cobertura.Forma == 2)//porcentaje montos
            {
                cobertura.ParticipacionGrupo = regla["ParticipacionGrupo"] == null ? false : bool.Parse(regla["ParticipacionGrupo"].ToString());
                cobertura.ParticipacionDivision = regla["ParticipacionDivision"] == null ? false : bool.Parse(regla["ParticipacionDivision"].ToString());
            }


            cobertura.DiasCoberturaMay = regla["DiasCoberturaMay"] == null ? 0 : int.Parse(regla["DiasCoberturaMay"].ToString());
            cobertura.DiasCoberturaCed = regla["DiasCoberturaCed"] == null ? 0 : int.Parse(regla["DiasCoberturaCed"].ToString());
            cobertura.DiasCoberturaPieCam = regla["DiasCoberturaPieCam"] == null ? 0 : int.Parse(regla["DiasCoberturaPieCam"].ToString());
            cobertura.Periodicidad = regla["Periodicidad"] == null ? 0 : int.Parse(regla["Periodicidad"].ToString());

            var user = this.GetUsuario();
            var coberturaResp = new CoberturaBusiness().GuardarCobertura(cobertura, user.IdUsuario);

            var json = Json(coberturaResp, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = 500000000;
            return json;
        }

        public ActionResult ConsultarCobertura(FormCollection regla)
        {
            CoberturaMontoPza cobertura = new CoberturaMontoPza();

            cobertura.Forma = int.Parse(regla["forma"].ToString());
            cobertura.Tipo = int.Parse(regla["Tipo"].ToString());
            if (cobertura.Tipo == 1)//rango montos
            {
                cobertura.ClasficacionMonto = regla["ClasficacionMonto"] == null ? 0 : int.Parse(regla["ClasficacionMonto"].ToString());
                cobertura.MontoDe = regla["MontoDe"] == null ? 0 : decimal.Parse(regla["MontoDe"].ToString());
                cobertura.MontoHasta = regla["MontoHasta"] == null ? 0 : decimal.Parse(regla["MontoHasta"].ToString());
                cobertura.ClasficacionPieza = 0;
                cobertura.PiezasDe = 0;
                cobertura.PiezasHasta = 0;
            }
            else if (cobertura.Tipo == 2)//rango piezas
            {
                cobertura.ClasficacionPieza = regla["ClasficacionPieza"] == null ? 0 : int.Parse(regla["ClasficacionPieza"].ToString());
                cobertura.PiezasDe = regla["PiezasDe"] == null ? 0 : int.Parse(regla["PiezasDe"].ToString());
                cobertura.PiezasHasta = regla["PiezasHasta"] == null ? 0 : int.Parse(regla["PiezasHasta"].ToString());
            }
            else if (cobertura.Tipo == 3)
            {
                cobertura.ClasficacionMonto = regla["ClasficacionMonto"] == null ? 0 : int.Parse(regla["ClasficacionMonto"].ToString());
                cobertura.MontoDe = regla["MontoDe"] == null ? 0 : decimal.Parse(regla["MontoDe"].ToString());
                cobertura.MontoHasta = regla["MontoHasta"] == null ? 0 : decimal.Parse(regla["MontoHasta"].ToString());
                cobertura.ClasficacionPieza = regla["ClasficacionPieza"] == null ? 0 : int.Parse(regla["ClasficacionPieza"].ToString());
                cobertura.PiezasDe = regla["PiezasDe"] == null ? 0 : int.Parse(regla["PiezasDe"].ToString());
                cobertura.PiezasHasta = regla["PiezasHasta"] == null ? 0 : int.Parse(regla["PiezasHasta"].ToString());
            }
            if (cobertura.Forma == 2)//porcentaje montos
            {
                cobertura.ParticipacionGrupo = regla["ParticipacionGrupo"] == null ? false : bool.Parse(regla["ParticipacionGrupo"].ToString());
                cobertura.ParticipacionDivision = regla["ParticipacionDivision"] == null ? false : bool.Parse(regla["ParticipacionDivision"].ToString());
            }


            cobertura.DiasCoberturaMay = regla["DiasCoberturaMay"] == null ? 0 : int.Parse(regla["DiasCoberturaMay"].ToString());
            cobertura.DiasCoberturaCed = regla["DiasCoberturaCed"] == null ? 0 : int.Parse(regla["DiasCoberturaCed"].ToString());
            cobertura.DiasCoberturaPieCam = regla["DiasCoberturaPieCam"] == null ? 0 : int.Parse(regla["DiasCoberturaPieCam"].ToString());
            cobertura.Periodicidad = regla["Periodicidad"] == null ? 0 : int.Parse(regla["Periodicidad"].ToString());

            var coberturaResp = new CoberturaBusiness().ConsultaCobertura(cobertura);
            var json = Json(coberturaResp, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = 500000000;
            return json;
        }

        public ActionResult EliminarCobertura(int f, int t, int m, int p)
        {
            int forma = f;
            int tipo = t;
            int clasfiMonto = m;
            int ClasficacionPieza = p;

            var coberturaResp = new CoberturaBusiness().EliminarCobertura(forma, tipo, clasfiMonto, ClasficacionPieza);
            var json = Json(coberturaResp, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = 500000000;
            return json;
        }

        public ActionResult ValidarForma(int IdForma)
        {
            var resp = new CoberturaBusiness().ValidarFormaBLL(IdForma);
            var json = Json(resp, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = 500000000;
            return json;
        }
        
        public ActionResult EliminarCambioForma(int IdForma)
        {
            var user = this.GetUsuario();

            var resp = new CoberturaBusiness().EliminarCambioFormaBLL(IdForma, user.IdUsuario);
            var json = Json(resp, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = 500000000;
            return json;
        }

        
    }
}