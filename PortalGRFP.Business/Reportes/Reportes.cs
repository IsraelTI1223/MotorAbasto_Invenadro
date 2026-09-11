using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PortalGRFP.Data.ConsultaReportes;
using PortalGRFP.Entities.Common;
using OfficeOpenXml;
using OfficeOpenXml.Drawing;
using System.IO;
using PortalGRFP.Entities.Response;

namespace PortalGRFP.Business.Reportes
{
    public class Reportes
    {
        private readonly ReportesData reportes = new ReportesData();


        public List<ReporteCatProductosModel> GetProductosNuevos()
        {
            var listProductosNuevos = reportes.GetProductosNuevos().ToList();

            return listProductosNuevos;
        }
        public List<ReporteSucursalesNuevasModel> GetSucursalesNuevas()
        {
            var listSucursales = reportes.GetSucursalesNuevas();

            return listSucursales;
        }
        public List<ReporteCatProductosModel> GetProductsReport()
        {
            var listProducts = reportes.GetAllProducts().ToList();

            return listProducts;
        }
        public List<ReporteInvenadroModel> GetInvenadroBLL()
        {
            var listInv = reportes.GetInvenadro().ToList();

            return listInv;
        }

        public List<ConsultaArticuloModel> GetConsultaArticulos()
        {
            var listConsultaArticulos = reportes.GetConsultaArticulo().ToList();

            return listConsultaArticulos;
        }
        //public List<ConsultaProductosModel> GetConsultaProductos()
        //{
        //    var listConsultaArticulos = reportes.GetConsultaArticulo().ToList();

        //    return listConsultaArticulos;
        //}

        public ResponseList<ConsultaProductosModel> GetProductosListBLL(string search, int user)
        {
            var response = new ResponseList<ConsultaProductosModel>();
            try
            {
                response = reportes.GetProductosListData(search, user);
            }
            catch (Exception ex)
            {
                response.Message = "Error al realizar la consulta de Productos. " + ex.Message;
            }
            return response;
        }



        public ResponseList<ConsultaArticuloSucursalModel> GetConsultaArticulosSucursal()
        {
            var response = new ResponseList<ConsultaArticuloSucursalModel>();
            try
            {
                response = reportes.ResumenCargaArticulosSucursal();
            }
            catch (Exception ex)
            {
                response.Message = "Error al realizar la consulta de información. " + ex.Message;
            }
            return response;
        }


        public ResponseList<ConsultaSucursales> GetConsultaSucursales(int Cadena)//int Cadena
        {
            var response = new ResponseList<ConsultaSucursales>();
            try
            {
                response = reportes.ResumenCargaSucursales(Cadena);
            }
            catch (Exception ex)
            {
                response.Message = "Error al realizar la consulta de información. " + ex.Message;
            }
            return response;
        }

        public ResponseList<AutocompleteString> AutocompletarArticulo(string letras)
        {
            var response = new ResponseList<AutocompleteString>();
            try
            {
                response = reportes.Autocompletar(letras);
            }
            catch (Exception ex)
            {
                response.Message = "Error al realizar la consulta de información. " + ex.Message;
            }
            return response;
        }

        public ResponseList<ComboSucursalesProveedor> GetConsultaSucursalesProveedor(int Sucursal)//int Cadena
        {
            var response = new ResponseList<ComboSucursalesProveedor>();
            try
            {
                response = reportes.ResumenCargaSucursalesProveedores(Sucursal);
            }
            catch (Exception ex)
            {
                response.Message = "Error al realizar la consulta de información. " + ex.Message;
            }
            return response;
        }

