using PortalGRFP.Extensions;
using PortalGRFP.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace PortalGRFP.Controllers
{

    public class HomeController : Controller
    {
        #region [Vistas]
        [HttpGet]
        public ActionResult Index()
        {
            if (HttpContext.Session.Count == 0)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                return View("~/Views/Home/Welcome.cshtml");
            }
        }
        [HttpGet]
        public ActionResult ErrorPermiso()
        {
            ViewBag.ErrorMessage = this.GetErrorPermisos();
            return View();
        }
        #endregion
        //public ActionResult Index(string FechaPedido, string Marcas, string Sucursales)
        //{
        //    string Action1 = "SUC";
        //    string Action2 = "MAR";

        //    List<Sucursales> suc = DataContext.GetSucursal("ubictum.sp_get_track_suc_ped" + " " + "'" + Action1 + "'" );

        //    List<SelectListItem> Sucursal = suc.ConvertAll(s =>
        //       {
        //           return new SelectListItem()
        //           {
        //               Text = s.sucursal.ToString(),
        //               Value = s.sucursal.ToString(),
        //               Selected = false
        //           };
        //       });
        //    ViewBag.sucursales = Sucursal;

        //    List<Sucursales> mrca = DataContext.GetCadena("ubictum.sp_get_track_suc_ped" + " " + "'" + Action2 + "'");

        //    List<SelectListItem> Marca = mrca.ConvertAll(m =>
        //       {
        //           return new SelectListItem()
        //           {
        //               Text = m.marca.ToString(),
        //               Value = m.marca.ToString(),
        //               Selected = false
        //           };
        //       });
        //    ViewBag.marcas = Marca;


        //    if (FechaPedido != null)
        //    {
        //        CifraTotal cifra = DataContext.GetCifrasTotal("ubictum.sp_get_track_cifra_total" + " " + "'" + FechaPedido + "'");
        //        ViewBag.cifra = cifra;
        //    }

        //    if (Marcas == "Cadena..." && Sucursales == "")
        //    {
        //        List<EstatusPedidos> stts = DataContext.GetEstatus("ubictum.sp_get_track_estatus_ped_MARCA" + " " + "'" + FechaPedido + "',"+"null" +"," + "null");
        //        ViewBag.data = stts;

        //    } else if (Marcas != "Cadena..." && Sucursales == "")
        //    {
        //        List<EstatusPedidos> stts = DataContext.GetEstatus("ubictum.sp_get_track_estatus_ped_MARCA" + " " + "'" + FechaPedido + "'," + "'" + Marcas + "'," + "null");
        //        ViewBag.data = stts;

        //    } else if (Marcas == "Cadena..." && Sucursales != "")
        //    {
        //        List<EstatusPedidos> stts = DataContext.GetEstatus("ubictum.sp_get_track_estatus_ped_MARCA" + " " + "'" + FechaPedido + "'," + "null" + "," + "'" + Sucursales + "'");
        //        ViewBag.data = stts;
        //    }else
        //    {
        //        List<EstatusPedidos> stts = DataContext.GetEstatus("ubictum.sp_get_track_estatus_ped_MARCA" + " " + "'" + FechaPedido + "'," + "'" + Marcas + "'," + "'" +Sucursales+"'");
        //        ViewBag.data = stts;
        //    }

        //    return View();
        //}

        public ActionResult GetPedido(string folio)
        {
            List<PedidoDetalle> PedDet = DataContext.GetPedidoDet("ubictum.sp_get_track_pedido_det" + " " + "'" + folio + "'");
            ViewBag.pedido = PedDet;

            PedidoDetalle PedDist = DataContext.GetPedidoDistinct("ubictum.sp_get_track_pedido_distinct" + " " + "'" + folio + "'");
            ViewBag.distinct = PedDist;


            return View();
        }

    }
}