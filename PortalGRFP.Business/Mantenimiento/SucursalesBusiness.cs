using OfficeOpenXml;
using PortalGRFP.Data.Mantenimiento;
using PortalGRFP.Entities.Common;
using PortalGRFP.Entities.Response;
using PortalGRFP.Utilities.TableType;
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
    public class SucursalesBusiness
    {
        private readonly SucursalData sucursal;

        public SucursalesBusiness()
        {
            sucursal = new SucursalData();
        }
        public ResponseList<ComboGenerico> Filtros(int accion)
        {
            var response = new ResponseList<ComboGenerico>();
            try
            {
                response = sucursal.Filtros(accion);
            }
            catch (Exception ex)
            {
                response.Message = "Error al realizar la consulta de información. " + ex.Message;
            }
            return response;
        }

        public Response GuardarManttoSuc(MantenimientoSucursal mantenimiento, int usuario)
        {
            var response = new Response();
            try
            {
                response = sucursal.ManttoSucGuardar(mantenimiento, usuario);
                response.Message = response.Success ? "La operación se realizo con exito." : "No se pudo completar la operación";


            }
            catch (Exception ex)
            {
                response.Message = "Error al realizar la consulta de información. " + ex.Message;
            }
            return response;
        }

        public ResponseList<MantenimientoSucursal> GetManttoSuc(MantenimientoSucursal mantenimiento)
        {
            var response = new ResponseList<MantenimientoSucursal>();
            try
            {
                response = sucursal.MantenimientoGet(mantenimiento);
            }
            catch (Exception ex)
            {
                response.Message = "Error al realizar la consulta de información. " + ex.Message;
            }
            return response;
        }


        public Response GuardarSucursalProveedor(SucursalProveedor sucursalP, int usuario)
        {
            var response = new Response();
            try
            {
                var dt = SucursalProveedorType.Definicion();

                DataRow renglon = dt.NewRow();
                renglon[0] = sucursalP.IdProveedor;
                renglon[1] = sucursalP.IdSucursal;
                renglon[2] = sucursalP.LeadTime;
                renglon[3] = sucursalP.DiasCobertura;
                renglon[4] = sucursalP.IdAgencia;
                renglon[5] = sucursalP.ClienteProveedor;
                renglon[6] = sucursalP.Lunes;
                renglon[7] = sucursalP.Martes;
                renglon[8] = sucursalP.Miercoles;
                renglon[9] = sucursalP.Jueves;
                renglon[10] = sucursalP.Viernes;
                renglon[11] = sucursalP.Sabado;
                renglon[12] = sucursalP.Domingo;
                renglon[13] = "N";
                renglon[14] = DateTime.Now;
                renglon[15] = DateTime.Now;
                renglon[16] = usuario;
                dt.Rows.Add(renglon);


                response = sucursal.GuardarSucProveedor(dt);
                response.Message = response.Success ? "La operación se realizo con exito." : "No se pudo completar la operación";
            }
            catch (Exception ex)
            {
                response.Message = "Error al realizar la consulta de información. " + ex.Message;

                if(ex.Message.Contains("Violation of PRIMARY KEY"))
                {
                    response.Message = "Esta Configuración ya existe, consultela por favor";
                }
            }
            return response;
        }

        public ResponseList<SucursalProveedorExt> ConsultaSucursalProv(SucursalProveedor sucursalP)
        {
            var response = new ResponseList<SucursalProveedorExt>();
            try
            {
                response = sucursal.ObtenerSucProv(sucursalP);
            }
            catch (Exception ex)
            {
                response.Message = "Error al realizar la consulta de información. " + ex.Message;
            }
            return response;
        }

        public Response CargaMasiva(HttpFileCollectionBase httpFile, int usuario)
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

                        var hojaActual = package.Workbook.Worksheets["sucursalProveedor"];

                        if (hojaActual == null)
                                throw new Exception("El nombre de la hoja debe ser sucursalProveedor");

                        if (hojaActual.Name != "sucursalProveedor")
                            throw new Exception("El nombre de la hoja debe ser sucursalProveedor");

                        var hoja = hojaActual;
                            int noCol = hoja.Dimension.End.Column;
                            int noRenglon = hoja.Dimension.End.Row;
                            int i = 0;

                            List<SucursalProveedor> listaEnExcel = new List<SucursalProveedor>();

                            for (i = 2; i <= noRenglon; i++)
                            {
                                SucursalProveedor cvp = new SucursalProveedor()
                                {
                                    IdProveedor = hoja.Cells[i, 1].Value == null ? 0 : int.Parse(hoja.Cells[i, 1].Value.ToString()),
                                    IdSucursal = hoja.Cells[i, 2].Value == null ? 0 : int.Parse(hoja.Cells[i, 2].Value.ToString()),
                                    LeadTime = hoja.Cells[i, 3].Value == null ? 0 : int.Parse(hoja.Cells[i, 3].Value.ToString()),
                                    DiasCobertura = hoja.Cells[i, 4].Value == null ? 0 : int.Parse(hoja.Cells[i, 4].Value.ToString()),
                                    IdAgencia = hoja.Cells[i, 5].Value == null ? 0 : int.Parse(hoja.Cells[i, 5].Value.ToString()),
                                    ClienteProveedor = hoja.Cells[i, 6].Value == null ? 0 : int.Parse(hoja.Cells[i, 6].Value.ToString()),
                                    Lunes = hoja.Cells[i, 7].Value == null ? 0 : int.Parse(hoja.Cells[i, 7].Value.ToString()),
                                    Martes = hoja.Cells[i, 8].Value == null ? 0 : int.Parse(hoja.Cells[i, 8].Value.ToString()),
                                    Miercoles = hoja.Cells[i, 9].Value == null ? 0 : int.Parse(hoja.Cells[i, 9].Value.ToString()),
                                    Jueves = hoja.Cells[i, 10].Value == null ? 0 : int.Parse(hoja.Cells[i, 10].Value.ToString()),
                                    Viernes = hoja.Cells[i, 11].Value == null ? 0 : int.Parse(hoja.Cells[i, 11].Value.ToString()),
                                    Sabado = hoja.Cells[i, 12].Value == null ? 0 : int.Parse(hoja.Cells[i, 12].Value.ToString()),
                                    Domingo = hoja.Cells[i, 13].Value == null ? 0 : int.Parse(hoja.Cells[i, 13].Value.ToString())
                                };
                                if (cvp.IdSucursal != 0 && cvp.IdProveedor != 0 && cvp.IdAgencia != 0)
                                {
                                    listaEnExcel.Add(cvp);
                                }

                            }

                            var dt = SucursalProveedorType.Definicion();

                            foreach (var item in listaEnExcel)
                            {
                                DataRow renglon = dt.NewRow();
                                renglon[0] = item.IdProveedor;
                                renglon[1] = item.IdSucursal;
                                renglon[2] = item.LeadTime;
                                renglon[3] = item.DiasCobertura;
                                renglon[4] = item.IdAgencia;
                                renglon[5] = item.ClienteProveedor;
                                renglon[6] = item.Lunes;
                                renglon[7] = item.Martes;
                                renglon[8] = item.Miercoles;
                                renglon[9] = item.Jueves;
                                renglon[10] = item.Viernes;
                                renglon[11] = item.Sabado;
                                renglon[12] = item.Domingo;
                                renglon[13] = "N";
                                renglon[14] = DateTime.Now;
                                renglon[15] = DateTime.Now;
                                renglon[16] = usuario;
                                dt.Rows.Add(renglon);
                            }

                            response = sucursal.GuardarSucProveedor(dt);
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

        public ResponseList<SucursalProveedorExt> ConsultaMasiva(HttpFileCollectionBase httpFile, int usuario)
        {
            var response = new ResponseList<SucursalProveedorExt>();
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

                        var hojaActual = package.Workbook.Worksheets["sucursalProveedor"];
                        //if(hojaActual.Name != "sucursalProveedor")
                        //{

                        //}
                        var hoja = hojaActual;
                        int noCol = hoja.Dimension.End.Column;
                        int noRenglon = hoja.Dimension.End.Row;
                        int i = 0;

                        List<SucursalProveedor> listaEnExcel = new List<SucursalProveedor>();

                        for (i = 2; i <= noRenglon; i++)
                        {
                            SucursalProveedor cvp = new SucursalProveedor()
                            {
                                IdProveedor = hoja.Cells[i, 1].Value == null ? 0 : int.Parse(hoja.Cells[i, 1].Value.ToString()),
                                IdSucursal = hoja.Cells[i, 2].Value == null ? 0 : int.Parse(hoja.Cells[i, 2].Value.ToString()),
                                LeadTime = hoja.Cells[i, 3].Value == null ? 0 : int.Parse(hoja.Cells[i, 3].Value.ToString()),
                                DiasCobertura = hoja.Cells[i, 4].Value == null ? 0 : int.Parse(hoja.Cells[i, 4].Value.ToString()),
                                IdAgencia = hoja.Cells[i, 5].Value == null ? 0 : int.Parse(hoja.Cells[i, 5].Value.ToString()),
                                ClienteProveedor = hoja.Cells[i, 6].Value == null ? 0 : int.Parse(hoja.Cells[i, 6].Value.ToString()),
                                Lunes = hoja.Cells[i, 7].Value == null ? 0 : int.Parse(hoja.Cells[i, 7].Value.ToString()),
                                Martes = hoja.Cells[i, 8].Value == null ? 0 : int.Parse(hoja.Cells[i, 8].Value.ToString()),
                                Miercoles = hoja.Cells[i, 9].Value == null ? 0 : int.Parse(hoja.Cells[i, 9].Value.ToString()),
                                Jueves = hoja.Cells[i, 10].Value == null ? 0 : int.Parse(hoja.Cells[i, 10].Value.ToString()),
                                Viernes = hoja.Cells[i, 11].Value == null ? 0 : int.Parse(hoja.Cells[i, 11].Value.ToString()),
                                Sabado = hoja.Cells[i, 12].Value == null ? 0 : int.Parse(hoja.Cells[i, 12].Value.ToString()),
                                Domingo = hoja.Cells[i, 13].Value == null ? 0 : int.Parse(hoja.Cells[i, 13].Value.ToString())
                            };
                            if (cvp.IdSucursal != 0 && cvp.IdProveedor != 0 && cvp.IdAgencia != 0)
                            {
                                listaEnExcel.Add(cvp);
                            }

                        }


                        var dt = SucursalProveedorType.Definicion();

                        foreach (var item in listaEnExcel)
                        {
                            DataRow renglon = dt.NewRow();
                            renglon[0] = item.IdProveedor;
                            renglon[1] = item.IdSucursal;
                            renglon[2] = item.LeadTime;
                            renglon[3] = item.DiasCobertura;
                            renglon[4] = item.IdAgencia;
                            renglon[5] = item.ClienteProveedor;
                            renglon[6] = item.Lunes;
                            renglon[7] = item.Martes;
                            renglon[8] = item.Miercoles;
                            renglon[9] = item.Jueves;
                            renglon[10] = item.Viernes;
                            renglon[11] = item.Sabado;
                            renglon[12] = item.Domingo;
                            renglon[13] = "N";
                            renglon[14] = DateTime.Now;
                            renglon[15] = DateTime.Now;
                            renglon[16] = usuario;
                            dt.Rows.Add(renglon);
                        }

                        response = sucursal.ConsultaMasiva(dt);
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
    }
}
