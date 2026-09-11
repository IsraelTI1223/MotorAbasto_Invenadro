using Microsoft.Owin;
using PortalGRFP.Business.ComprasEspeciales;
using PortalGRFP.Business.Configuracion_Invenadro;
using PortalGRFP.Entities.Common;
using PortalGRFP.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PortalGRFP.Controllers
{
    public class ConfiguracionInvenadroController : Controller
    {
        #region Vista conf general
        // GET: ConfiguracionInvenadro
        public ActionResult Index()
        {
            //TempData["CDRs"] = new ReglaInvenadroBusiness().GetCDRsInfoBLL(1,0,0).Result;
            return View();
        }
        // GET: ConfiguracionInvenadro/Details/5
        public ActionResult Details()
        {
            var aplica = new ReglaInvenadroBusiness().GetInvenadroRegla();
            var json = Json(aplica, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = 500000000;
            return json;
        }

        // GET: ConfiguracionInvenadro/Create
        public ActionResult GuardarRegla(System.Web.Mvc.FormCollection formulario)
        {
            Invenadro invenadro = new Invenadro();
            invenadro.LimiteCostoProductos = bool.Parse(formulario["LimiteCosto"].ToString());
            invenadro.Monto = decimal.Parse(formulario["txtMonto"].ToString());
            invenadro.PiezasProductos = int.Parse(formulario["txtCantidad"].ToString());
            invenadro.PVD = bool.Parse(formulario["PVD"].ToString());
            invenadro.InvenadroCero = bool.Parse(formulario["Inve0"].ToString());
            invenadro.ActivarProducto = bool.Parse(formulario["Activar"].ToString());
            invenadro.InactivarProductos = bool.Parse(formulario["Inactiva"].ToString());
            invenadro.PorcentajeCoincidencia = decimal.Parse(formulario["txtPorcentajeC"].ToString());
            invenadro.Actualizacion_automatica = formulario["Actualizacion_automatica"] != null && bool.Parse(formulario["Actualizacion_automatica"]);
            invenadro.diasPVD = int.Parse(formulario["textDiasPVD"].ToString());
            invenadro.diasAutomatico = int.Parse(formulario["textDiasAutomatico"].ToString());

            var user = this.GetUsuario();

            var aplica = new ReglaInvenadroBusiness().GuardarInvenadro(invenadro, user.IdUsuario);
            var json = Json(aplica, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = 500000000;
            return json;
        }

        #endregion

        #region Balanceo
        public ActionResult BalanceoCDRs()
        {
            TempData["CDRs"] = new ReglaInvenadroBusiness().GetCDRsInfoBLL(1, 0,0).Result;
            return View();
        }

        public ActionResult ActualizaCDRs(System.Web.Mvc.FormCollection formCDR)
        {
            InfoCDR cedis = new InfoCDR();
            cedis.Id = int.Parse(formCDR["txtIdAgencia"].ToString());
            cedis.Farmacias = int.Parse(formCDR["txtFarmacias"].ToString());
            cedis.MontoCDR = decimal.Parse(formCDR["txtMontoPesos"].ToString());
            cedis.MontoNecesidad = decimal.Parse(formCDR["txtMontoNecesidad"].ToString());
            cedis.PedidosRealizar = int.Parse(formCDR["txtPedidos"].ToString());
            cedis.PorcentajeCompraMayor = decimal.Parse(formCDR["txtCompraMayorPrcnt"].ToString());
            cedis.PorcentajeCompraMenor = decimal.Parse(formCDR["txtCompraMenorPrcnt"].ToString());
            cedis.MontoCompraMayor = decimal.Parse(formCDR["txtMontoMayor"].ToString());
            cedis.MontoCompraMenor = decimal.Parse(formCDR["txtMontoMenor"].ToString());

            var user = this.GetUsuario();

            var update = new ReglaInvenadroBusiness().ActualizaCDRsBLL(cedis, user.IdUsuario);
            var json = Json(update, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = 500000000;
            return json;
        }

        public JsonResult GetCDRsInfo(int agencia_id, decimal MontoCDR)
        {
            var user = this.GetUsuario();
            if(MontoCDR == 0)
            {
                var info = new ReglaInvenadroBusiness().GetCDRsInfoBLL(2, agencia_id, MontoCDR).Result;
                return Json(new
                {
                    Success = info != null && info.Any(),
                    Result = info
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var info = new ReglaInvenadroBusiness().GetCDRsBalanceBLL(agencia_id, MontoCDR).Result;
                return Json(new
                {
                    Success = info != null && info.Any(),
                    Result = info
                }, JsonRequestBehavior.AllowGet);
            }
        }


        public void ExportarBloquesCDR(int agencia)
        {
            var csv = new ReglaInvenadroBusiness().GetExportBloquesCDRBLL(agencia);
            Response.Clear();
            Response.ContentType = "text/csv";
            Response.AddHeader(
                "content-disposition",
                $"attachment; filename=Bloques_CDR_{agencia}_{DateTime.Now:yyyyMMdd}.csv"
            );
            Response.BinaryWrite(csv);
            Response.End();
        }

        #endregion

        #region Vista Excluidos 
        public ActionResult Exclusiones()
        {
            TempData["Sucursal"] = new ReglaInvenadroBusiness().GetSucursalesComboBLL().Result;
            return View();
        }

        //public JsonResult GetSucursalesCombo()
        //{
        //    var ExcluidosTbl = new ReglaInvenadroBusiness().GetSucursalesComboBLL();

        //    var json = Json(ExcluidosTbl, JsonRequestBehavior.AllowGet);
        //    json.MaxJsonLength = 500000000;
        //    return json;
        //}
        public JsonResult GetProdExcluidos(string SKU, string Sucursales)
        {
            var ExcluidosTbl = new ReglaInvenadroBusiness().GetExcluidosBLL(SKU, Sucursales);

            var json = Json(ExcluidosTbl, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = 500000000;
            return json;
        }

        public void DescargarExcelExcluidos(string SKU = "", string Sucursales = "")
        {
            var excel = new ReglaInvenadroBusiness().getExcelExcluidosInvenadroBLL(SKU, Sucursales);

            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader(
                "content-disposition",
                $"attachment; filename=Excluidos_Invenadro_{DateTime.Now:yyyyMMdd}.xlsx"
            );

            Response.BinaryWrite(excel);
            Response.End();
        }

        //public JsonResult AutorizarExcluidosMasivo(HttpPostedFileBase archivoExcel)
        //{
        //    var user = this.GetUsuario();
        //    if (archivoExcel == null || archivoExcel.ContentLength == 0)
        //    {
        //        return Json(new { Success = false, Message = "Archivo inválido" });
        //    }

        //    var result = new ReglaInvenadroBusiness().AutorizarExcluidosMasivoBLL(archivoExcel,user.IdUsuario);

        //    return Json(result);
        //}

        #endregion

        #region vista de carga inv excel
        // POST: ConfiguracionInvenadro/Create
        public ActionResult CargaManual()
        {
            var Sucursales = new ReglaInvenadroBusiness().Filtros(8).Result;
            TempData["MarcasSucTMP"] = Sucursales;

            return View();
        }

        [HttpPost]
        public JsonResult CargaInvenadro(System.Web.Mvc.FormCollection formulario)
        {
            var farmacias = formulario["Farmacias"];//"13213,4645,87989"

            var cargaMasiva = new ReglaInvenadroBusiness().CargaInvenadro(Request.Files, this.GetUsuario().IdUsuario, farmacias);

            SalidaInvenadro salida = new SalidaInvenadro();
            if (cargaMasiva.Success)
            {
                var bitacora = new ReglaInvenadroBusiness().Getinvenadro(2);
                salida.Bitacora = bitacora.Result;
                var log = new ReglaInvenadroBusiness().Getinvenadro(1);
                salida.Log = log.Result;
                Session["salida"] = salida;
            }
            var json = Json(cargaMasiva, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = 500000000;
            return json;
        }

        public ActionResult SalidaInvenadro()
        {
            var salida = (SalidaInvenadro)Session["salida"];
            var json = Json(salida, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = 500000000;
            return json;
        }


        [HttpPost]
        public JsonResult BorrarInvenadro(System.Web.Mvc.FormCollection formulario)
        {
            var farmacias = formulario["MarcaSuc"];

            var user = this.GetUsuario();

            var cargaMasiva = new ReglaInvenadroBusiness().BorrarInvenadro(farmacias, user.IdUsuario);

            var json = Json(cargaMasiva, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = 500000000;
            return json;
        }
        #endregion

        #region Resumen Invenadro

        public ActionResult ResumenInvenadro()
        {
            TempData["Motivos"] = new ReglaInvenadroBusiness().GetMotivosComboBLL().Result;
            return View();
        }
        public JsonResult GetTablaEstatus(string[] Estatus)
        {
            var ExcluidosTbl = new ReglaInvenadroBusiness().GetEstatusBLL(Estatus);

            var json = Json(ExcluidosTbl, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = 500000000;
            return json;
        }

        public ActionResult DescargarDetalleResumen(string[] Estatus)
        {
            var excel = new ReglaInvenadroBusiness().getExcelResumenInvenadroBLL(Estatus);

            var cookie = new HttpCookie("download", "true");
            cookie.Path = "/";

            Response.Cookies.Add(cookie);

            return File(
                excel,
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                $"Detalle_Estatus_Invenadro_{DateTime.Now:yyyyMMdd}.xlsx"
            );
        }

        public ActionResult DescargarDetalleTabla(string oFlag, string oEx, string Motivo)
        {
            var excel = new ReglaInvenadroBusiness().getExcelDetalleInvenadroBLL(oFlag, oEx);

            var cookie = new HttpCookie("download", "true");
            cookie.Path = "/";

            Response.Cookies.Add(cookie);

            return File(
                excel,
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                $"Detalle_{Motivo}.xlsx"
            );
        }

        [HttpPost]
        public JsonResult AutorizarArchivo(string oFlag, string oEx)
        {
            int user = this.GetUsuario().IdUsuario;

            var cargaMasiva = new ReglaInvenadroBusiness().AutorizaInvenadro(Request.Files, oEx,user);

            var json = Json(cargaMasiva, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = 500000000;
            return json;
        }

        #endregion
    }
}
