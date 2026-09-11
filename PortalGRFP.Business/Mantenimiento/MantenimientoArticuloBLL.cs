using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PortalGRFP.Entities.Common.MantenimientoArticulo;
using PortalGRFP.Data.Mantenimiento;
using PortalGRFP.Entities.Common;
using PortalGRFP.Entities.Response;
using static PortalGRFP.Entities.Common.MantenimientoArticulo.ArticuloPermisoCompraModel;
using System.Data;
using System.Web;
using System.IO;
using OfficeOpenXml;

namespace PortalGRFP.Business.Mantenimiento
{
    public class MantenimientoArticuloBLL
    {
        private readonly MantenimientoArticuloData mArticulo = new MantenimientoArticuloData();


        public ArticuloTipoCompraModel GetAArticuloTipoCompBLL(int flag, string txt)
        {
            var resp = mArticulo.GetAArticuloTipoCompData(flag, txt);

            return resp;
        }

        public ResponseList<Autocomplete> GetAArticuloTipoListCompraBLL(int flag)
        {
            var response = new ResponseList<Autocomplete>();
            try
            {
                response = mArticulo.GetAArticuloTipoListCompraData(flag);
            }
            catch (Exception ex)
            {
                response.Message = "Error al realizar la consulta de información. " + ex.Message;
            }
            return response;
        }
        public ResponseList<Autocomplete> GetConceptosListCompraBLL()
        {
            var response = new ResponseList<Autocomplete>();
            try
            {
                response = mArticulo.GetConceptosistCompraData();
            }
            catch (Exception ex)
            {
                response.Message = "Error al realizar la consulta de información. " + ex.Message;
            }
            return response;
        }
        public ResponseList<Autocomplete> GetAutoCompleteProductListBLL(int flag)
        {
            var response = new ResponseList<Autocomplete>();
            try
            {
                response = mArticulo.GetAutoCompleteProductListData(flag);
            }
            catch (Exception ex)
            {
                response.Message = "Error al realizar la consulta de información. " + ex.Message;
            }
            return response;
        }



        public List<ArticuloExcel> CargarExcelBLL(HttpFileCollectionBase httpFile)
        {
            var response = new List<ArticuloExcel>();



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

                        //var hojaActual = package.Workbook.Worksheets["Hoja1"];
                        var hoja = package.Workbook.Worksheets[0];
                        int noCol = package.Workbook.Worksheets[0].Dimension.End.Column;
                        int noRenglon = package.Workbook.Worksheets[0].Dimension.End.Row;


                        //var hoja = hojaActual;
                        //int noCol = hoja.Dimension.End.Column;
                        //int noRenglon = hoja.Dimension.End.Row;
                        int i = 0;

                        List<ArticuloExcel> listaEnExcel = new List<ArticuloExcel>();

                        for (i = 2; i <= noRenglon; i++)
                        {
                            ArticuloExcel cvp = new ArticuloExcel()
                            {  
                               IdSucursal = hoja.Cells[i, 1].Value == null ? string.Empty : hoja.Cells[i, 1].Value.ToString(),
                               Sku = hoja.Cells[i, 2].Value == null ? string.Empty : hoja.Cells[i, 2].Value.ToString(),
                            };
                            listaEnExcel.Add(cvp);
                        }
                        
                        response = listaEnExcel;
                        
                    }
                    catch (Exception ex)
                    {
                        
                    }
                }
                else
                {
                    
                }
            }
            else
            {
                
            }
            return response;
        }


        
        public Response UIPermisoCompraArticuloBLL(CargaArticuloMViewModel model, List<ArticuloExcel> excel )
        {
            var response = new Response();
            try
            {
            response = mArticulo.UIPermisoCompraArticuloData(model,excel);
            }
            catch (Exception ex)
            {
                response.Message = "Error al guardar. " + ex.Message;
            }
            return response;
        }

        public Response UIPermisoCompraArticuloPorSucursalBLL(ArticuloXSucursalModel model)
        {
            var response = new Response();
            try
            {
                response = mArticulo.UIPermisoCompraArticuloPorSucursalData(model);
            }
            catch (Exception ex)
            {
                response.Message = "Error al guardar. " + ex.Message;
            }
            return response;
        }
        
        public Response PermisosPorSucursalBLL(ArticulosPorSucursalModel model)
        {
            var response = new Response();
            try
            {
                response = mArticulo.PermisosPorSucursalData(model);
            }
            catch (Exception ex)
            {
                response.Message = "Error al guardar. " + ex.Message;
            }
            return response;
        }

        public ResponseList<CadenasSucursalesModel> GetCadenaSucursalBLL(int flag,string search)
        {
            var response = new ResponseList<CadenasSucursalesModel>();
            try
            {
                response = mArticulo.GetCadenaSucursalData(flag,search);
            }
            catch (Exception ex)
            {
                response.Message = "Error al realizar la consulta de información. " + ex.Message;
            }
            return response;
        }

        public ResponseList<LogErrorPermisoCompraModel> GetLogErrorBLL(int IdUsuario)
        {
            var response = new ResponseList<LogErrorPermisoCompraModel>();
            try
            {
                response = mArticulo.GetLogErrorData(IdUsuario);
            }
            catch (Exception ex)
            {
                response.Message = "Error al realizar la consulta de información. " + ex.Message;
            }
            return response;
        }



        public ResponseList<ComboGenerico> GetCadenasSucursalesBLL(int flag, int idusuario, string cadenas)
        {
            var response = new ResponseList<ComboGenerico>();
            try
            {
                response = mArticulo.GetCadenasSucursalesData(flag, idusuario, cadenas);
            }
            catch (Exception ex)
            {
                response.Message = "Error al realizar la consulta de información. " + ex.Message;
            }
            return response;
        }
        public ResponseList<PermisoArticuloSucursalModel> GetPermisosArticuloSucursalBLL(int flag, int idusuario, string sku, string sucursales) 
        { 
            var response = new ResponseList<PermisoArticuloSucursalModel>();
            try
            {
                response = mArticulo.GetPermisosArticuloSucursalData(flag, idusuario, sku, sucursales);
            }
            catch (Exception ex)
            {
                response.Message = "Error al realizar la consulta de información. " + ex.Message;
            }
            return response;
        }




    }
}
