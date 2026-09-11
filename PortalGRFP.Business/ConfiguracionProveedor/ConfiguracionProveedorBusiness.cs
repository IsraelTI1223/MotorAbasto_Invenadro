using OfficeOpenXml;
using PortalGRFP.Data.ConfiguracionProveerdores;
using PortalGRFP.Entities.Common;
using PortalGRFP.Entities.Response;
using PortalGRFP.Utilities;
using PortalGRFP.Utilities.TableType;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace PortalGRFP.Business.ConfiguracionProveedor
{
    public class ConfiguracionProveedorBusiness
    {
        private readonly ConfiguracionProveedorData configuracion;

        public ConfiguracionProveedorBusiness()
        {
            configuracion = new ConfiguracionProveedorData();
        }

        public ResponseList<Autocomplete> Autocompletar(int accion)
        {
            var response = new ResponseList<Autocomplete>();
            try
            {
                response = configuracion.Autocompletar(accion);
            }
            catch (Exception ex)
            {
                response.Message = "Error al realizar la consulta de información. " + ex.Message;
            }
            return response;
        }

        public Response AgregarAgencia(AgenciaProveedor agencia, int usuario)
        {
            var response = new Response();
            try
            {
                var dt = AgenciaType.Definicion();

                DataRow renglon = dt.NewRow();
                renglon[0] = agencia.IdProveedor;
                renglon[1] = agencia.IdAgencia;
                renglon[2] = agencia.OrdenCompraAutomatica;
                renglon[3] = agencia.FacturaAutomatica;
                renglon[4] = agencia.CatalogoAutomatico;
                renglon[5] = agencia.PermiteRemisiones;
                renglon[6] = agencia.RespuestaFaltante;
                renglon[7] = usuario;

                dt.Rows.Add(renglon);
                response = configuracion.AgregarAgencias(dt);
                response.Message = response.Success ? "La operación se realizo con exito." : "No se pudo completar la operación";
            }
            catch (Exception ex)
            {
                response.Message = "Error al realizar la consulta de información. " + ex.Message;
                if (ex.Message.Contains("Violation of PRIMARY KEY"))
                {
                    response.Message = "Ya se esta utilizando la agencia con otro proveedor";
                }
               
            }
            return response;
        }

        public ResponseList<AgenciaProveedorExt> ConsultarAgencias()
        {
            var response = new ResponseList<AgenciaProveedorExt>();
            try
            {
                response = configuracion.AgenciasAgregadas();
            }
            catch (Exception ex)
            {
                response.Message = "Error al realizar la consulta de información. " + ex.Message;
            }
            return response;
        }

        public ResponseList<ProveedorAgencia> ProveedorAgencia(int accion)
        {
            var response = new ResponseList<ProveedorAgencia>();
            try
            {
                response = configuracion.GetProveedorAgencia(accion);
            }
            catch (Exception ex)
            {
                response.Message = "Error al realizar la consulta de información. " + ex.Message;
            }
            return response;
        }

        public readonly ConfiguracionProveedorData _insertData = new ConfiguracionProveedorData();

        public int InsertData(InsertFTP model, string user)
        {
            return _insertData.InsertProvFTP(model, user);
        }

        public ResponseList<ProveedoresAgenciaFTP> ObtenerConsultaFTP()

        {
            var response = new ResponseList<ProveedoresAgenciaFTP>();
            try
            {
                response = configuracion.ObtenerConsultaFTP();
            }
            catch (Exception ex)
            {
                response.Message = "Error al realizar la consulta de información. " + ex.Message;
            }
            return response;
        }

        public readonly ConfiguracionProveedorData _selectDataFTP = new ConfiguracionProveedorData();

        public ProveedoresAgenciaFTPCompleto SelectDataFTP(string Folio)
        {
            return _selectDataFTP.SelectDataFTP(Folio);
        }



        public readonly ConfiguracionProveedorData _modDataFTP = new ConfiguracionProveedorData();

        public int UpdateFTP(ProveedoresAgenciaFTPCompleto model, string user)
        {
            return _modDataFTP.UpdateFTP(model, user);
        }

        public readonly ConfiguracionProveedorData _deleteDataFTP = new ConfiguracionProveedorData();

        public int DeleteFTP(ProveedoresAgenciaFTPCompleto model, string user)
        {
            return _deleteDataFTP.DeleteFTP(model, user);
        }

        public Response CargaExclusiones(HttpFileCollectionBase httpFile, int usuario, int idProveedor, bool eliminar, bool agregar)
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
                        string fileContentType = archivo.ContentType;
                        byte[] fileBytes = new byte[archivo.ContentLength];
                        var data = archivo.InputStream.Read(fileBytes, 0, Convert.ToInt32(archivo.ContentLength));

                        var package = new ExcelPackage(archivo.InputStream);

                        var hojaActual = package.Workbook.Worksheets["Exclusiones"];

                        if (hojaActual == null)
                            throw new Exception("El nombre de la hoja debe ser Exclusiones");

                        if (hojaActual.Name.ToLower() != "exclusiones")
                            throw new Exception("El nombre de la hoja debe ser Exclusiones");

                        var hoja = hojaActual;
                        int noCol = hoja.Dimension.End.Column;
                        int noRenglon = hoja.Dimension.End.Row;
                        int i = 0;

                        List<Invenadro_Carga> listaEnExcel = new List<Invenadro_Carga>();
                        int linea = 0;

                        var dt = ExclusionesType.Definicion();

                        for (i = 2; i <= noRenglon; i++)
                        {
                            linea = i;
                            DataRow renglon = dt.NewRow();
                            renglon[0] = hoja.Cells[i, 1].Value.ToString();
                            renglon[1] = eliminar;
                            renglon[2] = agregar;
                            renglon[3] = idProveedor;
                            renglon[4] = usuario;
                            dt.Rows.Add(renglon);

                        }

                        response = configuracion.CargaExclusiones(dt);
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

        public ResponseList<BitacoraExclusiones> GetLogExclusiones(int idproveedor)
        {
            var response = new ResponseList<BitacoraExclusiones>();
            try
            {
                BitacoraExclusiones bitacoraExclusiones = new BitacoraExclusiones();
                bitacoraExclusiones.Exclusiones = configuracion.GetExclusionesCarga(idproveedor).Result;

                bitacoraExclusiones.LogExclusiones = configuracion.GetLogExclusiones(idproveedor).Result;
                List<BitacoraExclusiones> lista = new List<BitacoraExclusiones>();
                lista.Add(bitacoraExclusiones);
                response.Result = lista;
            }
            catch (Exception ex)
            {
                response.Message = "Error al realizar la consulta de información. " + ex.Message;
            }
            return response;
        }
    }
}
