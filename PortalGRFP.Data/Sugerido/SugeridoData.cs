using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PortalGRFP.Entities.Common.Sugerido;
using PortalGRFP.Data.Sugerido;
using PortalGRFP.Entities.Response;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data;
using PortalGRFP.Data.Extensions;
using PortalGRFP.Entities.Common;

namespace PortalGRFP.Data.Sugerido
{
    public class SugeridoData
    {
        public ResponseList<ListaSugeridoModel> GetListaSugeridoData(int usuario)
        {
            var response = new ResponseList<ListaSugeridoModel>();

            var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");
            var command = db.GetStoredProcCommand("mta.SP_GRFP_CONF_GET_LIST_SUGERIDO");//solo para inicializar
            db.AddInParameter(command, "@usuario", DbType.Int32, usuario);

            command.CommandTimeout = 0;

            var read = db.ExecuteReader(command);
            response.Result = read.Reader(x => x.ToListaSugerido());
            response.Success = response.Result.Any();
            if (response.Result.Count == 0)
            {
                response.Message = "No hay registros por mostrar";
            }

            return response;
        }

        public ResponseList<ValidacionMontoSugeridoModel> GetListMontosValidarData(string idsugerido, int user)
        {
            var response = new ResponseList<ValidacionMontoSugeridoModel>();

            var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");
            var command = db.GetStoredProcCommand("mta.SP_GRFP_GET_SUG_VAL_MONTOS");

            db.AddInParameter(command, "@IdSugerido", DbType.Int64, long.Parse(idsugerido));
            db.AddInParameter(command, "@iduser", DbType.Int32, user);

            command.CommandTimeout = 0;

            var read = db.ExecuteReader(command);
            response.Result = read.Reader(x => x.ToListMontosValidar());
            response.Success = response.Result.Any();
            if (response.Result.Count == 0)
            {
                response.Message = "No hay registros por mostrar";
            }

            return response;
        }

        public ResponseList<ListaSugeridoModel> DeleteSugeridoListData(string idsugerido, int user)
        {
            var response = new ResponseList<ListaSugeridoModel>();

            var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");

            var command = db.GetStoredProcCommand("mta.SP_GRFP_CONF_DELETE_LIST_SUGERIDO");

            db.AddInParameter(command, "@idsugerido", DbType.Int64, long.Parse(idsugerido));
            db.AddInParameter(command, "@userid", DbType.Int32, user);

            command.CommandTimeout = 0;

            var exito = db.ExecuteNonQuery(command);
            response.Success = exito != default;

            return response;
        }

        public ResponseList<ComboGenerico> GetListaSucursales(Int64 idSugerido)
        {
            var response = new ResponseList<ComboGenerico>();

            var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");
            var command = db.GetStoredProcCommand("[mta].[SP_GRFP_GET_LISTSUCURSALES_NEGADOS]");//solo para inicializar
            db.AddInParameter(command, "@IDSUGERIDO", DbType.Int64, idSugerido);
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

        public Response UpdateSugeridoListData(List<ValidarMontosModel> model, int user)
        {
            var response = new Response();


            var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");

            foreach (var item in model)
            {
                var command = db.GetStoredProcCommand("mta.SP_GRFP_U_VAL_MONTOS");

                db.AddInParameter(command, "@IdSugerido", DbType.Int64, item.IdSugerido);
                db.AddInParameter(command, "@IdSucursal", DbType.Int32, item.Idsucursal);
                db.AddInParameter(command, "@Autorizado", DbType.Boolean, item.Autorizado);
                db.AddInParameter(command, "@IdUsuario", DbType.Int32, user);


                command.CommandTimeout = 0;

                var exito = db.ExecuteNonQuery(command);
            }

            response.Message = "Se Validaron los Montos del Sugerido";
            response.Success = true != default;

            return response;
        }

        public Response AplicarSugeridoData(string idsugerido, int user, int estatus)
        {
            var response = new Response();


            var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");

            var command = db.GetStoredProcCommand("mta.SP_GRFP_UPDATE_ESTATUS_SUGERIDO");

            db.AddInParameter(command, "@idsugerido", DbType.Int64, long.Parse(idsugerido));
            db.AddInParameter(command, "@userid", DbType.Int32, user);
            db.AddInParameter(command, "@estatus", DbType.Int32, estatus);


            command.CommandTimeout = 0;

            var read = db.ExecuteReader(command);


            response.Message = "Sugerido Aplicado";
            response.Success = true != default;

            return response;
        }

        public Response GetValidarPermisoData(string Correo, int IdModulo, int usuario)
        {
            var response = new Response();


            var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");

            var command = db.GetStoredProcCommand("mta.SP_GRFP_GET_VAL_MONTOS_PERMISO");

            db.AddInParameter(command, "@Correo", DbType.String, Correo);
            db.AddInParameter(command, "@IdModulo", DbType.Int32, IdModulo);
            db.AddInParameter(command, "@idusuario", DbType.Int32, usuario);


            command.CommandTimeout = 0;

            int resp = 0;
            using (IDataReader dr = db.ExecuteReader(command))
            {
                while (dr.Read())
                {
                    resp = dr.Get<int>("Autorizado");
                }
            }
            if (resp == 1)
            {
                response.Success = true;
                response.Message = "Autorizado Para Validar Montos";
            }
            else
            {
                response.Success = false;
                response.Message = "Sin Permiso Para Validar Montos";
            }


            return response;
        }

        public ResponseList<DetalladoSugerido> GetDetalleSugerido(Int64 idSugerido, int user)//int idCarga
        {
            var response = new ResponseList<DetalladoSugerido>();

            var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");

            var command = db.GetStoredProcCommand("mta.sp_grfp_getDetalleSugerido");
            command.CommandTimeout = 0;


            db.AddInParameter(command, "@idSugerido", DbType.Int64, idSugerido);
            db.AddInParameter(command, "@usuario", DbType.Int32, user);

            var read = db.ExecuteReader(command);
            response.Result = read.Reader(x => x.ToDetalladoSugerido());
            response.Success = response.Result.Any();
            return response;
        }

        public int GetAccessValidarMontosData(long idsugerido, int user)
        {
            var response = 0;
            var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");

            var command = db.GetStoredProcCommand("mta.SP_GRFP_GET_VAL_MONTOS_ACCESS");
            db.AddInParameter(command, "@IdSugerido", DbType.Int64, idsugerido);
            db.AddInParameter(command, "@IdUser", DbType.Int32, user);


            using (IDataReader dr = db.ExecuteReader(command))
            {


                while (dr.Read())
                {

                    response = dr.Get<int>("SUC_VALIDAR");

                }

            }
            return response;
        }
    }
}
