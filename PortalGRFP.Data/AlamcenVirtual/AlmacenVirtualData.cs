using Microsoft.Practices.EnterpriseLibrary.Data;
using PortalGRFP.Entities.Common.AlmacenVirtual;
using PortalGRFP.Entities.Response;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalGRFP.Data.AlmacenVirtual
{

    public class AlmacenVirtualData
    {
        public ResponseAV CargarAlmacenVXML(string serializadoXml, int opc)//int idCarga
        {
            var Modelo = new ResponseAV();

            var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");
            
            var command = db.GetStoredProcCommand("[mta].[sp_GRFP_Load_TRAN_SRC_AVIRTUALXML]");
            command.CommandTimeout = 0;
            var parameters = new[]
            {
                new SqlParameter("@opcion", opc),
                new SqlParameter("@tblAlamcenVirtual", serializadoXml)
            };
            command.Parameters.AddRange(parameters);

            var dt = db.ExecuteDataSet(command);
            if (dt != null)
            {
                foreach (DataRow fila in dt.Tables[0].Rows)
                {

                    Modelo.resp = !DBNull.Value.Equals(fila["resp"]) ? Convert.ToInt16(fila["resp"].ToString()) : 0;
                    Modelo.Success = !DBNull.Value.Equals(fila["Estatus"]) ? Convert.ToBoolean(fila["Estatus"].ToString()) : false;
                    Modelo.Message = !DBNull.Value.Equals(fila["Respuesta"]) ? fila["Respuesta"].ToString() : string.Empty;
                }


                if (Modelo.resp == 1)
                {
                    foreach (DataRow fila in dt.Tables[1].Rows)
                    {
                        var model = new tblError();
                        model.Reg = !DBNull.Value.Equals(fila["Reg"]) ? Convert.ToInt16(fila["Reg"].ToString()) : 0;
                        model.Fecha = !DBNull.Value.Equals(fila["Fecha"]) ? fila["Fecha"].ToString() : string.Empty;
                        model.Contrato = !DBNull.Value.Equals(fila["Contrato"]) ? fila["Contrato"].ToString() : string.Empty;
                        model.Dato = !DBNull.Value.Equals(fila["Dato"]) ? fila["Dato"].ToString() : string.Empty;
                        model.Descripcion= !DBNull.Value.Equals(fila["Descripcion"]) ? fila["Descripcion"].ToString() : string.Empty;
                        Modelo.tblerror.Add(model);
                    }
                }
                if (Modelo.resp == 2)
                {
                    foreach (DataRow fila in dt.Tables[1].Rows)
                    {
                        var model = new tblUpdate();
                        model.Contrato = !DBNull.Value.Equals(fila["Contrato"]) ? fila["Contrato"].ToString() : string.Empty;
                        model.Almacen = !DBNull.Value.Equals(fila["Almacen"]) ? fila["Almacen"].ToString() : string.Empty;
                        model.Codigo = !DBNull.Value.Equals(fila["Codigo"]) ? fila["Codigo"].ToString() : string.Empty;
                        model.PActual= !DBNull.Value.Equals(fila["PActual"]) ? Convert.ToInt16(fila["PActual"].ToString()) : 0;
                        model.PNueva = !DBNull.Value.Equals(fila["PNueva"]) ? Convert.ToInt16(fila["PNueva"].ToString()) : 0;
                        model.CActual = !DBNull.Value.Equals(fila["CActual"]) ? Convert.ToDecimal(fila["CActual"].ToString()) : 0;
                        model.CNueva = !DBNull.Value.Equals(fila["CNueva"]) ? Convert.ToInt16(fila["CNueva"].ToString()) : 0;
                        Modelo.tblupdate.Add(model);
                    }
                }
            }

            //var read = db.ExecuteReader(command);
            //if (read.Read())
            //{
            //    var status = Convert.ToBoolean(read[0]);
            //    Mensaje = read[1].ToString();
            //    Dictionary<string, string> pairs = new Dictionary<string, string>();
            //    pairs.Add("Mensaje", Mensaje);
            //    List<object> lista = new List<object>();
            //    lista.Add(pairs);
            //    response.Result = lista;
            //    response.Success = status;
            //    response.Message = Mensaje;
            //}
            //else
            //{
            //    response.Success = false;
            //}

            //var exito = db.ExecuteNonQuery(command);
            //response.Success = exito != default;

            return Modelo;
        }

        public ResponseList<object> GetDatosAlmacenL()
        {
            var response = new ResponseList<object>();

            var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");

            //var command = db.GetStoredProcCommand("[mta].[sp_GRFP_Load_Ped_Comp_Especial]");
            var command = db.GetStoredProcCommand("[mta].[sp_Load_TRAN_SRC_AVIRTUAL]");
            command.CommandTimeout = 0;

            string folioRegistro = "";
            var read = db.ExecuteReader(command);
            if (read.Read())
            {
                folioRegistro = read[0].ToString();
                Dictionary<string, string> pairs = new Dictionary<string, string>();
                pairs.Add("folio", folioRegistro);
                List<object> lista = new List<object>();
                lista.Add(pairs);
                response.Result = lista;
                response.Success = response.Result.Any();
            }
            else
            {
                response.Success = false;
            }

            //var exito = db.ExecuteNonQuery(command);
            //response.Success = exito != default;

            return response;
        }


        public ResponseAVM insertLoadAlmacen(int opc, int usuario, string Nombre, string Cadena)
        {
            var Modelo = new ResponseAVM();

            var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");

            var command = db.GetStoredProcCommand("[mta].[sp_LoadUpdate_Alamcen]");
            command.CommandTimeout = 0;
            var parameters = new[]
            {
                new SqlParameter("@opcion", opc),
                new SqlParameter("@Nombre", Nombre),
                new SqlParameter("@CadenaAsociada", Cadena),
                new SqlParameter("@usuario", usuario)
            };
            command.Parameters.AddRange(parameters);

            var dt = db.ExecuteDataSet(command);
            if (dt != null)
            {
                foreach (DataRow fila in dt.Tables[0].Rows)
                {

                    Modelo.Success = !DBNull.Value.Equals(fila["Estatus"]) ? Convert.ToBoolean(fila["Estatus"].ToString()) : false;
                    Modelo.Message = !DBNull.Value.Equals(fila["Respuesta"]) ? fila["Respuesta"].ToString() : string.Empty;
                }


                if (Modelo.Success)
                {
                    foreach (DataRow fila in dt.Tables[1].Rows)
                    {
                        var model = new tblAlmacen();
                        model.Id = !DBNull.Value.Equals(fila["Id"]) ? Convert.ToInt16(fila["Id"].ToString()) : 0;
                        model.Nombre = !DBNull.Value.Equals(fila["Nombre"]) ? fila["Nombre"].ToString() : string.Empty;
                        model.CadenaAsociada = !DBNull.Value.Equals(fila["CadenaAsociada"]) ? fila["CadenaAsociada"].ToString() : string.Empty;
                        Modelo.tblalmacen.Add(model);
                    }
                }
            }

            return Modelo;
        }
    }
}
