using PortalGRFP.Data.Modulos;
using PortalGRFP.Entities.Common;
using PortalGRFP.Entities.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalGRFP.Business.Modulos
{
    public class ModuloBusiness
    {
        private readonly ModuloData moduloData;

        public ModuloBusiness()
        {
            moduloData = new ModuloData();
        }

        public ResponseList<Modulo> GetList()
        {
            var response = new ResponseList<Modulo>();
            try
            {
                response = moduloData.GetList();
            }
            catch (Exception ex)
            {
                response.Message = "Error al realizar la consulta de información." + ex.Message;
            }
            return response;
        }
        public ResponseList<ModuloAccion> GetListByPadre(int IdPadre, int TipoOperacion, int IdPerfil)
        {
            var response = new ResponseList<ModuloAccion>();
            try
            {
                response = moduloData.GetListByPadre(IdPadre, TipoOperacion, IdPerfil);
            }
            catch (Exception ex)
            {
                response.Message = "Error al realizar la consulta de información." + ex.Message;
            }
            return response;
        }

    }
}
