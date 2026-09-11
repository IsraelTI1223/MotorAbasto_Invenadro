using PortalGRFP.Business.GeneralesAbasto;
using PortalGRFP.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PortalGRFP.Controllers
{
    public class PVDPredictivoController : Controller
    {
        // GET: PVDPredictivo
        public ActionResult Index()
        {
            var lst = new GeneralesAbastoBLL().GetParamsPVD();

            if (lst == null) return View();

            return View(lst);
        }
        public ActionResult GuardarConf(FormCollection data)
        {
            var gAbasto = new GeneralesAbastoBLL();
            ParametrosPVDpredictivo model = new ParametrosPVDpredictivo();

            model.ID = int.Parse(data["ID"].ToString());
            model.PorcCrecimiento = int.Parse(data["PorcCrecimiento"].ToString());
            model.PorcDecremento = int.Parse(data["PorcDecremento"].ToString());
            model.PVDHistorico = int.Parse(data["PVDHistorico"].ToString());

            model.PVDActual = int.Parse(data["PVDActual"].ToString());

            string resp_messaje = "";

            if (gAbasto.UIParamsPVDBLL(model) == 1)
            {
                resp_messaje = "Los cambios se guardaron de forma correcta";
            }
            else
            {
                resp_messaje = "Error al guardar, intente nuevamente";
            }

            var json = Json(resp_messaje, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = 500000000;
            return json;
        }
    }
}