using Microsoft.Practices.EnterpriseLibrary.Data;
using PortalGRFP.Entities.Common.Capacity;
using PortalGRFP.Entities.Response;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PortalGRFP.Data.Extensions;

namespace PortalGRFP.Data.Capacity
{
    public class Capacity
    {
        public cCapacity GetMarca(int flag, int Usuario, string Cadena)
        {
            var response = new cCapacity();

            var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");
            var command = db.GetStoredProcCommand("mta.SP_GRFP_SUCURSALyCARGA_CAPACITY");

            command.Parameters.Add(new SqlParameter("@flag", flag));
            command.Parameters.Add(new SqlParameter("@usuario", Usuario));
            command.Parameters.Add(new SqlParameter("@cadenas", Cadena));
            //command.Parameters.Add(new SqlParameter("@tblLayoutCarga", LayoutCarga));


            using (IDataReader dr = db.ExecuteReader(command))
            {
                while (dr.Read())
                {
                    response.comboCapacity.Add(new ComboCapacity
                    {
                        Value = dr.Get<string>("value"),
                        Descripcion = dr.Get<string>("Descripcion")
                    });

                }
            }



            response.catCapacity = SelectedCapacity(flag).ToList();


            ////var command2 = db.GetStoredProcCommand("mta.SP_GRFP_Cat_CAPACITY");

            ////command2.Parameters.Add(new SqlParameter("@flag", flag));

            ////using (IDataReader dr = db.ExecuteReader(command2))
            ////{
            ////    while (dr.Read())
            ////    {
            ////        response.catCapacity.Add(new tblCatCapacity
            ////        {
            ////            Id = dr.Get<int>("Id"),
            ////            TipoCapacity = dr.Get<string>("TipoCapacity"),
            ////            Invenadro = dr.Get<string>("Invenadros"),
            ////        });
            ////    }
            ////}

            return response;
        }

        public responseCapacity setMarca(int flag, int Usuario, string marca, string LayoutCarga)
        {
            var response = new responseCapacity();

            var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");
            var command = db.GetStoredProcCommand("mta.SP_GRFP_SUCURSALyCARGA_CAPACITY");

            //command.Parameters.Add(new SqlParameter("@flag", flag));
            //command.Parameters.Add(new SqlParameter("@usuario", Usuario));
            //command.Parameters.Add(new SqlParameter("@cadenas", marca));
            //command.Parameters.Add(new SqlParameter("@tblLayoutCarga", LayoutCarga));


            command.CommandTimeout = 0;
            var parameters = new[]
            {
                new SqlParameter("@flag", flag),
                new SqlParameter("@usuario", Usuario),
                new SqlParameter("@cadenas", marca),
                new SqlParameter("@tblLayoutCarga", LayoutCarga)
            };
            command.Parameters.AddRange(parameters);

            var dt = db.ExecuteDataSet(command);

            //using (IDataReader dr = db.ExecuteReader(command))
            //{
            //    while (dr.Read())
            //    {
            //        response.respuesta.Success = dr.Get<bool>("Success");
            //        response.respuesta.Message = dr.Get<string>("Message");
            //    }
            //}


            if (dt != null)
            {
                foreach (DataRow fila in dt.Tables[0].Rows)
                {

                    response.respuesta.Success = !DBNull.Value.Equals(fila["Success"]) ? Convert.ToBoolean(fila["Success"].ToString()) : false;
                    response.respuesta.Message = !DBNull.Value.Equals(fila["Message"]) ? fila["Message"].ToString() : string.Empty;
                }

                if (response.respuesta.Success)
                {
                    foreach (DataRow fila in dt.Tables[1].Rows)
                    {
                        var model = new tblValida();
                        model.SKUAceptados = !DBNull.Value.Equals(fila["SKUAceptados"]) ? Convert.ToInt32(fila["SKUAceptados"].ToString()) : 0;
                        model.SKURechazados = !DBNull.Value.Equals(fila["SKURechazados"]) ? Convert.ToInt32(fila["SKURechazados"].ToString()) : 0;
                        model.Marca = !DBNull.Value.Equals(fila["Marca"]) ? fila["Marca"].ToString() : string.Empty;
                        response.listValida.Add(model);
                    }
                }

                if (response.respuesta.Success)
                {
                    foreach (DataRow fila in dt.Tables[2].Rows)
                    {
                        var model = new tblReporte();
                        model.IdSucursal = !DBNull.Value.Equals(fila["IdSucursal"]) ? Convert.ToInt64(fila["IdSucursal"].ToString()) : 0;
                        model.SKU = !DBNull.Value.Equals(fila["SKU"]) ? fila["SKU"].ToString() : string.Empty;
                        model.Producto = !DBNull.Value.Equals(fila["Producto"]) ? fila["Producto"].ToString() : string.Empty;
                        model.Capacity = !DBNull.Value.Equals(fila["Capacity"]) ? Convert.ToInt16(fila["Capacity"].ToString()) : 0;
                        model.StatusCarga =!DBNull.Value.Equals(fila["EstatusCarga"])? fila["EstatusCarga"].ToString() : string.Empty;
                        response.listReportes.Add(model);
                    }
                }
            }

            return response;
        }

