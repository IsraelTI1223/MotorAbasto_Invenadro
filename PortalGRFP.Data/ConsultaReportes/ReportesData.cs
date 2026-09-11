using Microsoft.Practices.EnterpriseLibrary.Data;
using PortalGRFP.Data.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PortalGRFP.Entities.Common;
using System.Data;
using System.Data.SqlClient;
using PortalGRFP.Entities.Response;

namespace PortalGRFP.Data.ConsultaReportes
{
    public class ReportesData
    {

        public List<ReporteCatProductosModel> GetProductosNuevos()
        {
            var response = new List<ReporteCatProductosModel>();

            var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");

            var command = db.GetStoredProcCommand("mta.SP_GRFP_GET_PROD_N");

            using (IDataReader dr = db.ExecuteReader(command))
            {


                while (dr.Read())
                {
                    response.Add(new ReporteCatProductosModel
                    {
                        Sku = dr.Get<string>("Sku"),
                        Descripcion = dr.Get<string>("Descripcion"),
                        Grupo = dr.Get<string>("Grupo"),
                        Familia = dr.Get<string>("Familia"),
                        Categoria = dr.Get<string>("Categoria"),
                        Clasificacion = dr.Get<string>("Clasificacion"),
                        Fecha = dr.Get<string>("fecha")
                    });
                }

            }
            return response;
        }
        public List<ReporteSucursalesNuevasModel> GetSucursalesNuevas()
        {
            var response = new List<ReporteSucursalesNuevasModel>();

            var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");

            var command = db.GetStoredProcCommand("mta.SP_GRFP_GET_SUC_N");

            using (IDataReader dr = db.ExecuteReader(command))
            {


                while (dr.Read())
                {
                    response.Add(new ReporteSucursalesNuevasModel
                    {
                        Cadena = dr.Get<string>("CADENA"),
                        Subcadena = dr.Get<string>("SUBCADENA"),
                        Id = dr.Get<string>("ID"),
                        Nombre = dr.Get<string>("Nombre"),
                        Regiones = dr.Get<string>("REGIONES"),
                        Fecha = dr.Get<string>("Fecha")
                    });
                }

            }
            return response;
        }

        public List<ReporteCatProductosModel> GetAllProducts()
        {
            var response = new List<ReporteCatProductosModel>();

            var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");

            var command = db.GetStoredProcCommand("mta.SP_GRFP_GET_CAT_PROD");

            using (IDataReader dr = db.ExecuteReader(command))
            {


                while (dr.Read())
                {
                    response.Add(new ReporteCatProductosModel
                    {
                        Sku = dr.Get<string>("Sku"),
                        Descripcion = dr.Get<string>("Descripcion"),
                        Grupo = dr.Get<string>("Grupo"),
                        Familia = dr.Get<string>("Familia"),
                        Categoria = dr.Get<string>("Categoria"),
                        Clasificacion = dr.Get<string>("Clasificacion"),
                        Fecha = dr.Get<string>("fecha")
                    });
                }

            }
            return response;
        }

        public List<ReporteInvenadroModel> GetInvenadro()
        {
            var response = new List<ReporteInvenadroModel>();

            var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");

            var command = db.GetStoredProcCommand("mta.SP_GRFP_GET_INVENADRO");

            using (IDataReader dr = db.ExecuteReader(command))
            {


                while (dr.Read())
                {
                    response.Add(new ReporteInvenadroModel
                    {
                        Fecha_Consulta = dr.Get<string>("Fecha_Consulta"),
                        Marca = dr.Get<string>("Marca"),
                        Suc_id = dr.Get<int>("Suc_id"),
                        Nombre_Corto = dr.Get<string>("Nombre_Corto"),
                        Articulo_id = dr.Get<string>("Articulo_id"),
                        DescripcionCorta = dr.Get<string>("DescripcionCorta"),
                        Optimo = dr.Get<int>("Optimo"),

                    });
                }

            }
            return response;
        }


