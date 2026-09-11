using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.EnterpriseLibrary.Data;
using PortalGRFP.Entities.Common.MantenimientoArticulo;
using PortalGRFP.Entities.Common;
using PortalGRFP.Entities.Response;
using PortalGRFP.Data.Extensions;
using System.Data.SqlClient;
using static PortalGRFP.Entities.Common.MantenimientoArticulo.ArticuloPermisoCompraModel;

namespace PortalGRFP.Data.Mantenimiento
{
    public class MantenimientoArticuloData
    {
        public ArticuloTipoCompraModel GetAArticuloTipoCompData(int flag, string txt)
        {
            var response = new ArticuloTipoCompraModel();

            var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");
            var command = db.GetStoredProcCommand("mta.SP_GRFP_CONF_GET_PRODUCT_M");
            command.Parameters.Add(new SqlParameter("@flag", flag));
            command.Parameters.Add(new SqlParameter("@text", txt));


            using (IDataReader dr = db.ExecuteReader(command))
            {
                while (dr.Read())
                {
                    response.ProductId = dr.Get<string>("ProductId");
                    response.Sku = dr.Get<string>("Sku");
                    response.FormaSurtirFR = dr.Get<string>("Forma_Surtir_FR");
                    response.FormaSurtirLD = dr.Get<string>("Forma_Surtir_LD");
                    response.FormaSurtirSSAE = dr.Get<string>("Forma_Surtir_SSAE");
                    response.FactorEmpaque = dr.Get<string>("FACTOR_EMPAQUE");
                    response.EanEmpaque = dr.Get<string>("EAN_EMPAQUE");
                    response.Proveedor = dr.Get<string>("PROVEEDOR");
                    response.Descripcion = dr.Get<string>("DESCRIPCION_LARGA");


                }
            }

            return response;
        }
        public ResponseList<Autocomplete> GetAArticuloTipoListCompraData(int flag)//1=formas 2=tipos 3=clasificacion 4=periodicidad
        {
            var response = new ResponseList<Autocomplete>();

            var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");
            var command = db.GetStoredProcCommand("mta.SP_GRFP_CONF_GET_AUTOC_LIST_PRODUCT");//solo para inicializar

            db.AddInParameter(command, "@flag", DbType.Int32, flag);

            command.CommandTimeout = 0;

            var read = db.ExecuteReader(command);
            response.Result = read.Reader(x => x.ToAutocomplete());
            response.Success = response.Result.Any();

            return response;
        }
        public ResponseList<Autocomplete> GetConceptosistCompraData()//1=formas 2=tipos 3=clasificacion 4=periodicidad
        {
            var response = new ResponseList<Autocomplete>();

            var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");
            var command = db.GetStoredProcCommand("mta.SP_GRFP_CONF_GET_CONC_LIST_PRODUCT");//solo para inicializar

            command.CommandTimeout = 0;

            var read = db.ExecuteReader(command);
            response.Result = read.Reader(x => x.ToAutocomplete());
            response.Success = response.Result.Any();

            return response;
        }

        public Response UIPermisoCompraArticuloData(CargaArticuloMViewModel model, List<ArticuloExcel> excel)//int idCarga
        {
            var response = new Response();
            try
            {

                var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");

                foreach (var item in excel)
                {
                    var command = db.GetStoredProcCommand("mta.SP_GRFP_CONF_UI_PERMISO_COMPRA");

                    db.AddInParameter(command, "@IdSucursal", DbType.Int32, int.Parse(item.IdSucursal));
                    db.AddInParameter(command, "@Sku", DbType.String, item.Sku);
                    db.AddInParameter(command, "@Compra", DbType.Boolean, model.Compra);
                    db.AddInParameter(command, "@Venta", DbType.Boolean, model.Venta);
                    db.AddInParameter(command, "@IdUsuario", DbType.Int32, model.IdUsuario);
                    db.AddInParameter(command, "@ConceptoCompra", DbType.Int32, model.Conceptocompra);
                    //db.AddInParameter(command, "@IdUsuario", DbType.Int32, model.Conceptoventa);


                    command.CommandTimeout = 0;

                    var exito = db.ExecuteNonQuery(command);
                }

                response.Message = "Termino Carga de Articulos";
                response.Success = 1 != default;

                var commandAlert = db.GetStoredProcCommand("dbo.sp_alert_Movimiento_Permiso_de_Compra_Motor_Abasto");

                commandAlert.CommandTimeout = 0;
                var exitoAlert = db.ExecuteNonQuery(commandAlert);

            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Success = 0 != default;
            }

            return response;
        }
        public Response UIPermisoCompraArticuloPorSucursalData(ArticuloXSucursalModel model)//int idCarga
        {
            var response = new Response();


            var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");

            foreach (var item in model.Sucursales)
            {
                var command = db.GetStoredProcCommand("mta.SP_GRFP_CONF_UI_PERMISO_COMPRA");

                db.AddInParameter(command, "@IdSucursal", DbType.Int32, int.Parse(item.ID));
                db.AddInParameter(command, "@Sku", DbType.String, model.Sku);
                db.AddInParameter(command, "@Compra", DbType.Boolean, model.Compra);
                db.AddInParameter(command, "@Venta", DbType.Boolean, model.Venta);
                db.AddInParameter(command, "@IdUsuario", DbType.Int32, model.IdUsuario);


                command.CommandTimeout = 0;

                var exito = db.ExecuteNonQuery(command);
            }

            response.Message = "Termino Carga de Articulos";
            response.Success = 1 != default;

            return response;
        }

