using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PortalGRFP.Entities.Common;
using PortalGRFP.Entities.Models;

using Microsoft.Practices.EnterpriseLibrary.Data;
using PortalGRFP.Data.Extensions;
using System.Data;
using System.Data.SqlClient;
using PortalGRFP.Entities.Response;

namespace PortalGRFP.Data.Mantenimiento
{
    public class GruposData
    {
        public List<GruposModel> GetGruposData(int flag, int id)
        {
            var response = new List<GruposModel>();

            var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");
            var command = db.GetStoredProcCommand("mta.SP_GRFP_CONF_GET_GRUPOS");

            command.Parameters.Add(new SqlParameter("@flag", flag));
            command.Parameters.Add(new SqlParameter("@id", id));


            using (IDataReader dr = db.ExecuteReader(command))
            {
                while (dr.Read())
                {
                    response.Add(new GruposModel
                    {
                        Id = dr.Get<int>("Id"),
                        Nombre = dr.Get<string>("Nombre"),
                        Estatus = dr.Get<bool>("Estatus")
                    });

                }
            }

            return response;
        }
        public List<CatUsersModel.Grupo> GetGruposDataUsuario(int flag, int id)
        {
            var response = new List<CatUsersModel.Grupo>();

            var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");
            var command = db.GetStoredProcCommand("mta.SP_GRFP_CONF_GET_GRUPOS");

            command.Parameters.Add(new SqlParameter("@flag", flag));
            command.Parameters.Add(new SqlParameter("@id", id));


            using (IDataReader dr = db.ExecuteReader(command))
            {
                while (dr.Read())
                {
                    response.Add(new CatUsersModel.Grupo
                    {
                        Id = dr.Get<string>("Id"),
                        Nombre = dr.Get<string>("Nombre"),
                        Estatus = dr.Get<bool>("Estatus")
                    });

                }
            }

            return response;
        }

        public List<CatUsersModel.Grupo> GetGruposUsuarioSelectedData(int id)
        {
            var response = new List<CatUsersModel.Grupo>();

            var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");
            var command = db.GetStoredProcCommand("mta.SP_GRFP_CONF_GET_GRUPO_USUARIO");

            command.Parameters.Add(new SqlParameter("@idusuario", id));


            using (IDataReader dr = db.ExecuteReader(command))
            {
                while (dr.Read())
                {
                    response.Add(new CatUsersModel.Grupo
                    {
                        Id = dr.Get<string>("Id"),
                        Nombre = dr.Get<string>("Nombre"),
                        Estatus = dr.Get<bool>("Estatus")
                    });

                }
            }

            return response;
        }

        public int UIGruposData(GruposModel model)
        {
            var id = 0;
            try
            {


                int flag;

                if (model.Id > 0) flag = 2;
                else flag = 1;

                var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");
                var command = db.GetStoredProcCommand("mta.SP_GRFP_CONF_UI_GRUPOS");

                command.Parameters.Add(new SqlParameter("@flag", flag));
                command.Parameters.Add(new SqlParameter("@Id", model.Id));
                command.Parameters.Add(new SqlParameter("@Nombre", model.Nombre));
                command.Parameters.Add(new SqlParameter("@Estatus", model.Estatus));
                command.Parameters.Add(new SqlParameter("@Usuario", model.IdUsuario));


                var dr = db.ExecuteReader(command);
                while (dr.Read())
                {
                    id = dr.Get<int>("done");
                }

                return id;
            }
            catch (ExecutionEngineException e)
            {

                return id;
            }

        }

        public int UIGruposusuarioData(int idgrupo, int idusuario, int usuario)
        {
            var id = 0;
            try
            {


                int flag;

                if (idgrupo > 0) flag = 2;
                else flag = 1;

                var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");
                var command = db.GetStoredProcCommand("mta.SP_GRFP_CONF_UI_GRUPOS");

                //command.Parameters.Add(new SqlParameter("@flag", flag));
                //command.Parameters.Add(new SqlParameter("@Id", model.Id));
                //command.Parameters.Add(new SqlParameter("@Nombre", model.Nombre));
                //command.Parameters.Add(new SqlParameter("@Estatus", model.Estatus));
                //command.Parameters.Add(new SqlParameter("@Usuario", model.IdUsuario));


                var dr = db.ExecuteReader(command);
                while (dr.Read())
                {
                    id = dr.Get<int>("done");
                }

                return id;
            }
            catch (ExecutionEngineException e)
            {

                return id;
            }        }

        public ResponseList<Combo3> Get_Grupos_Usuario(int accion)
        {
            var response = new ResponseList<Combo3>();

            var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");
            var command = db.GetStoredProcCommand("mta.sp_AutocompleteProveedores");//solo para inicializar

            db.AddInParameter(command, "@accion", DbType.Int32, accion);

            command.CommandTimeout = 0;

            var read = db.ExecuteReader(command);
            response.Result = read.Reader(x => x.ToCombo3());
            response.Success = response.Result.Any();

            return response;

        }
    }
}
