using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PortalGRFP.Business.BConsultaMTA;
using PortalGRFP.Entities.Common;
using System.IO;
using System.Security.Cryptography;
using PortalGRFP.Extensions;
using PortalGRFP.Business.ComprasEspeciales;

namespace PortalGRFP.Controllers
{
    public class MTAConsultaController : Controller
    {
        // GET: MTAConsulta
        public ActionResult Index()
        {
            var consultamta = new GetListaConsultaMTABusiness().ObtenerConsultaMTA();

            return View(consultamta);
        }



        //[HttpPost]
        //// SELECT DETALLE CONSULTA
        //public ActionResult DetalleConsulta(string id_input)
        //{

        //    var consultadetalle = new GetListaConsultaMTABusiness().ObtenerConsultaDetalleMTA(id_input);

        //    return View(consultadetalle);
        //}

        

        public ActionResult DetalleConsulta(string id_input)
        {
               var consultadetalle = new GetListaConsultaMTABusiness().ObtenerConsultaDetalleMTA(id_input);

              return View(consultadetalle);

           //// return View();
        }

        public ActionResult DetalleCifrasInvenadro(string id_input)
        {

            var user = this.GetUsuario().IdUsuario;
            var consultacifras = new GetListaConsultaMTABusiness().ObtenerCifrasInvenadroMTA(id_input,user);

            return View(consultacifras);

            //// return View();
        }

        public ActionResult DetalleCifrasVenta(string id_input)
        {
            var user = this.GetUsuario().IdUsuario;
            var consultacifras = new GetListaConsultaMTABusiness().ObtenerConsultaCifrasMTA(id_input,user);

            return View(consultacifras);

            //// return View();
        }

        public ActionResult DetalleCifrasNegados(string id_input)
        {
            var user = this.GetUsuario().IdUsuario;
            var consultacifras = new GetListaConsultaMTABusiness().ObtenerCifrasNegadoMTA(id_input, user);

            return View(consultacifras);

            //// return View();
        }
        public ActionResult DetalleCifrasTransito(string id_input)
        {
            var user = this.GetUsuario().IdUsuario;
            var consultacifras = new GetListaConsultaMTABusiness().ObtenerCifrasTransitoMTA(id_input, user);

            return View(consultacifras);

            //// return View();
        }

        public ActionResult DetalleCifrasLista(string id_input)
        {
            var user = this.GetUsuario().IdUsuario;
            var consultacifras = new GetListaConsultaMTABusiness().ObtenerCifrasListaMTA(id_input, user);

            return View(consultacifras);

            //// return View();
        }
    }
}