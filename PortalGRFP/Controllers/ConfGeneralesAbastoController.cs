using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PortalGRFP.Entities.Common;
using PortalGRFP.Entities.Common.Capacity;
using PortalGRFP.Business.GeneralesAbasto;
using PortalGRFP.Business.Capacity;
using System.Dynamic;
using PortalGRFP.Extensions;
using PortalGRFP.Business.Mantenimiento;

namespace PortalGRFP.Controllers
{
    public class ConfGeneralesAbastoController : Controller
    {
        // GET: ConfGeneralesAbasto
        public ActionResult Negados()
        {
            var lst = new GeneralesAbastoBLL().GetNegadosBLL();

            if (lst == null) return View();

            return View(lst);
        }

        [HttpPost]
        public ActionResult Negados(NegadosViewModel model)
        {
            //model.IdNegado = this.GetUsuario().IdUsuario;

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var gAbasto = new GeneralesAbastoBLL();

            if (gAbasto.UINegadosBLL(model) == 1)
            {

                return View(model);
            }

            // return Redirect(Url.Content("~/ConfGeneralesAbasto/Negados"));
            return View(model);
        }

        public ActionResult GuardarNegados(FormCollection data)
        {


            var gAbasto = new GeneralesAbastoBLL();
            NegadosViewModel model = new NegadosViewModel();


            //PiezasXArticulo
            //Costo_Articulo
            //PiezasXFarmacia
            //Imp_Negados_XDia
            //Invenadro
            //Resto_Productos
            //Dias_Vigencia_Mayorista
            //Dias_Vigencia_Cedis
            //Dias_Vigencia_Pie_Camion

            //model.chkRanking = (data["chkRanking"] ?? "").Equals("true", StringComparison.CurrentCultureIgnoreCase);

            model.IdNegado = int.Parse(data["IdNegado"].ToString());
            model.PiezasXArticulo = int.Parse(data["PiezasXArticulo"].ToString());
            model.Costo_Articulo = decimal.Parse(data["Costo_Articulo"].ToString());
            model.PiezasXFarmacia = int.Parse(data["PiezasXFarmacia"].ToString());
            model.Imp_Negados_XDia = decimal.Parse(data["Imp_Negados_XDia"].ToString());
            model.Invenadro = data["Invenadro"].ToString();
            model.Resto_Productos = data["Resto_Productos"].ToString();
            model.Dias_Vigencia_Mayorista = int.Parse(data["Dias_Vigencia_Mayorista"].ToString());
            model.Dias_Vigencia_Cedis = int.Parse(data["Dias_Vigencia_Cedis"].ToString());
            model.Dias_Vigencia_Pie_Camion = int.Parse(data["Dias_Vigencia_Pie_Camion"].ToString());
            model.chkRanking = bool.Parse(data["chkRanking2"].ToString());
            model.Rank_montos = decimal.Parse(data["Rank_montos"].ToString());
            model.Rank_piezas = Int32.Parse(data["Rank_piezas"].ToString());


            string resp_messaje = "";



            if (gAbasto.UINegadosBLL(model) == 1)
            {
                resp_messaje = "Los cambios se guardaron de forma correcta";
                //return View(model);
            }
            else
            {
                resp_messaje = "Error al guardar, intente nuevamente";
            }


            var json = Json(resp_messaje, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = 500000000;
            return json;
        }

        public ActionResult PedidosCompEspeciales()
        {
            //dynamic model = new ExpandoObject();
            //lstPedidos = null;
            var lstPedidos = new GeneralesAbastoBLL().GetPedidosComprasEspecialesBLL(1, 0).OrderByDescending(d => d.Id).ToList();
            //model.lstPedidos = new GeneralesAbastoBLL().GetPedidosComprasEspecialesBLL(1, 0).OrderBy(d => d.Id).ToList();
            //model.tipoPedido = new PedidosCompEspecialesViewModel();

            return View(lstPedidos);
        }

        [HttpPost]
        public ActionResult PedidosCompEspeciales(PedidosCompEspecialesViewModel model)
        {

            if (!ModelState.IsValid)
            {
                return View(model);
            }
            model.IdUsuario = this.GetUsuario().IdUsuario;
            var resp = new GeneralesAbastoBLL().UIPedidosComprasEspecialesBLL(model);

            //return Content(resp.ToString());

            var json = Json(resp, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = 500000000;
            return json;
        }

        //public ActionResult EliminarPedidoEspecial(PedidosCompEspecialesViewModel model)
        //{

        //    //var user = this.GetUsuario();


        //    //GetListaProvBusiness Datos = new GetListaProvBusiness();
        //    //var idUs = Datos.DeleteData(model, user.IdUsuario.ToString());

        //    if (!ModelState.IsValid)
        //    {
        //        return View(model);
        //    }
        //    model.IdUsuario = this.GetUsuario().IdUsuario;
        //    var resp = new GeneralesAbastoBLL().UIPedidosComprasEspecialesBLL(model);

        //    //return Content(resp.ToString());

        //    return RedirectToAction("PrioridadCompraProveedor", "ConfGeneralesAbastoComProv");

        //}

        [HttpPost]
        public JsonResult EliminarPedidoEspecial(PedidosCompEspecialesViewModel model)
        {

            model.IdUsuario = this.GetUsuario().IdUsuario;

            var resp = new GeneralesAbastoBLL().UIPedidosComprasEspecialesBLL(model);

            var json = Json("ok", JsonRequestBehavior.AllowGet);

            json.MaxJsonLength = 500000000;
            return json;
        }



        public ActionResult AlgoritmoCompra()
        {

            var lst = new GeneralesAbastoBLL().GetAlgoritmoCompraBLL();

            if (lst == null)
            {
                return View();

            }
            return View(lst);
        }

        [HttpPost]
        public ActionResult AlgoritmoCompra(AlgoritmoCompraViewModel model)
        {
            //model.TotalPonderacion = model.PonderacionSemana1 + model.PonderacionSemana2 + model.PonderacionSemana3 + model.PonderacionSemana4;



            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var resp = new GeneralesAbastoBLL().UIAlgoritmoCompraBLL(model);

            return View(model);
            //var json = Json("ok", JsonRequestBehavior.AllowGet);

            //json.MaxJsonLength = 500000000;
            //return json;


        }




        public ActionResult Capacity()
        {
            var lstMarca = new CapacityBusiness().GetMarcas(1, this.GetUsuario().IdUsuario);
            TempData["lstMarca"] = lstMarca;
            return View(lstMarca);

        }



        [HttpPost]
        public JsonResult ResponseCapacity(FormCollection formulario)
        {
            var flag = Convert.ToInt16(formulario["Flags"]);
            var marca = formulario["Marca"];
            responseCapacity Respuesta = new responseCapacity();
            //var lstMarca = new cCapacity();
            Session["Descarga"] = null;

            if (flag == 2)
            {
                var files = Request.Files;
                var archivo = files["fileExcel"];

                Respuesta = new CapacityBusiness().setMarcas(flag, this.GetUsuario().IdUsuario, marca, files);

            }

            Session["Descarga"] = Respuesta.listReportes;

            var json = Json(new { Respuesta.respuesta, tblValida = Respuesta.listValida, tblReporte = Respuesta.listReportes }, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = 500000000;
            return json;
        }

        public void DescargarExcel()
        {

            var reportes = (List<tblReporte>)Session["Descarga"];

            var excel = new CapacityBusiness().GetExcelSucursalesNuevas(reportes);

            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment: filename=report.xlsx");
            Response.BinaryWrite(excel);
            Response.End();
        }

        [HttpPost]
        public JsonResult CatCapacity(FormCollection formulario)
        {

            var flag = 0;
            var nombreInput = formulario["nombreInput"];
            var checkinv = formulario["checkinv"];
            var idinput = formulario["idInput"];
            var Invenadro = checkinv == "on" ? true : false;

            if (idinput == "0")
            {
                flag = 2;
            }
            else
            {
                flag = 5;
            }

            var Resp = new CapacityBusiness().setCatCapacity(flag, nombreInput, Invenadro, idinput, this.GetUsuario().IdUsuario);

            var json = Json(new { Resp }, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = 500000000;
            return json;
        }

        [HttpPost]
        public JsonResult DelCatCapacity(int Id)
        {
            var flag = 3;

            var Resp = new CapacityBusiness().DelCatCapacity(flag, Id);

            var json = Json(new { Resp }, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = 500000000;
            return json;

        }

        [HttpPost]
        public JsonResult DelCapacity(FormCollection formulario)
        {
            var flag = Convert.ToInt16(formulario["Flags"]);
            var nombreInput = formulario["Marca"];
            var usuario = this.GetUsuario().IdUsuario;

            var Resp = new CapacityBusiness().DelCapacity(flag, nombreInput, usuario);
            var json = Json(new { Resp }, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = 500000000;
            return json;

        }
         
        public ActionResult ExcepcionesInvenadro()
        {
            return View();
        }

        public ActionResult CargaExcepcionesInvenadro(FormCollection formulario)
        {
            ExcepcionesInvenadroModel consulta = new ExcepcionesInvenadroModel();

            consulta.FechaInicio = formulario["FechaInicial"] == null ? "" : formulario["FechaInicial"];
            consulta.FechaFin = formulario["FechaFinal"] == null ? "" : formulario["FechaFinal"];

            var cargaMasiva = new GeneralesAbastoBLL().CargaExcepcionesInvenadroBll(consulta, this.GetUsuario().IdUsuario);

            var json = Json(cargaMasiva, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = 500000000;
            return json;
        }

    }
}

