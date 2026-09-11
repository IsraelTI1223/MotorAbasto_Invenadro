using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PortalGRFP.Entities.Response;
using PortalGRFP.Entities.Common.Capacity;
using System.Web;
using System.IO;
using OfficeOpenXml;
using PortalGRFP.Utilities.TableType;
using System.Data;
using PortalGRFP.Data.Capacity;
using System.Globalization;

namespace PortalGRFP.Business.Capacity
{
    public class CapacityBusiness
    {
        public cCapacity GetMarcas(int Flag, int Usuario, string marca = "")
        {
            var lst = new Data.Capacity.Capacity(); 
            var Resp = lst.GetMarca(Flag, Usuario, marca);

            return Resp;
        }

        public responseCapacity setMarcas(int Flag, int Usuario, string marca, HttpFileCollectionBase httpFile)
        {

            responseCapacity response = new responseCapacity();
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

                    var hojaActual = package.Workbook.Worksheets["Capacity"];
                    if (hojaActual == null)
                    {
                        response.respuesta.Message = "Para leer el archivo, el nombre de la hoja debe ser 'Capacity'";
                        return response;
                    }
                    var hoja = hojaActual;
                    int noCol = hoja.Dimension.End.Column;
                    int noRenglon = hoja.Dimension.End.Row;
                    int i = 0;


                    List<tblCapacity> listaEnExcel = new List<tblCapacity>();
                    var sku = string.Empty;
                    var Capacity = string.Empty;
                    var IdMotivo = string.Empty;
                    var error = string.Empty;
                    var idSucursal = string.Empty;
                    for (i = 2; i <= noRenglon; i++)
                    {

                        tblCapacity cvp = new tblCapacity();

                        //Validamos que la sucursal no contenga valores con decimales
                        if (hoja.Cells[i, 1].Value != null)
                            if (Int64.TryParse(hoja.Cells[i, 1].Value.ToString(), out Int64 s))
                                if (Int64.Parse(hoja.Cells[i, 1].Value.ToString()) > 0)
                                    cvp.IdSucrusal = int.Parse(hoja.Cells[i, 1].Value == null ? "" : hoja.Cells[i, 1].Value.ToString());
                                else
                                    idSucursal += i.ToString() + ",";
                            else
                                idSucursal += i.ToString() + ",";
                        else
                            idSucursal += i.ToString() + ",";

                        //cvp.IdSucrusal = hoja.Cells[i, 1].Value == null ? 0 : int.Parse(hoja.Cells[i, 1].Value.ToString());

                        //Validamos que el sku no contenga valores con decimales
                        if (hoja.Cells[i, 2].Value != null)
                            if (Int64.TryParse(hoja.Cells[i, 2].Value.ToString(), out Int64 s))
                                if (Int64.Parse(hoja.Cells[i, 2].Value.ToString()) > 0)
                                    cvp.SKU = hoja.Cells[i, 2].Value == null ? "" : hoja.Cells[i, 2].Value.ToString();
                                else
                                    sku += i.ToString() + ",";
                            else
                                sku += i.ToString() + ",";
                        else
                            sku += i.ToString() + ",";

                        //Validamos que el Capacity no contenga valores con decimales
                        if (hoja.Cells[i, 3].Value!=null)
                            if (Int32.TryParse(hoja.Cells[i, 3].Value.ToString(), out Int32 c))
                                if(int.Parse(hoja.Cells[i, 3].Value.ToString())>0)
                                    cvp.Capacity = hoja.Cells[i, 3].Value == null ? 0 : int.Parse(hoja.Cells[i, 3].Value.ToString());
                                else
                                    Capacity += i.ToString() + ",";
                            else
                                Capacity +=  i.ToString()+ ",";
                        else
                            Capacity += i.ToString() + ",";

                        cvp.FechaIni = hoja.Cells[i, 4].Value == null ? "" : hoja.Cells[i, 4].Value.ToString();
                        cvp.FechaFin = hoja.Cells[i, 5].Value == null ? "" : hoja.Cells[i, 5].Value.ToString();

                        //Validamos que el idMotivo no contenga decimales
                        if (hoja.Cells[i, 6].Value != null)
                            if (Int32.TryParse(hoja.Cells[i, 6].Value.ToString(), out Int32 m))
                                if (int.Parse(hoja.Cells[i, 6].Value.ToString()) > 0)
                                    cvp.idMotivo = hoja.Cells[i, 6].Value == null ? 0 : int.Parse(hoja.Cells[i, 6].Value.ToString());
                                else
                                    IdMotivo += i.ToString() + ",";
                            else
                                IdMotivo += i.ToString()+ ",";
                        else
                            IdMotivo += i.ToString() + ",";

                        cvp.User = Usuario;
                        
                        listaEnExcel.Add(cvp);
                    }

                    if(!string.IsNullOrEmpty(Capacity) || !string.IsNullOrEmpty(IdMotivo) || !string.IsNullOrEmpty(sku) || !string.IsNullOrEmpty(idSucursal))
                    {
                        response.respuesta.Message = "Existen Datos no validos en:";
                        if(Capacity.Length>0)
                            Capacity = Capacity.Remove(Capacity.Length - 1);
                        if (IdMotivo.Length > 0)
                            IdMotivo = IdMotivo.Remove(IdMotivo.Length - 1);
                        if (sku.Length > 0)
                            sku = sku.Remove(sku.Length - 1);
                        if (idSucursal.Length > 0)
                            idSucursal = idSucursal.Remove(idSucursal.Length - 1);
                        
                        if (!string.IsNullOrEmpty(Capacity))
                            response.respuesta.Message += "\n\rCapacity, Fila:" + Capacity;
                        if (!string.IsNullOrEmpty(IdMotivo))
                            response.respuesta.Message += "\n\rIdMotivo, Fila:" + IdMotivo;
                        if (!string.IsNullOrEmpty(sku))
                            response.respuesta.Message += "\n\rSKU, Fila:" + sku;
                        if (!string.IsNullOrEmpty(idSucursal))
                            response.respuesta.Message += "\n\ridSucursal, Fila;" + idSucursal;


                        return response;
                    }


                    var serializado = AlmacenVirtual.Serialization.SerializeObject(listaEnExcel);
                    var resp = new Data.Capacity.Capacity();
                    response = resp.setMarca(Flag, Usuario, marca, serializado);


                }
                catch (Exception ex)
                {
                    response.respuesta.Message = "Error al procesar el layout de Capacity. Flag=3, " + ex.Message;
                }
            }
            else
            {
                response.respuesta.Message = "No se ha seleccionado un archivo";
            }