        public Response PermisosPorSucursalData(ArticulosPorSucursalModel model)
                {
            var response = new Response();

            try
            {
                var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");



                foreach (var item in model.Sucursales)
                {

                    if (item.Compra == true)
                    {
                        item.Conceptocompra = 0;
                    }
                    else
                    {
                        item.Conceptocompra = model.Concepto;
                    }

                    var command = db.GetStoredProcCommand("mta.SP_GRFP_CONF_U_PERMISO_COMPRA");

                    db.AddInParameter(command, "@flag", DbType.Int32, model.Flag);
                    db.AddInParameter(command, "@IdSucursal", DbType.Int32, item.IdSucursal);
                    db.AddInParameter(command, "@Sku", DbType.String, model.SKU);
                    db.AddInParameter(command, "@Compra", DbType.Boolean, item.Compra);
                    db.AddInParameter(command, "@Venta", DbType.Boolean, item.Venta);
                    db.AddInParameter(command, "@IdUsuario", DbType.Int32, model.IdUsuario);
                    db.AddInParameter(command, "@ConceptoCompra", DbType.Int32, item.Conceptocompra);
                    //db.AddInParameter(command, "@ConceptoVenta", DbType.Int32,  model.Concepto);


                    command.CommandTimeout = 0;

                    var exito = db.ExecuteNonQuery(command);
                }

                response.Message = "Cambios Guardados";
                response.Success = 1 != default;

                //var commandAlert = db.GetStoredProcCommand("dbo.sp_alert_Movimiento_Permiso_de_Compra_Motor_Abasto");

                //commandAlert.CommandTimeout = 0;
                //var exitoAlert = db.ExecuteNonQuery(commandAlert);

            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Success = 0 != default;
            }
            return response;

        }


        public ResponseList<CadenasSucursalesModel> GetCadenaSucursalData(int flag, string search)//1=formas 2=tipos 3=clasificacion 4=periodicidad
        {
            var response = new ResponseList<CadenasSucursalesModel>();

            var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");
            var command = db.GetStoredProcCommand("mta.SP_GRFP_MANT_GET_LIST_CADENAS_SUCURSALES");//solo para inicializar

            db.AddInParameter(command, "@flag", DbType.Int32, flag);
            db.AddInParameter(command, "@subcadena", DbType.String, search);

            command.CommandTimeout = 0;

            var read = db.ExecuteReader(command);
            response.Result = read.Reader(x => x.ToCadenaSucursales());

            response.Success = response.Result.Any();

            return response;
        }


        public ResponseList<LogErrorPermisoCompraModel> GetLogErrorData(int IdUsuario)
        {
            var response = new ResponseList<LogErrorPermisoCompraModel>();

            var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");
            var command = db.GetStoredProcCommand("mta.SP_GRFP_GET_PERMISO_COMPRA_LOG_ERROR");//solo para inicializar

            db.AddInParameter(command, "@IdUsuario", DbType.Int32, IdUsuario);

            command.CommandTimeout = 0;

            var read = db.ExecuteReader(command);
            response.Result = read.Reader(x => x.ToLogErrorPermisoCompra());
            response.Success = response.Result.Any();
            if (response.Result.Count == 0)
            {
                response.Message = "No hay registros por mostrar";
            }

            return response;
        }


        public ResponseList<ComboGenerico> GetCadenasSucursalesData(int flag, int idusuario, string cadenas)
        {
            var response = new ResponseList<ComboGenerico>();

            var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");
            var command = db.GetStoredProcCommand("mta.SP_GRFP_GET_PERMISO_ARTICULOS_SUCURSAL");

            db.AddInParameter(command, "@flag", DbType.Int32, flag);
            db.AddInParameter(command, "@usuario", DbType.Int32, idusuario);
            db.AddInParameter(command, "@SKU", DbType.String, "");
            db.AddInParameter(command, "@sucursales", DbType.String, "");
            db.AddInParameter(command, "@cadenas", DbType.String, cadenas);

            command.CommandTimeout = 0;

            var read = db.ExecuteReader(command);
            response.Result = read.Reader(x => x.ToComboGenerico());

            response.Success = response.Result.Any();

            return response;
        }

        public ResponseList<PermisoArticuloSucursalModel> GetPermisosArticuloSucursalData(int flag, int idusuario, string sku, string sucursales)
        {
            var response = new ResponseList<PermisoArticuloSucursalModel>();

            var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");
            var command = db.GetStoredProcCommand("mta.SP_GRFP_GET_PERMISO_ARTICULOS_SUCURSAL");

            db.AddInParameter(command, "@flag", DbType.Int32, flag);
            db.AddInParameter(command, "@usuario", DbType.Int32, idusuario);
            db.AddInParameter(command, "@SKU", DbType.String, sku);
            db.AddInParameter(command, "@sucursales", DbType.String, sucursales);
            db.AddInParameter(command, "@cadenas", DbType.String, "");

            command.CommandTimeout = 0;

            var read = db.ExecuteReader(command);
            response.Result = read.Reader(x => x.ToPermisosArticuloSucursal());

            response.Success = response.Result.Any();

            return response;
        }

        public ResponseList<Autocomplete> GetAutoCompleteProductListData(int flag)
        {
            var response = new ResponseList<Autocomplete>();

            var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");
            var command = db.GetStoredProcCommand("mta.SP_GRFP_CONF_GET_AUTOC_LIST_PRODUCT_PERMISOS");

            db.AddInParameter(command, "@flag", DbType.Int32, flag);

            command.CommandTimeout = 0;

            var read = db.ExecuteReader(command);
            response.Result = read.Reader(x => x.ToAutocomplete());
            response.Success = response.Result.Any();

            return response;
        }



    }
}
