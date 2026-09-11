using OfficeOpenXml;
using PortalGRFP.Data.Mantenimiento;
using PortalGRFP.Entities.Common;
using PortalGRFP.Entities.Response;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace PortalGRFP.Business.Mantenimiento
{
    public class TopedeCompraBLL
    {
        private readonly TopedeCompraData gTope = new TopedeCompraData();

        public Response CargaTopeBll(HttpFileCollectionBase httpFile, int usuario)
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

                        var hojaActual = package.Workbook.Worksheets["Tope de Compra"];
                        if (hojaActual == null)
                        {
                            response.Message = "Para leer el archivo, el nombre de la hoja debe ser 'Tope de Compra'";
                            return response;
                        }
                        var hoja = hojaActual;
                        int noCol = hoja.Dimension.End.Column;
                        int noRenglon = hoja.Dimension.End.Row;
                        int i = 0;

                        List<TopeDeCompraModel> listaEnExcel = new List<TopeDeCompraModel>();

                        var IdSucursal = string.Empty;
                        var SKU = string.Empty;
                        var limite = string.Empty;
                        var concepto = string.Empty;

                        for (i = 2; i <= noRenglon; i++)
                        {
                            TopeDeCompraModel cvp = new TopeDeCompraModel();

                            //Validación de IdSucursal no decimales ni letras
                            if (hoja.Cells[i, 1].Value != null)
                                if (int.TryParse(hoja.Cells[i, 1].Value.ToString(), out int s))
                                    if (int.Parse(hoja.Cells[i, 1].Value.ToString()) > 0)
                                        cvp.IdSucursal = int.Parse(hoja.Cells[i, 1].Value == null ? "" : hoja.Cells[i, 1].Value.ToString());
                                    else
                                        IdSucursal += i.ToString() + ",";
                                else
                                    IdSucursal += i.ToString() + ",";
                            else
                                IdSucursal += i.ToString() + ",";

                            //Validación de SKU no decimales ni letras
                            if (hoja.Cells[i, 2].Value != null)
                                if (Int64.TryParse(hoja.Cells[i, 2].Value.ToString(), out Int64 s))
                                    if (Int64.Parse(hoja.Cells[i, 2].Value.ToString()) > 0)
                                        cvp.SKU = hoja.Cells[i, 2].Value == null ? "" : hoja.Cells[i, 2].Value.ToString();
                                    else
                                        SKU += i.ToString() + ",";
                                else
                                    SKU += i.ToString() + ",";
                            else
                                SKU += i.ToString() + ",";

                            //Validación de Limite de Compra no decimales ni letras
                            if (hoja.Cells[i, 3].Value != null)
                                if (int.TryParse(hoja.Cells[i, 3].Value.ToString(), out int s))
                                    if (int.Parse(hoja.Cells[i, 3].Value.ToString()) > 0)
                                        cvp.Limite = int.Parse(hoja.Cells[i, 3].Value == null ? "" : hoja.Cells[i, 3].Value.ToString());
                                    else
                                        limite += i.ToString() + ",";
                                else
                                    limite += i.ToString() + ",";
                            else
                                limite += i.ToString() + ",";

                          
                            //Fechas
                            cvp.FechaInicio = hoja.Cells[i, 4].Value == null ? "" : hoja.Cells[i, 4].Value.ToString();
                            cvp.FechaFin = hoja.Cells[i, 5].Value == null ? "" : hoja.Cells[i, 5].Value.ToString();

                            //Validación de IdConcepto no decimales ni letras
                            if (hoja.Cells[i, 6].Value != null)
                                if (int.TryParse(hoja.Cells[i, 6].Value.ToString(), out int s))
                                    if (int.Parse(hoja.Cells[i, 6].Value.ToString()) > 0)
                                        cvp.IdConcepto = int.Parse(hoja.Cells[i, 6].Value == null ? "" : hoja.Cells[i, 6].Value.ToString());
                                    else
                                        concepto += i.ToString() + ",";
                                else
                                    concepto += i.ToString() + ",";
                            else
                                concepto += i.ToString() + ",";

                            listaEnExcel.Add(cvp);
                        }

                        if (!string.IsNullOrEmpty(IdSucursal) || !string.IsNullOrEmpty(SKU) || !string.IsNullOrEmpty(limite) || !string.IsNullOrEmpty(concepto))
                        {
                            response.Message = "Existen Datos no validos en:";
                            if (IdSucursal.Length > 0)
                                IdSucursal = IdSucursal.Remove(IdSucursal.Length - 1);
                            if (SKU.Length > 0)
                                SKU = SKU.Remove(SKU.Length - 1);
                            if (limite.Length > 0)
                                limite = limite.Remove(limite.Length - 1);
                            if (concepto.Length > 0)
                                concepto = concepto.Remove(concepto.Length - 1);

                            if (!string.IsNullOrEmpty(IdSucursal))
                                response.Message += "\n\r ID Sucursal, Fila:" + IdSucursal;
                            if (!string.IsNullOrEmpty(SKU))
                                response.Message += "\n\r SKU, Fila;" + SKU;
                            if (!string.IsNullOrEmpty(limite))
                                response.Message += "\n\r limite, Fila;" + limite;
                            if (!string.IsNullOrEmpty(concepto))
                                response.Message += "\n\r concepto, Fila;" + concepto;

                            return response;
                        }

                        var dt = ExcepcionesInvenadroModel.Definicion();
                        var numLineas = listaEnExcel.Count();

                        foreach (var item in listaEnExcel)
                        {
                            DataRow renglon = dt.NewRow();
                            renglon[0] = item.IdSucursal;
                            renglon[1] = item.SKU;
                            renglon[2] = item.Limite;
                            renglon[3] = item.FechaInicio;
                            renglon[4] = item.FechaFin;
                            renglon[5] = item.IdConcepto;
                            renglon[6] = usuario;
                            dt.Rows.Add(renglon);
                        }

                        response = gTope.CargaExcepcionesData(dt);
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

        public ResponseList<TopeDeCompraModel> ConsultaTopeBLL()
        {
            var response = new ResponseList<TopeDeCompraModel>();
            try
            {
                response = gTope.ConsultaTopeData();
            }
            catch (Exception ex)
            {
                response.Message = "Error al realizar la consulta de información. " + ex.Message;
                response.Success = false;
            }
            return response;
        }
        public byte[] GetExcelTope()
        {
            var response = new ResponseList<TopeDeCompraModel>();
            ExcelPackage pck = new ExcelPackage();
            var ws = pck.Workbook.Worksheets.Add("Configuración Tope de Compra");
            byte[] xlsx = pck.GetAsByteArray();

            try
            {
                response = gTope.ExcelTopeData();

                pck = new ExcelPackage();

                ws = pck.Workbook.Worksheets.Add("Configuración Tope de Compra");

                ws.Cells["A1"].Value = "ID Sucursal";
                ws.Cells["B1"].Value = "Sucursal";
                ws.Cells["C1"].Value = "SKU";
                ws.Cells["D1"].Value = "Descrición artículo";
                ws.Cells["E1"].Value = "Limite de Compra";
                ws.Cells["F1"].Value = "Fecha Inicio Vigencia";
                ws.Cells["G1"].Value = "Fecha Fin Vigencia";
                ws.Cells["H1"].Value = "Concepto";
                ws.Cells["I1"].Value = "Usuario Carga";

                int renglon = 2;
                foreach (var item in response.Result)
                {
                    ws.Cells[string.Format("A{0}", renglon)].Value = item.IdSucursal;
                    ws.Cells[string.Format("B{0}", renglon)].Value = item.Sucursal;
                    ws.Cells[string.Format("C{0}", renglon)].Value = item.SKU;
                    ws.Cells[string.Format("D{0}", renglon)].Value = item.DescripcionSKU;
                    ws.Cells[string.Format("E{0}", renglon)].Value = item.Limite;
                    ws.Cells[string.Format("F{0}", renglon)].Value = item.FechaInicio;
                    ws.Cells[string.Format("G{0}", renglon)].Value = item.FechaFin;
                    ws.Cells[string.Format("H{0}", renglon)].Value = item.Concepto;
                    ws.Cells[string.Format("I{0}", renglon)].Value = item.Usuario;
                    renglon++;
                }
                ws.Cells["A:AZ"].AutoFitColumns();
                xlsx = pck.GetAsByteArray();
            }
            catch (Exception ex)
            {
                response.Message = "Error al realizar la consulta de información. " + ex.Message;
            }

            return xlsx;
        }

    }
}
