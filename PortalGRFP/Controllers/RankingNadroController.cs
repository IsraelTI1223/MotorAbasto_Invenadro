using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PortalGRFP.Business.ConfiguracionRanking;
using PortalGRFP.Extensions;
using PortalGRFP.Entities.Common;

namespace PortalGRFP.Controllers
{
    public class RankingNadroController : Controller
    {
        // GET: RankingNadro
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult BorrarRanking(FormCollection formulario)
        {
            //var farmacias = formulario["MarcaSuc"];

            var user = this.GetUsuario();

            var cargaMasiva = new ReglaRanking().BorrarRanking(user.IdUsuario);

            var json = Json(cargaMasiva, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = 500000000;
            return json;
        }

        [HttpPost]
        public JsonResult CargaRanking(FormCollection formulario)
        {
            //var farmacias = formulario["Farmacias"];//"13213,4645,87989"
            var cargaMasiva = new ReglaRanking().CargaRanking(Request.Files, this.GetUsuario().IdUsuario);

            SalidaRanking salida = new SalidaRanking(); //,me quede aqui 
            SalidaRankingCont salida2 = new SalidaRankingCont(); //,me quede aqui 

            if (cargaMasiva.Success)
            {
           
                var log = new ReglaRanking().GetRanking(1);
                salida.Log = log.Result;
                Session["salida"] = salida;
                var log2 = new ReglaRanking().ConteoRanking(2);
                salida2.Cont = log2.Result;
                Session["salida2"] = salida2;
            }
            var json = Json(cargaMasiva, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = 500000000;
            return json;
        }

        public ActionResult SalidaRanking()
        {
            var salida = (SalidaRanking)Session["salida"];
            var json = Json(salida, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = 500000000;
            return json;
        }
        public ActionResult ConteoRanking()
        {
            var salida2 = (SalidaRankingCont)Session["salida2"];
            var json = Json(salida2, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = 500000000;
            return json;
        }


    
    }
}