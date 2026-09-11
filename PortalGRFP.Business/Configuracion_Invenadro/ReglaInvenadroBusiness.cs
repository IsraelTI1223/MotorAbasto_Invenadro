using OfficeOpenXml;
using PortalGRFP.Data;
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

namespace PortalGRFP.Business.Configuracion_Invenadro
{
    public class ReglaInvenadroBusiness
    {
        private readonly ReglaInvenadroData reglaInvenadroData;
        public ReglaInvenadroBusiness()
        {
            reglaInvenadroData = new ReglaInvenadroData();
        }
        public ResponseList<Invenadro> GuardarInvenadro(Invenadro invenadro, int usuario)
        {
            var response = new Response();
            var respuesta = new ResponseList<Invenadro>();
            try
            {
                response = reglaInvenadroData.InvenadroGuardar(invenadro, usuario);
                response.Message = response.Success ? "La operación se realizo con exito." : "No se pudo completar la operación";

                respuesta = GetInvenadroRegla();
            }
            catch (Exception ex)
            {
                response.Message = "Error al realizar la consulta de información. " + ex.Message;
            }
            return respuesta;
        }

        public ResponseList<Invenadro> GetInvenadroRegla()
        {
            var response = new ResponseList<Invenadro>();
            try
            {
                response = reglaInvenadroData.InvenadroGet();
            }
            catch (Exception ex)
            {
                response.Message = "Error al realizar la consulta de información. " + ex.Message;
            }
            return response;
        }

        public Response CargaInvenadro(HttpFileCollectionBase httpFile, int usuario, string farmacias)
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

                        var hojaActual = package.Workbook.Worksheets["Invenadro"];

                        if (hojaActual == null)
                            throw new Exception("EL nombre de la hoja debe ser Invenadro");

                        if (hojaActual.Name != "Invenadro")
                            throw new Exception("EL nombre de la hoja debe ser Invenadro");

                        var hoja = hojaActual;
                        int noCol = hoja.Dimension.End.Column;
                        int noRenglon = hoja.Dimension.End.Row;
                        int i = 0;

                        List<Invenadro_Carga> listaEnExcel = new List<Invenadro_Carga>();
                        int linea = 0;

                        for (i = 2; i <= noRenglon; i++)
                        {
                            linea = i;
                            Invenadro_Carga cvp = new Invenadro_Carga();
                            cvp.Tipo_Invenadro = hoja.Cells[i, 1].Value.ToString();
                            cvp.NumSucursal = int.Parse(hoja.Cells[i, 2].Value.ToString());
                            cvp.NombreSucursal = hoja.Cells[i, 3].Value.ToString();
                            cvp.IdCliente = int.Parse(hoja.Cells[i, 4].Value.ToString());
                            cvp.NombreCliente = hoja.Cells[i, 5].Value.ToString();
                            cvp.MontoInversion = decimal.Parse(hoja.Cells[i, 6].Value.ToString());
                            cvp.MaterialId = hoja.Cells[i, 7].Value.ToString();
                            cvp.Material = hoja.Cells[i, 8].Value.ToString();
                            cvp.Subem = hoja.Cells[i, 9].Value.ToString();
                            cvp.Ean_Upc = hoja.Cells[i, 10].Value.ToString();
                            cvp.Fabricante = hoja.Cells[i, 11].Value.ToString();
                            cvp.CveJqiaProductos = hoja.Cells[i, 12].Value.ToString();
                            cvp.JquiaProductos = hoja.Cells[i, 13].Value.ToString();
                            cvp.CategoriaMaterial = hoja.Cells[i, 14].Value.ToString();
                            cvp.MontoAdicional = hoja.Cells[i, 15].Value.ToString();
                            cvp.Optimo = int.Parse(hoja.Cells[i, 16].Value.ToString());
                            cvp.PrecioFarmacia = decimal.Parse(hoja.Cells[i, 17].Value.ToString());
                            cvp.Importemaximo = decimal.Parse(hoja.Cells[i, 18].Value.ToString());

                            listaEnExcel.Add(cvp);


                        }

                        var dt = InvenadroType.Definicion();

