using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PortalGRFP.Entities.Common;
using PortalGRFP.Data.Mantenimiento;
using PortalGRFP.Entities.Models;
using PortalGRFP.Entities.Response;

namespace PortalGRFP.Business.Mantenimiento
{
    public class GruposBLL
    {
        private readonly GruposData oGrupos = new GruposData();

        public List<GruposModel> GetGruposBLL(int flag, int id)
        {
            var listUsrs = oGrupos.GetGruposData(flag, id).ToList();

            return listUsrs;
        }
        public List<CatUsersModel.Grupo> GetGruposBLLUsuario(int flag, int id)
        {
            var listUsrs = oGrupos.GetGruposDataUsuario(flag, id).ToList();

            return listUsrs;
        }

        public List<CatUsersModel.Grupo> GetGruposUsuarioSelectedBLL(int id)
        {
            var listUsrs = oGrupos.GetGruposUsuarioSelectedData(id).ToList();

            return listUsrs;
        }

        public int UIGruposBLL(GruposModel model)
        {
            return oGrupos.UIGruposData(model);
        }

        public int UIGruposUsuarioBLL(int idgrupo, int idusuario, int usuario)
        {
            return oGrupos.UIGruposusuarioData(idgrupo, idusuario, usuario);
        }

        public ResponseList<Combo3> GetGrupos_Usuario(int accion)
        {

            var response = new ResponseList<Combo3>();
            try
            {
                response = new GruposData().Get_Grupos_Usuario(accion);
            }
            catch (Exception ex)
            {
                response.Message = "Error al realizar la consulta de información. " + ex.Message;
            }
            return response;

        }
    }
}