        public List<ConsultaArticuloModel> GetConsultaArticulo()
        {
            var response = new List<ConsultaArticuloModel>();

            var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");

            var command = db.GetStoredProcCommand("mta.SP_GRFP_GET_CONSULTA_ARTICULO");

            using (IDataReader dr = db.ExecuteReader(command))
            {


                while (dr.Read())
                {
                    response.Add(new ConsultaArticuloModel
                    {
                        SKU = dr.Get<string>("SKU"),
                        DESCRIPCION = dr.Get<string>("DESCRIPCION"),
                        Grupo = dr.Get<string>("Grupo"),
                        Familia = dr.Get<string>("Familia"),
                        Tipo_Proveedor = dr.Get<string>("Tipo_Proveedor"),
                        Estatus = dr.Get<string>("Estatus"),
                        Empaque = dr.Get<string>("Empaque"),
                        Controlado = dr.Get<string>("Controlado"),
                        Refrigerado = dr.Get<string>("Refrigerado"),
                        AltaEspecialidad = dr.Get<string>("AltaEspecialidad"),
                        PermisoDevolver = dr.Get<string>("PermisoDevolver"),
                        Invenadro = dr.Get<string>("Invenadro")

                    });
                }

            }
            return response;
        }

        //public List<ConsultaProductosModel> GetConsultaProductos()
        //{
        //    var response = new List<ConsultaProductosModel>();

        //    var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");

        //    var command = db.GetStoredProcCommand("mta.SP_GRFP_GET_CONSULTA_ARTICULO");

        //    using (IDataReader dr = db.ExecuteReader(command))
        //    {


        //        while (dr.Read())
        //        {
        //            response.Add(new ConsultaProductosModel
        //            {
        //                SKU = dr.Get<string>("SKU"),
        //                //SKU = dr.Get<string>("SKU"),
        //                //SKU = dr.Get<string>("SKU"),
        //                //SKU = dr.Get<string>("SKU"),
        //                //SKU = dr.Get<string>("SKU"),
        //                //SKU = dr.Get<string>("SKU"),
        //                //SKU = dr.Get<string>("SKU"),
        //                //SKU = dr.Get<string>("SKU"),
        //                //SKU = dr.Get<string>("SKU"),
        //                //SKU = dr.Get<string>("SKU"),
        //                //SKU = dr.Get<string>("SKU"),
        //                //SKU = dr.Get<string>("SKU"),
        //                //SKU = dr.Get<string>("SKU"),
        //                //SKU = dr.Get<string>("SKU"),
        //                //SKU = dr.Get<string>("SKU"),



        //            });
        //        }

        //    }
        //    return response;
        //}




        public ResponseList<ConsultaArticuloSucursalModel> ResumenCargaArticulosSucursal()//int idCarga
        {
            var response = new ResponseList<ConsultaArticuloSucursalModel>();

            var db = DatabaseFactory.CreateDatabase("DBPORTALRCB");

            var command = db.GetStoredProcCommand("mta.SP_GRFP_GET_CONSULTA_ARTICULO_SUCURSAL");

            //db.AddInParameter(command, "@IdCarga", DbType.Int32, idCarga);

            var read = db.ExecuteReader(command);
            response.Result = read.Reader(x => x.ToResumenCargaArticulosSucursal());
            response.Success = response.Result.Any();

            return response;
        }


        public ResponseList<Entities.Common.ConsultaSucursales> ResumenCargaSucursales(int Cadena)//int Cadena
        {


            var response = new ResponseList<Entities.Common.ConsultaSucursales>();

            var db = DatabaseFactory.CreateDatabase("DBPORTALRCB");

            var command = db.GetStoredProcCommand("mta.SP_GRFP_CONF_SELECT__SUCURSAL");

            db.AddInParameter(command, "@Cadena", DbType.Int32, Cadena);

            var read = db.ExecuteReader(command);
            response.Result = read.Reader(x => x.ToResumenCargaSucursales());
            response.Success = response.Result.Any();

            return response;
        }