                        foreach (var item in listaEnExcel)
                        {
                            DataRow renglon = dt.NewRow();
                            renglon[0] = item.Tipo_Invenadro;
                            renglon[1] = item.NumSucursal;
                            renglon[2] = item.NombreSucursal;
                            renglon[3] = item.IdCliente;
                            renglon[4] = item.NombreCliente;
                            renglon[5] = item.MontoInversion;
                            renglon[6] = item.MaterialId;
                            renglon[7] = item.Material;
                            renglon[8] = item.Subem;
                            renglon[9] = item.Ean_Upc;
                            renglon[10] = item.Fabricante;
                            renglon[11] = item.CveJqiaProductos;
                            renglon[12] = item.JquiaProductos;
                            renglon[13] = item.CategoriaMaterial;
                            renglon[14] = item.MontoAdicional;
                            renglon[15] = item.Optimo;
                            renglon[16] = item.PrecioFarmacia;
                            renglon[17] = item.Importemaximo;
                            renglon[18] = usuario;
                            renglon[19] = farmacias;
                            dt.Rows.Add(renglon);
                        }

                        response = reglaInvenadroData.CargaInvenadro(dt);
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

        public ResponseList<Combo4> Filtros(int accion)
        {
            var response = new ResponseList<Combo4>();
            try
            {
                response = reglaInvenadroData.Filtros(accion);
            }
            catch (Exception ex)
            {
                response.Message = "Error al realizar la consulta de información. " + ex.Message;
            }
            return response;
        }


        public ResponseList<LogInvenadro> Getinvenadro(int accion)
        {
            var response = new ResponseList<LogInvenadro>();
            try
            {
                response = reglaInvenadroData.GetInvenadro(accion);
            }
            catch (Exception ex)
            {
                response.Message = "Error al realizar la consulta de información. " + ex.Message;
            }
            return response;
        }

        public Response BorrarInvenadro(string sucursales, int usuario)
        {
            var response = new Response();
            try
            {
                response = reglaInvenadroData.BorrarInvenadro(sucursales, usuario);
            }
            catch (Exception ex)
            {
                response.Message = "Error al realizar la consulta de información. " + ex.Message;
            }
            return response;
        }

        #region Excluidos

        public ResponseList<ComboGenerico> GetSucursalesComboBLL()
        {
            var response = new ResponseList<ComboGenerico>();
            try
            {
                response = reglaInvenadroData.GetSucursalesComboData();
            }
            catch (Exception ex)
            {
                response.Message = "Error al realizar la consulta de información. " + ex.Message;
            }
            return response;
        }
        public ResponseList<Excluidos_Invenadro_Aut> GetExcluidosBLL(string SKU, string Sucursales)
        {
            var response = new ResponseList<Excluidos_Invenadro_Aut>();
            try
            {
                response = reglaInvenadroData.GetExcluidosData(SKU, Sucursales);
            }
            catch (Exception ex)
            {
                response.Message = "Error al realizar la consulta de información. " + ex.Message;
            }
            return response;
        }
        public byte[] getExcelExcluidosInvenadroBLL(string SKU, string Sucursales)
        {
            var response = new ResponseList<Excluidos_Invenadro_Aut>();
            byte[] xlsx;

            try
            {
                response = reglaInvenadroData.GetExcluidosData(SKU, Sucursales);

                using (ExcelPackage pck = new ExcelPackage())
                {
                    var ws = pck.Workbook.Worksheets.Add("Excluidos Invenadro");

                    ws.Cells["A1"].Value = "Id Sucursal";
                    ws.Cells["B1"].Value = "Sucursal";
                    ws.Cells["C1"].Value = "SKU";
                    ws.Cells["D1"].Value = "Descripcion";
                    ws.Cells["E1"].Value = "Optimo Anterior";
                    ws.Cells["F1"].Value = "Optimo Nuevo";
                    ws.Cells["G1"].Value = "PVD";
                    ws.Cells["H1"].Value = "Relación Invenadro Venta";
                    ws.Cells["I1"].Value = "Motivo";

                    int renglon = 2;

                    foreach (var item in response.Result)
                    {
                        ws.Cells[$"A{renglon}"].Value = item.IdSucursal;
                        ws.Cells[$"B{renglon}"].Value = item.NombreSucursal;
                        ws.Cells[$"C{renglon}"].Value = item.SKU;
                        ws.Cells[$"D{renglon}"].Value = item.NombreSKU;
                        ws.Cells[$"E{renglon}"].Value = item.OptimoAnterior;
                        ws.Cells[$"F{renglon}"].Value = item.OptimoActualizado;
                        ws.Cells[$"G{renglon}"].Value = item.PVD;
                        ws.Cells[$"H{renglon}"].Value = item.RelacionInvenadroVenta;
                        ws.Cells[$"I{renglon}"].Value = item.Motivo;

                        renglon++;
                    }

                    ws.Cells["A:L"].AutoFitColumns();
                    xlsx = pck.GetAsByteArray();
                }
            }
            catch
            {
                xlsx = new byte[0];
            }

            return xlsx;
        }

