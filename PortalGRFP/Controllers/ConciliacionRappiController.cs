using PortalGRFP.Business.ConciliacionRappi;
using PortalGRFP.Data.ConciliacionRappi;
using PortalGRFP.Entities.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;

namespace PortalGRFP.Controllers
{
    public class ConciliacionRappiController : Controller
    {
        // GET: ConciliacionRappi
        public ActionResult Index()
        {
            var rappiConciliaciones = new GetListaConciliacionesBusiness().ResumenCargaConciliacion();

            return View(rappiConciliaciones);
        }

        public ActionResult Conciliaciones(Int32 anio, Int32 mes)
        {

            ComboList cmbEmpresas = new ComboList();
            var empresas = cmbEmpresas.ComboEmpresa();
            TempData["empresas"] = empresas;

            TempData["Anio"] = anio;
            TempData["Mes"] = mes;

            var rappiConciliaciones = new GetListaConciliacionesBusiness().ObtenerConciliaciones(anio, mes);
            return View(rappiConciliaciones);
        }

        [HttpPost]
        public ActionResult Obtenerfarmacias(FormCollection datos)
        {
            int _empresa = int.Parse(datos["cadena"].ToString());
            ComboList cmbFarmacias = new ComboList();
            var farmacias = cmbFarmacias.ComboFarmacias(_empresa);
            var json = Json(new { data = farmacias }, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = 500000000;
            return json;
        }


        [HttpPost]
        public ActionResult TicketConciliar(FormCollection conciliar)
        {
            int empresa = int.Parse(conciliar["cadena"].ToString());
            string farmacia = conciliar["farmacia"].ToString();
            string ticket = conciliar["ticket"].ToString();

            ComboList cmbEmpresas = new ComboList();
            var ListaEmpresas = cmbEmpresas.ComboEmpresa();
            string razonSocial = (from e in ListaEmpresas where e.Id == empresa select e.Valor).FirstOrDefault();

            var ticketventa = new GetListaConciliacionesBusiness().ObtenerTicketVenta(razonSocial, ticket, farmacia);
            var json = Json(new { data = ticketventa.Result }, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = 500000000;
            return json;
        }

        [HttpPost]
        public ActionResult ConciliacionManual(FormCollection manual)
        {
            string orderId = manual["orderId"].ToString();
            string operacion = manual["operacion"].ToString();
            string idSucursal = manual["idSucursal"].ToString();
            decimal importeTotal = decimal.Parse(manual["importeTotal"].ToString());
            int accion = int.Parse(manual["accion"].ToString());
            int incidencia = int.Parse(manual["incidencia"].ToString());
            string observacion = manual["observacion"].ToString();
            string ticket = manual["ticket"].ToString();


            var conciliacion = new GetListaConciliacionesBusiness().ConciliacionManual(orderId, operacion, idSucursal, importeTotal, accion, incidencia, observacion, ticket);
            var json = Json(conciliacion, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = 500000000;
            return json;
        }

        [HttpPost]
        public ActionResult ObtenerCatalogoIncidencias()
        {
            ComboList cmbIncidencias = new ComboList();
            var incidencias = cmbIncidencias.ComboIncidencias();
            var json = Json(new { data = incidencias }, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = 500000000;
            return json;
        }

        [HttpPost]
        public JsonResult ConciliacionMensualRappi()
        {
            var cargaMensual = new GetListaConciliacionesBusiness().ConciliacionMensual(Request.Files);

            var json = Json(cargaMensual, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = 500000000;
            return json;
        }
        [HttpPost]
        public ActionResult ResumenCargaRappi()
        {
            var rappiConciliaciones = new GetListaConciliacionesBusiness().ResumenCargaConciliacion();

            return View(rappiConciliaciones);
        }
        [HttpPost]
        public ActionResult MostrarBotonesCadena(FormCollection datos)
        {
            int anio = int.Parse(datos["anio"].ToString());
            int mes = int.Parse(datos["mes"].ToString());
            var muestra = new GetListaConciliacionesBusiness().MostrarBotonesCadena(anio, mes);

            var json = Json(muestra, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = 500000000;
            return json;
        }
        [HttpPost]
        public ActionResult AplicarConciliacion(FormCollection aplicado)
        {
            int anio = int.Parse(aplicado["anio"].ToString());
            int mes = int.Parse(aplicado["mes"].ToString());
            int opcion = int.Parse(aplicado["opcion"].ToString());
            var aplica = new GetListaConciliacionesBusiness().AplicarConciliacion(anio, mes, opcion);

            var json = Json(aplica, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = 500000000;
            return json;
        }

        [HttpPost]
        public ActionResult MontoConciliar(FormCollection conciliar)
        {
            int empresa = int.Parse(conciliar["cadena"].ToString());
            string farmacia = conciliar["farmacia"].ToString();
            string importe = conciliar["importe"].ToString();
            string fecha = conciliar["fecha"].ToString();

            ComboList cmbEmpresas = new ComboList();
            var ListaEmpresas = cmbEmpresas.ComboEmpresa();
            string razonSocial = (from e in ListaEmpresas where e.Id == empresa select e.Valor).FirstOrDefault();

            var ticketventa = new GetListaConciliacionesBusiness().ObtenerTicketVentaCard(razonSocial, farmacia, fecha, importe);
            var json = Json(new { data = ticketventa.Result }, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = 500000000;
            return json;
        }

        [HttpPost]
        public ActionResult ConciliacionManualCard(FormCollection manual)
        {
            int Cadena = int.Parse(manual["razons"].ToString());
            string operacion = manual["operacion"].ToString();
            string idSucursal = manual["idSucursal"].ToString();
            decimal importeTotal = decimal.Parse(manual["importeTotal"].ToString());
            int accion = int.Parse(manual["accion"].ToString());
            int incidencia = int.Parse(manual["incidencia"].ToString());
            string observacion = manual["observacion"].ToString();
            string ticket = manual["ticket"].ToString();

            ComboList cmbEmpresas = new ComboList();
            var ListaEmpresas = cmbEmpresas.ComboEmpresa();
            string razonSocial = (from e in ListaEmpresas where e.Id == Cadena select e.Valor).FirstOrDefault();

            var conciliacion = new GetListaConciliacionesBusiness().ConciliacionManualCard(razonSocial, operacion, idSucursal, importeTotal, accion, incidencia, observacion, ticket);
            var json = Json(conciliacion, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = 500000000;
            return json;
        }


        public void DescargarExcel(int anio, int mes)
        {
            var excel = new GetListaConciliacionesBusiness().DescargarExcel(anio, mes);

            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment: filename=conciliacion" + mes.ToString() + anio.ToString() + ".xlsx");
            Response.BinaryWrite(excel);
            Response.End();

            //return new FileStreamResult(excel, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
            //{
            //    FileDownloadName = "conciliacion"+mes.ToString()+anio.ToString()+".xlsx"
            //};
        }

        public ActionResult Plantilla()
        {

            return View();
        }
    }
}