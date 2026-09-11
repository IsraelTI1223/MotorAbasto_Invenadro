using PortalGRFP.Business.ComprasEspeciales;
using PortalGRFP.Entities;
using PortalGRFP.Entities.Common;
using PortalGRFP.Entities.Response;
using PortalGRFP.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static PortalGRFP.Entities.Common.ListSucursalesGeneralesSugeridoValidarModel;
using PortalGRFP.Business.Mantenimiento;

namespace PortalGRFP.Controllers
{
    public class ValidaSugeridoController : Controller
    {
        // GET: ValidaSugerido
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

            var Proveedore = new SugeridoBusiness().Filtros2(5);
            Session["ProveedorTMP"] = Proveedore.Result;

            var Conceptos = new SugeridoBusiness().Filtros(9);
            TempData["ReglasSug"] = Conceptos.Result;

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

        public ActionResult GetProveedores(FormCollection datos)
        {
            string _idSucursal = datos["idSucursal"].ToString();
            var buscar = _idSucursal.Split(',');
            List<Combo3> proveedores = (List<Combo3>)Session["ProveedorTMP"];
            var jsonProveedor = (from N in proveedores
                                 where buscar.Contains(N.IdFiltro.ToString())
                                 select new { N.Id, N.Valor }).Distinct();
            return Json(jsonProveedor, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CrearSugerido(FormCollection form)
        {

            if (form["Grupo"] != null && form["Sucursal"] != null && form["Proveedor"] != null && form["Conceptos"] != null)
            {
                var grupo = form["Grupo"];
                var sucursales = form["Sucursal"];
                var proveedores = form["Proveedor"];
                var reglas = form["Conceptos"].ToString().Split(',');

                bool Negados = false, CompraEsp = false, ResurtidoNat = false, Invenadro = false, EspecialesFarm = false, PieCam = false;

                foreach (var item in reglas)
                {
                    switch (int.Parse(item))
                    {
                        case 1:
                            Negados = true;
                            break;
                        case 2:
                            CompraEsp = true;
                            break;
                        case 3:
                            ResurtidoNat = true;
                            break;
                        case 4:
                            Invenadro = true;
                            break;
                        case 5:
                            EspecialesFarm = true;
                            break;
                        case 6:
                            PieCam = true;
                            break;
                        default:
                            break;
                    }
                }

                List<Combo3> sucursalesList = (List<Combo3>)Session["SucursalTMP"];
                List<Combo3> proveedoresList = (List<Combo3>)Session["ProveedorTMP"];
                var listaSuge = (from suc in sucursalesList
                                 join prov in proveedoresList on suc.Id equals prov.IdFiltro
                                 where sucursales.Split(',').Contains(suc.Id.ToString())
                                 && proveedores.Split(',').Contains(prov.Id.ToString())
                                 select new ValidaSugeridoBean()
                                 {
                                     IdGrupo = suc.IdFiltro,
                                     IdSucursal = suc.Id,
                                     IdProveedor = prov.Id,
                                     Negados = Negados,
                                     CompraEspecial = CompraEsp,
                                     ResurtidoNatural = ResurtidoNat,
                                     Invenadro = Invenadro,
                                     EspecialesFarmacia = EspecialesFarm,
                                     PieCamionMayorista = PieCam
                                 }).ToList();


                var user = this.GetUsuario();
                SugeridoBusiness sugerido = new SugeridoBusiness();
                var response = sugerido.CrearSugerido(listaSuge, user.IdUsuario);
                Session["FolioSugerido"] = sugerido.NuevoFolio;
                ViewBag.IdSugerido = sugerido.NuevoFolio;
                return Json(response, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var response = new Response();
                response.Success = false;
                response.Message = "Faltan parametros por seleccionar";
                return Json(response, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult ValidarNegados()
        {
            return View();
        }

        public ActionResult AplicarNegados()
        {
            return View();
        }

        public ActionResult ConsultaSugerido(string IdSugerido)
        {

            var user = this.GetUsuario();

            var Grupos = new SugeridoBusiness().Filtros(3).Result;
            var Sucursales = new SugeridoBusiness().Filtros2(4).Result;

            //var gruposUsuario = (List<Combo3>)Session["Grupos_Usuario"];
            Session["Grupos_Usuario"] = new GruposBLL().GetGrupos_Usuario(4).Result;

            List<Combo3> gruposUsuario = (List<Combo3>)Session["Grupos_Usuario"];

            var usuarioGrupos = (from g in gruposUsuario where g.IdFiltro == user.IdUsuario && g.Valor == "True" select g.Id).ToList();

            var grupos2 = (from gr in Grupos where usuarioGrupos.Contains(gr.Id) select gr).ToList();

            var sucGrups = (from g in grupos2
                            join s in Sucursales on g.Id equals s.IdFiltro
                            select new MultiselectAgrupado()
                            {
                                IdGrupo = s.IdFiltro,
                                Grupo = g.Valor,
                                IdSucursal = s.Id,
                                Sucursal = s.Valor
                            }).ToList();
            if (IdSugerido == null && IdSugerido == "")
            {
                IdSugerido = Session["FolioSugerido"].ToString();
            }
            TempData["IdSugerido"] = IdSugerido;

            TempData["SucGruposTMP"] = sucGrups;

            var Proveedore = new SugeridoBusiness().Filtros(2).Result;
            TempData["ProveedorTMP"] = Proveedore; // (from p in Proveedore select new ComboGenerico { Id = p.Id, Valor = p.Valor }).Distinct().ToList();
            //TempData["ProveedorTMP"] = Proveedore.Result.Select(x => new ComboGenerico {Id= x.Id,Valor= x.Valor }).ToList().Distinct().ToList();

            var EstatusSug = new SugeridoBusiness().Filtros(6).Result;
            TempData["EstatusSug"] = EstatusSug;
            var GpoArticulos = new SugeridoBusiness().Filtros(7).Result;
            TempData["GpoArticulos"] = GpoArticulos;

            var Conceptos = new SugeridoBusiness().Filtros(9);
            TempData["ReglasSug"] = Conceptos.Result;

            return View();

        }

        public ActionResult AplicarConsulta(FormCollection form)
        {
            return View();
        }

        [HttpPost]
        public JsonResult ValidarNegados(ListSucursales model)
        {
            long folioSugerido = long.Parse(Session["FolioSugerido"].ToString());

            var response = new SugeridoBusiness().ValidarNegadosBLL(model, folioSugerido);

            var json = Json(new { data = response }, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = 500000000;
            return json;
        }

        public JsonResult GetIncidenciasList(string tiendas)
        {
            var response = new SugeridoBusiness().GetIncidenciasListBLL(2, tiendas).Result;

            var json = Json(new { data = response }, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = 500000000;
            return json;
        }

        public ActionResult GetListLogError(string incidencia, string tiendas)
        {
            //string incidencia = data["value"].ToString();

            var log = new SugeridoBusiness().GetListLogErrorBLL(incidencia, 3, tiendas).Result;
            var json = Json(log, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = 500000000;
            return json;
        }
        public ActionResult GetDTLogError(string tiendas)
        {
            //string incidencia = data["value"].ToString();
            int usuario = this.GetUsuario().IdUsuario;

            var log = new SugeridoBusiness().GetDTLogErrorBLL(3, tiendas, usuario).Result;
            var json = Json(log, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = 500000000;
            return json;
        }

        public ActionResult GetDTCuenta()
        {
            Int64 NuevoFolio = (Int64)Session["FolioSugerido"];
            //string incidencia = data["value"].ToString();
            int usuario = this.GetUsuario().IdUsuario;

            var log = new SugeridoBusiness().GetDTCuenta(NuevoFolio);
            var json = Json(log, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = 500000000;
            return json;
        }

        public JsonResult CalcularSugerido()
        {
            Int64 NuevoFolio = (Int64)Session["FolioSugerido"];
            //ViewBag.IdSugerido = NuevoFolio;

            var response = new SugeridoBusiness().CalcularSugerido(NuevoFolio);
            var json = Json(response, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = 500000000;
            return json;
        }

        public JsonResult AsignacionProveedor()
        {
            Int64 NuevoFolio = (Int64)Session["FolioSugerido"];
            var response = new SugeridoBusiness().AsignacionProveedor(NuevoFolio);
            if (response.Success)
            {
                response.Message += "-" + NuevoFolio.ToString();
            }
            var json = Json(response, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = 500000000;
            return json;
        }


        public ActionResult GetListadoSugerido(FormCollection form)
        {
            var Fecha = form["Fecha"];
            var GrupoSuc = form["GrupoSuc"];
            var Proveedor = form["Proveedor"];
            var TipoCalculo = form["TipoCalculo"];
            var EstatusSug = form["EstatusSug"];
            var GrupoArt = form["GrupoArt"];
            var Conceptos = form["Conceptos"];
            var Consulto = form["Consulto"].ToString();
            var idsugerido = form["idSugerido"].ToString();
            var user = this.GetUsuario();
            var response = new SugeridoBusiness().GetListadoSugerido(idsugerido, Fecha, GrupoSuc, Proveedor, TipoCalculo, EstatusSug, GrupoArt, Conceptos, int.Parse(Consulto), user.IdUsuario);
            var json = Json(response, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = 500000000;
            return json;

        }
        public ActionResult GetResumen(string idSugerido, int accion)
        {
            var response = new SugeridoBusiness().GetResumen(Int64.Parse(idSugerido), accion);
            var json = Json(response, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = 500000000;
            return json;

        }

        public ActionResult ActualizarPedidoFinal(string idSugerido, string farmacia, string proveedor, string sku, string pedFinal, string PedModificado)
        {
            var aplica = new SugeridoBusiness().ActualizarPedidoFinal(idSugerido, farmacia, proveedor, sku, pedFinal, PedModificado);
            var json = Json(aplica, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = 500000000;
            return json;
        }

        public ActionResult EliminarSkuSugerido(List<EliminarSugeridoSKU> model)
        {
            var usuario = this.GetUsuario();
            var aplica = new SugeridoBusiness().EliminarSkuSugerido(model, usuario.IdUsuario);
            var json = Json(aplica, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = 500000000;
            return json;
        }

        public ActionResult GetRechazos(string idSugerido)
        {
            var response = new SugeridoBusiness().GetRechazos(Int64.Parse(idSugerido));
            var json = Json(response, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = 500000000;
            return json;

        }

        [HttpPost]
        public void DescargarExcel(FormCollection form)
        {
            var Fecha = form["Fecha"];
            var GrupoSuc = form["GrupoSuc"];
            var Proveedor = form["Proveedor"];
            var TipoCalculo = form["TipoCalculo"];
            var EstatusSug = form["EstatusSug"];
            var GrupoArt = form["GrupoArt"];
            var Conceptos = form["Conceptos"];
            var Consulto = form["Consulto"].ToString();
            var idsugerido = form["idSugerido"].ToString();
            var user = this.GetUsuario();
            var excel = new SugeridoBusiness().DescargarExcel(idsugerido, Fecha, GrupoSuc, Proveedor, TipoCalculo, EstatusSug, GrupoArt, Conceptos, int.Parse(Consulto), user.IdUsuario);

            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment: filename=consultaSugerido.xlsx");
            Response.BinaryWrite(excel);
            Response.End();

        }
    }
}