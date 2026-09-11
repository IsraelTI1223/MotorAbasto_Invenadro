using PortalGRFP.Entities.Response;
using Microsoft.Practices.EnterpriseLibrary.Data;
using PortalGRFP.Data.Extensions;
using PortalGRFP.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace PortalGRFP.Data.ConciliacionRappi
{
    public class GetListaConciliacionesData
    {
        public ResponseList<ConciliacionVentaRappi> ObtenerConciliaciones(int anio, int mes)//int idCarga
        {
            var response = new ResponseList<ConciliacionVentaRappi>();

            var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");

            var command = db.GetStoredProcCommand("rappi.Sp_ObtenerConciliacionesVentas");
            command.CommandTimeout = 0;

            db.AddInParameter(command, "@anio", DbType.Int32, anio);
            db.AddInParameter(command, "@mes", DbType.Int32, mes);

            var read = db.ExecuteReader(command);
            response.Result = read.Reader(x => x.ToConciliacionVentaRappi());
            response.Success = response.Result.Any();

            return response;
        }


        public ResponseList<TicketVenta> ObtenerTicketVenta(string Cadena, string Ticket, string Sucursal)//int idCarga
        {
            var response = new ResponseList<TicketVenta>();

            var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");

            var command = db.GetStoredProcCommand("rappi.sp_GRFP_GET_INFO_TICKET_CONCILIACION");
            command.CommandTimeout = 0;

            db.AddInParameter(command, "@Razon", DbType.String, Cadena);
            db.AddInParameter(command, "@ticket", DbType.String, Ticket);
            db.AddInParameter(command, "@sucursal", DbType.String, Sucursal);

            var read = db.ExecuteReader(command);
            response.Result = read.Reader(x => x.ToConciliacionTicketventa());
            response.Success = response.Result.Any();

            return response;
        }


        public Response ConciliacionManual(string OrderId, string Operacion, string IdSucursal, decimal ImporteTotal, int Accion, int Incidencia, string Observacion, string ticket)//int idCarga
        {
            var response = new Response();

            var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");

            var command = db.GetStoredProcCommand("rappi.GRFP_UPDATE_CONCILIATE");
            command.CommandTimeout = 0;
            db.AddInParameter(command, "@OrderID", DbType.String, OrderId);
            db.AddInParameter(command, "@DiaOperacion", DbType.String, Operacion);
            db.AddInParameter(command, "@Sucursal", DbType.String, IdSucursal);
            db.AddInParameter(command, "@importeTotal", DbType.Decimal, ImporteTotal);
            db.AddInParameter(command, "@aplica", DbType.Int32, Accion);
            db.AddInParameter(command, "@Observacion", DbType.String, Observacion);
            db.AddInParameter(command, "@incidencia", DbType.Int32, Incidencia);
            db.AddInParameter(command, "@ticket", DbType.String, ticket);

            var exito = db.ExecuteNonQuery(command);
            response.Success = exito != default;

            return response;
        }

        public Response ConciliacionManualCard(string razons, string Operacion, string IdSucursal, decimal ImporteTotal, int Accion, int Incidencia, string Observacion, string ticket)//int idCarga
        {
            var response = new Response();

            var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");

            var command = db.GetStoredProcCommand("rappi.GRFP_UPDATE_CONCILIATE_Card");
            command.CommandTimeout = 0;
            db.AddInParameter(command, "@Razones", DbType.String, razons);
            db.AddInParameter(command, "@DiaOperacion", DbType.String, Operacion);
            db.AddInParameter(command, "@Sucursal", DbType.String, IdSucursal);
            db.AddInParameter(command, "@importeTotal", DbType.Decimal, ImporteTotal);
            db.AddInParameter(command, "@aplica", DbType.Int32, Accion);
            db.AddInParameter(command, "@Observacion", DbType.String, Observacion);
            db.AddInParameter(command, "@incidencia", DbType.Int32, Incidencia);
            db.AddInParameter(command, "@ticket", DbType.String, ticket);

            var exito = db.ExecuteNonQuery(command);
            response.Success = exito != default;

            return response;
        }

        public Response CargaConciliacionMensual(List<ConciliacionCargaVentaRappi> conciliacionCargas)//int idCarga
        {
            var response = new Response();

            var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");
            foreach (var item in conciliacionCargas)
            {
                var command = db.GetStoredProcCommand("rappi.sp_GRFP_LOAD_RAPPI_CONCILIACION");

                db.AddInParameter(command, "@DiaOperación", DbType.DateTime, DateTime.FromOADate(long.Parse(item.DiaOperacion)));
                db.AddInParameter(command, "@StoreID", DbType.String, item.StoreID);
                db.AddInParameter(command, "@Store", DbType.String, item.Store);
                db.AddInParameter(command, "@RazónSocial", DbType.String, item.RazonSocial);
                db.AddInParameter(command, "@Marca", DbType.String, item.Marca);
                db.AddInParameter(command, "@IdSucursal", DbType.String, item.IdSucursal);
                db.AddInParameter(command, "@ImporteTotal", DbType.Decimal, decimal.Parse(item.ImporteTotal));
                db.AddInParameter(command, "@FormadePago", DbType.String, item.FormaPago);
                db.AddInParameter(command, "@BIN", DbType.String, item.BinesCard);
                db.AddInParameter(command, "@LAST_FOUR_DIGITS", DbType.String, item.Ultimos4Digitos);
                db.AddInParameter(command, "@AUTH_CODE", DbType.String, item.CodigoAutorizacion);
                db.AddInParameter(command, "@TicketURL", DbType.String, item.TicketUrl);
                db.AddInParameter(command, "@OrderID", DbType.String, item.OrderId);

                command.CommandTimeout = 0;

                var exito = db.ExecuteNonQuery(command);
            }

            response.Success = 1 != default;

            return response;
        }

        public ResponseList<ResumenCargaRappi> ResumenCargaConciliacion()//int idCarga
        {
            var response = new ResponseList<ResumenCargaRappi>();

            var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");

            var command = db.GetStoredProcCommand("rappi.GRFP_MES_CARGADO_CONCILIACION");
            command.CommandTimeout = 0;
            //db.AddInParameter(command, "@IdCarga", DbType.Int32, idCarga);

            var read = db.ExecuteReader(command);
            response.Result = read.Reader(x => x.ToResumenCargaRappi());
            response.Success = response.Result.Any();

            return response;
        }

        public ResponseList<ComboGenerico> MostrarBotones(int anio, int mes)//int idCarga
        {
            var response = new ResponseList<ComboGenerico>();

            var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");

            var command = db.GetStoredProcCommand("rappi.sp_pendientesConciliar");

            db.AddInParameter(command, "@mes", DbType.Int32, mes);
            db.AddInParameter(command, "@anio", DbType.Int32, anio);
            command.CommandTimeout = 0;

            var read = db.ExecuteReader(command);
            response.Result = read.Reader(x => x.ToComboGenerico());
            response.Success = response.Result.Any();

            return response;
        }

        public Response AplicarConciliacion(int anio, int mes, int opcion)//int idCarga
        {
            var response = new Response();

            var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");
            string sp = "";
            if (opcion == 1)//fdu
            {
                sp = "[rappi].[sp_conciliaciones_ldcom_fdu]";
            }
            if (opcion == 2)//Bella
            {
                sp = "[rappi].[sp_conciliaciones_bellavista]";
            }
            if (opcion == 3)//cofar
            {
                sp = "[rappi].[sp_conciliaciones_cofar]";
            }
            if (opcion == 4)//fsf
            {
                sp = "[rappi].[sp_conciliaciones_fr_sc_fsf]";
            }
            var command = db.GetStoredProcCommand(sp);
            command.CommandTimeout = 0;
            db.AddInParameter(command, "@mes", DbType.Int32, mes);
            db.AddInParameter(command, "@anio", DbType.Int32, anio);

            var exito = db.ExecuteNonQuery(command);

            response.Success = 1 != default;

            return response;
        }

        public ResponseList<TicketVenta> ObtenerTIcketVentaCard(string Cadena, string Sucursal, string fecha, string importe)//int idCarga
        {
            var response = new ResponseList<TicketVenta>();

            var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");

            var command = db.GetStoredProcCommand("rappi.sp_GRFP_GET_INFO_TICKET_CONCILIACION_CARD");

            db.AddInParameter(command, "@Razon", DbType.String, Cadena);
            db.AddInParameter(command, "@DiaOperacion", DbType.String, fecha);
            db.AddInParameter(command, "@sucursal", DbType.String, Sucursal);
            db.AddInParameter(command, "@monto", DbType.String, importe);

            command.CommandTimeout = 0;

            var read = db.ExecuteReader(command);
            response.Result = read.Reader(x => x.ToConciliacionTicketventa());
            response.Success = response.Result.Any();

            return response;
        }

        public ResponseList<ConciliacionVentaRappi> ExportarExcel(int anio, int mes)//int idCarga
        {
            var response = new ResponseList<ConciliacionVentaRappi>();

            var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");

            var command = db.GetStoredProcCommand("rappi.Sp_ExportarExcel");
            command.CommandTimeout = 0;

            db.AddInParameter(command, "@anio", DbType.Int32, anio);
            db.AddInParameter(command, "@mes", DbType.Int32, mes);

            var read = db.ExecuteReader(command);
            response.Result = read.Reader(x => x.ToConciliacionVentaRappi());
            response.Success = response.Result.Any();

            return response;
        }
    }
}
