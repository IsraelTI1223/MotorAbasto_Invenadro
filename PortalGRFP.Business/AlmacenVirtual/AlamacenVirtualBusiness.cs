using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PortalGRFP.Entities.Response;
using PortalGRFP.Entities.Common.AlmacenVirtual;
using System.Web;
using System.IO;
using OfficeOpenXml;
using PortalGRFP.Utilities.TableType;
using System.Data;
using PortalGRFP.Data.AlmacenVirtual;
using System.Globalization;

namespace PortalGRFP.Business.AlmacenVirtual
{
    public class AlamacenVirtualBusiness
    {
        public ResponseAV CargaAlmacen(HttpFileCollectionBase httpFile, string fecha, int usuario)
        {
            var response = new ResponseAV();
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

                        var hojaActual = package.Workbook.Worksheets["AlmacenVirtual"];
                        if (hojaActual == null)
                        {
                            response.Message = "Para leer el archivo, el nombre de la hoja debe ser 'AlmacenVirtual'";
                            return response;
                        }
                        var hoja = hojaActual;
                        int noCol = hoja.Dimension.End.Column;
                        int noRenglon = hoja.Dimension.End.Row;
                        int i = 0;

                        var z = int.Parse(hoja.Cells[2, 6].Value.ToString());
                        var z1 = ((object[,])hoja.Cells.Value)[1, 6];
                        var z11 = decimal.Parse(hoja.Cells[2, 7].Value.ToString(), NumberStyles.Currency, CultureInfo.GetCultureInfo("en-US"));
                        var z2 = int.Parse(hoja.Cells[2, 7].Value.ToString());

                        List<AlmacenVirtualModel> listaEnExcel = new List<AlmacenVirtualModel>();

                        for (i = 2; i <= noRenglon; i++)
                        {
                            AlmacenVirtualModel cvp = new AlmacenVirtualModel()
                            {
                                Fecha = hoja.Cells[i, 1].Value == null ? "" : hoja.Cells[i, 1].Value.ToString(),//fecha,
                                Contrato = hoja.Cells[i, 1].Value == null ? "" : hoja.Cells[i, 2].Value.ToString(),
                                Sku = hoja.Cells[i, 2].Value == null ? "" : hoja.Cells[i, 3].Value.ToString(),
                                Descripcion = hoja.Cells[i, 3].Value == null ? "" : hoja.Cells[i, 4].Value.ToString(),
                                Laboratorio = hoja.Cells[i, 4].Value == null ? "" : hoja.Cells[i, 5].Value.ToString(),
                                Piezas = hoja.Cells[i, 5].Value == null ? 0 : int.Parse(hoja.Cells[i, 6].Value.ToString()),
                                Costo = hoja.Cells[i, 6].Value == null ? 0 : decimal.Parse(hoja.Cells[i, 7].Value.ToString()),
                                AlmacenV = hoja.Cells[i, 7].Value == null ? 0 : int.Parse(hoja.Cells[i, 8].Value.ToString()),
                                user = usuario,
                            };
                            listaEnExcel.Add(cvp);
                        }

                        var listDuplicado = listaEnExcel.GroupBy(x => new { x.Fecha, x.Contrato, x.Sku, x.AlmacenV }).ToList();
                        if (listaEnExcel.Count > listDuplicado.Count)
                        {
                            response.resp = 0;
                            response.Message = "Existe Registros Duplicados Favor de Revisar el Layout Seleccionado";
                            response.Success = false;
                        }
                        else
                        {
                            var serializado = Serialization.SerializeObject(listaEnExcel);
                            AlmacenVirtualData CargaA = new AlmacenVirtualData();

                            response = procesaAlmacenV(serializado, 1);
                            response.serializado = serializado;
                            var responses = new cargaAlmacenLayout();

                            responses.FechaCarga = fecha;
                            responses.FilenName = nombreArchivo;
                            responses.items = listaEnExcel.Count;
                            //responses.usuario = usuario;
                            response.tblCargaAV.Add(responses);

                        }

                        
                    }
                    catch (Exception ex)
                    {
                        response.Message = "Error al procesar el layout de almacen virtual. opc=1, " + ex.Message;
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

        public ResponseAV procesaAlmacenV(string serializado, int opc)
        {
            var response = new ResponseAV();
            try
            {
                response.serializado = serializado;

                AlmacenVirtualData CargaA = new AlmacenVirtualData();

                response = CargaA.CargarAlmacenVXML(response.serializado, opc);
                var responses = new cargaAlmacenLayout();

            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error al procesar el layout de almacen virtual. opc=2, " + ex.Message;
                
            }

            return response;
            
        }


        /*Metodos de almacen matenimiento*/

        public ResponseAVM procesaAlmacenLoad(int opc, int usuario, string Nombre, string Cadena)
        {
            var response = new ResponseAVM();
            try
            {

                AlmacenVirtualData CargaA = new AlmacenVirtualData();

                response = CargaA.insertLoadAlmacen(opc, usuario, Nombre, Cadena);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error al procesar la tabla de almacen opc="+Convert.ToString(opc)+", " + ex.Message;

            }

            return response;

        }
    }
}
