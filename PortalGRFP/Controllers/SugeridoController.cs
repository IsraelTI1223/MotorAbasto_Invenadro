using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PortalGRFP.Business.ComprasEspeciales;
using PortalGRFP.Business.Sugerido;
using PortalGRFP.Entities.Common;
using PortalGRFP.Entities.Common.Sugerido;
using PortalGRFP.Entities.Response;
using PortalGRFP.Extensions;
using static PortalGRFP.Entities.Common.ListSucursalesGeneralesSugeridoValidarModel;

namespace PortalGRFP.Controllers
{
    public class SugeridoController : Controller
    {
        // GET: Sugerido
        public ActionResult ListaSugerido()
        {
            return View();
        }

        public ActionResult GetListaSugerido()
        {
            var user = this.GetUsuario();

            var log = new SugeridoBLL().GetListaSugeridoBLL(user.IdUsuario);
            var json = Json(log.Result, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = 500000000;
            return json;
        }

        public ActionResult ValidarMontosSugerido(string IdSugerido)
        {
            long sugerido = long.Parse(IdSugerido);

            var suc_validar = new SugeridoBLL().GetAccessValidarMontosBLL(sugerido, this.GetUsuario().IdUsuario);

            if (suc_validar == 0)
            {
                return RedirectToAction("OrdenesCompra", "OC");
            }

            ViewBag.IdSugerido = IdSugerido;    
            return View();
            


            
        }
        public ActionResult GetListMontosValidar(string idsugerido)
        {
            //idsugerido = "202102231233";
            var usuario = this.GetUsuario();

            //var action = per.FirstOrDefault().Acciones.ToList();

            var log = new SugeridoBLL().GetListMontosValidarBLL(idsugerido, usuario.IdUsuario);
            var json = Json(log.Result, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = 500000000;
            return json;
        }


        public ActionResult EliminarSugerido(string idsugerido)
        {
            int user = this.GetUsuario().IdUsuario;

            var resp = new SugeridoBLL().DeleteSugeridoListBLL(idsugerido, user);

            var json = Json(resp, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = 500000000;
            return json;
        }

        public ActionResult Accionar(string IdSugerido, string estatus)
        {
            string accion = "";

            var json = Json(accion, JsonRequestBehavior.AllowGet);
            if (estatus == "Negados")//Validar Negados ok 
            {
                var listaSucNeg = new SugeridoBusiness().ObtenerListaSucursales(Int64.Parse(IdSugerido)).Result.Select(x => new SucursalesSugerido { ID = x.Id.ToString(), Valor = x.Valor }).ToList();
                ListSucursales listSucursales = new ListSucursales();
                listSucursales.Sucursales = listaSucNeg;

                var response = new SugeridoBusiness().ValidarNegadosBLL(listSucursales, long.Parse(IdSugerido));

                json = Json(response, JsonRequestBehavior.AllowGet);
                json.MaxJsonLength = 500000000;
            }
            else if (estatus == "Calculado")//calcular sugerido
            {
                var response = new SugeridoBusiness().CalcularSugerido(Int64.Parse(IdSugerido));
                json = Json(response, JsonRequestBehavior.AllowGet);
                json.MaxJsonLength = 500000000;
            }
            else if (estatus == "Asignacion") //asignar proveedor ok 
            {
                var response = new SugeridoBusiness().AsignacionProveedor(Int64.Parse(IdSugerido));
                json = Json(response, JsonRequestBehavior.AllowGet);
                json.MaxJsonLength = 500000000;

            }
            else if (estatus == "Validado")//validacion de montos
            {
                return RedirectToAction("ValidarMontosSugerido", "Sugerido", new { IdSugerido = IdSugerido.ToString() });
            }
            else if (estatus == "Aplicado") //aplicar sugerido
            {
                int user = this.GetUsuario().IdUsuario;
                var resp = new SugeridoBLL().AplicarSugeridoBLL(IdSugerido, user, 6);
                json = Json(resp, JsonRequestBehavior.AllowGet);
                json.MaxJsonLength = 500000000;
            }
            else if (estatus == "Ordenado") //ordenar sugerido
            {
                return RedirectToAction("OrdenesCompra", "OC");//new { id_sugerido = IdSugerido.ToString() }
            }
            else if (estatus == "Enviado")//Sugerido enviado
            {
                //var response = new SugeridoBusiness().AsignacionProveedor(Int64.Parse(IdSugerido));
                json = Json("", JsonRequestBehavior.AllowGet);
                json.MaxJsonLength = 500000000;
            }
            return json;
        }
        [HttpPost]
        public JsonResult GuardarValidacionMontos(List<ValidarMontosModel> model)
        {

            int IdUsuario = this.GetUsuario().IdUsuario;

            var resp = new SugeridoBLL().UpdateSugeridoListBLL(model, IdUsuario);

            var json = Json(resp, JsonRequestBehavior.AllowGet);

            json.MaxJsonLength = 500000000;
            return json;
        }

        public ActionResult AplicarSugerido(string idsugerido)
        {
            int user = this.GetUsuario().IdUsuario;

            var resp = new SugeridoBLL().AplicarSugeridoBLL(idsugerido, user, 6);

            var json = Json(resp, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = 500000000;
            return json;
        }

        public ActionResult ValidarPermiso(string cuenta)
        {
            var usuario = this.GetUsuario();

            if (cuenta != usuario.Correo)
            {
                var response = new Response();

                response.Success = false;
                response.Message = "Cuenta no autorizada para validar los montos de este sugerido";

                var jsonResult = Json(response, JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = 500000000;
                return jsonResult;
            }
            else
            {
             
                var pp = usuario.Permisos.ToList().Where(x => x.Modulo == "Sugerido");
        
                var per = pp.FirstOrDefault().SubModulos.Where(x => x.SubModulo == "Validación de Montos");
                       
                var IdModulo = per.FirstOrDefault().IdSubModulo;
                        
                int user = this.GetUsuario().IdUsuario;
                        
                var resp = new SugeridoBLL().GetValidarPermisoBLL(cuenta, IdModulo, user);

                var json = Json(resp, JsonRequestBehavior.AllowGet);
                        
                json.MaxJsonLength = 500000000;
                        
                return json;
            }

          
        }

        public ActionResult GetDetalladoSugerido(string idSugerido)
        {
            var user = this.GetUsuario();

            var response = new SugeridoBLL().GetDetalladoSugerido(Int64.Parse(idSugerido), user.IdUsuario);
            var json = Json(response, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = 500000000;
            return json;
        }


    }
}