        public ResponseList<ComboSucursalesProveedor> ResumenCargaSucursalesProveedores(int Sucursal)//int Cadena
        {


            var response = new ResponseList<ComboSucursalesProveedor>();

            var db = DatabaseFactory.CreateDatabase("DBPORTALRCB");

            var command = db.GetStoredProcCommand("mta.sp_Reporte_Suc_Proveedor_GRFP");

            db.AddInParameter(command, "@Sucursal", DbType.Int32, Sucursal);

            var read = db.ExecuteReader(command);
            response.Result = read.Reader(x => x.ToResumenCargaSucursalesProveedores());
            response.Success = response.Result.Any();

            return response;
        }




        #region resultados par armado del excel 


        public ResponseList<ReporteSucursalesNuevasModel> ExportExcelSucursalesNuevas()
        {
            var response = new ResponseList<ReporteSucursalesNuevasModel>();

            var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");

            var command = db.GetStoredProcCommand("mta.SP_GRFP_GET_SUC_N");
            command.CommandTimeout = 0;


            var read = db.ExecuteReader(command);
            response.Result = read.Reader(x => x.ToReporteSucursalesNuevasExcel());
            response.Success = response.Result.Any();

            return response;
        }


        public ResponseList<ReporteCatProductosModel> ExportExcelAllProducts()
        {
            var response = new ResponseList<ReporteCatProductosModel>();

            var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");

            var command = db.GetStoredProcCommand("mta.SP_GRFP_GET_CAT_PROD");
            command.CommandTimeout = 0;


            var read = db.ExecuteReader(command);
            response.Result = read.Reader(x => x.ToReporteCatProdExcel());
            response.Success = response.Result.Any();

            return response;
        }

        public ResponseList<ReporteCatProductosModel> ExportExcelProductosNuevos()
        {
            var response = new ResponseList<ReporteCatProductosModel>();

            var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");

            var command = db.GetStoredProcCommand("mta.SP_GRFP_GET_PROD_N");
            command.CommandTimeout = 0;


            var read = db.ExecuteReader(command);
            response.Result = read.Reader(x => x.ToReporteCatProdExcel());
            response.Success = response.Result.Any();

            return response;
        }

        public ResponseList<ReporteInvenadroModel> ExportExcelInvenadro()
        {
            var response = new ResponseList<ReporteInvenadroModel>();

            var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");

            var command = db.GetStoredProcCommand("mta.SP_GRFP_GET_INVENADRO");
            command.CommandTimeout = 0;


            var read = db.ExecuteReader(command);
            response.Result = read.Reader(x => x.ToReporteInvenadroExcel());
            response.Success = response.Result.Any();

            return response;
        }

        #endregion


        public ResponseList<ConsultaArticuloModel> ExportExcelArticulos()
        {
            var response = new ResponseList<ConsultaArticuloModel>();

            var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");

            var command = db.GetStoredProcCommand("mta.SP_GRFP_GET_CONSULTA_ARTICULOS_REPORTE");
            command.CommandTimeout = 0;


            var read = db.ExecuteReader(command);
            response.Result = read.Reader(x => x.ToReporteArticulosExcel());
            response.Success = response.Result.Any();

            return response;
        }



        public ResponseList<ConsultaArticuloSucursalModel> ExportExcelArticulosSucursal()
        {
            var response = new ResponseList<ConsultaArticuloSucursalModel>();

            var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");

            var command = db.GetStoredProcCommand("mta.SP_GRFP_GET_CONSULTA_ARTICULO_SUCURSAL_REPORTE");
            command.CommandTimeout = 0;


            var read = db.ExecuteReader(command);
            response.Result = read.Reader(x => x.ToReporteArticulosSucursalExcel());
            response.Success = response.Result.Any();

            return response;
        }

        public ResponseList<ReporteArticulosProveedorModel> GetProveedorAgenciaProdData(int idProveedor, int idagencia)
        {
            var response = new ResponseList<ReporteArticulosProveedorModel>();

            var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");
            var command = db.GetStoredProcCommand("mta.SP_GRFP_GET_PROV_AGE_PROD");

            db.AddInParameter(command, "@IdProveedor", DbType.Int32, idProveedor);
            db.AddInParameter(command, "@IdAgencia", DbType.Int32, idagencia);

            command.CommandTimeout = 0;

            var read = db.ExecuteReader(command);
            response.Result = read.Reader(x => x.ToProveedorAgenciaProd());
            response.Success = response.Result.Any();

            if (response.Result.Count == 0)
            {
                response.Message = "No hay registros por mostrar";
            }

            return response;
        }