            return response;
        }


        public byte[] GetExcelSucursalesNuevas(List<tblReporte> reprote)
        {
            ExcelPackage pck = new ExcelPackage();
            var ws = pck.Workbook.Worksheets.Add("SucursalesNuevas");
            byte[] xlsx = pck.GetAsByteArray();

            try
            {

                pck = new ExcelPackage();

                ws = pck.Workbook.Worksheets.Add("SucursalesNuevas");

                ws.Cells["A1"].Value = "IdSucursal";
                ws.Cells["B1"].Value = "SKU";
                ws.Cells["C1"].Value = "Producto";
                ws.Cells["D1"].Value = "Capacity";
                ws.Cells["E1"].Value = "StatusCarga";


                int renglon = 2;
                foreach (var item in reprote)
                {
                    ws.Cells[string.Format("A{0}", renglon)].Value = item.IdSucursal;
                    ws.Cells[string.Format("B{0}", renglon)].Value = item.SKU;
                    ws.Cells[string.Format("C{0}", renglon)].Value = item.Producto;
                    ws.Cells[string.Format("D{0}", renglon)].Value = item.Capacity;
                    ws.Cells[string.Format("E{0}", renglon)].Value = item.StatusCarga;

                    renglon++;
                }
                ws.Cells["A:AZ"].AutoFitColumns();
                xlsx = pck.GetAsByteArray();
            }
            catch (Exception ex)
            {
                throw ex; //("Error al realizar la consulta de información. " + ex.Message) ;
            }


            return xlsx;


        }

        public CapacityModel setCatCapacity(int Flag, string Nombre, bool Invenadro, string idinput, int Usuario)
        {

            CapacityModel response = new CapacityModel();
            try {
                var resp = new Data.Capacity.Capacity();
                response = resp.setCatCapacity(Flag, Nombre, Invenadro, Usuario, idinput);

            }
            catch (Exception ex)
            {
                response.Message = "Error al insertar el tipo de capacity, " + ex.Message;
            }
            

            return response;
        }

        public CapacityModel DelCatCapacity(int Flag, int id)
        {

            CapacityModel response = new CapacityModel();
            try
            {
                var resp = new Data.Capacity.Capacity();
                response = resp.DelCatCapacity(Flag, id);

            }
            catch (Exception ex)
            {
                response.Message = "Error al Eliminar el tipo de capacity, " + ex.Message;
            }


            return response;
        }

        public CapacityModel DelCapacity(int Flag, string storeId, int usuario)
        {

            CapacityModel response = new CapacityModel();
            try
            {
                var resp = new Data.Capacity.Capacity();
                response = resp.DelCapacity(Flag, storeId, usuario);

            }
            catch (Exception ex)
            {
                response.Message = "Error al Eliminar el tipo de capacity, " + ex.Message;
            }


            return response;
        }

        public List<tblCatCapacity> SelectedCapacity(int Flag)
        {

            List<tblCatCapacity> response = new List<tblCatCapacity>();
            try
            {
                var resp = new Data.Capacity.Capacity();

                response = resp.SelectedCapacity(Flag);

            }
            catch (Exception ex)
            {
                throw (ex);
            }


            return response;
        }

        public List<tblCapacity> ReporteCapacity(int flag, string tipoCapacity, string feini, string fefin)
        {
            List<tblCapacity> reporte = new List<tblCapacity>();


            try
            {
                var resp = new Data.Capacity.Capacity();

                reporte = resp.ReporteCapacity(flag, tipoCapacity, feini, fefin);

            }
            catch (Exception ex)
            {
                throw (ex);
            }


            return reporte;
        }
        
    }
}
