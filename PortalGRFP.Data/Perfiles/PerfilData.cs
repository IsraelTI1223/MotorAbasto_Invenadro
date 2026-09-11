using Microsoft.Practices.EnterpriseLibrary.Data;
using PortalGRFP.Entities.Response;
using PortalGRFP.Entities.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PortalGRFP.Entities.Parameters;
using PortalGRFP.Data.Extensions;
using System.Runtime.InteropServices;

namespace PortalGRFP.Data.Perfiles
{
    public class PerfilData
    {
        public ResponseList<Perfil> GetAllPerfil(int Activo)
        {
            var response = new ResponseList<Perfil>();
            var factory = new DatabaseProviderFactory();
            var db = factory.Create("DBPORTALRCB");
            var command = db.GetStoredProcCommand("SP_GRFP_CAT_PERFIL_GETALL");
            db.AddInParameter(command, "@Activo", DbType.Int32, Activo);
            var read = db.ExecuteReader(command);
            response.Result = read.Reader(x => x.ToPerfil());
            response.Success = response.Result.Any();
            return response;
        }

        public ResponseList<Perfil> GetPerfilById(int PerfilId)
        {
            var response = new ResponseList<Perfil>();
            var factory = new DatabaseProviderFactory();
            var db = factory.Create("DBPORTALRCB");
            var command = db.GetStoredProcCommand("SP_GRFP_CAT_PERFIL_BYID");
            db.AddInParameter(command, "@IdPerfil", DbType.Int32, PerfilId);
            var read = db.ExecuteReader(command);
            response.Result = read.Reader(x => x.ToPerfil());
            response.Success = response.Result.Any();
            return response;
        }

        public Response InsertCTRLPERFIL(PerfilParameter request)
        {
            var IdPerfilExiste = 0;
            var response = new Response();

            var db = DatabaseFactory.CreateDatabase("DBPORTALRCB");

            var command = db.GetStoredProcCommand("SP_CTRL_PERFIL_INSERT");
            command.CommandTimeout = 0;
            db.AddInParameter(command, "@Nombre", DbType.String, request.Nombre);
            db.AddInParameter(command, "@IdUsuario", DbType.Int32, request.IdUsuario);
            command.Parameters.Add(new SqlParameter("@Modulos", SqlDbType.Structured) { Value = request.Modulos });

            //var result = db.ExecuteNonQuery(command);
            var dr = db.ExecuteReader(command);
            while (dr.Read())
            {
                IdPerfilExiste = dr.Get<int>("IdPerfil");
            }

            if (IdPerfilExiste == 0)
            {
                response.Success = true;
            }
            else
            {
                response.Success = false;
                response.Message = IdPerfilExiste.ToString();
            }
            //response.Success = result != 0;

            return response;
        }

        public ResponseList<CtrlPerfil> GetAllCtrlPerfilMod(int IdPerfil)
        {
            var response = new ResponseList<CtrlPerfil>();
            var factory = new DatabaseProviderFactory();
            var db = factory.Create("DBPORTALRCB");
            var command = db.GetStoredProcCommand("SP_CTRL_PERFIL_GETMOD");
            db.AddInParameter(command, "@IdPerfil", DbType.String, IdPerfil);
            var read = db.ExecuteReader(command);
            response.Result = read.Reader(x => x.ToCtrlPerfil());
            response.Success = response.Result.Any();
            return response;
        }

        public Response UpdateCTRLPERFIL(PerfilParameter request)
        {
            var response = new Response();

            var db = DatabaseFactory.CreateDatabase("DBPORTALRCB");

            var command = db.GetStoredProcCommand("SP_CAT_CTRL_PERFIL_UPDATE");
            command.CommandTimeout = 0;
            db.AddInParameter(command, "@IdPerfil", DbType.Int32, request.IdPerfil);
            db.AddInParameter(command, "@Nombre", DbType.String, request.Nombre);
            command.Parameters.Add(new SqlParameter("@Modulos", SqlDbType.Structured) { Value = request.Modulos });
            db.AddInParameter(command, "@Activo", DbType.Boolean, request.Activo);
            db.AddInParameter(command, "@IdUsuario", DbType.Int32, request.IdUsuario);

            var result = db.ExecuteNonQuery(command);
            response.Success = result != 0;

            return response;
        }

        public List<PerfilModuloAccion> GetPerfilModuloAccion(int TipoOperacion, int IdPerfil)
        {
            List<PerfilModuloAccion> lstPerfilModuloAccion = new List<PerfilModuloAccion> { };
            var db = DatabaseFactory.CreateDatabase("DBPORTALRCB");
            var command = db.GetStoredProcCommand("SP_GRFP_CAT_MODULO_ACCION_PERFIL");
            db.AddInParameter(command, "@TipoOperacion", DbType.Int32, TipoOperacion);
            db.AddInParameter(command, "@IdPerfil", DbType.Int32, IdPerfil);
            var read = db.ExecuteReader(command);
            lstPerfilModuloAccion = read.Reader(x => x.ToPerfilModuloAccion());
            //response.Success = response.Result.Any();
            return lstPerfilModuloAccion;
        }

    }
}