        public ResponseList<ComboGenerico> GetListProvAgenciaData(int flag, int search)//1=formas 2=tipos 3=clasificacion 4=periodicidad
        {
            var response = new ResponseList<ComboGenerico>();

            var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");
            var command = db.GetStoredProcCommand("mta.SP_GRFP_GET_LIST_PROV_AGE");//solo para inicializar

            db.AddInParameter(command, "@flag", DbType.Int32, flag);
            db.AddInParameter(command, "@idprov", DbType.Int32, search);

            command.CommandTimeout = 0;

            var read = db.ExecuteReader(command);
            response.Result = read.Reader(x => x.ToComboGenerico());

            response.Success = response.Result.Any();

            if (response.Result.Count == 0)
            {
                response.Message = "No hay registros por mostrar";
            }

            return response;
        }



        public ResponseList<Entities.Common.ConsultaSucursales> ExportExcelSucursales()
        {
            var response = new ResponseList<Entities.Common.ConsultaSucursales>();

            var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");

            var command = db.GetStoredProcCommand("mta.SP_GRFP_CONF_SELECT_SUCURSAL_REPORTE");
            command.CommandTimeout = 0;


            var read = db.ExecuteReader(command);
            response.Result = read.Reader(x => x.ToReporteSucursalExcel());
            response.Success = response.Result.Any();

            return response;
        }



        public ResponseList<ComboSucursalesProveedor> ExportExcelSucursalesProveedores()
        {
            var response = new ResponseList<ComboSucursalesProveedor>();

            var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");

            var command = db.GetStoredProcCommand("mta.sp_Reporte_Suc_Proveedor_GRFP_Excel");
            command.CommandTimeout = 0;


            var read = db.ExecuteReader(command);
            response.Result = read.Reader(x => x.ToReporteSucursalProveedoresExcel());
            response.Success = response.Result.Any();

            return response;
        }


        public ResponseList<ConsultaProductosModel> GetProductosListData(string search, int user)
        {
            var response = new ResponseList<ConsultaProductosModel>();

            var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");

            var command = db.GetStoredProcCommand("mta.SP_GRFP_GET_CONSULTA_ARTICULOS");

            db.AddInParameter(command, "@searchText", DbType.String, search);
            db.AddInParameter(command, "@user", DbType.Int32, user);

            command.CommandTimeout = 0;

            var read = db.ExecuteReader(command);
            response.Result = read.Reader(x => x.ToConsultaProductos());

            response.Success = response.Result.Any();

            return response;

        }


        public ResponseList<ComboGenerico> GetFiltrosArticuloSucursalData(int user, int flag, string search)
        {
            var response = new ResponseList<ComboGenerico>();

            var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");
            var command = db.GetStoredProcCommand("mta.SP_GRFP_GET_CONSULTA_ARTICULO_POR_SUCURSAL");//solo para inicializar

            db.AddInParameter(command, "@Idusuario", DbType.String, user);
            db.AddInParameter(command, "@storeId", DbType.Int32, 0);
            db.AddInParameter(command, "@flag", DbType.Int32, flag);
            db.AddInParameter(command, "@estatusProducto", DbType.String, "");
            db.AddInParameter(command, "@searchTxt", DbType.String, search);



            command.CommandTimeout = 0;

            var read = db.ExecuteReader(command);
            response.Result = read.Reader(x => x.ToComboGenerico());
            response.Success = response.Result.Any();

            return response;
        }