        //public Response AutorizarExcluidosMasivoBLL(HttpPostedFileBase archivoExcel, int idUsuario)
        //{
        //    var response = new Response();

        //        var tabla = new DataTable();
        //        tabla.Columns.Add("IdSucursal", typeof(string));
        //        tabla.Columns.Add("NombreSucursal", typeof(string));
        //        tabla.Columns.Add("SKU", typeof(string));
        //        tabla.Columns.Add("OptimoAnterior", typeof(int));
        //        tabla.Columns.Add("OptimoActualizado", typeof(int));

        //        using (var package = new ExcelPackage(archivoExcel.InputStream))
        //        {
        //            var ws = package.Workbook.Worksheets[0];
        //            int filas = ws.Dimension.End.Row;

        //            for (int i = 2; i <= filas; i++) // empieza en 2 por encabezados
        //            {
        //                tabla.Rows.Add(
        //                    ws.Cells[i, 1].Text,  // IdSucursal
        //                    ws.Cells[i, 2].Text,  // NombreSucursal
        //                    ws.Cells[i, 3].Text,  // SKU
        //                    int.Parse(ws.Cells[i, 5].Text), // OptimoAnterior
        //                    int.Parse(ws.Cells[i, 6].Text)  // OptimoActualizado
        //                );
        //            }
        //        }

        //        response= reglaInvenadroData.AutorizarExcluidosMasivoData(tabla, idUsuario);

        //        return new Response
        //        {
        //            Success = true
        //        };

        //}

        #endregion

        #region CDRs

        public ResponseList<InfoCDR> GetCDRsInfoBLL(int Flag, int agencia_id, decimal MontoCDR)
        {
            var response = new ResponseList<InfoCDR>();
            try
            {
                response = reglaInvenadroData.GetCRDsInfoData(Flag,agencia_id, MontoCDR);
            }
            catch (Exception ex)
            {
                response.Message = "Error al realizar la consulta de información. " + ex.Message;
            }
            return response;


        }

        public ResponseList<InfoCDR> GetCDRsBalanceBLL(int agencia_id, decimal MontoCDR)
        {
            var response = new ResponseList<InfoCDR>();
            try
            {
                response = reglaInvenadroData.GetBalanceData(agencia_id,MontoCDR);
            }
            catch (Exception ex)
            {
                response.Message = "Error al realizar la consulta de información. " + ex.Message;
            }
            return response;


        }
        public Response ActualizaCDRsBLL(InfoCDR cedis, int usuario)
        {
            var response = new Response();
            try
            {
                response = reglaInvenadroData.ActualizaCDRsData(cedis, usuario);
                response.Message = response.Success ? "La operación se realizo con exito." : "No se pudo completar la operación";
            }
            catch (Exception ex)
            {
                response.Message = "Error al realizar la consulta de información. " + ex.Message;
            }
            return response;
        }