        public ResponseList<EstadisticaBean> ReporteEstadisticaCompras(string articulo, int grupo)
        {
            var response = new ResponseList<EstadisticaBean>();
            try
            {
                EstadisticaBean rpt = new EstadisticaBean(); 

                var comp = reportes.ReporteEstadistica(articulo, grupo);
                rpt.Compras = comp.Result;

                var proveedor = reportes.ReporteEstadisticaProveedor(articulo, grupo);
                rpt.Proveedor = proveedor.Result;

                //var ventas28 = reportes.ReporteEstadisticaVentas28Dias(articulo, grupo);
                //rpt.Ventas28 = ventas28.Result;

                List<EstadisticaBean> beans = new List<EstadisticaBean>();
                beans.Add(rpt);
                response.Result = beans;
                if (response.Result.Count == 0)
                {
                    response.Message = "No hay Registros";
                }
                response.Success = response.Result.Any();
            }
            catch (Exception ex)
            {
                response.Message = "Error al realizar la consulta de información. " + ex.Message;
            }
            return response;
        }

        #region Generación de data para el reporte

        public byte[] getExcelProductosNuevos()
        {
            var response = new ResponseList<ReporteCatProductosModel>();
            ExcelPackage pck = new ExcelPackage();
            var ws = pck.Workbook.Worksheets.Add("Productos Nuevos");
            byte[] xlsx = pck.GetAsByteArray();

            try
            {
                ReportesData reportes = new ReportesData();
                response = reportes.ExportExcelProductosNuevos();

                pck = new ExcelPackage();

                ws = pck.Workbook.Worksheets.Add("Productos Nuevos");

                ws.Cells["A1"].Value = "SKU";
                ws.Cells["B1"].Value = "Descripción";
                ws.Cells["C1"].Value = "Grupo";
                ws.Cells["D1"].Value = "Familia";
                ws.Cells["E1"].Value = "Categoria";
                ws.Cells["F1"].Value = "Clasificacion";
                ws.Cells["G1"].Value = "Fecha Sincronización";


                int renglon = 2;
                foreach (var item in response.Result)
                {
                    ws.Cells[string.Format("A{0}", renglon)].Value = item.Sku;
                    ws.Cells[string.Format("B{0}", renglon)].Value = item.Descripcion;
                    ws.Cells[string.Format("C{0}", renglon)].Value = item.Grupo;
                    ws.Cells[string.Format("D{0}", renglon)].Value = item.Familia;
                    ws.Cells[string.Format("E{0}", renglon)].Value = item.Categoria;
                    ws.Cells[string.Format("F{0}", renglon)].Value = item.Clasificacion;
                    ws.Cells[string.Format("G{0}", renglon)].Value = item.Fecha;

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

        public byte[] getExcelCatProduct()
        {
            var response = new ResponseList<ReporteCatProductosModel>();
            ExcelPackage pck = new ExcelPackage();
            var ws = pck.Workbook.Worksheets.Add("Catalogo de Productos");
            byte[] xlsx = pck.GetAsByteArray();

            try
            {
                ReportesData reportes = new ReportesData();
                response = reportes.ExportExcelAllProducts();

                pck = new ExcelPackage();

                ws = pck.Workbook.Worksheets.Add("Catalogo de Productos");

                ws.Cells["A1"].Value = "SKU";
                ws.Cells["B1"].Value = "Descripción";
                ws.Cells["C1"].Value = "Grupo";
                ws.Cells["D1"].Value = "Familia";
                ws.Cells["E1"].Value = "Categoria";
                ws.Cells["F1"].Value = "Clasificacion";
                ws.Cells["G1"].Value = "Fecha Sincronización";


                int renglon = 2;
                foreach (var item in response.Result)
                {
                    ws.Cells[string.Format("A{0}", renglon)].Value = item.Sku;
                    ws.Cells[string.Format("B{0}", renglon)].Value = item.Descripcion;
                    ws.Cells[string.Format("C{0}", renglon)].Value = item.Grupo;
                    ws.Cells[string.Format("D{0}", renglon)].Value = item.Familia;
                    ws.Cells[string.Format("E{0}", renglon)].Value = item.Categoria;
                    ws.Cells[string.Format("F{0}", renglon)].Value = item.Clasificacion;
                    ws.Cells[string.Format("G{0}", renglon)].Value = item.Fecha;

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

        public byte[] GetExcelSucursalesNuevas()
        {
            var response = new ResponseList<ReporteSucursalesNuevasModel>();
            ExcelPackage pck = new ExcelPackage();
            var ws = pck.Workbook.Worksheets.Add("SucursalesNuevas");
            byte[] xlsx = pck.GetAsByteArray();

            try
            {
                ReportesData reportes = new ReportesData();
                response = reportes.ExportExcelSucursalesNuevas();

                pck = new ExcelPackage();

                ws = pck.Workbook.Worksheets.Add("SucursalesNuevas");

                ws.Cells["A1"].Value = "Cadena";
                ws.Cells["B1"].Value = "Subcadena";
                ws.Cells["C1"].Value = "Id";
                ws.Cells["D1"].Value = "Nombre";
                ws.Cells["E1"].Value = "Regiones";
                ws.Cells["F1"].Value = "Fecha";


                int renglon = 2;
                foreach (var item in response.Result)
                {
                    ws.Cells[string.Format("A{0}", renglon)].Value = item.Cadena;
                    ws.Cells[string.Format("B{0}", renglon)].Value = item.Subcadena;
                    ws.Cells[string.Format("C{0}", renglon)].Value = item.Id;
                    ws.Cells[string.Format("D{0}", renglon)].Value = item.Nombre;
                    ws.Cells[string.Format("E{0}", renglon)].Value = item.Regiones;
                    ws.Cells[string.Format("F{0}", renglon)].Value = item.Fecha;

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

        public byte[] GetExcelInvenadro()
        {
            var response = new ResponseList<ReporteInvenadroModel>();
            ExcelPackage pck = new ExcelPackage();
            var ws = pck.Workbook.Worksheets.Add("Invenadro");
            byte[] xlsx = pck.GetAsByteArray();

            try
            {
                ReportesData reportes = new ReportesData();
                response = reportes.ExportExcelInvenadro();

                pck = new ExcelPackage();

                ws = pck.Workbook.Worksheets.Add("Invenadro");

                ws.Cells["A1"].Value = "Marca";
                ws.Cells["B1"].Value = "Id Sucursal";
                ws.Cells["C1"].Value = "Nombre Sucursal";
                ws.Cells["D1"].Value = "Id Articulo";
                ws.Cells["E1"].Value = "Descripcion Articulo";
                ws.Cells["F1"].Value = "Optimo";
                ws.Cells["G1"].Value = "Fecha de Sincronizacion";



                int renglon = 2;
                foreach (var item in response.Result)
                {
                    ws.Cells[string.Format("A{0}", renglon)].Value = item.Marca;
                    ws.Cells[string.Format("B{0}", renglon)].Value = item.Suc_id;
                    ws.Cells[string.Format("C{0}", renglon)].Value = item.Nombre_Corto;
                    ws.Cells[string.Format("D{0}", renglon)].Value = item.Articulo_id;
                    ws.Cells[string.Format("E{0}", renglon)].Value = item.DescripcionCorta;
                    ws.Cells[string.Format("F{0}", renglon)].Value = item.Optimo;
                    ws.Cells[string.Format("G{0}", renglon)].Value = item.Fecha_Consulta;
                    ;

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
        #endregion

        #region GetExcelInvenadroAut
        public byte[] GetExcelInvenadroAut(string sucursales, string sku)
        {
            var response = new ResponseList<ReporteInvenadroAutModel>();
            ExcelPackage pck = new ExcelPackage();
            var ws = pck.Workbook.Worksheets.Add("InvenadroAut");
            byte[] xlsx = pck.GetAsByteArray();

            try
            {
                ReportesData reportes = new ReportesData();
                response = reportes.GetReporteInvenadroAutData(sucursales, sku);

                pck = new ExcelPackage();
                ws = pck.Workbook.Worksheets.Add("InvenadroAut");

                ws.Cells["A1"].Value = "Id Sucursal";
                ws.Cells["B1"].Value = "Sucursal";
                ws.Cells["C1"].Value = "SKU";
                ws.Cells["D1"].Value = "Descripcion";
                ws.Cells["E1"].Value = "PVD";
                ws.Cells["F1"].Value = "Inventario Objetivo";
                ws.Cells["G1"].Value = "Monto Objetivo";
                ws.Cells["H1"].Value = "Optimo Anterior";
                ws.Cells["I1"].Value = "Inv Monto Anterior";
                ws.Cells["J1"].Value = "Optimo Actualizado";
                ws.Cells["K1"].Value = "Inv Monto Nuevo";
                ws.Cells["L1"].Value = "Relacion Invenadro Venta";
                ws.Cells["M1"].Value = "Estatus Producto";
                ws.Cells["N1"].Value = "% Dif Piezas";
                ws.Cells["O1"].Value = "% Dif Monto";

                int renglon = 2;
                foreach (var item in response.Result)
                {
                    ws.Cells[string.Format("A{0}", renglon)].Value = item.IdSucursal;
                    ws.Cells[string.Format("B{0}", renglon)].Value = item.Sucursal;
                    ws.Cells[string.Format("C{0}", renglon)].Value = item.SKU;
                    ws.Cells[string.Format("D{0}", renglon)].Value = item.Descripcion;
                    ws.Cells[string.Format("E{0}", renglon)].Value = item.PVD;
                    ws.Cells[string.Format("F{0}", renglon)].Value = item.InventarioObjetivo;
                    ws.Cells[string.Format("G{0}", renglon)].Value = item.MontoObjetivo;
                    ws.Cells[string.Format("H{0}", renglon)].Value = item.OptimoAnterior;
                    ws.Cells[string.Format("I{0}", renglon)].Value = item.InvMontoAnterior;
                    ws.Cells[string.Format("J{0}", renglon)].Value = item.OptimoActualizado;
                    ws.Cells[string.Format("K{0}", renglon)].Value = item.InvMontoNuevo;
                    ws.Cells[string.Format("L{0}", renglon)].Value = item.RelacionInvenadroVenta;
                    ws.Cells[string.Format("M{0}", renglon)].Value = item.EstatusProducto;
                    ws.Cells[string.Format("N{0}", renglon)].Value = item.PorcDifPiezas;
                    ws.Cells[string.Format("O{0}", renglon)].Value = item.PorcDifMonto;
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
        #endregion


        public byte[] GetExcelConsultaArticulos()
        {
            var response = new ResponseList<ConsultaArticuloModel>();
            ExcelPackage pck = new ExcelPackage();
            var ws = pck.Workbook.Worksheets.Add("Articulos");
            byte[] xlsx = pck.GetAsByteArray();

            try
            {
                ReportesData reportes = new ReportesData();
                response = reportes.ExportExcelArticulos();

                pck = new ExcelPackage();

                ws = pck.Workbook.Worksheets.Add("Articulos");

                ws.Cells["A1"].Value = "SKU";
                ws.Cells["B1"].Value = "DESCRIPCION";
                ws.Cells["C1"].Value = "GRUPO";
                ws.Cells["D1"].Value = "FAMILIA";
                ws.Cells["E1"].Value = "TIPO_PROVEEDOR";
                ws.Cells["F1"].Value = "ESTATUS";
                ws.Cells["G1"].Value = "EMPAQUE";
                ws.Cells["H1"].Value = "CONTRLOLADO";
                ws.Cells["I1"].Value = "REFRIGERADO";
                ws.Cells["J1"].Value = "ALTAESPECIALIDAD";
                ws.Cells["K1"].Value = "PERMISODELVOLVER";
                ws.Cells["L1"].Value = "INVENADRO";




                int renglon = 2;
                foreach (var item in response.Result)
                {
                    ws.Cells[string.Format("A{0}", renglon)].Value = item.SKU;
                    ws.Cells[string.Format("B{0}", renglon)].Value = item.DESCRIPCION;
                    ws.Cells[string.Format("C{0}", renglon)].Value = item.Grupo;
                    ws.Cells[string.Format("D{0}", renglon)].Value = item.Familia;
                    ws.Cells[string.Format("E{0}", renglon)].Value = item.Tipo_Proveedor;
                    ws.Cells[string.Format("F{0}", renglon)].Value = item.Estatus;
                    ws.Cells[string.Format("G{0}", renglon)].Value = item.Empaque;
                    ws.Cells[string.Format("H{0}", renglon)].Value = item.Controlado;
                    ws.Cells[string.Format("I{0}", renglon)].Value = item.Refrigerado;
                    ws.Cells[string.Format("J{0}", renglon)].Value = item.AltaEspecialidad;
                    ws.Cells[string.Format("K{0}", renglon)].Value = item.PermisoDevolver;
                    ws.Cells[string.Format("L{0}", renglon)].Value = item.Invenadro;
                    ;

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



        public byte[] GetExcelConsultaArticulosSucursal()
        {
            var response = new ResponseList<ConsultaArticuloSucursalModel>();
            ExcelPackage pck = new ExcelPackage();
            var ws = pck.Workbook.Worksheets.Add("ArticulosSucursal");
            byte[] xlsx = pck.GetAsByteArray();

            try
            {
                ReportesData reportes = new ReportesData();
                response = reportes.ExportExcelArticulosSucursal();

                pck = new ExcelPackage();

                ws = pck.Workbook.Worksheets.Add("ArticulosSucursal");

                ws.Cells["A1"].Value = "IDSUC";
                ws.Cells["B1"].Value = "SKU";
                ws.Cells["C1"].Value = "DESCRIPCION";
                ws.Cells["D1"].Value = "GRUPO";
                ws.Cells["E1"].Value = "FAMILIA";
                ws.Cells["F1"].Value = "EXISTENCIA";
                ws.Cells["G1"].Value = "PVD";
                ws.Cells["H1"].Value = "FECHAVENTA";
                ws.Cells["I1"].Value = "VENTA30DIAS";




                int renglon = 2;
                foreach (var item in response.Result)
                {
                    ws.Cells[string.Format("A{0}", renglon)].Value = item.ID_Sucursal;
                    ws.Cells[string.Format("B{0}", renglon)].Value = item.SKU;
                    ws.Cells[string.Format("C{0}", renglon)].Value = item.DESCRIPCION_CORTA;
                    ws.Cells[string.Format("D{0}", renglon)].Value = item.GRUPO;
                    ws.Cells[string.Format("E{0}", renglon)].Value = item.FAMILIA;
                    ws.Cells[string.Format("F{0}", renglon)].Value = item.Existencia;
                    ws.Cells[string.Format("G{0}", renglon)].Value = item.PVD;
                    ws.Cells[string.Format("H{0}", renglon)].Value = item.Fecha_Venta;
                    ws.Cells[string.Format("I{0}", renglon)].Value = item.piezas_total;
                    ;

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




        public byte[] GetExcelConsultaSucursales()
        {
            var response = new ResponseList<ConsultaSucursales>();
            ExcelPackage pck = new ExcelPackage();
            var ws = pck.Workbook.Worksheets.Add("Sucursal");
            byte[] xlsx = pck.GetAsByteArray();

            try
            {
                ReportesData reportes = new ReportesData();
                response = reportes.ExportExcelSucursales();

                pck = new ExcelPackage();

                ws = pck.Workbook.Worksheets.Add("Sucursal");

                ws.Cells["A1"].Value = "CADENA";
                ws.Cells["B1"].Value = "MARCA";
                ws.Cells["C1"].Value = "ID SUCURSAL";
                ws.Cells["D1"].Value = "NOMBRE SUCURSAL";
                ws.Cells["E1"].Value = "LICENCIA";
                ws.Cells["F1"].Value = "SDOM";
                ws.Cells["G1"].Value = "24HORAS";
                ws.Cells["H1"].Value = "INVENADRO";
                ws.Cells["I1"].Value = "ESTATUS";
                ws.Cells["J1"].Value = "CIUDAD";
                ws.Cells["K1"].Value = "ESTADO";




                int renglon = 2;
                foreach (var item in response.Result)
                {
                    ws.Cells[string.Format("A{0}", renglon)].Value = item.Cadena;
                    ws.Cells[string.Format("B{0}", renglon)].Value = item.Marca;
                    ws.Cells[string.Format("C{0}", renglon)].Value = item.Suc_Id;
                    ws.Cells[string.Format("D{0}", renglon)].Value = item.Suc_Nombre;
                    ws.Cells[string.Format("E{0}", renglon)].Value = item.Licencia;
                    ws.Cells[string.Format("F{0}", renglon)].Value = item.Sdom;
                    ws.Cells[string.Format("G{0}", renglon)].Value = item.hrs;
                    ws.Cells[string.Format("H{0}", renglon)].Value = item.INVENADRO;
                    ws.Cells[string.Format("I{0}", renglon)].Value = item.ESTATUS;
                    ws.Cells[string.Format("J{0}", renglon)].Value = item.CIUDAD;
                    ws.Cells[string.Format("K{0}", renglon)].Value = item.ESTADO;
                    ;

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

        /*public byte[] GetExcelConsultaSucursales()
        {
            var response = new ResponseList<ConsultaSucursales>();
            ExcelPackage pck = new ExcelPackage();
            var ws = pck.Workbook.Worksheets.Add("Sucursal");
            byte[] xlsx = pck.GetAsByteArray();

            try
            {
                ReportesData reportes = new ReportesData();
                response = reportes.ExportExcelSucursales();

                pck = new ExcelPackage();

                ws = pck.Workbook.Worksheets.Add("Sucursal");

                ws.Cells["A1"].Value = "CADENA";
                ws.Cells["B1"].Value = "MARCA";
                ws.Cells["C1"].Value = "ID SUCURSAL";
                ws.Cells["D1"].Value = "NOMBRE SUCURSAL";
                ws.Cells["E1"].Value = "LICENCIA";
                ws.Cells["F1"].Value = "SDOM";
                ws.Cells["G1"].Value = "24HORAS";
                ws.Cells["H1"].Value = "INVENADRO";
                ws.Cells["I1"].Value = "ESTATUS";
                ws.Cells["J1"].Value = "CIUDAD";
                ws.Cells["K1"].Value = "ESTADO";




                int renglon = 2;
                foreach (var item in response.Result)
                {
                    ws.Cells[string.Format("A{0}", renglon)].Value = item.Cadena;
                    ws.Cells[string.Format("B{0}", renglon)].Value = item.Marca;
                    ws.Cells[string.Format("C{0}", renglon)].Value = item.Suc_Id;
                    ws.Cells[string.Format("D{0}", renglon)].Value = item.Suc_Nombre;
                    ws.Cells[string.Format("E{0}", renglon)].Value = item.Licencia;
                    ws.Cells[string.Format("F{0}", renglon)].Value = item.Sdom;
                    ws.Cells[string.Format("G{0}", renglon)].Value = item.hrs;
                    ws.Cells[string.Format("H{0}", renglon)].Value = item.INVENADRO;
                    ws.Cells[string.Format("I{0}", renglon)].Value = item.ESTATUS;
                    ws.Cells[string.Format("J{0}", renglon)].Value = item.CIUDAD;
                    ws.Cells[string.Format("K{0}", renglon)].Value = item.ESTADO;
                    ;

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


        }*/



        public byte[] GetExcelConsultaSucursalesProveedor()
        {
            var response = new ResponseList<ComboSucursalesProveedor>();
            ExcelPackage pck = new ExcelPackage();
            var ws = pck.Workbook.Worksheets.Add("SucursalProveedor");
            byte[] xlsx = pck.GetAsByteArray();

            try
            {
                ReportesData reportes = new ReportesData();
                response = reportes.ExportExcelSucursalesProveedores();

                pck = new ExcelPackage();

                ws = pck.Workbook.Worksheets.Add("SucursalProveedor");

                ws.Cells["A1"].Value = "NOMBRE MARCA";
                ws.Cells["B1"].Value = "CODIGO SUCURSAL";
                ws.Cells["C1"].Value = "SUCURSAL NOMBRE";
                ws.Cells["D1"].Value = "PROVEEDOR";
                ws.Cells["E1"].Value = "AGENCIA";
                ws.Cells["F1"].Value = "LUNES";
                ws.Cells["G1"].Value = "MARTES";
                ws.Cells["H1"].Value = "MIERCOLES";
                ws.Cells["I1"].Value = "JUEVES";
                ws.Cells["J1"].Value = "VIERNES";
                ws.Cells["K1"].Value = "SABADO";
                ws.Cells["L1"].Value = "DOMINGO";




                int renglon = 2;
                foreach (var item in response.Result)
                {
                    ws.Cells[string.Format("A{0}", renglon)].Value = item.Nombre_Marca;
                    ws.Cells[string.Format("B{0}", renglon)].Value = item.CodigoSucursal;
                    ws.Cells[string.Format("C{0}", renglon)].Value = item.Suc_Nombre;
                    ws.Cells[string.Format("D{0}", renglon)].Value = item.Proveedor;
                    ws.Cells[string.Format("E{0}", renglon)].Value = item.Agencia;
                    ws.Cells[string.Format("F{0}", renglon)].Value = item.lunes;
                    ws.Cells[string.Format("G{0}", renglon)].Value = item.martes;
                    ws.Cells[string.Format("H{0}", renglon)].Value = item.miercoles;
                    ws.Cells[string.Format("I{0}", renglon)].Value = item.jueves;
                    ws.Cells[string.Format("J{0}", renglon)].Value = item.viernes;
                    ws.Cells[string.Format("K{0}", renglon)].Value = item.sabado;
                    ws.Cells[string.Format("L{0}", renglon)].Value = item.domingo;
                    ;

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
        public ResponseList<ReporteArticulosProveedorModel> GetProveedorAgenciaProdBLL(int idProveedor, int idagencia)
        {
            var response = new ResponseList<ReporteArticulosProveedorModel>();
            try
            {
                response = reportes.GetProveedorAgenciaProdData(idProveedor, idagencia);
            }
            catch (Exception ex)
            {
                response.Message = "Error al realizar la consulta de información. " + ex.Message;
            }
            return response;
        }

        public ResponseList<ComboGenerico> GetListProvAgenciaBLL(int flag, int search)
        {
            var response = new ResponseList<ComboGenerico>();
            try
            {
                response = reportes.GetListProvAgenciaData(flag, search);
            }
            catch (Exception ex)
            {
                response.Message = "Error al realizar la consulta de información. " + ex.Message;
            }
            return response;
        }

        public ResponseList<ComboGenerico> GetFiltrosArticuloSucursalBLL(int user, int flag, string search)
        {
            var response = new ResponseList<ComboGenerico>();
            try
            {
                response = reportes.GetFiltrosArticuloSucursalData(user, flag, search);
            }
            catch (Exception ex)
            {
                response.Message = "Error al realizar la consulta de información. " + ex.Message;
            }
            return response;
        }

        public ResponseList<ReporteArticulosSucursalModel> GetProductosSucursalListBLL(int user, string storeId, string estatusProd)
        {
            var response = new ResponseList<ReporteArticulosSucursalModel>();
            try
            {
                response = reportes.GetProductosSucursalListData(user, storeId, estatusProd);
            }
            catch (Exception ex)
            {
                response.Message = "Error al realizar la consulta de Productos. " + ex.Message;
            }
            return response;
        }

        /// <summary>
        /// Obtiene el reporte de Invenadro Automatizado desde la capa de datos
        /// </summary>
        /// <param name="sucursales">Lista de IDs de sucursales separadas por coma</param>
        /// <param name="sku">SKU del artículo a buscar (opcional)</param>
        /// <returns>Lista de registros del reporte de Invenadro Automatizado con información de éxito y mensajes</returns>
        public ResponseList<ReporteInvenadroAutModel> GetReporteInvenadroAutBLL(string sucursales, string sku)
        {
            var response = new ResponseList<ReporteInvenadroAutModel>();
            try
            {
                response = reportes.GetReporteInvenadroAutData(sucursales, sku);

                // Validación adicional de reglas de negocio si es necesario
                if (response.Success && response.Result != null && response.Result.Any())
                {
                    response.Message = $"Reporte generado exitosamente con {response.Result.Count} registros.";
                }
                else if (!response.Success)
                {
                    response.Message = response.Message ?? "No se pudo generar el reporte de Invenadro Automatizado.";
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"Error en la capa de negocio al consultar reporte de Invenadro Automatizado: {ex.Message}";
            }
            return response;
        }
    }
}
