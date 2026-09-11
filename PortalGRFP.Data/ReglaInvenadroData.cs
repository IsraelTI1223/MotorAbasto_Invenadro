using Microsoft.Practices.EnterpriseLibrary.Data;
using PortalGRFP.Data.Extensions;
using PortalGRFP.Entities.Common;
using PortalGRFP.Entities.Response;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalGRFP.Data
{
    public class ReglaInvenadroData
    {
        public Response InvenadroGuardar(Invenadro invenadro, int idUsuario)//int idCarga
        {
            var response = new ResponseList<Invenadro>();

            var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");

            var command = db.GetStoredProcCommand("mta.sp_conf_invenadro_Actualiza");

            db.AddInParameter(command, "@LimiteCosto", DbType.Boolean, invenadro.LimiteCostoProductos);
            db.AddInParameter(command, "@Monto", DbType.Decimal, invenadro.Monto);
            db.AddInParameter(command, "@Piezas", DbType.Int32, invenadro.PiezasProductos);
            db.AddInParameter(command, "@PVD", DbType.Boolean, invenadro.PVD);
            db.AddInParameter(command, "@Invenadro_cero", DbType.Boolean, invenadro.InvenadroCero);
            db.AddInParameter(command, "@activarProducto", DbType.Boolean, invenadro.ActivarProducto);
            db.AddInParameter(command, "@inactivarProducto", DbType.Boolean, invenadro.InactivarProductos);
            db.AddInParameter(command, "@PorcentajeCoincidencia", DbType.Decimal, invenadro.PorcentajeCoincidencia);
            db.AddInParameter(command, "@AutoUpdate", DbType.Boolean, invenadro.Actualizacion_automatica);
            db.AddInParameter(command, "@diasPVD", DbType.Int32, invenadro.diasPVD);
            db.AddInParameter(command, "@diasAutomtico", DbType.Int32, invenadro.diasAutomatico);
            db.AddInParameter(command, "@usuario", DbType.Int32, idUsuario);

            command.CommandTimeout = 0;


            var exito = db.ExecuteNonQuery(command);
            response.Success = exito != default;

            return response;
        }

        public ResponseList<Invenadro> InvenadroGet()//int idCarga
        {
            var response = new ResponseList<Invenadro>();

            var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");

            var command = db.GetStoredProcCommand("mta.sp_conf_get_invenadro");

            //db.AddInParameter(command, "@LimiteCosto", DbType.Boolean, limiteCosto);
            //db.AddInParameter(command, "@Monto", DbType.Decimal, monto);
            //db.AddInParameter(command, "@Piezas", DbType.Int32, piezas);
            //db.AddInParameter(command, "@PVD", DbType.Boolean, pvd);
            //db.AddInParameter(command, "@Invenadro_cero", DbType.Boolean, cero);
            //db.AddInParameter(command, "@activarProducto", DbType.Boolean, activar);
            //db.AddInParameter(command, "@inactivarProducto", DbType.Boolean, inactivar);

            command.CommandTimeout = 0;

            var read = db.ExecuteReader(command);
            response.Result = read.Reader(x => x.ToInvenadroRegla());
            response.Success = response.Result.Any();

            return response;
        }

        public Response CargaInvenadro(DataTable DtInvenadro)//int idCarga
        {
            var response = new Response();

            var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");

            var command = db.GetStoredProcCommand("mta.sp_Invenadro_CargaMasiva");
            command.CommandTimeout = 0;
            command.Parameters.Add(new SqlParameter("@Tipo_Invenadro", SqlDbType.Structured) { Value = DtInvenadro });

            var exito = db.ExecuteNonQuery(command);
            response.Success = exito != default;

            return response;
        }

        public ResponseList<LogInvenadro> GetInvenadro(int accion)//int idCarga
        {
            var response = new ResponseList<LogInvenadro>();

            var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");
            string sp = "mta.sp_getTotalizado_Invenadro";
            if (accion == 1)
            {
                sp = "mta.sp_getLOG_Invenadro";
            }
            var command = db.GetStoredProcCommand(sp);
            command.CommandTimeout = 0;
            //command.Parameters.Add(new SqlParameter("@Tipo_Invenadro", SqlDbType.Structured) { Value = DtInvenadro });

            var read = db.ExecuteReader(command);

            response.Result = read.Reader(x => x.ToLogInvenadro());
            response.Success = response.Result.Any();

            return response;
        }


        public ResponseList<Combo4> Filtros(int accion)//1=Grupos 2=Sucursale 
        {
            var response = new ResponseList<Combo4>();

            var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");
            var command = db.GetStoredProcCommand("[mta].sp_grfp_filtros_sugeridos");//solo para inicializar

            db.AddInParameter(command, "@accion", DbType.Int32, accion);

            command.CommandTimeout = 0;

            var read = db.ExecuteReader(command);
            response.Result = read.Reader(x => x.ToCombo4());
            response.Success = response.Result.Any();

            return response;
        }

        public Response BorrarInvenadro(string sucursales, int usuario)//int idCarga
        {
            var response = new Response();

            var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");

            var command = db.GetStoredProcCommand("mta.sp_borrar_invenadro_sucursal");
            command.CommandTimeout = 0;
            command.Parameters.Add(new SqlParameter("@sucursales", SqlDbType.VarChar) { Value = sucursales });
            command.Parameters.Add(new SqlParameter("@idusuario", SqlDbType.Int) { Value = usuario });



            var exito = db.ExecuteNonQuery(command);
            response.Success = exito != default;

            return response;
        }

        #region Excluidos

        public ResponseList<Excluidos_Invenadro_Aut> GetExcluidosData(string SKU, string Sucursales)
        {
            var response = new ResponseList<Excluidos_Invenadro_Aut>();

            var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");

            var command = db.GetStoredProcCommand("mta.SP_GRFP_GET_EXCLUIDOS_INVENADRO");
            db.AddInParameter(command, "@SKU", DbType.String, SKU);
            db.AddInParameter(command, "@Sucursales", DbType.String, Sucursales);

            command.CommandTimeout = 900;

            var read = db.ExecuteReader(command);
            response.Result = read.Reader(x => x.ToExcluidosInvenadro());
            response.Success = response.Result.Any();

            return response;
        }
        public ResponseList<ComboGenerico> GetSucursalesComboData()
        {
            var response = new ResponseList<ComboGenerico>();

            var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");

            var command = db.GetStoredProcCommand("[mta].[SP_GRFP_CONF_INVENADRO_GET_SUCURSAL_EXCLUIDOS]");

            command.CommandTimeout = 900;

            var read = db.ExecuteReader(command);
            response.Result = read.Reader(x => x.ToComboSucursalCadena());
            response.Success = response.Result.Any();

            return response;
        }
        #endregion

        #region CDRs config
        public ResponseList<InfoCDR> GetCRDsInfoData(int Flag, int agencia_id, decimal MontoCDR) 
        {
            var response = new ResponseList<InfoCDR>();

            var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");

            var command = db.GetStoredProcCommand("MTA.SP_CONF_GET_CDR_LIST");
            db.AddInParameter(command, "@flag", DbType.Int32, Flag);
            db.AddInParameter(command, "@agencia_id", DbType.Int32, agencia_id);
            db.AddInParameter(command, "@MontoCDR", DbType.Decimal, MontoCDR);

            command.CommandTimeout = 0;

            var read = db.ExecuteReader(command);
            response.Result = read.Reader(x => x.ToBalanceCDRs());
            response.Success = response.Result.Any();

            return response;
        }
        public ResponseList<InfoCDR> GetBalanceData(int agencia_id, decimal MontoCDR) 
        {
            var response = new ResponseList<InfoCDR>();

            var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");

            var command = db.GetStoredProcCommand("MTA.SP_CONF_GET_CDR_BALANCE");
            db.AddInParameter(command, "@agencia_id", DbType.Int32, agencia_id);
            db.AddInParameter(command, "@MontoCDR", DbType.Decimal, MontoCDR);

            command.CommandTimeout = 0;

            var read = db.ExecuteReader(command);
            response.Result = read.Reader(x => x.ToBalanceCDRs());
            response.Success = response.Result.Any();

            return response;
        }

        public DataTable GetExportBloquesCDRData(int agencia)
        {
            var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");
            var command = db.GetStoredProcCommand("mta.SP_GET_EXPORT_BLOQUES_CDR");
            db.AddInParameter(command, "@agencia", DbType.Int32, agencia);
            command.CommandTimeout = 120;
            var ds = db.ExecuteDataSet(command);
            return ds.Tables.Count > 0 ? ds.Tables[0] : new DataTable();
        }

        public Response ActualizaCDRsData(InfoCDR cedis, int idUsuario)
        {
            var response = new ResponseList<object>();

            var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");

            var command = db.GetStoredProcCommand("mta.SP_CONF_UPDATE_CDRS_STS");

            db.AddInParameter(command, "@agencia_id", DbType.Int32, cedis.Id);
            db.AddInParameter(command, "@MontoCDRs", DbType.Decimal,cedis.MontoCDR);
            db.AddInParameter(command, "@Farmacias", DbType.Int32,cedis.Farmacias);
            db.AddInParameter(command, "@MontoNecesidad", DbType.Decimal,cedis.MontoNecesidad);
            db.AddInParameter(command, "@Pedidos", DbType.Int32,cedis.PedidosRealizar);
            db.AddInParameter(command, "@PorcentCompraMayor", DbType.Decimal,cedis.PorcentajeCompraMayor);
            db.AddInParameter(command, "@PorcentCompraMenor", DbType.Decimal,cedis.PorcentajeCompraMenor);
            db.AddInParameter(command, "@MontoCompraMayor", DbType.Decimal,cedis.MontoCompraMayor);
            db.AddInParameter(command, "@MOntoCompraMenor", DbType.Decimal,cedis.MontoCompraMenor);
            db.AddInParameter(command, "@usuario", DbType.Int32, idUsuario);

            command.CommandTimeout = 0;


            var exito = db.ExecuteNonQuery(command);
            response.Success = exito != default;

            return response;
        }

        #endregion

        public Response AutorizarExcluidosMasivoData(DataTable tabla, int idUsuario)
        {
            Response response = new Response();

            try
            {
                var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");
                var command = db.GetStoredProcCommand("[mta].[SP_GRFP_AUTORIZA_INVENADRO_MANUAL]");

                command.CommandTimeout = 0;
                command.Parameters.Add(new SqlParameter("@tablaExcluidosPorAutorizar", SqlDbType.Structured) { Value = tabla });
                command.Parameters.Add(new SqlParameter("@idusuario", SqlDbType.Int) { Value = idUsuario });

                var exito = db.ExecuteNonQuery(command);
                response.Success = exito != default;
                response.Message = "Autorización masiva realizada correctamente.";
                
            }
            catch (SqlException ex)
            {
                response.Success = false;
                response.Message = "Error de base de datos: " + ex.Message;
            }

            return response;
        }

        #region Resumen

        public ResponseList<ComboGenerico> GetMotivosComboData()
        {
            var response = new ResponseList<ComboGenerico>();

            var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");

            var command = db.GetStoredProcCommand("[mta].[SP_GRFP_CONF_INVENADRO_GET_ESTATUS_RESUMEN]");

            command.CommandTimeout = 900;

            var read = db.ExecuteReader(command);
            response.Result = read.Reader(x => x.ToComboGenericoResumen());
            response.Success = response.Result.Any();

            return response;
        }
        public ResponseList<Resumen_Invenadro> GetEstatusData(string Estatus)
        {
            var response = new ResponseList<Resumen_Invenadro>();

            var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");

            var command = db.GetStoredProcCommand("mta.SP_GRFP_GET_RESUMEN_INVENADRO");
            db.AddInParameter(command, "@Estatus", DbType.String, Estatus);

            command.CommandTimeout = 900;

            var read = db.ExecuteReader(command);
            response.Result = read.Reader(x => x.ToResumenInvenadro());
            response.Success = response.Result.Any();

            return response;
        }
        public ResponseList<Excluidos_Invenadro_Aut> GetDetalleEstatusData(string Estatus)
        {
            var response = new ResponseList<Excluidos_Invenadro_Aut>();

            var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");

            var command = db.GetStoredProcCommand("[mta].[SP_GRFP_GET_DETALLE_RESUMEN_INVENADRO]");
            db.AddInParameter(command, "@Estatus", DbType.String, Estatus);

            command.CommandTimeout = 900;

            var read = db.ExecuteReader(command);
            response.Result = read.Reader(x => x.ToDetalleEstatusInvenadro());
            response.Success = response.Result.Any();

            return response;
        }
        public ResponseList<Excluidos_Invenadro_Aut> GetDetalleTablaEstatusData(string oFlag, string oEx)
        {
            var response = new ResponseList<Excluidos_Invenadro_Aut>();

            var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");

            var command = db.GetStoredProcCommand("[mta].[SP_GRFP_GET_DETALLE_TABLA_ESTATUS_INVENADRO]");
            db.AddInParameter(command, "@oFlag", DbType.String, oFlag);
            db.AddInParameter(command, "@oEx", DbType.String, oEx);

            command.CommandTimeout = 900;

            var read = db.ExecuteReader(command);
            response.Result = read.Reader(x => x.ToDetalleEstatusInvenadro());
            response.Success = response.Result.Any();

            return response;
        }

        public Response AutorizaInvenadroData(DataTable DtInvenadro)
        {
            var response = new Response();

            try
            {
                var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");

                var command = db.GetStoredProcCommand("[mta].[SP_GRFP_AUTORIZA_INVENADRO_MANUAL]");
                command.CommandTimeout = 0;

                command.Parameters.Add(
                    new SqlParameter("@Tipo_AutorizaInvenadro", SqlDbType.Structured)
                    {
                        Value = DtInvenadro
                    });

                var exito = db.ExecuteNonQuery(command);

                
                response.Success = exito != default;
                response.Message = "Operación realizada correctamente.";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }

        #endregion

    }
}
