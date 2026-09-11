using Microsoft.Practices.EnterpriseLibrary.Data;
using PortalGRFP.Data.Extensions;
using PortalGRFP.Entities.Models;
using PortalGRFP.Entities.Response;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalGRFP.Data.Users
{
    public class UserData
    {
        public List<CatUsersModel> GetAll()
        {
            var response = new List<CatUsersModel>();
            var db = DatabaseFactory.CreateDatabase("DBPORTALRCB");

            var command = db.GetStoredProcCommand("SP_CAT_USUARIO_GETALL");
            using (IDataReader dr = db.ExecuteReader(command))
            {
                while (dr.Read())
                {
                    response.Add(new CatUsersModel
                    { Correo = dr.Get<string>("Correo"), idPerfil = dr.Get<int>("IdPerfil"), IdUsusario = dr.Get<int>("IdUsuario"),
                     Nombre = dr.Get<string>("Nombre"), Perfil = dr.Get<string>("Perfil"), ApPaterno = dr.Get<string>("APaterno"),
                     ApMaterno = dr.Get<string>("AMaterno"), Activo = dr.Get<bool>("Activo"), FechaAlta = dr.Get<DateTime>("FechaRegistro")
                    });
                    
                }
            }

            return response;
        }



        public int InsertUser(CatUsersModel model)
        {
            var id = 0;
            try
            {
                var db = DatabaseFactory.CreateDatabase("DBPORTALRCB");
                var command = db.GetStoredProcCommand("SP_CAT_USUARIO_INSERT");
                command.Parameters.Add(new SqlParameter("@Correo", model.Correo));
                command.Parameters.Add(new SqlParameter("@idPerfil", model.Perfil));
                command.Parameters.Add(new SqlParameter("@nombre", model.Nombre));
                command.Parameters.Add(new SqlParameter("@aPaterno", model.ApPaterno));
                command.Parameters.Add(new SqlParameter("@aMaterno", model.ApMaterno));
                command.Parameters.Add(new SqlParameter("@activo", model.Activo));
                command.Parameters.Add(new SqlParameter("@idUsuarioRegistro", model.UsuarioALta));
                command.Parameters.Add(new SqlParameter("@fechaRegistro", model.FechaAlta));
                var dr = db.ExecuteReader(command);
                while (dr.Read())
                {
                    id = dr.Get<int>("IdUsuario");
                }
                model.IdUsusario = id;
                SetGrupoUsuario(model, 1);


                return id;
            }
            catch
            {
                return id;
            }

        }

        public List<Perfil> GetAllPerfiles()
        {
            var list = new List<Perfil>();
            var db = DatabaseFactory.CreateDatabase("DBPORTALRCB");

            var command = db.GetStoredProcCommand("SP_GRFP_CAT_PERFIL_GETALL");
            using (IDataReader dr = db.ExecuteReader(command))
            {
                while (dr.Read())
                {
                    list.Add(new Perfil { IdPerfil = dr.Get<int>("IdPerfil"), Nombre = dr.Get<string>("Nombre") });
                }
            }

            return list;
        }

        public bool UpdateUser(CatUsersModel model)
        {
            
            try
            {
                var db = DatabaseFactory.CreateDatabase("DBPORTALRCB");
                var command = db.GetStoredProcCommand("SP_CAT_USUARIO_UPDATE");
                command.Parameters.Add(new SqlParameter("@id", model.IdUsusario));
                command.Parameters.Add(new SqlParameter("@Correo", model.Correo));
                //command.Parameters.Add(new SqlParameter("@idPerfil", model.idPerfil));
                command.Parameters.Add(new SqlParameter("@idPerfil", model.Perfil));
                command.Parameters.Add(new SqlParameter("@nombre", model.Nombre));
                command.Parameters.Add(new SqlParameter("@aPaterno", model.ApPaterno));
                command.Parameters.Add(new SqlParameter("@aMaterno", model.ApMaterno));
                command.Parameters.Add(new SqlParameter("@activo", model.Activo));
                command.Parameters.Add(new SqlParameter("@idUsuarioActualiza", model.UsuarioALta));
                var dr = db.ExecuteReader(command);

                CleanListGrupUser(model,1);



                return true;
            }
            catch(Exception e)
            {
                return false;
            }

        }





        public static void CleanListGrupUser(CatUsersModel model, int flag)
        {

            try
            {
                var db = DatabaseFactory.CreateDatabase("DBPORTALRCB");
                //var command = db.GetStoredProcCommand("mta.SP_GRFP_CONF_UI_GRUPO_USUARIO");

                if (model.Grupos != null)
                {
                    
                        var command = db.GetStoredProcCommand("mta.SP_GRFP_CONF_DEL_GRUPO_USUARIO");

                        command.Parameters.Add(new SqlParameter("@IdUsuario", model.IdUsusario));
                        var dr = db.ExecuteNonQuery(command);
                    
                }

                SetGrupoUsuario(model, 1);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message.ToString());

            }
        }

        public static void SetGrupoUsuario(CatUsersModel model,int flag)
        {

            try
            {
                var db = DatabaseFactory.CreateDatabase("DBPORTALRCB");
                //var command = db.GetStoredProcCommand("mta.SP_GRFP_CONF_UI_GRUPO_USUARIO");

                if (model.Grupos != null)
                {
                    foreach (var item in model.Grupos)
                    {
                        var command = db.GetStoredProcCommand("mta.SP_GRFP_CONF_UI_GRUPO_USUARIO");

                        command.Parameters.Add(new SqlParameter("@flag", flag));
                        command.Parameters.Add(new SqlParameter("@IdGrupo", item.Id));
                        command.Parameters.Add(new SqlParameter("@Correo", model.Correo));
                        command.Parameters.Add(new SqlParameter("@Usuario", model.UsuarioALta));

                        var dr = db.ExecuteNonQuery(command);
                    }
                }



            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message.ToString());
                
            }
        }
    }
}
