using PortalGRFP.Data.Users;
using PortalGRFP.Entities.Models;
using PortalGRFP.Entities.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalGRFP.Business.Users
{
    
    public class UserConfiguration
    {
        private readonly UserData _userData = new UserData();
        
        public List<CatUsersModel> GetUsuarios()
        {
            var listUsrs = _userData.GetAll().ToList();
            
            return listUsrs;
        }

        public int InsertUser(CatUsersModel model)
        {
            return _userData.InsertUser(model);
        }

        public List<Perfil> GetPerfilAll()
        {
            var listPerfil = _userData.GetAllPerfiles();
            
            return listPerfil;
        }

        public bool UpdateUser(CatUsersModel model)
        {
            return _userData.UpdateUser(model);
        }
    }
}
