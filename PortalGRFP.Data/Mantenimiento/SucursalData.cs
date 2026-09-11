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

namespace PortalGRFP.Data.Mantenimiento
{
    public class SucursalData
    {
        public ResponseList<ComboGenerico> Filtros(int accion)//1=Grupos 2=Sucursale 
        {
            var response = new ResponseList<ComboGenerico>();

            var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");
            var command = db.GetStoredProcCommand("mta.sp_cat_mantto_suc");//solo para inicializar

            db.AddInParameter(command, "@catal", DbType.Int32, accion);

            command.CommandTimeout = 0;

            var read = db.ExecuteReader(command);
            response.Result = read.Reader(x => x.ToComboGenerico());
            response.Success = response.Result.Any();

            return response;
        }

        public Response ManttoSucGuardar(MantenimientoSucursal mantenimiento, int idUsuario)//int idCarga
        {
            var response = new ResponseList<Invenadro>();

            var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");

            var command = db.GetStoredProcCommand("mta.sp_Sucursal_Grupo");


            db.AddInParameter(command, "@idSucursal", DbType.Int32, mantenimiento.IdSucursal);
            db.AddInParameter(command, "@idGrupo", DbType.Int32, mantenimiento.Grupo);
            db.AddInParameter(command, "@aplicaInvenadro", DbType.Boolean, mantenimiento.Invenadro);
            db.AddInParameter(command, "@aplicaPVD", DbType.Boolean, mantenimiento.PVD);
            db.AddInParameter(command, "@aplicaNegados", DbType.Boolean, mantenimiento.Negados);
            db.AddInParameter(command, "@usuario", DbType.Int32, idUsuario);

            command.CommandTimeout = 0;


            var exito = db.ExecuteNonQuery(command);
            response.Success = exito != default;

            return response;
        }

        public ResponseList<MantenimientoSucursal> MantenimientoGet(MantenimientoSucursal sucursal)//int idCarga
        {
            var response = new ResponseList<MantenimientoSucursal>();

            var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");

            var command = db.GetStoredProcCommand("mta.sp_getManttoSuc");

            db.AddInParameter(command, "@idSucursal", DbType.Int32, sucursal.IdSucursal);

            command.CommandTimeout = 0;

            var read = db.ExecuteReader(command);
            response.Result = read.Reader(x => x.ToMantenimientoSucursal());
            response.Success = response.Result.Any();

            return response;
        }

        public Response GuardarSucProveedor(DataTable DtSucrProveedor)//int idCarga
        {
            var response = new Response();

            var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");

            var command = db.GetStoredProcCommand("MTA.[sp_GRFP_Load_mant_sucprov]");
            command.CommandTimeout = 0;
            command.Parameters.Add(new SqlParameter("@Tipo_Tipo_MANT_SUCPROV", SqlDbType.Structured) { Value = DtSucrProveedor });

            var exito = db.ExecuteNonQuery(command);
            response.Success = exito != default;

            return response;
        }

        public ResponseList<SucursalProveedorExt> ObtenerSucProv(SucursalProveedor DtSucrProveedor)//int idCarga
        {
            var response = new ResponseList<SucursalProveedorExt>();

            var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");

            var command = db.GetStoredProcCommand("mta.sp_GRFP_get_man_sucprov");
            command.CommandTimeout = 0;

            //db.AddInParameter(command, "@id_proveedor", DbType.Int32, DtSucrProveedor.IdProveedor);
            db.AddInParameter(command, "@id_suc", DbType.Int32, DtSucrProveedor.IdSucursal);
            //db.AddInParameter(command, "@agencia", DbType.Int32, DtSucrProveedor.IdAgencia);
            var read = db.ExecuteReader(command);
            response.Result = read.Reader(x => x.ToSucursalProveedorExt());
            if (response.Result.Count == 0)
            {
                response.Message = "No hay Registros";
            }
            response.Success = response.Result.Any();

            return response;
        }

        public ResponseList<SucursalProveedorExt> ConsultaMasiva(DataTable DtSucrProveedor)//int idCarga
        {
            var response = new ResponseList<SucursalProveedorExt>();

            var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");

            var command = db.GetStoredProcCommand("mta.sp_GRFP_get_man_sucprov_masivo");
            command.CommandTimeout = 0;

            command.Parameters.Add(new SqlParameter("@Tipo_Tipo_MANT_SUCPROV", SqlDbType.Structured) { Value = DtSucrProveedor });
            var read = db.ExecuteReader(command);
            response.Result = read.Reader(x => x.ToSucursalProveedorExt());
            if (response.Result.Count == 0)
            {
                response.Message = "No hay Registros";
            }
            response.Success = response.Result.Any();

            return response;
        }
    }
}