        public CapacityModel setCatCapacity(int flag, string Nombre, bool Invenadro, int Usuario, string idinput)
        {
            var response = new CapacityModel();

            var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");
            var command = db.GetStoredProcCommand("mta.SP_GRFP_Cat_CAPACITY");

            command.Parameters.Add(new SqlParameter("@flag", flag));
            command.Parameters.Add(new SqlParameter("@TipoCapacity", Nombre));
            command.Parameters.Add(new SqlParameter("@Invenadro", Invenadro));
            command.Parameters.Add(new SqlParameter("@usuario", Usuario));
            command.Parameters.Add(new SqlParameter("@idinput", idinput));
            //command.Parameters.Add(new SqlParameter("@tblLayoutCarga", LayoutCarga));


            using (IDataReader dr = db.ExecuteReader(command))
            {
                while (dr.Read())
                {

                    response.Success = dr.Get<bool>("Success");
                    response.Message = dr.Get<string>("Message");

                }
            }

            return response;
        }

        public CapacityModel DelCatCapacity(int flag, int id)
        {
            var response = new CapacityModel();

            var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");
            var command = db.GetStoredProcCommand("mta.SP_GRFP_Cat_CAPACITY");

            command.Parameters.Add(new SqlParameter("@flag", flag));
            command.Parameters.Add(new SqlParameter("@TipoCapacity","" ));
            command.Parameters.Add(new SqlParameter("@Invenadro", ""));
            command.Parameters.Add(new SqlParameter("@usuario", ""));
            command.Parameters.Add(new SqlParameter("@id", id));
            //command.Parameters.Add(new SqlParameter("@tblLayoutCarga", LayoutCarga));


            using (IDataReader dr = db.ExecuteReader(command))
            {
                while (dr.Read())
                {

                    response.Success = dr.Get<bool>("Success");
                    response.Message = dr.Get<string>("Message");

                }
            }

            return response;
        }

        public CapacityModel DelCapacity(int flag, string storeId, int usuario)
        {
            var response = new CapacityModel();

            var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");
            var command = db.GetStoredProcCommand("mta.SP_GRFP_SUCURSALyCARGA_CAPACITY");

            command.Parameters.Add(new SqlParameter("@flag", flag));
            command.Parameters.Add(new SqlParameter("@usuario", usuario));
            command.Parameters.Add(new SqlParameter("@cadenas", ""));
            command.Parameters.Add(new SqlParameter("@tblLayoutCarga",""));
            command.Parameters.Add(new SqlParameter("@storeId", storeId));


            using (IDataReader dr = db.ExecuteReader(command))
            {
                while (dr.Read())
                {

                    response.Success = dr.Get<bool>("Success");
                    response.Message = dr.Get<string>("Message");

                }
            }

            return response;
        }

        public List<tblCatCapacity> SelectedCapacity(int flag)
        {
            List<tblCatCapacity> resp = new List<tblCatCapacity>();
            
            var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");
            var command2 = db.GetStoredProcCommand("mta.SP_GRFP_Cat_CAPACITY");

            command2.Parameters.Add(new SqlParameter("@flag", flag));

            using (IDataReader dr = db.ExecuteReader(command2))
            {
                while (dr.Read())
                {
                    resp.Add(new tblCatCapacity
                    {
                        Id = dr.Get<int>("Id"),
                        TipoCapacity = dr.Get<string>("TipoCapacity"),
                        Invenadro = dr.Get<string>("Invenadros"),
                    });
                }
            }

            return resp;

        }

        public List<tblCapacity> ReporteCapacity(int flag, string tipoCapacity, string feini, string fefin)
        {
            List<tblCapacity> resp = new List<tblCapacity>();

            var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");
            var command = db.GetStoredProcCommand("mta.SP_GRFP_Cat_CAPACITY");
            command.Parameters.Add(new SqlParameter("@flag", flag));
            command.Parameters.Add(new SqlParameter("@TipoCapacity", tipoCapacity));
            command.Parameters.Add(new SqlParameter("@Invenadro", ""));
            command.Parameters.Add(new SqlParameter("@usuario", ""));
            command.Parameters.Add(new SqlParameter("@id", 0));
            command.Parameters.Add(new SqlParameter("@feini", feini));
            command.Parameters.Add(new SqlParameter("@fefin", fefin));


            using (IDataReader dr = db.ExecuteReader(command))
            {
                while (dr.Read())
                {
                    resp.Add(new tblCapacity
                    {
                        IdSucrusal = dr.Get<int>("IdSucursal"),
                        SKU = dr.Get<string>("SKU"),
                        Capacity = dr.Get<int>("Capacity"),
                        FechaIni = dr.Get<string>("FechaIni"),
                        FechaFin = dr.Get<string>("FechaFin"),
                        idMotivo = dr.Get<int>("idMotivo"),
                        User = dr.Get<int>("o_user"),
                    });
                }
            }

            return resp;

        }
    }
}