        public ResponseList<ReporteArticulosSucursalModel> GetProductosSucursalListData(int user, string storeId, string estatusProd)
        {
            var response = new ResponseList<ReporteArticulosSucursalModel>();
            int flag = 3;

            if (estatusProd == "Todos")
            {
                flag = 4;
            }


            var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");

            var command = db.GetStoredProcCommand("mta.SP_GRFP_GET_CONSULTA_ARTICULO_POR_SUCURSAL");

            db.AddInParameter(command, "@Idusuario", DbType.String, user);
            db.AddInParameter(command, "@storeId", DbType.String, storeId);
            db.AddInParameter(command, "@flag", DbType.Int32, flag);
            db.AddInParameter(command, "@estatusProducto", DbType.String, estatusProd);
            db.AddInParameter(command, "@searchTxt", DbType.String, "");


            command.CommandTimeout = 0;

            var read = db.ExecuteReader(command);
            response.Result = read.Reader(x => x.ToArticulosSucursal());

            response.Success = response.Result.Any();

            return response;

        }

        public ResponseList<AutocompleteString> Autocompletar(string letras)
        {
            var response = new ResponseList<AutocompleteString>();

            var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");
            var command = db.GetStoredProcCommand("mta.sp_articulo_autocompletar");//solo para inicializar

            db.AddInParameter(command, "@letras", DbType.String, letras);

            command.CommandTimeout = 0;

            var read = db.ExecuteReader(command);
            response.Result = read.Reader(x => x.ToAutocompleteString());
            response.Success = response.Result.Any();

            return response;
        }

        public ResponseList<EstadisticaCompras> ReporteEstadistica(String idArticulo, int idGrupo)//int idCarga
        {
            var response = new ResponseList<EstadisticaCompras>();

            var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");
            var command = db.GetStoredProcCommand("MTA.SP_EstadisticaCompras");

            command.CommandTimeout = 0;
            db.AddInParameter(command, "@idArticulo", DbType.String, idArticulo);
            db.AddInParameter(command, "@idGrupo", DbType.Int32, idGrupo);

            var read = db.ExecuteReader(command);
            response.Result = read.Reader(x => x.ToEstadisticaCompras());

            return response;
        }

        public ResponseList<EstadisticaProveedor> ReporteEstadisticaProveedor(String idArticulo, int idGrupo)//int idCarga
        {
            var response = new ResponseList<EstadisticaProveedor>();

            var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");
            var command = db.GetStoredProcCommand("MTA.SP_EstadisticaProveedor");

            command.CommandTimeout = 0;
            db.AddInParameter(command, "@idArticulo", DbType.String, idArticulo);
            db.AddInParameter(command, "@idGrupo", DbType.Int32, idGrupo);

            var read = db.ExecuteReader(command);
            response.Result = read.Reader(x => x.ToEstadisticaProveedor());

            return response;
        }

        public ResponseList<EstadisticaVenta28dias> ReporteEstadisticaVentas28Dias(String idArticulo, int idGrupo)//int idCarga
        {
            var response = new ResponseList<EstadisticaVenta28dias>();

            var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");
            var command = db.GetStoredProcCommand("MTA.SP_EstadisticaVenta28dias");

            command.CommandTimeout = 0;
            db.AddInParameter(command, "@idArticulo", DbType.String, idArticulo);
            db.AddInParameter(command, "@idGrupo", DbType.Int32, idGrupo);

            var read = db.ExecuteReader(command);
            response.Result = read.Reader(x => x.ToEstadisticaVenta28dias());

            return response;
        }

        public ResponseList<ReporteInvenadroAutModel> GetReporteInvenadroAutData(string sucursales, string sku)
        {
            var response = new ResponseList<ReporteInvenadroAutModel>();

            var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");
            var command = db.GetStoredProcCommand("mta.SP_GRFP_GET_REPORTE_INVENADRO_AUT");

            db.AddInParameter(command, "@Sucursales", DbType.String, sucursales);
            db.AddInParameter(command, "@SKU", DbType.String, string.IsNullOrEmpty(sku) ? (object)DBNull.Value : sku);

            command.CommandTimeout = 120;

            var read = db.ExecuteReader(command);
            response.Result = read.Reader(x => x.ToReporteInvenadroAut());
            response.Success = response.Result != null && response.Result.Any();

            return response;
        }
    }
}
