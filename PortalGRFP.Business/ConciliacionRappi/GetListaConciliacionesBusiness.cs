using OfficeOpenXml;
using OfficeOpenXml.Drawing;
using PortalGRFP.Data.ConciliacionRappi;
using PortalGRFP.Entities.Common;
using PortalGRFP.Entities.Response;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace PortalGRFP.Business.ConciliacionRappi
{
    public class GetListaConciliacionesBusiness
    {
        private readonly GetListaConciliacionesData getListaConciliacionesData;
        public GetListaConciliacionesBusiness()
        {
            getListaConciliacionesData = new GetListaConciliacionesData();
        }

        public ResponseList<ConciliacionVentaRappi> ObtenerConciliaciones(int anio, int mes)
        {
            var response = new ResponseList<ConciliacionVentaRappi>();
            try
            {
                response = getListaConciliacionesData.ObtenerConciliaciones(anio, mes);
            }
            catch (Exception ex)
            {
                response.Message = "Error al realizar la consulta de información. " + ex.Message;
            }
            return response;
        }

        public ResponseList<TicketVenta> ObtenerTicketVenta(string Cadena, string Ticket, string Sucursal)
        {
            var response = new ResponseList<TicketVenta>();
            try
            {
                response = getListaConciliacionesData.ObtenerTicketVenta(Cadena, Ticket, Sucursal);
            }
            catch (Exception ex)
            {
                response.Message = "Error al realizar la consulta de información. " + ex.Message;
            }
            return response;
        }

        public Response ConciliacionManual(string OrderId, string Operacion, string IdSucursal, decimal ImporteTotal, int Accion, int Incidencia, string Observacion, string ticket)
        {
            var response = new Response();
            try
            {
                response = getListaConciliacionesData.ConciliacionManual(OrderId, Operacion, IdSucursal, ImporteTotal, Accion, Incidencia, Observacion, ticket);
                response.Message = response.Success ? "La operación se realizo con exito." : "No se pudo completar la operación";
            }
            catch (Exception ex)
            {
                response.Message = "Error al realizar la consulta de información. " + ex.Message;
            }
            return response;
        }

        public Response ConciliacionManualCard(string razon, string Operacion, string IdSucursal, decimal ImporteTotal, int Accion, int Incidencia, string Observacion, string ticket)
        {
            var response = new Response();
            try
            {
                response = getListaConciliacionesData.ConciliacionManualCard(razon, Operacion, IdSucursal, ImporteTotal, Accion, Incidencia, Observacion, ticket);
                response.Message = response.Success ? "La operación se realizo con exito." : "No se pudo completar la operación";
            }
            catch (Exception ex)
            {
                response.Message = "Error al realizar la consulta de información. " + ex.Message;
            }
            return response;
        }

        public Response ConciliacionMensual(HttpFileCollectionBase httpFile)
        {
            var response = new Response();
            if (httpFile.AllKeys.Any())
            {
                var archivo = httpFile["fileExcel"];
                if (archivo != null && archivo.ContentLength > 0)
                {
                    try
                    {
                        var nombreArchivo = Path.GetFileName(archivo.FileName);
                        string fileContentType = archivo.ContentType;
                        byte[] fileBytes = new byte[archivo.ContentLength];
                        var data = archivo.InputStream.Read(fileBytes, 0, Convert.ToInt32(archivo.ContentLength));

                        var package = new ExcelPackage(archivo.InputStream);

                        var hojaActual = package.Workbook.Worksheets["Sheet1"];
                        var hoja = hojaActual;
                        int noCol = hoja.Dimension.End.Column;
                        int noRenglon = hoja.Dimension.End.Row;
                        int i = 0;

                        List<ConciliacionCargaVentaRappi> listaEnExcel = new List<ConciliacionCargaVentaRappi>();

                        for (i = 2; i <= noRenglon; i++)
                        {
                            ConciliacionCargaVentaRappi cvp = new ConciliacionCargaVentaRappi()
                            {
                                DiaOperacion = hoja.Cells[i, 1].Value == null ? string.Empty : hoja.Cells[i, 1].Value.ToString(),
                                StoreID = hoja.Cells[i, 2].Value == null ? string.Empty : hoja.Cells[i, 2].Value.ToString(),
                                Store = hoja.Cells[i, 3].Value == null ? string.Empty : hoja.Cells[i, 3].Value.ToString(),
                                RazonSocial = hoja.Cells[i, 4].Value == null ? string.Empty : hoja.Cells[i, 4].Value.ToString(),
                                Marca = hoja.Cells[i, 5].Value == null ? string.Empty : hoja.Cells[i, 5].Value.ToString(),
                                IdSucursal = hoja.Cells[i, 6].Value == null ? string.Empty : hoja.Cells[i, 6].Value.ToString(),
                                ImporteTotal = hoja.Cells[i, 7].Value == null ? string.Empty : hoja.Cells[i, 7].Value.ToString(),
                                FormaPago = hoja.Cells[i, 8].Value == null ? string.Empty : hoja.Cells[i, 8].Value.ToString(),
                                BinesCard = hoja.Cells[i, 9].Value == null ? string.Empty : hoja.Cells[i, 9].Value.ToString(),
                                Ultimos4Digitos = hoja.Cells[i, 10].Value == null ? string.Empty : hoja.Cells[i, 10].Value.ToString(),
                                CodigoAutorizacion = hoja.Cells[i, 11].Value == null ? string.Empty : hoja.Cells[i, 11].Value.ToString(),
                                TicketUrl = hoja.Cells[i, 12].Value == null ? string.Empty : hoja.Cells[i, 12].Value.ToString(),
                                OrderId = hoja.Cells[i, 13].Value == null ? string.Empty : hoja.Cells[i, 13].Value.ToString()
                            };
                            listaEnExcel.Add(cvp);
                        }
                        response = getListaConciliacionesData.CargaConciliacionMensual(listaEnExcel);
                        response.Message = response.Success ? "La operación se realizo con exito." : "No se pudo completar la operación";
                    }
                    catch (Exception ex)
                    {
                        response.Message = "Error al realizar la consulta de información. " + ex.Message;
                    }
                }
                else
                {
                    response.Message = "No se ha seleccionado un archivo";
                }
            }
            else
            {
                response.Message = "No se ha seleccionado un archivo";
            }
            return response;
        }

        public ResponseList<ResumenCargaRappi> ResumenCargaConciliacion()
        {
            var response = new ResponseList<ResumenCargaRappi>();
            try
            {
                response = getListaConciliacionesData.ResumenCargaConciliacion();
            }
            catch (Exception ex)
            {
                response.Message = "Error al realizar la consulta de información. " + ex.Message;
            }
            return response;
        }


        public ResponseList<ComboGenerico> MostrarBotonesCadena(int anio, int mes)
        {
            var response = new ResponseList<ComboGenerico>();
            try
            {
                response = getListaConciliacionesData.MostrarBotones(anio, mes);
            }
            catch (Exception ex)
            {
                response.Message = "Error al realizar la consulta de información. " + ex.Message;
            }
            return response;
        }

        public Response AplicarConciliacion(int anio, int mes, int opcion)
        {
            var response = new Response();
            try
            {
                response = getListaConciliacionesData.AplicarConciliacion(anio, mes, opcion);
                response.Message = response.Success ? "La operación se realizo con exito." : "No se pudo completar la operación";
            }
            catch (Exception ex)
            {
                response.Message = "Error al realizar la consulta de información. " + ex.Message;
            }
            return response;
        }

        public ResponseList<TicketVenta> ObtenerTicketVentaCard(string Cadena, string Sucursal, string fecha, string importe)
        {
            var response = new ResponseList<TicketVenta>();
            try
            {
                string newFecha = fecha.Split('/')[2] + '-' + fecha.Split('/')[1] + '-' + fecha.Split('/')[0];
                response = getListaConciliacionesData.ObtenerTIcketVentaCard(Cadena, Sucursal, newFecha, importe);
            }
            catch (Exception ex)
            {
                response.Message = "Error al realizar la consulta de información. " + ex.Message;
            }
            return response;
        }

        public byte[] DescargarExcel(int anio, int mes)
        {
            var response = new ResponseList<ConciliacionVentaRappi>();
            ExcelPackage pck = new ExcelPackage();
            var ws = pck.Workbook.Worksheets.Add("Conciliación");
            byte[] xlsx = pck.GetAsByteArray();
            try
            {
                response = getListaConciliacionesData.ExportarExcel(anio, mes);
                pck = new ExcelPackage();
                ws = pck.Workbook.Worksheets.Add("Conciliación");
                ws.Cells["A1"].Value = "Dia Operación";
                ws.Cells["B1"].Value = "Store Id";
                ws.Cells["C1"].Value = "Store";
                ws.Cells["D1"].Value = "Razón Social";
                ws.Cells["E1"].Value = "Marca";
                ws.Cells["F1"].Value = "Id Sucursal";
                ws.Cells["G1"].Value = "Importe Total";
                ws.Cells["H1"].Value = "Forma de Pago";
                ws.Cells["I1"].Value = "BIN";
                ws.Cells["J1"].Value = "Últ. 4 Dígitos";
                ws.Cells["K1"].Value = "Código Aut.";
                ws.Cells["L1"].Value = "Ticket Url";
                ws.Cells["M1"].Value = "Order Id";
                ws.Cells["N1"].Value = "Conciliate";
                ws.Cells["O1"].Value = "Iteración";
                ws.Cells["P1"].Value = "Ticket";
                ws.Cells["Q1"].Value = "Observación";

                int renglon = 2;
                foreach (var item in response.Result)
                {
                    ws.Cells[string.Format("A{0}", renglon)].Value = item.DiaOperacion;
                    ws.Cells[string.Format("B{0}", renglon)].Value = item.StoreID;
                    ws.Cells[string.Format("C{0}", renglon)].Value = item.Store;
                    ws.Cells[string.Format("D{0}", renglon)].Value = item.RazonSocial;
                    ws.Cells[string.Format("E{0}", renglon)].Value = item.Marca;
                    ws.Cells[string.Format("F{0}", renglon)].Value = item.IdSucursal;
                    ws.Cells[string.Format("G{0}", renglon)].Value = item.ImporteTotal;
                    ws.Cells[string.Format("H{0}", renglon)].Value = item.FormaPago;
                    ws.Cells[string.Format("I{0}", renglon)].Value = item.BinesCard;
                    ws.Cells[string.Format("J{0}", renglon)].Value = item.Ultimos4Digitos;
                    ws.Cells[string.Format("K{0}", renglon)].Value = item.CodigoAutorizacion;
                    ws.Cells[string.Format("L{0}", renglon)].Value = item.TicketUrl;
                    ws.Cells[string.Format("M{0}", renglon)].Value = item.OrderId;
                    ws.Cells[string.Format("N{0}", renglon)].Value = item.Conciliate;
                    ws.Cells[string.Format("O{0}", renglon)].Value = item.Iteracion;
                    ws.Cells[string.Format("P{0}", renglon)].Value = item.TicketVenta;
                    ws.Cells[string.Format("Q{0}", renglon)].Value = item.Observacion;
                    renglon++;
                }
                ws.Cells[string.Format("F{0}", renglon)].Value = "Total";
                ws.Cells[string.Format("G{0}", renglon)].Value = response.Result.Sum(x => x.ImporteTotal).ToString(); ;
                ws.Cells["A:AZ"].AutoFitColumns();
                xlsx = pck.GetAsByteArray();
            }
            catch (Exception ex)
            {
                response.Message = "Error al realizar la consulta de información. " + ex.Message;
            }
            //List<ConciliacionVentaRappi> toExcel
            return xlsx;

        }
    }
}
