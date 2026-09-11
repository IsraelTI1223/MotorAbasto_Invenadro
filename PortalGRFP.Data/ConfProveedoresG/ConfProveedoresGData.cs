using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PortalGRFP.Entities.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using PortalGRFP.Data.Extensions;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;
using static System.Net.Mime.MediaTypeNames;

namespace PortalGRFP.Data.ConfProveedoresG
{
    public class ConfProveedoresGData
    {

        public List<SelectListItem> GetProveedoresListData()
        {
  
            var response = new List<SelectListItem>();

            var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");
            var command = db.GetStoredProcCommand("mta.SP_GRFP_GET_CAT_PROVEEDORES_ERP");

            using (IDataReader dr = db.ExecuteReader(command))
            {
                while (dr.Read())
                {
                    response.Add(new SelectListItem
                    {
                        Value = dr.Get<string>("Id"),
                        Text = dr.Get<string>("Nombre")
                    });

                }
            }

            return response;
        }



      /*  public ConfiguracionesProveedorModel GetProveedorData(string idProveedor)
        {
            string[] data = idProveedor.Split('|');


            var response = new ConfiguracionesProveedorModel();

            var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");
            var command = db.GetStoredProcCommand("mta.SP_GRFP_CONF_GET_PROVEEDORI");


            command.Parameters.Add(new SqlParameter("@Emp_id", data[0]));
            command.Parameters.Add(new SqlParameter("@IdProveedor", data[1]));

            using (IDataReader dr = db.ExecuteReader(command))
            {
                while (dr.Read())
                {
                    response.Id = dr.Get<int>("Id");
                    response.IdEmpresaERP = dr.Get<int>("IdEmpresaERP");
                    response.IdProveedorPos = dr.Get<int>("IdProveedorPos");
                    response.Nombre = dr.Get<string>("Nombre");
                    response.AplicaAgencia = dr.Get<bool>("AplicaAgencia");
                    response.AplicaMinimo = dr.Get<bool>("AplicaMinimo");
                    response.MinimoMonto = dr.Get<decimal>("MinimoMonto");
                    response.minimoPiezas = dr.Get<int>("minimoPiezas");
                    response.DiasVigenciaOC = dr.Get<int>("DiasVigenciaOC");
                    response.IdUsuarioAlta = dr.Get<int>("IdUsuarioAlta");
                    response.IdProveedorERP = dr.Get<int>("IdProveedorERP");



                }
            }

            return response;
        }*/




        public int UIProveedorData(ConfProveedoresModel model)
        {
            var response = 0;
            try
            {
                int flag;

                if (model.Nombre == null)
                {
                    flag = 3;
                    model.Nombre = "delete";
                    model.IdProveedorERP = "";
                    model.RFC = "";
                    //model.IdProveedorPos = "";

                }
                else
                {
                    if (model.Id > 0) flag = 2;
                    else flag = 1;
                }

                //if (model.Id > 0) flag = 2;
                //else flag = 1;

                var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");
                var command = db.GetStoredProcCommand("mta.SP_GRFP_CONF_UI_PROVEEDORES");

                command.Parameters.Add(new SqlParameter("@flag", flag));
                command.Parameters.Add(new SqlParameter("@ID", model.Id));
                //command.Parameters.Add(new SqlParameter("@IdEmpresaERP", model.IdEmpresaERP));
                command.Parameters.Add(new SqlParameter("@IdProveedorERP", model.IdProveedorERP));
                //command.Parameters.Add(new SqlParameter("@IdProveedorPos", model.IdProveedorPos));
                command.Parameters.Add(new SqlParameter("@Nombre", model.Nombre));
                command.Parameters.Add(new SqlParameter("@RFC", model.RFC));
                command.Parameters.Add(new SqlParameter("@AplicaAgencia", model.AplicaAgencia));
                command.Parameters.Add(new SqlParameter("@AplicaMinimo", model.AplicaMinimo));
                command.Parameters.Add(new SqlParameter("@MinimoMonto", model.MinimoMonto));
                command.Parameters.Add(new SqlParameter("@minimoPiezas", model.minimoPiezas));
                command.Parameters.Add(new SqlParameter("@DiasVigenciaOC", model.DiasVigenciaOC));
                command.Parameters.Add(new SqlParameter("@MinimoSurtido", model.MinimoSurtido));
                command.Parameters.Add(new SqlParameter("@IdUsuarioAlta", model.IdUsuarioAlta));

                var dr = db.ExecuteReader(command);
                while (dr.Read())
                {
                    response = dr.Get<int>("done");
                }

                return response;
            }
            catch (ExecutionEngineException e)
            {

                return response;
            }

        }



        public List<ConfProveedoresModel> GetProveedoresData(int flag)
        {
            


            var response = new List<ConfProveedoresModel>();

            var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");
            var command = db.GetStoredProcCommand("mta.SP_GRFP_CONF_GET_PROVEEDOR_UI");
            command.Parameters.Add(new SqlParameter("@flag", flag));

            using (IDataReader dr = db.ExecuteReader(command))
            {
                while (dr.Read())
                {

                    response.Add(new ConfProveedoresModel
                    {
                        
                    Id = dr.Get<int>("Id"),
                    //IdEmpresaERP = dr.Get<int>("IdEmpresaERP"),
                    //IdProveedorPos = dr.Get<string>("IdProveedorPos"),
                    Nombre = dr.Get<string>("Nombre"),
                    RFC = dr.Get<string>("RFC"),
                    AplicaAgencia = dr.Get<bool>("AplicaAgencia"),
                    AplicaMinimo = dr.Get<bool>("AplicaMinimo"),
                    MinimoMonto = dr.Get<decimal>("MinimoMonto"),
                    minimoPiezas = dr.Get<int>("minimoPiezas"),
                    DiasVigenciaOC = dr.Get<int>("DiasVigenciaOC"),
                    IdUsuarioAlta = dr.Get<int>("IdUsuarioAlta"),
                    IdProveedorERP = dr.Get<string>("IdProveedorERP"),
                    MinimoSurtido = dr.Get<int>("MinimoSurtido")
                    });


                }
            }

            return response;
        }

    }
}
