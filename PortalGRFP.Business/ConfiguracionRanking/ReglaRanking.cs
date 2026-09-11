using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using OfficeOpenXml;
using PortalGRFP.Data.ConfiguracionRanking;
using PortalGRFP.Entities.Response;
using PortalGRFP.Entities.Common;
using PortalGRFP.Utilities.TableType;
using System.Data;
using System.Globalization;
using System.Reflection;
using System.Text.RegularExpressions;

namespace PortalGRFP.Business.ConfiguracionRanking
{
    public class ReglaRanking
    {
        private readonly ReglaRankingData reglaRankingData;
        public ReglaRanking()
        {
            reglaRankingData = new ReglaRankingData();
        }

        public Response BorrarRanking(int usuario)
        {
            var response = new Response();
            try
            {
                response = reglaRankingData.BorrarRanking(usuario);
            }
            catch (Exception ex)
            {
                response.Message = "Error al realizar la consulta de información. " + ex.Message;
            }
            return response;
        }

        public Response CargaRanking(HttpFileCollectionBase httpFile, int usuario)
        {
            var response = new Response();

            if (httpFile.AllKeys.Any())
            {
                var archivo = httpFile["fileExcel2"];


                if (archivo != null && archivo.ContentLength > 0)
                {
                    try
                    {
                        var nombreArchivo = Path.GetFileName(archivo.FileName);
                        var permittedExtensions = ".xlsx";
                        var checkextension = Path.GetExtension(archivo.FileName).ToLower();

                        if (!permittedExtensions.Contains(checkextension))
                        {
                            response.Message = "Formato de archivo no valido, debe ser .xlsx";
                            return response;
                        }

                        string fileContentType = archivo.ContentType;
                        byte[] fileBytes = new byte[archivo.ContentLength];
                        var data = archivo.InputStream.Read(fileBytes, 0, Convert.ToInt32(archivo.ContentLength));

                        var package = new ExcelPackage(archivo.InputStream);
                        var hojaActual = package.Workbook.Worksheets["Ranking"];

                        if (hojaActual == null)
                        {
                            response.Message = "EL nombre de la hoja debe ser Ranking";
                            return response;
                        }
                        if (hojaActual.Name != "Ranking")
                        {
                            response.Message = "EL nombre de la hoja debe ser Ranking";
                            return response;
                        }

                        var hoja = hojaActual;
                        int noCol = hoja.Dimension.End.Column;
                        int noRenglon = hoja.Dimension.End.Row;
                        int i = 0;

                        List<Ranking_Carga> listaEnExcel = new List<Ranking_Carga>();

                        var IdAgencia = string.Empty;
                        var SKU = string.Empty;
                        var Desc_SKU = string.Empty;
                        var fecha = string.Empty;
                        var Rank_Monto = string.Empty;
                        var Rank_Piezas = string.Empty;

                        for (i = 2; i <= noRenglon; i++)
                        {
                            Ranking_Carga cvp = new Ranking_Carga();

                            //Validacion IdAgencia
                            if (hoja.Cells[i, 1].Value != null)
                                if (Int64.TryParse(hoja.Cells[i, 1].Value.ToString(), out Int64 s))
                                    if (int.Parse(hoja.Cells[i, 1].Value.ToString()) > 0)
                                        cvp.IdAgencia = int.Parse(hoja.Cells[i, 1].Value == null ? "" : hoja.Cells[i, 1].Value.ToString());
                                    else
                                        IdAgencia += i.ToString() + ",";
                                else
                                    IdAgencia += i.ToString() + ",";
                            else
                                IdAgencia += i.ToString() + ",";

                            //validacion SKU
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

                            if (hoja.Cells[i, 3].Value != null)
                                cvp.Desc_SKU = hoja.Cells[i, 3].Value == null ? "" : hoja.Cells[i, 3].Value.ToString();
                            else
                                Desc_SKU += i.ToString() + ",";

                                //validar fecha 
                            if (hoja.Cells[i, 4].Value != null)
                                cvp.Fecha = hoja.Cells[i, 4].Value == null ? "" : hoja.Cells[i, 4].Value.ToString();
                            else
                                fecha += i.ToString() + ",";
                            //validaremos que la columna de fecha cumpla con el formato requerido AAAA-MM-DD


                            if (hoja.Cells[i, 5].Value != null)
                                if (Int64.TryParse(hoja.Cells[i, 5].Value.ToString(), out Int64 s))
                                    if (int.Parse(hoja.Cells[i, 5].Value.ToString()) > 0)
                                        cvp.Rank_monto = int.Parse(hoja.Cells[i, 5].Value == null ? "" : hoja.Cells[i, 5].Value.ToString());
                                    else
                                        Rank_Monto += i.ToString() + ",";
                                else
                                    Rank_Monto += i.ToString() + ",";
                            else
                                Rank_Monto += i.ToString() + ",";

                        
                            if (hoja.Cells[i, 6].Value != null)
                                if (Int64.TryParse(hoja.Cells[i, 6].Value.ToString(), out Int64 s))
                                    if (int.Parse(hoja.Cells[i, 6].Value.ToString()) > 0)
                                        cvp.Rank_piezas = int.Parse(hoja.Cells[i, 6].Value == null ? "" : hoja.Cells[i, 6].Value.ToString());
                                    else
                                        Rank_Piezas += i.ToString() + ",";
                                else
                                    Rank_Piezas += i.ToString() + ",";
                            else
                                Rank_Piezas += i.ToString() + ",";

                          
                            cvp.IdUsuario = usuario;
                            listaEnExcel.Add(cvp);
                        }

                        if (!string.IsNullOrEmpty(IdAgencia) || !string.IsNullOrEmpty(SKU) || !string.IsNullOrEmpty(Desc_SKU) || !string.IsNullOrEmpty(fecha) || !string.IsNullOrEmpty(Rank_Monto) || !string.IsNullOrEmpty(Rank_Piezas))
                        {
                            response.Message = "Existen Datos no validos en:";
                            if (Rank_Piezas.Length > 0)
                                Rank_Piezas = Rank_Piezas.Remove(Rank_Piezas.Length - 1);
                            if (Rank_Monto.Length > 0)
                                Rank_Monto = Rank_Monto.Remove(Rank_Monto.Length - 1);
                            if (fecha.Length > 0)
                                fecha = fecha.Remove(fecha.Length - 1);
                            if (Desc_SKU.Length > 0)
                                Desc_SKU = Desc_SKU.Remove(Desc_SKU.Length - 1);
                            if (SKU.Length > 0)
                                SKU = SKU.Remove(SKU.Length - 1);
                            if (IdAgencia.Length > 0)
                                IdAgencia = IdAgencia.Remove(IdAgencia.Length - 1);

                            if (!string.IsNullOrEmpty(Rank_Piezas))
                                response.Message += "\n\r Piezas, Fila:" + Rank_Piezas;
                            if (!string.IsNullOrEmpty(Rank_Monto))
                                response.Message += "\n\r monto, Fila:" + Rank_Monto;
                            if (!string.IsNullOrEmpty(fecha))
                                response.Message += "\n\r fecha, Fila:" + fecha;
                            if (!string.IsNullOrEmpty(Desc_SKU))
                                response.Message += "\n\r desc_sku, Fila:" + Desc_SKU;
                            if (!string.IsNullOrEmpty(SKU))
                                response.Message += "\n\r SKU, Fila:" + SKU;
                            if (!string.IsNullOrEmpty(IdAgencia))
                                response.Message += "\n\r idSucursal, Fila;" + IdAgencia;

                            return response;
                        }
                        var dt = RankingType.Definicion();

                        foreach (var item in listaEnExcel)
                        {
                            DataRow renglon = dt.NewRow();
                            renglon[0] = item.IdAgencia;
                            renglon[1] = item.SKU;
                            renglon[2] = item.Desc_SKU;
                            renglon[3] = item.Fecha;
                            renglon[4] = item.Rank_monto;
                            renglon[5] = item.Rank_piezas;
                            renglon[6] = usuario;
                            dt.Rows.Add(renglon);
                        }
                        response = reglaRankingData.CargaRanking(dt);
                        response.Message = response.Success ? "La operación se realizo con exito." : "No se pudo completar la operación";
                    }
                    catch (Exception ex)
                    {
                        var mensaje = ex.Message;

                        if (ex.InnerException != null)
                        {
                            mensaje = mensaje + "   " + ex.InnerException.Message;
                        }
                        mensaje = mensaje + "    " + ex.StackTrace;


                        response.Message = "Error al realizar la consulta de información. " + mensaje;
                    }
                }
            }
            else
            {
                response.Message = "No se ha seleccionado un archivo";
            }
            return response;
        }

        public ResponseList<LogRanking> GetRanking(int accion)
        {
            var response = new ResponseList<LogRanking>();
            try
            {
                response = reglaRankingData.GetRanking(accion);
            }
            catch (Exception ex)
            {
                response.Message = "Error al realizar la consulta de información. " + ex.Message;
            }
            return response;
        }
        public ResponseList<ConteoRanking> ConteoRanking(int accion)
        {
            var response = new ResponseList<ConteoRanking>();
            try
            {
                response = reglaRankingData.ConteoRanking(accion);
            }
            catch (Exception ex)
            {
                response.Message = "Error al realizar la consulta de información. " + ex.Message;
                throw;
            }
            return response;
        }



    }
}
