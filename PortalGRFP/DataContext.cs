using PortalGRFP.Entities.Common;
using PortalGRFP.Models;
using SQLCrConn.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace PortalGRFP
{
    public class DataContext
    {
        public static List<Sucursales> GetCadena(string sp)
        {
            try
            {
                List<Sucursales> mrca = null; Sucursales o = null;
                string q = string.Format("{0}", sp);

                DBSCommand.IsNonQuery = false;
                
                //retrieve the data
                using (DataTable dt = (DataTable)DBSCommand.Execute(q))
                {
                    if (dt != null)
                    {
                        mrca = new List<Sucursales>();

                        foreach (DataRow row in dt.Rows)
                        {
                            o = new Sucursales();
                            //o.sucursal = row["NOM_SUC"].ToString();
                            o.marca = row["MARCA"].ToString();
                            mrca.Add(o);
                        }
                    }
                }

                return mrca;
            }
            catch (Exception ex)
            {
                ExHandler.Log.Write(string.Format("DataContext.GetRoutes {0}", ex.Message));
                return null;
            }
        }
        public static List<Sucursales> GetSucursal(string sp)
        {
            try
            {
                List<Sucursales> suc = null; Sucursales o = null;
                string q = string.Format("{0}", sp);

                DBSCommand.IsNonQuery = false;

                //retrieve the data
                using (DataTable dt = (DataTable)DBSCommand.Execute(q))
                {
                    if (dt != null)
                    {
                        suc = new List<Sucursales>();

                        foreach (DataRow row in dt.Rows)
                        {
                            o = new Sucursales();
                            o.sucursal = row["NOM_SUC"].ToString();
                            //o.marca = row["MARCA"].ToString();
                            suc.Add(o);
                        }
                    }
                }

                return suc;
            }
            catch (Exception ex)
            {
                ExHandler.Log.Write(string.Format("DataContext.GetRoutes {0}", ex.Message));
                return null;
            }
        }

        public static List<EstatusPedidos> GetEstatus(string sp)
        {
            try
            {

                List<EstatusPedidos> stts = null; EstatusPedidos o = null;
                string q = string.Format("{0}", sp);

                DBSCommand.IsNonQuery = false;

                //retrieve the data
                using (DataTable dt = (DataTable)DBSCommand.Execute(q))
                {
                    if (dt != null)
                    {
                        stts = new List<EstatusPedidos>();

                        foreach (DataRow row in dt.Rows)
                        {
                            o = new EstatusPedidos();
                            o.foliopedido = row["foliopedido"].ToString();
                            o.Fecha = row["Fecha"].ToString();
                            o.Estatus = row["Estatus"].ToString();
                            o.foliofacturafinal = row["foliofacturafinal"].ToString();
                            o.ID_UBICT = row["ID_UBICT"].ToString();
                            o.MARCA = row["MARCA"].ToString();
                            o.COD_SUC = row["COD_SUC"].ToString();
                            o.NOM_SUC = row["NOM_SUC"].ToString();
                            o.no_entrega_sap = row["no_entrega_sap"].ToString();
                            o.folio_cliente = row["folio_cliente"].ToString();

                            stts.Add(o);
                        }
                    }
                }

                return stts;
            }
            catch (Exception ex)
            {
                ExHandler.Log.Write(string.Format("DataContext.GetRoutes {0}", ex.Message));
                return null;
            }
        }

        public static CifraTotal GetCifrasTotal(string sp)
        {

            try
            {
                string q = string.Format("{0}", sp);

                DBSCommand.IsNonQuery = false;

                CifraTotal Cifra = null;
                //retrieve the data
                using (DataTable dt = (DataTable)DBSCommand.Execute(q))
                {
                    if (dt != null)
                    {

                        Cifra = new CifraTotal();
                        foreach (DataRow row in dt.Rows)
                        {
                            Cifra.pedido = decimal.Parse(row["pedido"].ToString());
                            Cifra.fecha_ped = row["fecha_ped"].ToString();
                            Cifra.cantidadpedida = decimal.Parse(row["cantidadpedida"].ToString());
                            Cifra.cantidadconf = decimal.Parse(row["cantidadconf"].ToString());
                            Cifra.cantidadfact = decimal.Parse(row["cantidadfact"].ToString());
                            Cifra.pedido_conf = decimal.Parse(row["pedido_conf"].ToString());
                            Cifra.pedido_fact = decimal.Parse(row["pedido_fact"].ToString());
                            Cifra.cantidadRecSuc = decimal.Parse(row["cantidadRecSuc"].ToString());
                            Cifra.pedido_conf_suc = decimal.Parse(row["pedido_conf_suc"].ToString());
                        }
                    }
                }

                return Cifra;
            }
            catch (Exception ex)
            {
                ExHandler.Log.Write(string.Format("DataContext.GetRoutes {0}", ex.Message));
                return null;
            }
        }
        public static List<PedidoDetalle> GetPedidoDet(string sp)
        {
            try
            {

                List<PedidoDetalle> PedDet = null; PedidoDetalle a = null;
                string q = string.Format("{0}", sp);

                DBSCommand.IsNonQuery = false;

                //retrieve the data
                using (DataTable dt = (DataTable)DBSCommand.Execute(q))
                {
                    if (dt != null)
                    {
                        PedDet = new List<PedidoDetalle>();

                        foreach (DataRow row in dt.Rows)
                        {
                            a = new PedidoDetalle();
                            a.folioPedido = row["folioPedido"].ToString();
                            a.folio_cliente = row["folio_cliente"].ToString();
                            a.codigoSucursal = row["codigoSucursal"].ToString();
                            a.material = row["material"].ToString();
                            a.cantidad = int.Parse(row["cantidad"].ToString());
                            a.ean = row["ean"].ToString();
                            a.codigoProducto = row["codigoProducto"].ToString();
                            a.no_entrega_sap = row["no_entrega_sap"].ToString();
                            a.fecha_conf = row["fecha_conf"].ToString();
                            a.cantidad_conf = int.Parse(row["cantidad_conf"].ToString());
                            a.folioFacturaFinal = row["folioFacturaFinal"].ToString();
                            a.fechaFactura = row["fechaFactura"].ToString();
                            a.cantidad_facturada = int.Parse(row["cantidad_facturada"].ToString());
                            a.fecha_rec_suc = row["fecha_rec_suc"].ToString();
                            a.cantidad_rcbo = int.Parse(row["cantidad_rcbo"].ToString());
                            a.descripcion = row["descripcion"].ToString();
                        

                            PedDet.Add(a);
                        }
                    }
                }

                return PedDet;
            }
            catch (Exception ex)
            {
                ExHandler.Log.Write(string.Format("DataContext.GetRoutes {0}", ex.Message));
                return null;
            }
        }

        public static PedidoDetalle GetPedidoDistinct(string sp)
        {

            try
            {
                string q = string.Format("{0}", sp);

                DBSCommand.IsNonQuery = false;

                PedidoDetalle Dist = null;
                //retrieve the data
                using (DataTable dt = (DataTable)DBSCommand.Execute(q))
                {
                    if (dt != null)
                    {

                        Dist = new PedidoDetalle();
                        foreach (DataRow row in dt.Rows)
                        {
                            Dist.folioPedido = row["folioPedido"].ToString();
                            Dist.codigoSucursal = row["codigoSucursal"].ToString();
                            Dist.MARCA = row["MARCA"].ToString();
                            Dist.NOM_SUC = row["NOMBRE"].ToString();
                        }
                    }
                }

                return Dist;
            }
            catch (Exception ex)
            {
                ExHandler.Log.Write(string.Format("DataContext.GetRoutes {0}", ex.Message));
                return null;
            }
        }

    }
}