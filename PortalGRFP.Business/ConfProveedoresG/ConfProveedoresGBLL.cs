using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PortalGRFP.Entities.Common;
using PortalGRFP.Data.ConfProveedoresG;
using System.Web.Mvc;

namespace PortalGRFP.Business.ConfProveedoresG
{
    public class ConfProveedoresGBLL
    {
        private readonly ConfProveedoresGData dConfProv = new ConfProveedoresGData();

        public List<SelectListItem> GetProveedoresListBLL()
        {
            var lst = dConfProv.GetProveedoresListData();

            return lst;
        }



        //public ConfiguracionesProveedorModel GetProveedorBLL(string idProveedor)
        //{
        //    var lst = dConfProv.GetProveedorData(idProveedor);

        //    return lst;
        //}



        public int UIProveedorBLL(ConfProveedoresModel model)
        {
            //if (model.AplicaMinimo &&(model.minimoPiezas == 0 || model.MinimoMonto == 0)  )
            //{
            //    return 
            //}

            var response = dConfProv.UIProveedorData(model);

            return response;
        }


        //public int UIProveedorBLL(ConfiguracionProveedorGeneralModel model)
        //{
        //    //if (model.AplicaMinimo &&(model.minimoPiezas == 0 || model.MinimoMonto == 0)  )
        //    //{
        //    //    return 
        //    //}

        //    var response = dConfProv.UIProveedorData(model);

        //    return response;
        //}




        public List<ConfProveedoresModel> GetProveedoresBLL(int flag)
        {
            var lst = dConfProv.GetProveedoresData(flag).ToList();

            return lst;
        }
    }
}