        public byte[] GetExportBloquesCDRBLL(int agencia)
        {
            var tabla = reglaInvenadroData.GetExportBloquesCDRData(agencia);
            var sb = new StringBuilder();

            var headers = tabla.Columns.Cast<DataColumn>().Select(c => "\"" + c.ColumnName.Replace("\"", "\"\"") + "\"");
            sb.AppendLine(string.Join(",", headers));

            foreach (DataRow row in tabla.Rows)
            {
                var values = row.ItemArray.Select(v => "\"" + (v?.ToString() ?? "").Replace("\"", "\"\"") + "\"");
                sb.AppendLine(string.Join(",", values));
            }

            return Encoding.UTF8.GetPreamble().Concat(Encoding.UTF8.GetBytes(sb.ToString())).ToArray();
        }

        #endregion

        #region Resumen
        public ResponseList<ComboGenerico> GetMotivosComboBLL()
        {
            var response = new ResponseList<ComboGenerico>();
            try
            {
                response = reglaInvenadroData.GetMotivosComboData();
            }
            catch (Exception ex)
            {
                response.Message = "Error al realizar la consulta de información. " + ex.Message;
            }
            return response;
        }
        public ResponseList<Resumen_Invenadro> GetEstatusBLL(string[] Estatus)
        {
            var response = new ResponseList<Resumen_Invenadro>();
            try
            {
                var estatusString = string.Join("|", Estatus);

                response = reglaInvenadroData.GetEstatusData(estatusString);
            }
            catch (Exception ex)
            {
                response.Message = "Error al realizar la consulta de información. " + ex.Message;
            }
            return response;
        }
        public byte[] getExcelResumenInvenadroBLL(string[] Estatus)
        {
            var response = new ResponseList<Excluidos_Invenadro_Aut>();

            var estatusString = string.Join("|", Estatus);

            byte[] xlsx;
            try
            {
                response = reglaInvenadroData.GetDetalleEstatusData(estatusString);

                using (ExcelPackage pck = new ExcelPackage())
                {
                    var ws = pck.Workbook.Worksheets.Add("Detalle Estatus Invenadro");

                    ws.Cells["A1"].Value = "Id Sucursal";
                    ws.Cells["B1"].Value = "Sucursal";
                    ws.Cells["C1"].Value = "SKU";
                    ws.Cells["D1"].Value = "Descripcion";
                    ws.Cells["E1"].Value = "Optimo Nuevo";
                    ws.Cells["F1"].Value = "Inv. Monto Nuevo";
                    ws.Cells["G1"].Value = "Relación Invenadro Venta";
                    ws.Cells["H1"].Value = "Estatus Producto";

                    int renglon = 2;

                    foreach (var item in response.Result)
                    {
                        ws.Cells[$"A{renglon}"].Value = item.IdSucursal;
                        ws.Cells[$"B{renglon}"].Value = item.NombreSucursal;
                        ws.Cells[$"C{renglon}"].Value = item.SKU;
                        ws.Cells[$"D{renglon}"].Value = item.NombreSKU;
                        ws.Cells[$"E{renglon}"].Value = item.OptimoActualizado;
                        ws.Cells[$"F{renglon}"].Value = item.InvenadroMontoNuevo;
                        ws.Cells[$"G{renglon}"].Value = item.RelacionInvenadroVenta;
                        ws.Cells[$"H{renglon}"].Value = item.EstatusProducto;

                        renglon++;
                    }

                    ws.Cells["A:L"].AutoFitColumns();
                    xlsx = pck.GetAsByteArray();
                }
            }
            catch
            {
                xlsx = new byte[0];
            }

            return xlsx;
        }

