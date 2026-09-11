namespace PortalGRFP.Business.BulkLoads
{
    using OfficeOpenXml;
    using PortalGRFP.Business.Extensions;
    using PortalGRFP.Data.ConfiguracionCarga;
    using PortalGRFP.Data.Decuentos;
    using PortalGRFP.Data.TipoPerfil;
    using PortalGRFP.Entities.Common;
    using PortalGRFP.Entities.Enums;
    using PortalGRFP.Entities.Parameters;
    using PortalGRFP.Entities.Request;
    using PortalGRFP.Entities.Response;
    using PortalGRFP.Utilities.Files;
    using PortalGRFP.Utilities.TableType;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Web;
  

    public class BBulkLoadDiscount
    {
        private readonly BulkLoadData bulkLoadData;

        public BBulkLoadDiscount()
        {
            this.bulkLoadData = new BulkLoadData();
        }

        public Response FileUpload(int idUsuario, HttpPostedFileBase file, string idCliente, int idPerfil, string tipoPerfil, int IDTIPO2,int idSubCliente, string NombreCliente, string NombreSubcliente)
        {
            var response = new Response();

            try
            {
                var dt = DescuentoType.GetDefinition();
                var dtLog = LogDescuentoType.GetDefinition();
                var request = new Request<UpdLoadDiscoutParameter>();
                request.Parameters = new UpdLoadDiscoutParameter();

                using (ExcelPackage package = new ExcelPackage(file.InputStream))
                {
                    var ws = package.Workbook.Worksheets[0];
                    if (ws != null)
                    {
                        var wsStart = ws.Dimension.Start;
                        var wsEnd = ws.Dimension.End;                       

                        var tipoDescuento = (wsEnd.Column == 9) ? TipoCarga.FARMARETAIL : (wsEnd.Column == 2) ? TipoCarga.LDCOM : (wsEnd.Column == 10) ? TipoCarga.POPSAE : TipoCarga.TODOS;

                        if (tipoDescuento != TipoCarga.TODOS)
                        {
                            var responseValidHeaders = (tipoDescuento == TipoCarga.FARMARETAIL) ? ws.ValidHeadersFR()
                                : (tipoDescuento == TipoCarga.LDCOM) ? ws.ValidHeadersLDCOM()
                                : (tipoDescuento == TipoCarga.POPSAE) ? ws.ValidHeadersPOPSAE()
                                : new Response { Message = "No se especifico el tipo de Layout" };


                            if (responseValidHeaders.Success)
                            {
                                request.Parameters.Discounts = (tipoDescuento == TipoCarga.FARMARETAIL) ? readLayoutFR(ws, out dtLog)
                                : (tipoDescuento == TipoCarga.LDCOM) ? readLayoutLDCOM(ws, out dtLog)
                                : (tipoDescuento == TipoCarga.POPSAE) ? readLayoutPOPSAE(ws, out dtLog)
                                : DescuentoType.GetDefinition();

                                if (idPerfil == 1)
                                {

                                    string valor = tipoPerfil;

                                    if (IDTIPO2 == 3 || IDTIPO2 == 4)
                                    {                                 
                                        string newValor = tipoPerfil;
                                        newValor = newValor.Replace(tipoPerfil, "POPSAE");

                                        if (newValor == tipoDescuento.ToString())
                                        {
                                            request.Parameters.NombreArchivo = file.FileName;
                                            request.Parameters.IdTipoCarga = (int)tipoDescuento;
                                            request.Parameters.IdCliente = Convert.ToInt32(idCliente);
                                            request.Parameters.IdTipoCliente = IDTIPO2;
                                            request.Parameters.IdSubCliente = idSubCliente;
                                            request.Parameters.Nombre = NombreCliente;
                                            request.IdUsuario = idUsuario;
                                            
                                            request.Parameters.Errors = dtLog;

                                            response = bulkLoadData.Execute(request);

                                            return response;
                                        }
                                    }

                                    if (tipoPerfil == tipoDescuento.ToString())
                                    {
                                        

                                        request.Parameters.NombreArchivo = file.FileName;
                                        request.Parameters.IdTipoCarga = (int)tipoDescuento;
                                        request.Parameters.IdCliente = Convert.ToInt32(idCliente);
                                        request.Parameters.IdTipoCliente = IDTIPO2;
                                        request.Parameters.IdSubCliente = idSubCliente;
                                        request.Parameters.Nombre = NombreCliente;
                                        request.Parameters.NombreSubcliente = NombreSubcliente;
                                        request.IdUsuario = idUsuario;
                                        request.Parameters.Errors = dtLog;

                                        response = bulkLoadData.Execute(request);
                                    }

                                    else
                                    {
                                        response.Message = "El cliente seleccionado no corresponde al tipo de archivo cargado.";
                                    }

                                }


                                if (idPerfil == 4)
                                {

                                    if (tipoDescuento != TipoCarga.FARMARETAIL)
                                    {
                                        response.Message = "El archivo no corresponde al perfil ingresado.";
                                    }
                                    else
                                    {
                                        request.Parameters.NombreArchivo = file.FileName;
                                        request.Parameters.IdTipoCarga = (int)tipoDescuento;
                                        request.Parameters.IdCliente = Convert.ToInt32(idCliente);
                                        request.Parameters.IdSubCliente = idSubCliente;
                                        request.Parameters.IdTipoCliente = IDTIPO2;
                                        request.Parameters.Nombre = NombreCliente;
                                        request.Parameters.NombreSubcliente = NombreSubcliente;
                                        request.IdUsuario = idUsuario;
                                        request.Parameters.Errors = dtLog;



                                        response = bulkLoadData.Execute(request);
                                       


                                    }

                                }

                                if (idPerfil == 3)
                                {

                                    if (tipoDescuento != TipoCarga.POPSAE)
                                    {
                                        response.Message = "El archivo no corresponde al perfil ingresado.";
                                    }
                                    else
                                    {
                                        request.Parameters.NombreArchivo = file.FileName;
                                        request.Parameters.IdTipoCarga = (int)tipoDescuento;
                                        request.Parameters.IdCliente = Convert.ToInt32(idCliente);
                                        request.Parameters.IdSubCliente = idSubCliente;
                                        request.Parameters.IdTipoCliente = IDTIPO2;
                                        request.Parameters.Nombre = NombreCliente;
                                        request.IdUsuario = idUsuario;
                                        request.Parameters.Errors = dtLog;



                                        response = bulkLoadData.Execute(request);
                                        


                                    }

                                }

                                if (idPerfil == 2)
                                {

                                    if (tipoDescuento != TipoCarga.LDCOM)
                                    {
                                        response.Message = "El archivo no corresponde al perfil ingresado.";
                                    }
                                    else
                                    {
                                        request.Parameters.NombreArchivo = file.FileName;
                                        request.Parameters.IdTipoCarga = (int)tipoDescuento;
                                        request.Parameters.IdCliente = Convert.ToInt32(idCliente);
                                        request.Parameters.IdSubCliente = idSubCliente;
                                        request.Parameters.IdTipoCliente = IDTIPO2;
                                        request.Parameters.Nombre = NombreCliente;
                                        request.IdUsuario = idUsuario;
                                        request.Parameters.Errors = dtLog;

                                        response = bulkLoadData.Execute(request);
                                        //response.Message = response.Success ? "El archivo se cargo correctamente" : "No se pudo completar la carga del archivo";

                                    }
                                }
                            }
                            else
                            {
                                response.Message = responseValidHeaders.Message;
                            }
                        }
                        else
                        {
                            response.Message = "El número de columnas no coincide con el número de columnas de los layout validos.";
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error al procesar el archivo. " + ex.Message;
            }

            return response;
        }

        private DataTable readLayoutFR(ExcelWorksheet worksheet, out DataTable errors)
        {
            var dt = DescuentoType.GetDefinition();
            var dtLog = LogDescuentoType.GetDefinition();
            var wsStart = worksheet.Dimension.Start;
            var wsEnd = worksheet.Dimension.End;
            for (var row = wsStart.Row + 1; row <= wsEnd.Row; row++)
            {
                var descuentoValido = CellRegex.IsDecimal(worksheet.Cells[row, 3].Value.ToString());
                var precioValido = CellRegex.IsDecimal(worksheet.Cells[row, 4].Value.ToString());
                var estatusValido = CellRegex.IsBool(worksheet.Cells[row, 5].Value.ToString());
                var autorizacionValido = CellRegex.IsBool(worksheet.Cells[row, 7].Value.ToString());

                if (descuentoValido && precioValido && estatusValido && autorizacionValido)
                {
                    dt.Rows.Add((int)TipoCarga.FARMARETAIL, //IdTipoDescuento
                                                worksheet.Cells[row, 1].Value.ToString(), //CodigoEAN
                                                Convert.ToDecimal(worksheet.Cells[row, 4].Value), //PMP
                                                string.Empty, //Descripcion
                                                Convert.ToDecimal(worksheet.Cells[row, 3].Value), //Descuento
                                                Convert.ToBoolean(worksheet.Cells[row, 7].Value), //Autorización
                                                Convert.ToInt32(worksheet.Cells[row, 5].Value), //Estatus
                                                worksheet.Cells[row, 6].Value.ToString(), //TipoDescuento
                                                Convert.ToString(worksheet.Cells[row, 8].Value), //MsgCondiciones
                                                worksheet.Cells[row, 2].Value.ToString(), //BasePrecio
                                                0, //CostoPromedio
                                                0, //PrecioFijo 
                                                0, //Margen
                                                false, // SinUtilidad
                                                false, //Excluir
                                                Convert.ToString(worksheet.Cells[row,9].Value) // SkuFisico
                                                );
                }
                else
                {
                    dtLog.Rows.Add(worksheet.Cells[row, 1].Value.ToString(), string.Format("Las siguientes columnas no son validos: {0} {1} {2} {3}",
                     (!precioValido) ? "Precio" : "",
                     (!descuentoValido) ? "Descuento" : "",
                     (!estatusValido) ? "Estatus" : "",
                     (!autorizacionValido) ? "Autorización" : ""));
                }
            }
            errors = dtLog;
            return dt;
        }

        private DataTable readLayoutLDCOM(ExcelWorksheet worksheet, out DataTable errors)
        {
            var dt = DescuentoType.GetDefinition();
            var dtLog = LogDescuentoType.GetDefinition();
            var wsStart = worksheet.Dimension.Start;
            var wsEnd = worksheet.Dimension.End;
            for (var row = wsStart.Row + 1; row <= wsEnd.Row; row++)
            {
                var montoValido = CellRegex.IsDecimal(worksheet.Cells[row, 2].Value.ToString());
                if (montoValido)
                {
                    dt.Rows.Add((int)TipoCarga.LDCOM, //IdTipoDescuento
                                                worksheet.Cells[row, 1].Value.ToString(), //CodigoEAN
                                                Convert.ToDecimal(worksheet.Cells[row, 2].Value), //PMP
                                                string.Empty, //Descripcion
                                                0, //Descuento
                                                false, //Autorización
                                                0, //Estatus
                                                string.Empty, //TipoDescuento
                                                string.Empty, //MsgCondiciones
                                                0, //BasePrecio
                                                0, //CostoPromedio
                                                0, //PrecioFijo
                                                0, //Margen
                                                false, // SinUtilidad
                                                false, //Excluir
                                                string.Empty //SkuFisico
                                                );
                }
                else
                {
                    dtLog.Rows.Add(worksheet.Cells[row, 1].Value.ToString(), "Monto no valido");
                }
            }
            errors = dtLog;
            return dt;
        }

        private DataTable readLayoutPOPSAE(ExcelWorksheet worksheet, out DataTable errors)
        {
            var dt = DescuentoType.GetDefinition();
            var dtLog = LogDescuentoType.GetDefinition();
            var wsStart = worksheet.Dimension.Start;
            var wsEnd = worksheet.Dimension.End;
            for (var row = wsStart.Row + 1; row <= wsEnd.Row; row++)
            {
                var precioValido = CellRegex.IsDecimal(worksheet.Cells[row, 3].Value.ToString());
                var descuentoValido = CellRegex.IsDecimal(worksheet.Cells[row, 4].Value.ToString());
                var costoPromedioValido = CellRegex.IsDecimal(worksheet.Cells[row, 5].Value.ToString());
                var precioFijoValido = CellRegex.IsDecimal(worksheet.Cells[row, 6].Value.ToString());
                var margenValido = CellRegex.IsDecimal(worksheet.Cells[row, 7].Value.ToString());
                var autorizacionValido = CellRegex.IsBool(worksheet.Cells[row, 10].Value.ToString());
                var sinUtilidadValido = CellRegex.IsBool(worksheet.Cells[row, 8].Value.ToString());
                var excluirValido = CellRegex.IsBool(worksheet.Cells[row, 9].Value.ToString());

                if (precioValido && descuentoValido && costoPromedioValido && precioFijoValido && margenValido && autorizacionValido && sinUtilidadValido && excluirValido)
                {
                    dt.Rows.Add((int)TipoCarga.POPSAE, //IdTipoDescuento
                                                worksheet.Cells[row, 1].Value.ToString(), //CodigoEAN
                                                Convert.ToDecimal(worksheet.Cells[row, 3].Value), //PMP
                                                worksheet.Cells[row, 2].Value.ToString(), //Descripcion
                                                Convert.ToDecimal(worksheet.Cells[row, 4].Value), //Descuento
                                                 Convert.ToBoolean(worksheet.Cells[row, 10].Value), //Autorización
                                                0, //Estatus
                                                string.Empty, //TipoDescuento
                                                string.Empty, //MsgCondiciones
                                                0, //BasePrecio
                                                Convert.ToDecimal(worksheet.Cells[row, 5].Value), //CostoPromedio
                                                Convert.ToDecimal(worksheet.Cells[row, 6].Value), //PrecioFijo
                                                Convert.ToDecimal(worksheet.Cells[row, 7].Value), //Margen
                                                Convert.ToBoolean(worksheet.Cells[row, 8].Value), // SinUtilidad
                                                Convert.ToBoolean(worksheet.Cells[row, 9].Value), //Excluir
                                                string.Empty //SkuFisico
                                                );
                }
                else
                {
                    dtLog.Rows.Add(worksheet.Cells[row, 1].Value.ToString(), string.Format("Las siguientes columnas no son validos: {0} {1} {2} {3} {4} {5} {6} {7}",
                        !precioValido ? "Precio Público" : "",
                        !descuentoValido ? "Descuento" : "",
                        !costoPromedioValido ? "Costo Promedio" : "",
                        !precioFijoValido ? "Precio Fijo" : "",
                        !margenValido ? "Margen" : "",
                        !autorizacionValido ? "Autorizar" : "",
                        !sinUtilidadValido ? "Sin Utilidad" : "",
                        !excluirValido ? "Excluir" : ""));
                }
            }
            errors = dtLog;
            return dt;
        }
    }
}