        public byte[] getExcelDetalleInvenadroBLL(string oFlag, string oEx)
        {
            var response = new ResponseList<Excluidos_Invenadro_Aut>();

            byte[] xlsx;
            try
            {
                response = reglaInvenadroData.GetDetalleTablaEstatusData(oFlag, oEx);

                using (ExcelPackage pck = new ExcelPackage())
                {
                    var ws = pck.Workbook.Worksheets.Add("Detalle Estatus Invenadro");

                    ws.Cells["A1"].Value = "Id Sucursal";
                    ws.Cells["B1"].Value = "Sucursal";
                    ws.Cells["C1"].Value = "SKU";
                    ws.Cells["D1"].Value = "Descripcion";
                    ws.Cells["E1"].Value = "Optimo Nuevo";
                    ws.Cells["F1"].Value = "Inv. Monto Nuevo";
                    ws.Cells["G1"].Value = "Relación Invenadro Venta";
                    ws.Cells["H1"].Value = "Estatus Producto";

                    int renglon = 2;

                    foreach (var item in response.Result)
                    {
                        ws.Cells[$"A{renglon}"].Value = item.IdSucursal;
                        ws.Cells[$"B{renglon}"].Value = item.NombreSucursal;
                        ws.Cells[$"C{renglon}"].Value = item.SKU;
                        ws.Cells[$"D{renglon}"].Value = item.NombreSKU;
                        ws.Cells[$"E{renglon}"].Value = item.OptimoActualizado;
                        ws.Cells[$"F{renglon}"].Value = item.InvenadroMontoNuevo;
                        ws.Cells[$"G{renglon}"].Value = item.RelacionInvenadroVenta;
                        ws.Cells[$"H{renglon}"].Value = item.EstatusProducto;

                        renglon++;
                    }

                    ws.Cells["A:L"].AutoFitColumns();
                    xlsx = pck.GetAsByteArray();
                }
            }
            catch
            {
                xlsx = new byte[0];
            }

            return xlsx;
        }

        public Response AutorizaInvenadro(HttpFileCollectionBase httpFile, string oEx,int user)
        {
            var response = new Response();
            if (httpFile.AllKeys.Any())
            {
                var archivo = httpFile["ExcelAutorizar"];
                if (archivo != null && archivo.ContentLength > 0)
                {
                    try
                    {
                        var nombreArchivo = Path.GetFileName(archivo.FileName);
                        string fileContentType = archivo.ContentType;
                        byte[] fileBytes = new byte[archivo.ContentLength];
                        var data = archivo.InputStream.Read(fileBytes, 0, Convert.ToInt32(archivo.ContentLength));

                        var package = new ExcelPackage(archivo.InputStream);

                        var hojaActual = package.Workbook.Worksheets["Invenadro"];

                        if (hojaActual == null)
                            throw new Exception("EL nombre de la hoja debe ser Invenadro");

                        if (hojaActual.Name != "Invenadro")
                            throw new Exception("EL nombre de la hoja debe ser Invenadro");

                        var hoja = hojaActual;
                        int noCol = hoja.Dimension.End.Column;
                        int noRenglon = hoja.Dimension.End.Row;
                        int i = 0;

                        List<Relacion_Invenadro_Aut> listaEnExcel = new List<Relacion_Invenadro_Aut>();
                        int linea = 0;

                        for (i = 2; i <= noRenglon; i++)
                        {
                            linea = i;
                            Relacion_Invenadro_Aut cvp = new Relacion_Invenadro_Aut();
                            cvp.IdSucursal = hoja.Cells[i, 1].Value.ToString();
                            cvp.SKU = hoja.Cells[i, 2].Value.ToString();
                            cvp.Motivo = hoja.Cells[i, 3].Value.ToString();
                            cvp.Optimo = int.Parse(hoja.Cells[i, 4].Value.ToString());
                            cvp.oEx = oEx;
                            cvp.Usuario = user;

                            listaEnExcel.Add(cvp);
                        }

                        var dt = AutInvenadroType.Definicion();

                        foreach (var item in listaEnExcel)
                        {
                            DataRow renglon = dt.NewRow();
                            renglon[0] = item.IdSucursal;
                            renglon[1] = item.SKU;
                            renglon[2] = item.Motivo;
                            renglon[3] = item.Optimo;
                            renglon[4] = item.oEx;
                            renglon[5] = item.Usuario;
                            dt.Rows.Add(renglon);
                        }

                        response = reglaInvenadroData.AutorizaInvenadroData(dt);
                        response.Message = response.Success ? "La operación se realizo con exito." : response.Message;
                    }
                    catch (Exception ex)
                    {
                        var mensaje = ex.Message;

                        if (ex.InnerException != null)
                        {
                            mensaje = mensaje + "   " + ex.InnerException.Message;
                        }
                        mensaje = mensaje + "    " + ex.StackTrace;

                        response.Success = false;
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


        #endregion

    }
}
