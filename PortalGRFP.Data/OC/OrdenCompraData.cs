using Azure.Identity;
using FluentFTP;
using Microsoft.Graph;
using Microsoft.Graph.Models;
using Microsoft.Practices.EnterpriseLibrary.Data;
using PortalGRFP.Data.Extensions;
using PortalGRFP.Entities.Common;
using PortalGRFP.Entities.Common.Sugerido;
using PortalGRFP.Entities.Response;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using static PortalGRFP.Data.OC.PedidosData;

namespace PortalGRFP.Data.OC
{
    public class OrdenCompraData
    {


        public ResponseList<ComboGenerico> GetListProveedoresData(int usuario)//1=Grupos 2=Sucursale 
        {
            var response = new ResponseList<ComboGenerico>();

            var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");
            var command = db.GetStoredProcCommand("[mta].SP_GRFP_PROVEEDOR_SUGERIDO");
            db.AddInParameter(command, "@idusuario", DbType.Int32, usuario);


            command.CommandTimeout = 0;

            var read = db.ExecuteReader(command);
            response.Result = read.Reader(x => x.ToComboGenerico());
            response.Success = response.Result.Any();

            return response;
        }

        public ResponseList<ComboGenerico> GetListSucursalesData(int usuario, string proveedores)
        {
            var response = new ResponseList<ComboGenerico>();

            var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");
            var command = db.GetStoredProcCommand("mta.SP_GRFP_PROVEEDOR_SUCURSAL_SUGERIDO");

            db.AddInParameter(command, "@idusuario", DbType.Int32, usuario);
            db.AddInParameter(command, "@proveedores", DbType.String, proveedores);



            command.CommandTimeout = 0;

            var read = db.ExecuteReader(command);
            response.Result = read.Reader(x => x.ToComboGenerico());
            response.Success = response.Result.Any();

            return response;
        }




        //public ResponseList<SugeridoAplicadoModel> GetListaSugeridoAplicadoData(int usuario, string sucursales)
        //{
        //    var response = new ResponseList<SugeridoAplicadoModel>();

        //    var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");
        //    var command = db.GetStoredProcCommand("mta.SP_GRFP_GET_SUGERIDO_ORDEN_COMPRA"); 
        //    db.AddInParameter(command, "@IdUsuario", DbType.Int32, usuario);
        //    db.AddInParameter(command, "@Sucursales", DbType.String, sucursales);

        //    command.CommandTimeout = 0;

        //    var read = db.ExecuteReader(command);
        //    response.Result = read.Reader(x => x.ToListaSugeridoAplicado());
        //    response.Success = response.Result.Any();
        //    if (response.Result.Count == 0)
        //    {
        //        response.Message = "No hay registros por mostrar";
        //        response.Success = false;
        //    }

        //    return response;
        //} 
        public ResponseList<SugeridoAplicadoModel> GetListaSugeridoAplicadoData(int usuario)
        {
            var response = new ResponseList<SugeridoAplicadoModel>();

            var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");
            var command = db.GetStoredProcCommand("mta.SP_GRFP_GET_SUGERIDO_APLICADO");
            db.AddInParameter(command, "@IdUsuario", DbType.Int32, usuario);

            command.CommandTimeout = 0;

            var read = db.ExecuteReader(command);
            response.Result = read.Reader(x => x.ToListaSugeridoAplicado());
            response.Success = response.Result.Any();
            if (response.Result.Count == 0)
            {
                response.Message = "No hay registros por mostrar";
                response.Success = false;
            }

            return response;
        }


        public Response GenerarOrdenCompraData(List<GenerarOrdenModel> model, int usuario)
        {
            var response = new Response();

            var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");


            /*el sugerido esta llegando N numero de veces las veces que esta pintado en 
             * el grid de generar oc la opcion mas viable es hacer el registro por sugerido 
             * y grupo para no repetir las oc tambien agregar llave primaria de dia, proveedor
             * ,sucursal y sugerido (quitar la oc como llave)*/
            foreach (var item in model)
            {
                //var command = db.GetStoredProcCommand("mta.SP_GRFP_SUGERIDO_OC_ASIGNACION");
                var command = db.GetStoredProcCommand("mta.SP_GRFP_UI_ORDEN_COMPRA");


                db.AddInParameter(command, "@id_sugerido", DbType.Int64, item.IdSugerido);
                db.AddInParameter(command, "@idProveedor", DbType.Int64, item.IdProveedor);
                db.AddInParameter(command, "@idgrupo", DbType.Int64, item.IdGrupo);
                db.AddInParameter(command, "@o_usr", DbType.Int32, usuario);


                command.CommandTimeout = 0;

                var exito = db.ExecuteNonQuery(command);
            }

            response.Message = "Se crearon las ordenes de compra";
            response.Success = true != default;

            return response;
        }



        public ResponseList<OrdenCompraModel> GetListOrdenesCompraData(int usuario, string sucursales)
        {
            var response = new ResponseList<OrdenCompraModel>();

            var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");
            var command = db.GetStoredProcCommand("mta.SP_GRFP_LIST_ORDEN_COMPRA");
            db.AddInParameter(command, "@IdUsuario", DbType.Int32, usuario);
            db.AddInParameter(command, "@Sucursales", DbType.String, sucursales);

            command.CommandTimeout = 0;

            var read = db.ExecuteReader(command);
            response.Result = read.Reader(x => x.ToListaOrdenCompra());
            response.Success = response.Result.Any();
            if (response.Result.Count == 0)
            {
                response.Message = "No hay registros por mostrar";
                response.Success = false;
            }

            return response;
        }

        public Response EliminarOrdenCompraData(List<EliminarOrdenModel> model, int usuario)
        {
            var response = new Response();

            var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");

            foreach (var item in model)
            {
                var command = db.GetStoredProcCommand("mta.SP_GRFP_DELETE_ORDEN_COMPRA");

                db.AddInParameter(command, "@IdUsuario", DbType.Int64, usuario);
                db.AddInParameter(command, "@IdSugerido", DbType.Int64, item.IdSugerido);
                db.AddInParameter(command, "@Orden_Compra", DbType.Int64, item.Orden_Compra);
                db.AddInParameter(command, "@Id_Sucursal", DbType.Int64, item.Id_Sucursal);
                db.AddInParameter(command, "@IdProveedor", DbType.Int64, item.IdProveedor);

                command.CommandTimeout = 0;

                var exito = db.ExecuteNonQuery(command);
            }

            response.Message = "Se eliminaron las ordenes de compra";
            response.Success = true != default;

            return response;
        }

        //public Response EnviarOrdenesCompraData(List<EliminarOrdenModel> model, int usuario)
        //{
        //    var response = new Response();

        //    var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");

        //    foreach (var item in model)
        //    {
        //        var command = db.GetStoredProcCommand("mta.SP_GRFP_ENVIAR_ORDEN_COMPRA");

        //        //db.AddInParameter(command, "@IdUsuario", DbType.Int64, usuario);
        //        db.AddInParameter(command, "@idSugerido", DbType.String, item.IdSugerido.ToString());
        //        db.AddInParameter(command, "@IdOrdenCompra", DbType.String, item.Orden_Compra.ToString());

        //        command.CommandTimeout = 0;

        //        var exito = db.ExecuteNonQuery(command);
        //    }

        //    /*se asigna a una nueva lista ya que puede ser un envio de uno o mas sugeridos a la vez*/
        //    var newList = (from x in model select new { IdSugerido = x.IdSugerido }).Distinct();

        //    foreach (var item in newList)
        //    {
        //        try
        //        {
        //            var cmd = db.GetStoredProcCommand("mta.SP_GRFP_BRIDGE_CADENA");
        //            db.AddInParameter(cmd, "@IdSugerido", DbType.Int64, item.IdSugerido.ToString());
        //            db.AddInParameter(cmd, "@flag", DbType.String, "EOC");

        //            cmd.CommandTimeout = 0;

        //            db.ExecuteNonQuery(cmd);
        //        }
        //        catch (Exception ex)
        //        {
        //            ExHandler.Log.Write(string.Format("Error.EnviarOrdenesCompraData {0}", ex.Message));
        //            response.Message = "Error al realizar la consulta de información. " + ex.Message;
        //        }
        //    }

        //    //Response respuesaApi = null;

        //    //List<EliminarOrdenModel> ApiPedidos = ApiPedidosData.ListarPedidosPorAPI(model);
        //    ////List<EliminarOrdenModel> ApiPedidos = model.Where(p => p.IdProveedor == 9).ToList();

        //    //if (ApiPedidos.Count > 0)
        //    //{
        //    //    respuesaApi = ApiPedidosData.EnviarPedidosApi(ApiPedidos);

        //    //    model.RemoveAll(p => ApiPedidos.Any(r => r.Id_Sucursal == p.Id_Sucursal));
        //    //}

        //    //model.RemoveAll(p => p.IdProveedor ==9);
        //    var respuestaFTP = CrearYEnviarOC(model);

        //    if (respuesaApi.Success && respuestaFTP.Success)
        //    {
        //        response.Message = "Se Enviaron las Ordenes de compra";
        //        response.Success = true != default;

        //    }
        //    else if (!respuesaApi.Success && !respuestaFTP.Success)
        //    {
        //        response.Message = "Error Enviando Ordenes de Compra" + respuesaApi.Message + "; " + respuestaFTP.Message;
        //        response.Success = false != default;

        //    }
        //    else if (!respuesaApi.Success)
        //    {
        //        response.Message = "Envio mediante FTP exitoso, pero Error en envio de pedidos mediante API- " + respuesaApi.Message;
        //        response.Success = false != default;
        //    }
        //    else if (!respuestaFTP.Success)
        //    {
        //        response.Message = "Envio mediante API exitoso, pero Error en envio de pedidos mediante FTP- " + respuestaFTP.Message;
        //        response.Success = false != default;
        //    }

        //    return response;
        //}

        public Response EnviarOrdenesCompraData(List<EliminarOrdenModel> model, int usuario)
        {
            var response = new Response();

            var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");

            foreach (var item in model)
            {
                var command = db.GetStoredProcCommand("mta.SP_GRFP_ENVIAR_ORDEN_COMPRA");

                //db.AddInParameter(command, "@IdUsuario", DbType.Int64, usuario);
                db.AddInParameter(command, "@idSugerido", DbType.String, item.IdSugerido.ToString());
                db.AddInParameter(command, "@IdOrdenCompra", DbType.String, item.Orden_Compra.ToString());

                command.CommandTimeout = 0;

                var exito = db.ExecuteNonQuery(command);
            }

            /*se asigna a una nueva lista ya que puede ser un envio de uno o mas sugeridos a la vez*/
            var newList = (from x in model select new { IdSugerido = x.IdSugerido }).Distinct();

            foreach (var item in newList)
            {
                try
                {
                    var cmd = db.GetStoredProcCommand("mta.SP_GRFP_BRIDGE_CADENA");
                    db.AddInParameter(cmd, "@IdSugerido", DbType.Int64, item.IdSugerido.ToString());
                    db.AddInParameter(cmd, "@flag", DbType.String, "EOC");

                    cmd.CommandTimeout = 0;

                    db.ExecuteNonQuery(cmd);
                }
                catch (Exception ex)
                {
                    ExHandler.Log.Write(string.Format("Error.EnviarOrdenesCompraData {0}", ex.Message));
                    response.Message = "Error al realizar la consulta de información. " + ex.Message;
                }
            }

            //List<EliminarOrdenModel> ApiPedidos = ApiPedidosData.ListarPedidosPorAPI(model);
            ////List<EliminarOrdenModel> ApiPedidos = model.Where(p => p.IdProveedor == 9).ToList();

            //if (ApiPedidos.Count > 0)
            //{
            //    respuesaApi = ApiPedidosData.EnviarPedidosApi(ApiPedidos);

            //    model.RemoveAll(p => ApiPedidos.Any(r => r.Id_Sucursal == p.Id_Sucursal));
            //}

            //model.RemoveAll(p => p.IdProveedor == 9);

            var respuestaFTP = CrearYEnviarOC(model);

            response.Message = "Se enviaron las ordenes de compra";
            response.Success = true != default;

            return response;
        }

        public Response CrearYEnviarOC(List<EliminarOrdenModel> model)
        {
            var response = new Response();

            var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");

            var respuesta = new List<ArchivoModel>();
            long idSugerido = 0;
            var nombrearchivo = string.Empty;

            DataSet dataSet = new DataSet();
            try
            {
                foreach (var item in model)
                {
                    DataTable archivoDT = new DataTable();
                    archivoDT.TableName = item.Orden_Compra.ToString() + '_' + item.IdProveedor;

                    var ini = "C:/FV/" + archivoDT.TableName + ".txt";

                    if (File.Exists(ini))
                    {
                        File.Delete(ini);
                    }

                    FileStream Query = new FileStream(ini, FileMode.Create, FileAccess.Write);
                    StreamWriter Escriba = new StreamWriter(Query);

                    var PathFile = Query.Name;
                    var command = db.GetStoredProcCommand("mta.SP_GRFP_BRIDGE_CADENA");
                    db.AddInParameter(command, "@IdSugerido", DbType.Int64, item.IdSugerido.ToString());
                    db.AddInParameter(command, "@flag", DbType.String, "MOC");
                    db.AddInParameter(command, "@proveedor", DbType.String, item.IdProveedor);
                    db.AddInParameter(command, "@OrdenCompra", DbType.String, item.Orden_Compra);

                    idSugerido = item.IdSugerido;

                    command.CommandTimeout = 0;

                    using (IDataReader dr = db.ExecuteReader(command))
                    {
                        if (dr.FieldCount > 0)
                        {
                            try
                            {
                                while (dr.Read())
                                {
                                    respuesta.Add(new ArchivoModel()
                                    {
                                        archivo = dr.Get<string>("archivo").ToString(),
                                        Rw = dr.Get<string>("rw").ToString(),
                                        Registro = dr.Get<string>("Registro").ToString(),
                                    });
                                }
                                foreach (var reg in respuesta)
                                {
                                    Escriba.WriteLine(reg.Registro);
                                    nombrearchivo = reg.archivo;
                                }
                                Escriba.Close();
                            }
                            catch (Exception ex)
                            {
                                ExHandler.Log.Write(string.Format("Error.CrearYEnviarOC {0}", ex.Message));
                                response.Message = "Error. Create File Local Server" + ex.Message;
                            }
                        }
                        else
                        {
                            dr.Close();
                        }
                        dr.Close();
                        Query.Dispose();
                    }

                    if (respuesta.Count() > 0)
                    {
                        var nombre = (respuesta[0].archivo).ToString();
                        string oldName = Query.Name.ToString();
                        string newName = (("C:/FV/" + nombre).ToString());
                        System.IO.File.Copy(oldName, newName);
                        System.IO.File.Delete(Query.Name);

                        respuesta.Clear();

                        var ExecutionParams = new List<FTPParametersModel>();

                        var command2 = db.GetStoredProcCommand("mta.SP_GRFP_CONF_GET_PROVEEDOR_FTP_EXTENSION");
                        db.AddInParameter(command2, "@Sucursal", DbType.String, item.Id_Sucursal);
                        db.AddInParameter(command2, "@OrdenCompra", DbType.String, item.Orden_Compra);
                        db.AddInParameter(command2, "@IdProveedor", DbType.String, item.IdProveedor);
                        db.AddInParameter(command2, "@Sugerido", DbType.String, item.IdSugerido);

                        command2.CommandTimeout = 0;
                        int puerto = 0;

                        using (IDataReader dr2 = db.ExecuteReader(command2))
                        {
                            while (dr2.Read())
                            {
                                ExecutionParams.Add(new FTPParametersModel
                                {
                                    IdProveedor = dr2.Get<int>("Id_Proveedor"),
                                    TipoFormato = dr2.Get<int>("Tipo_Formato"),
                                    URL = dr2.Get<string>("URL").ToString(),
                                    Puerto = 21,
                                    Directorio = dr2.Get<string>("Directorio").ToString(),
                                    DirectorioRespaldo = dr2.Get<string>("Directorio_Respaldo").ToString(),
                                    CopiaRespaldo = dr2.Get<string>("Copia_Respaldo").ToString(),
                                    UsuarioFTP = dr2.Get<string>("Usuario_FTP").ToString(),
                                    ClaveFTP = dr2.Get<string>("Clave_FTP").ToString(),
                                    FTPActivo = dr2.Get<string>("FTP_Activo").ToString(),
                                    IdRazonSocial = dr2.Get<int>("Id_RazonSocial"),
                                });

                                DataColumn column = new DataColumn();
                                archivoDT.Columns.Add("ClaveFTP", typeof(System.String));
                                archivoDT.Columns.Add("DirectorioRespaldo", typeof(System.String));
                                archivoDT.Columns.Add("Directorio", typeof(System.String));
                                archivoDT.Columns.Add("IdProveedor", typeof(System.Int32));
                                archivoDT.Columns.Add("IdRazonSocial", typeof(System.Int32));
                                archivoDT.Columns.Add("Puerto", typeof(System.Int32));
                                archivoDT.Columns.Add("URL", typeof(System.String));
                                archivoDT.Columns.Add("UsuarioFTP", typeof(System.String));
                                archivoDT.Columns.Add("ArchivoRuta", typeof(System.String));
                                archivoDT.Columns.Add("Archivo", typeof(System.String));

                                int j = archivoDT.Columns.Count;
                                DataRow row;

                                row = archivoDT.NewRow();

                                foreach (var itemdt in ExecutionParams)
                                {
                                    row["ClaveFTP"] = itemdt.ClaveFTP;
                                    row["DirectorioRespaldo"] = itemdt.DirectorioRespaldo;
                                    row["Directorio"] = itemdt.Directorio;
                                    row["IdProveedor"] = itemdt.IdProveedor;
                                    row["IdRazonSocial"] = itemdt.IdRazonSocial;
                                    row["Puerto"] = itemdt.Puerto;
                                    row["URL"] = itemdt.URL;
                                    row["UsuarioFTP"] = itemdt.UsuarioFTP;
                                    row["ArchivoRuta"] = newName;
                                    row["Archivo"] = nombre;
                                }
                                archivoDT.Rows.Add(row);
                            }
                            dr2.Close();
                        }
                        Query.Dispose();
                    }
                    if (archivoDT.Rows.Count > 0)
                    {
                        dataSet.Tables.Add(archivoDT);
                    }

                    if (File.Exists(ini))
                    {
                        File.Delete(ini);
                    }
                }

            }
            catch (Exception ex)
            {
                ExHandler.Log.Write(string.Format("Error.CrearYEnviarOC {0}", ex.Message));
                response.Message = "Error. Create Files Local Server and Configuration Data" + ex.Message;
            }
            DataTable tablaOrigen = new DataTable();


            for (int f = 0; f < dataSet.Tables.Count; f++)
            {
                tablaOrigen = dataSet.Tables[0];
                DataTable tablaDestino = dataSet.Tables[f];
                tablaOrigen.AcceptChanges();
                tablaOrigen.Merge(tablaDestino, true, MissingSchemaAction.Add);
                tablaOrigen.TableName = "TablaCompras";

            }

            ExHandler.Log.Write(string.Format("Success.CrearYEnviarOC"));

            Alerta_Archivos(idSugerido); //Alerta de archivos creados

            #region Envio Archivos BlobStorage           
            //Vamos a filtrar la tabla por las que contengan la Cadena para enviar por Azure Blob storage
            //var newList = model.Select(x => new IdSugeridoDto { IdSugerido = x.IdSugerido }).Distinct().ToList();
            //var filasFiltradas = PedidosData.ListarCadenas(newList);

            //DataTable dtCoincidentes = new DataTable();

            //if (tablaOrigen.Rows.Count > 0 && filasFiltradas != null && filasFiltradas.Count > 0)
            //{
            //    var queryCoincidentes =
            //        from row in tablaOrigen.AsEnumerable()
            //        join item in filasFiltradas
            //        on new { KeyProv = row.Field<int>("IdProveedor"), KeyRazon = row.Field<int>("IdRazonSocial") }
            //        equals new { KeyProv = item.IdProveedor, KeyRazon = item.IDSubcadena }
            //        select row; // <-- Aquí decimos: "Quiero la fila entera del DataTable original"

            //    if (queryCoincidentes.Any())
            //    {
            //        // Si hay coincidencias, crea la tabla con los datos
            //        dtCoincidentes = queryCoincidentes.CopyToDataTable();
            //    }
            //    else
            //    {
            //        // Si NO hay coincidencias, creamos solo la estructura (columnas vacías)
            //        dtCoincidentes = tablaOrigen.Clone();
            //    }
            //}
            //else
            //{
            //    // Si alguna lista estaba vacía desde el inicio, devolvemos estructura vacía
            //    dtCoincidentes = tablaOrigen.Clone();
            //}

            //if (dtCoincidentes.Rows.Count > 0)
            //{
            //    ExHandler.Log.Write(string.Format("Start.UploadFiles via Azure"));

            //    PedidosData.UploadPedidosAzure(dtCoincidentes);

            //    // 1. Creamos un "diccionario" rápido de las llaves que queremos borrar.
            //    // Usamos una Tupla (int, int) para guardar IdProveedor y IdRazonSocial juntos.
            //    var llavesParaBorrar = new HashSet<(int, int)>();

            //    foreach (DataRow row in dtCoincidentes.Rows)
            //    {
            //        int idProv = row.Field<int>("IdProveedor");
            //        int idRazon = row.Field<int>("IdRazonSocial");
            //        llavesParaBorrar.Add((idProv, idRazon));
            //    }

            //    for (int i = tablaOrigen.Rows.Count - 1; i >= 0; i--)
            //    {
            //        DataRow row = tablaOrigen.Rows[i];

            //        int idProv = row.Field<int>("IdProveedor");
            //        int idRazon = row.Field<int>("IdRazonSocial");

            //        if (llavesParaBorrar.Contains((idProv, idRazon)))
            //        {
            //            tablaOrigen.Rows.RemoveAt(i);
            //        }
            //    }
            //    ExHandler.Log.Write(string.Format("Success.UploadFiles via Azure"));
            //}
            #endregion

            ExHandler.Log.Write(string.Format("Start.UploadFiles via FTP"));

            UploadFiles_OC(tablaOrigen);

            tablaOrigen.Dispose();
            dataSet.Dispose();

            Alerta_Archivos_FTP(idSugerido);

            response.Message = "Se Enviaron las ordenes de compra";
            response.Success = true != default;
            return response;
        }
        public Response UploadFiles_OC(DataTable Tablacompras)
        {
            var response = new Response();
            string marca = string.Empty;
            string dirlog = string.Empty;

            #region Envio archivos una sola conexion por prov

            //var TablaEnvio = new DataTable();
            //TablaEnvio.Columns.Add("ClaveFTP", typeof(string));
            //TablaEnvio.Columns.Add("DirectorioRespaldo", typeof(string));
            //TablaEnvio.Columns.Add("Directorio", typeof(string));
            //TablaEnvio.Columns.Add("IdProveedor", typeof(int));
            //TablaEnvio.Columns.Add("IdRazonSocial", typeof(int));
            //TablaEnvio.Columns.Add("Puerto", typeof(int));
            //TablaEnvio.Columns.Add("URL", typeof(string));
            //TablaEnvio.Columns.Add("UsuarioFTP", typeof(string));

            //Tablacompras.Rows
            //    .Cast<DataRow>()
            //    .Select(r => new
            //    {
            //        ClaveFTP = r.Field<string>("ClaveFTP"),
            //        DirectorioRespaldo = r.Field<string>("DirectorioRespaldo"),
            //        Directorio = r.Field<string>("Directorio"),
            //        IdProveedor = r.Field<int>("IdProveedor"),
            //        IdRazonSocial = r.Field<int>("IdRazonSocial"),
            //        Puerto = r.Field<int>("Puerto"),
            //        URL = r.Field<string>("URL"),
            //        UsuarioFTP = r.Field<string>("UsuarioFTP"),
            //    })
            //    .Distinct()
            //    .All(t => { TablaEnvio.Rows.Add(t.ClaveFTP, t.DirectorioRespaldo, t.Directorio, t.IdProveedor, t.IdRazonSocial, t.Puerto, t.URL, t.UsuarioFTP); return true; });


            //if (TablaEnvio.Rows.Count > 0)
            //{
            //    foreach (DataRow row in TablaEnvio.Rows)
            //    {
            //        marca = row.ItemArray[4].ToString();
            //    }

            //    if (marca == "1" || marca == "3" || marca == "4" || marca == "5" || marca == "7")
            //    {
            //        dirlog = string.Format("C:/FV/LOGSF");
            //    }
            //    else if (marca == "2")
            //    {
            //        dirlog = string.Format("C:/FV/LOGFT");
            //    }
            //    else if (marca == "6")
            //    {
            //        dirlog = string.Format("C:/FV/LOGUN");
            //    }
            //    else
            //    {
            //        response.Message = "Error en marca";
            //    }

            //    if (Directory.Exists(dirlog))
            //    {
            //        Directory.Delete(dirlog, true);
            //    }

            //    //id  Valor
            //    //1   Farmacentro
            //    //2   Farmatodo
            //    //3   GyM
            //    //4   San Francisco
            //    //5   Santa Cruz
            //    //6   Unión
            //    //7   Zapotlán

            #endregion  Envio archivos una sola conexion por prov

            #region multiples conexiones por proveedor

            if (Tablacompras.Rows.Count > 0)
            {
                foreach (DataRow row in Tablacompras.Rows)
                {
                    marca = row.ItemArray[4].ToString();
                }

                if (marca == "1" || marca == "3" || marca == "4" || marca == "5" || marca == "7")
                {
                    dirlog = string.Format("C:/FV/LOGSF");
                }
                else if (marca == "2")
                {
                    dirlog = string.Format("C:/FV/LOGFT");
                }
                else if (marca == "6")
                {
                    dirlog = string.Format("C:/FV/LOGUN");
                }
                else
                {
                    response.Message = "Error en marca";
                }

                if (Directory.Exists(dirlog))
                {
                    Directory.Delete(dirlog, true);
                }

                #endregion multiples conexiones por proveedor

                foreach (DataRow row in Tablacompras.Rows) //Es lo mismo en proceso anterior o Nuevo
                {
                    var nombrearchivo = string.Empty;
                    string directoryPath = string.Format("{0}", row.ItemArray[8]);
                    // Generamos el pathfile 
                    string pathfileComp = string.Format("{0}/{1}", row.ItemArray[1], row.ItemArray[9]);
                    string pathfile = string.Format("{0}", row.ItemArray[1]);

                    if (!Directory.Exists(pathfile)) Directory.CreateDirectory(pathfile);
                    try
                    {
                        if (File.Exists(pathfileComp))
                        {
                            File.Delete(pathfileComp);
                        }

                        File.Move(directoryPath, pathfileComp);

                    }
                    catch (Exception ex)
                    {
                        ExHandler.Log.Write(string.Format("Error.UploadFiles_OC {0}", ex.Message));
                        response.Message = "Error.UploadFiles_OC" + ex.Message;
                    }
                }

                bool exitloop = false;


                //foreach (DataRow row in TablaEnvio.Rows)   -- Este se usa en una sola conexion por prov
                foreach (DataRow row in Tablacompras.Rows) //-- Este se usa en multiples conexiones por prov (archivo por archivo)
                {
                    string pass = row.ItemArray[0].ToString();
                    string rutalocal = row.ItemArray[1].ToString();
                    string rutaftp = row.ItemArray[2].ToString();
                    string idproveedor = row.ItemArray[3].ToString();
                    string puerto = row.ItemArray[5].ToString();
                    string host = row.ItemArray[6].ToString();
                    string usuario = row.ItemArray[7].ToString();
                    string nombreArchivo = row.ItemArray[9].ToString();

                    int Prov = int.Parse(idproveedor);

                    exitloop = false;
                    if (exitloop) break;

                    switch (Prov)
                    {                        
                        case 7:

                            if (true)
                            {
                                //DoRecuperarInformacion(o);                            
                                upLoadFTPNadro(pass, rutalocal, rutaftp, puerto, idproveedor, usuario, host, nombreArchivo, dirlog); //Proceso viejo
                                //upLoadFTPNadro(pass, rutalocal, rutaftp, puerto, usuario, host, dirlog); //Proceso Nuevo
                                exitloop = true;
                            }
                            break;
                        
                        case 6:
                            if (true)
                            {
                                //DoRecuperarInformacion(o);
                                upLoadFTPMar(pass, rutalocal, rutaftp, (5022).ToString(), idproveedor, usuario, host, nombreArchivo, dirlog); //Proceso viejo
                                //upLoadFTPMar(pass, rutalocal, rutaftp, puerto, usuario, host, dirlog);
                                exitloop = true;
                            }
                            break;

                        //Procsoi para recuperar archivos de Hot Folder / Articulos / Baja
                        case 8:
                            //DoRecuperarInformacion(o);
                            if (true)
                            {
                                upLoadFTPFanasa(pass, rutalocal, rutaftp, puerto, idproveedor, usuario, host, nombreArchivo, dirlog); //Proceso viejo
                                //upLoadFTPFanasa(pass, rutalocal, rutaftp, puerto, usuario, host, dirlog);
                                exitloop = true;
                            }
                            break;

                        case 9:
                            //DoRecuperarInformacion(o);
                            if (true)
                            {
                                upLoadFTPLevic(pass, rutalocal, rutaftp, puerto, idproveedor, usuario, host, nombreArchivo, dirlog); //Proceso viejo
                                //upLoadFTPLevic(pass, rutalocal, rutaftp, puerto, usuario, host, dirlog);
                                exitloop = true;
                            }
                            break;


                        default: break;
                    }
                }

                //if (Directory.Exists(dirlog))
                //{
                //    var mensaje = Directory.GetFiles(dirlog);
                //    DirectoryInfo di = new DirectoryInfo(dirlog);
                //    var cadena = di.Name;
                //    try
                //    {
                //        EnviarCorreo(mensaje, cadena).GetAwaiter().GetResult();
                //    }
                //    catch (Exception ex)
                //    {
                //        ExHandler.Log.Write(string.Format("Error.sendGmail {0}", ex.Message));
                //    }
                //}
            }
            Tablacompras.Dispose();
            //TablaEnvio.Dispose(); //ProcesoN Nuevo

            response.Success = true != default;
            return response;
        }

        #region Envio por SFTP
        //public Response upLoadFTPNadro(string pass, string rutalocal, string rutaftp, string puerto, string idproveedor, string usuario, string host, string nombreArchivo, string dirlog)
        //{
        //    var response = new Response();
        //    var conn = new PasswordConnectionInfo(host, Convert.ToInt32(puerto), usuario, pass);
        //    // Realizamos la instanciación a la clase de SFT
        //    using (var sftp = new SftpClient(conn))
        //    {
        //        try
        //        {
        //            // Establecemos la conexión hacia el servidor SFTP Rappi
        //            sftp.Connect();

        //            // Validamos conexión
        //            if (sftp.IsConnected)
        //            {
        //                if (!Directory.Exists(dirlog)) Directory.CreateDirectory(dirlog);
        //                string directoryPath = string.Format("{0}/{1}", rutalocal, nombreArchivo);
        //                string pathfile = string.Format("{0}", rutalocal);
        //                string remoteFile = string.Format("{0}/{1}", rutaftp, nombreArchivo);
        //                string destFileName = string.Empty;
        //                string historyPath = string.Format("{0}/historico/{1}/{2}/{3}", pathfile, DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

        //                if (!Directory.Exists(historyPath)) Directory.CreateDirectory(historyPath);

        //                using (Stream file1 = File.OpenRead(directoryPath))
        //                {
        //                    sftp.UploadFile(file1, remoteFile);

        //                }

        //                historyPath = string.Format("{0}/{1}", historyPath, nombreArchivo);
        //                File.Move(directoryPath, historyPath);

        //                //actualizamos status en la tabla correspondiente
        //                Actualiza_Status(nombreArchivo);
        //            }
        //            else
        //            {
        //                response.Success = false;
        //                //string carpetalog = string.Format("C:/FV/LOG");
        //                if (!Directory.Exists(dirlog)) Directory.CreateDirectory(dirlog);
        //                string directoryPath = string.Format("{0}/{1}", rutalocal, nombreArchivo);
        //                dirlog = string.Format("{0}/{1}", dirlog, nombreArchivo);
        //                System.IO.File.Move(directoryPath, dirlog);
        //            }

        //            // Ejecutamos desconexión del SFTP
        //            sftp.Disconnect();
        //            // Realizamos dispose para liberar recursos
        //            sftp.Dispose();
        //        }
        //        catch (Exception ex)
        //        {
        //            ExHandler.Log.Write(string.Format("OrdenCompraData.UpLoadSFTPNadro {0}", ex.Message));
        //            response.Success = false;
        //            if (!Directory.Exists(dirlog)) Directory.CreateDirectory(dirlog);
        //            string directoryPath = string.Format("{0}/{1}", rutalocal, nombreArchivo);
        //            dirlog = string.Format("{0}/{1}", dirlog, nombreArchivo);
        //            System.IO.File.Move(directoryPath, dirlog);
        //        }
        //    }
        //    response.Success = true != default;
        //    return response;
        //}
        //public Response upLoadFTPMar(string pass, string rutalocal, string rutaftp, string puerto, string idproveedor, string usuario, string host, string nombreArchivo, string dirlog)
        //{
        //    var response = new Response();

        //    var conn = new PasswordConnectionInfo(host, Convert.ToInt32(puerto), usuario, pass);
        //    // Realizamos la instanciación a la clase de SFT
        //    using (var sftp = new SftpClient(conn))
        //    {
        //        try
        //        {
        //            // Establecemos la conexión hacia el servidor SFTP Rappi
        //            sftp.Connect();
        //            // Validamos conexión
        //            if (sftp.IsConnected)
        //            {
        //                if (!Directory.Exists(dirlog)) Directory.CreateDirectory(dirlog);
        //                string directoryPath = string.Format("{0}/{1}", rutalocal, nombreArchivo);
        //                string pathfile = string.Format("{0}", rutalocal);
        //                string remoteFile = string.Format("{0}/{1}", rutaftp, nombreArchivo);
        //                string destFileName = string.Empty;
        //                string historyPath = string.Format("{0}/historico/{1}/{2}/{3}", pathfile, DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

        //                if (!Directory.Exists(historyPath)) Directory.CreateDirectory(historyPath);

        //                using (Stream file1 = File.OpenRead(directoryPath))
        //                {
        //                    //var files = sftp.ListDirectory(rutaftp);

        //                    sftp.UploadFile(file1, remoteFile);
        //                }

        //                historyPath = string.Format("{0}/{1}", historyPath, nombreArchivo);
        //                File.Move(directoryPath, historyPath);

        //                //actualizamos status en la tabla correspondiente
        //                Actualiza_Status(nombreArchivo);
        //            }
        //            else
        //            {
        //                response.Success = false;
        //                //string carpetalog = string.Format("C:/FV/LOG");
        //                if (!Directory.Exists(dirlog)) Directory.CreateDirectory(dirlog);
        //                string directoryPath = string.Format("{0}/{1}", rutalocal, nombreArchivo);
        //                dirlog = string.Format("{0}/{1}", dirlog, nombreArchivo);
        //                System.IO.File.Move(directoryPath, dirlog);
        //            }

        //            // Ejecutamos desconexión del SFTP
        //            sftp.Disconnect();
        //            // Realizamos dispose para liberar recursos
        //            sftp.Dispose();
        //        }
        //        catch (Exception ex)
        //        {

        //            ExHandler.Log.Write(string.Format("OrdenCompraData.UpLoadSFTPMAR {0}", ex.Message));
        //            response.Success = false;
        //            if (!Directory.Exists(dirlog)) Directory.CreateDirectory(dirlog);
        //            string directoryPath = string.Format("{0}/{1}", rutalocal, nombreArchivo);
        //            dirlog = string.Format("{0}/{1}", dirlog, nombreArchivo);
        //            System.IO.File.Move(directoryPath, dirlog);

        //        }


        //    }
        //    response.Success = true != default;
        //    return response;
        //}

        //public Response upLoadFTPFanasa(string pass, string rutalocal, string rutaftp, string puerto, string idproveedor, string usuario, string host, string nombreArchivo, string dirlog)
        //{
        //    var response = new Response();
        //    var conn = new PasswordConnectionInfo(host, Convert.ToInt32(puerto), usuario, pass);
        //    // Realizamos la instanciación a la clase de SFTP
        //    using (var sftp = new SftpClient(conn))
        //    {
        //        try
        //        {
        //            // Establecemos la conexión hacia el servidor SFTP Rappi
        //            sftp.Connect();
        //            // Validamos conexión
        //            if (sftp.IsConnected)
        //            {
        //                if (!Directory.Exists(dirlog)) Directory.CreateDirectory(dirlog);
        //                string directoryPath = string.Format("{0}/{1}", rutalocal, nombreArchivo);
        //                string pathfile = string.Format("{0}", rutalocal);
        //                string remoteFile = string.Format("{0}/{1}", rutaftp, nombreArchivo);
        //                string destFileName = string.Empty;
        //                string historyPath = string.Format("{0}/historico/{1}/{2}/{3}", pathfile, DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

        //                if (!Directory.Exists(historyPath)) Directory.CreateDirectory(historyPath);

        //                using (Stream file1 = File.OpenRead(directoryPath))
        //                {
        //                    sftp.UploadFile(file1, remoteFile);
        //                }

        //                historyPath = string.Format("{0}/{1}", historyPath, nombreArchivo);
        //                File.Move(directoryPath, historyPath);

        //                //actualizamos status en la tabla correspondiente
        //                Actualiza_Status(nombreArchivo);
        //            }
        //            else
        //            {
        //                response.Success = false;
        //                //string carpetalog = string.Format("C:/FV/LOG");
        //                if (!Directory.Exists(dirlog)) Directory.CreateDirectory(dirlog);
        //                string directoryPath = string.Format("{0}/{1}", rutalocal, nombreArchivo);
        //                dirlog = string.Format("{0}/{1}", dirlog, nombreArchivo);
        //                System.IO.File.Move(directoryPath, dirlog);
        //            }

        //            // Ejecutamos desconexión del SFTP
        //            sftp.Disconnect();
        //            // Realizamos dispose para liberar recursos
        //            sftp.Dispose();
        //        }
        //        catch (Exception ex)
        //        {
        //            ExHandler.Log.Write(string.Format("OrdenCompraData.UpLoadSFTPfanasa {0}", ex.Message));
        //            response.Success = false;
        //            if (!Directory.Exists(dirlog)) Directory.CreateDirectory(dirlog);
        //            string directoryPath = string.Format("{0}/{1}", rutalocal, nombreArchivo);
        //            dirlog = string.Format("{0}/{1}", dirlog, nombreArchivo);
        //            System.IO.File.Move(directoryPath, dirlog);
        //        }
        //    }
        //    response.Success = true != default;
        //    return response;
        //}

        //public Response upLoadFTPLevic(string pass, string rutalocal, string rutaftp, string puerto, string idproveedor, string usuario, string host, string nombreArchivo, string dirlog)
        //{
        //    var response = new Response();
        //    var conn = new PasswordConnectionInfo(host, Convert.ToInt32(puerto), usuario, pass);
        //    // Realizamos la instanciación a la clase de SFT
        //    using (var sftp = new SftpClient(conn))
        //    {
        //        try
        //        {
        //            // Establecemos la conexión hacia el servidor SFTP Rappi
        //            sftp.Connect();
        //            // Validamos conexión
        //            if (sftp.IsConnected)
        //            {
        //                if (!Directory.Exists(dirlog)) Directory.CreateDirectory(dirlog);
        //                string directoryPath = string.Format("{0}/{1}", rutalocal, nombreArchivo);
        //                string pathfile = string.Format("{0}", rutalocal);
        //                string remoteFile = string.Format("{0}/{1}", rutaftp, nombreArchivo);
        //                string destFileName = string.Empty;
        //                string historyPath = string.Format("{0}/historico/{1}/{2}/{3}", pathfile, DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

        //                if (!Directory.Exists(historyPath)) Directory.CreateDirectory(historyPath);

        //                using (Stream file1 = File.OpenRead(directoryPath))
        //                {
        //                    sftp.UploadFile(file1, remoteFile);
        //                }

        //                historyPath = string.Format("{0}/{1}", historyPath, nombreArchivo);
        //                File.Move(directoryPath, historyPath);

        //                //actualizamos status en la tabla correspondiente
        //                Actualiza_Status(nombreArchivo);
        //            }
        //            else
        //            {
        //                response.Success = false;
        //                //string carpetalog = string.Format("C:/FV/LOG");
        //                if (!Directory.Exists(dirlog)) Directory.CreateDirectory(dirlog);
        //                string directoryPath = string.Format("{0}/{1}", rutalocal, nombreArchivo);
        //                dirlog = string.Format("{0}/{1}", dirlog, nombreArchivo);
        //                System.IO.File.Move(directoryPath, dirlog);
        //            }

        //            // Ejecutamos desconexión del SFTP
        //            sftp.Disconnect();
        //            // Realizamos dispose para liberar recursos
        //            sftp.Dispose();
        //        }
        //        catch (Exception ex)
        //        {
        //            ExHandler.Log.Write(string.Format("OrdenCompraData.UpLoadSFTPLevic {0}", ex.Message));
        //            response.Success = false;
        //            if (!Directory.Exists(dirlog)) Directory.CreateDirectory(dirlog);
        //            string directoryPath = string.Format("{0}/{1}", rutalocal, nombreArchivo);
        //            dirlog = string.Format("{0}/{1}", dirlog, nombreArchivo);
        //            System.IO.File.Move(directoryPath, dirlog);
        //        }
        //    }
        //    response.Success = true != default;
        //    return response;
        //}

        #endregion Envio por SFTP

        #region Envio por FTP archivo x archivo

        public Response upLoadFTPNadro(string pass, string rutalocal, string rutaftp, string puerto, string idproveedor, string usuario, string host, string nombreArchivo, string dirlog)
        {
            var response = new Response();
            int maxRetries = 3;
            int retryDelayMs = 2000;

            for (int attempt = 1; attempt <= maxRetries; attempt++)
            {
                using (var ftp = new FtpClient(host, usuario, pass))
                {
                    try
                    {
                        ftp.Port = int.Parse(puerto.ToString());
                        //ftp.Port = 5021;
                        //ftp.EncryptionMode = FtpEncryptionMode.Explicit;
                        ftp.EncryptionMode = FtpEncryptionMode.None;
                        ftp.DataConnectionType = FtpDataConnectionType.AutoPassive;
                        ftp.SocketKeepAlive = true;

                        ftp.ConnectTimeout = 30000;
                        ftp.DataConnectionReadTimeout = 120000;
                        ftp.ReadTimeout = 120000;
                        //Obtiene o establece el período de tiempo en milisegundos para esperar a que un intento 
                        //de conexión se realice correctamente antes de darse por vencido.
                        
                        // Configuración adicional para problemas de conexión
                        ftp.ValidateCertificate += (control, e) => { e.Accept = true; };
                        ftp.SocketPollInterval = 1000;
                        ftp.RetryAttempts = 2;

                        ExHandler.Log.Write(string.Format("UpLoadFTPNadro - Intento {0} de {1} para archivo: {2}", attempt, maxRetries, nombreArchivo));
                        ExHandler.Log.Write(string.Format("UpLoadFTPNadro - Configuración: Host={0}, Puerto={1}, Usuario={2}", host, puerto, usuario));
                        
                        if (attempt > 1) { System.Threading.Thread.Sleep(1000); }
                        
                        ftp.Connect();
                        if (ftp.IsConnected)
                        {
                            ExHandler.Log.Write(string.Format("IsConnected.UploadFiles via FTP Nadro - Host: {0}, Puerto: {1}", host, puerto));
                            if (!Directory.Exists(dirlog)) Directory.CreateDirectory(dirlog);
                            string directoryPath = string.Format("{0}\\{1}", rutalocal, nombreArchivo);
                            string pathfile = string.Format("{0}", rutalocal);
                            string remoteFile = string.Format("{0}\\{1}", rutaftp, nombreArchivo);
                            string destFileName = string.Empty;
                            string historyPath = string.Format("{0}\\historico\\{1}\\{2}\\{3}", pathfile, DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

                            if (!Directory.Exists(historyPath)) Directory.CreateDirectory(historyPath);

                            FileInfo fileInfo = new FileInfo(directoryPath);
                            ExHandler.Log.Write(string.Format("UpLoadFTPNadro - Iniciando upload de archivo: {0}, Tamaño: {1} bytes", nombreArchivo, fileInfo.Length));

                            if (FtpStatus.Success == ftp.UploadFile(directoryPath, remoteFile, FtpRemoteExists.Overwrite))
                            {
                                ExHandler.Log.Write(string.Format("UpLoadFTPNadro - Upload exitoso: {0}", nombreArchivo));
                                historyPath = string.Format("{0}\\{1}", historyPath, nombreArchivo);
                                File.Move(directoryPath, historyPath);

                                //actualizamos status en la tabla correspondiente
                                Actualiza_Status(nombreArchivo);

                                response.Success = true;
                                return response;
                            }
                            else
                            {
                                ExHandler.Log.Write(string.Format("UpLoadFTPNadro - Fallo en upload: {0}, Intento: {1}", nombreArchivo, attempt));
                                response.Success = false;

                                if (attempt == maxRetries)
                                {
                                    if (!Directory.Exists(dirlog)) Directory.CreateDirectory(dirlog);
                                    dirlog = string.Format("{0}\\{1}", dirlog, nombreArchivo);
                                    System.IO.File.Move(directoryPath, dirlog);
                                }
                            }
                        }
                        else
                        {
                            ExHandler.Log.Write(string.Format("UpLoadFTPNadro - No se pudo conectar al FTP: {0}, Intento: {1}", host, attempt));
                            response.Success = false;

                            if (attempt == maxRetries)
                            {
                                if (!Directory.Exists(dirlog)) Directory.CreateDirectory(dirlog);
                                string directoryPath = string.Format("{0}\\{1}", rutalocal, nombreArchivo);
                                dirlog = string.Format("{0}\\{1}", dirlog, nombreArchivo);
                                System.IO.File.Move(directoryPath, dirlog);
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        response.Success = false;
                        
                        bool isGreetingError = ex.Message.Contains("greeting") || ex.Message.Contains("terminated before");
                        bool isTimeoutError = ex.Message.Contains("Timed out") || ex.Message.Contains("timeout");
                        
                        ExHandler.Log.Write(string.Format("OrdenCompraData.UpLoadFTPNadro - Error en intento {0}/{1}: {2}", 
                            attempt, maxRetries, ex.Message));
                        ExHandler.Log.Write(string.Format("OrdenCompraData.UpLoadFTPNadro - Tipo de error: Greeting={0}, Timeout={1} | InnerException: {2}", 
                            isGreetingError, isTimeoutError, ex.InnerException?.Message ?? "N/A"));
                        
                        if (attempt == maxRetries)
                        {
                            ExHandler.Log.Write(string.Format("OrdenCompraData.UpLoadFTPNadro - StackTrace: {0}", ex.StackTrace));
                        }

                        if (attempt == maxRetries)
                        {
                            try
                            {
                                if (!Directory.Exists(dirlog)) Directory.CreateDirectory(dirlog);
                                string directoryPath = string.Format("{0}\\{1}", rutalocal, nombreArchivo);
                                dirlog = string.Format("{0}\\{1}", dirlog, nombreArchivo);
                                if (System.IO.File.Exists(directoryPath))
                                {
                                    System.IO.File.Move(directoryPath, dirlog);
                                    ExHandler.Log.Write(string.Format("UpLoadFTPNadro - Archivo movido a LOG: {0}", nombreArchivo));
                                }
                            }
                            catch (Exception moveEx)
                            {
                                ExHandler.Log.Write(string.Format("UpLoadFTPNadro - Error al mover archivo: {0}", moveEx.Message));
                            }

                            return response;
                        }

                        if (attempt < maxRetries)
                        {
                            int delayMs = retryDelayMs * (int)Math.Pow(2, attempt - 1);
                            if (isGreetingError)
                            {
                                delayMs = delayMs * 2;
                                ExHandler.Log.Write(string.Format("UpLoadFTPNadro - Error de greeting, esperando {0}ms (extendido)", delayMs));
                            }
                            else
                            {
                                ExHandler.Log.Write(string.Format("UpLoadFTPNadro - Esperando {0}ms antes del siguiente intento", delayMs));
                            }
                            System.Threading.Thread.Sleep(delayMs);
                        }
                    }
                    //ftp.Disconnect();
                }
            }
            response.Success = true != default;
            return response;
        }

        public Response upLoadFTPMar(string pass, string rutalocal, string rutaftp, string puerto, string idproveedor, string usuario, string host, string nombreArchivo, string dirlog)
        {
            var response = new Response();
            int maxRetries = 3;
            int retryDelayMs = 2000;

            for (int attempt = 1; attempt <= maxRetries; attempt++)
            {
                using (var ftp = new FtpClient(host, usuario, pass))
                {
                    try
                    {
                        ftp.Port = int.Parse(puerto.ToString());
                        ftp.EncryptionMode = FtpEncryptionMode.Explicit;
                        ftp.DataConnectionType = FtpDataConnectionType.AutoPassive;
                        ftp.SocketKeepAlive = true;

                        ftp.ConnectTimeout = 30000;
                        ftp.DataConnectionReadTimeout = 120000;
                        ftp.ReadTimeout = 120000;
                        
                        // Configuración adicional para problemas de conexión
                        ftp.ValidateCertificate += (control, e) => { e.Accept = true; };
                        ftp.SocketPollInterval = 1000;
                        ftp.RetryAttempts = 2;

                        ExHandler.Log.Write(string.Format("UpLoadFTPMar - Intento {0} de {1} para archivo: {2}", attempt, maxRetries, nombreArchivo));
                        ExHandler.Log.Write(string.Format("UpLoadFTPMar - Configuración: Host={0}, Puerto={1}, Usuario={2}, EncryptionMode=Explicit", host, puerto, usuario));
                        
                        if (attempt > 1) { System.Threading.Thread.Sleep(1000); }
                        
                        ftp.Connect();
                        if (ftp.IsConnected)
                        {
                            ExHandler.Log.Write(string.Format("IsConnected.UploadFiles via FTP Mar - Host: {0}, Puerto: {1}", host, puerto));
                            if (!Directory.Exists(dirlog)) Directory.CreateDirectory(dirlog);
                            string directoryPath = string.Format("{0}\\{1}", rutalocal, nombreArchivo);
                            string pathfile = string.Format("{0}", rutalocal);
                            string remoteFile = string.Format("{0}\\{1}", rutaftp, nombreArchivo);
                            string destFileName = string.Empty;
                            string historyPath = string.Format("{0}\\historico\\{1}\\{2}\\{3}", pathfile, DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

                            if (!Directory.Exists(historyPath)) Directory.CreateDirectory(historyPath);

                            FileInfo fileInfo = new FileInfo(directoryPath);
                            ExHandler.Log.Write(string.Format("UpLoadFTPMar - Iniciando upload de archivo: {0}, Tamaño: {1} bytes", nombreArchivo, fileInfo.Length));

                            if (FtpStatus.Success == ftp.UploadFile(directoryPath, remoteFile, FtpRemoteExists.Overwrite))
                            {
                                ExHandler.Log.Write(string.Format("UpLoadFTPMar - Upload exitoso: {0}", nombreArchivo));
                                historyPath = string.Format("{0}\\{1}", historyPath, nombreArchivo);
                                File.Move(directoryPath, historyPath);

                                //actualizamos status en la tabla correspondiente
                                Actualiza_Status(nombreArchivo);

                                response.Success = true;
                                return response;
                            }
                            else
                            {
                                ExHandler.Log.Write(string.Format("UpLoadFTPMar - Fallo en upload: {0}, Intento: {1}", nombreArchivo, attempt));
                                response.Success = false;

                                if (attempt == maxRetries)
                                {
                                    if (!Directory.Exists(dirlog)) Directory.CreateDirectory(dirlog);
                                    dirlog = string.Format("{0}\\{1}", dirlog, nombreArchivo);
                                    System.IO.File.Move(directoryPath, dirlog);
                                }
                            }
                        }
                        else
                        {
                            ExHandler.Log.Write(string.Format("UpLoadFTPMar - No se pudo conectar al FTP: {0}, Intento: {1}", host, attempt));
                            response.Success = false;

                            if (attempt == maxRetries)
                            {
                                if (!Directory.Exists(dirlog)) Directory.CreateDirectory(dirlog);
                                string directoryPath = string.Format("{0}\\{1}", rutalocal, nombreArchivo);
                                dirlog = string.Format("{0}\\{1}", dirlog, nombreArchivo);
                                System.IO.File.Move(directoryPath, dirlog);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        response.Success = false;
                        
                        bool isGreetingError = ex.Message.Contains("greeting") || ex.Message.Contains("terminated before");
                        bool isTimeoutError = ex.Message.Contains("Timed out") || ex.Message.Contains("timeout");
                        
                        ExHandler.Log.Write(string.Format("OrdenCompraData.UpLoadFTPMar - Error en intento {0}/{1}: {2}", 
                            attempt, maxRetries, ex.Message));
                        ExHandler.Log.Write(string.Format("OrdenCompraData.UpLoadFTPMar - Tipo de error: Greeting={0}, Timeout={1} | InnerException: {2}", 
                            isGreetingError, isTimeoutError, ex.InnerException?.Message ?? "N/A"));
                        
                        if (attempt == maxRetries)
                        {
                            ExHandler.Log.Write(string.Format("OrdenCompraData.UpLoadFTPMar - StackTrace: {0}", ex.StackTrace));
                        }

                        if (attempt == maxRetries)
                        {
                            try
                            {
                                if (!Directory.Exists(dirlog)) Directory.CreateDirectory(dirlog);
                                string directoryPath = string.Format("{0}\\{1}", rutalocal, nombreArchivo);
                                dirlog = string.Format("{0}\\{1}", dirlog, nombreArchivo);
                                if (System.IO.File.Exists(directoryPath))
                                {
                                    System.IO.File.Move(directoryPath, dirlog);
                                    ExHandler.Log.Write(string.Format("UpLoadFTPMar - Archivo movido a LOG: {0}", nombreArchivo));
                                }
                            }
                            catch (Exception moveEx)
                            {
                                ExHandler.Log.Write(string.Format("UpLoadFTPMar - Error al mover archivo: {0}", moveEx.Message));
                            }

                            return response;
                        }

                        if (attempt < maxRetries)
                        {
                            int delayMs = retryDelayMs * (int)Math.Pow(2, attempt - 1);
                            if (isGreetingError)
                            {
                                delayMs = delayMs * 2;
                                ExHandler.Log.Write(string.Format("UpLoadFTPMar - Error de greeting, esperando {0}ms (extendido)", delayMs));
                            }
                            else
                            {
                                ExHandler.Log.Write(string.Format("UpLoadFTPMar - Esperando {0}ms antes del siguiente intento", delayMs));
                            }
                            System.Threading.Thread.Sleep(delayMs);
                        }
                    }
                    //ftp.Disconnect();
                }
            }
            response.Success = true != default;
            return response;
        }

        public Response upLoadFTPFanasa(string pass, string rutalocal, string rutaftp, string puerto, string idproveedor, string usuario, string host, string nombreArchivo, string dirlog)
        {
            var response = new Response();
            int maxRetries = 3;
            int retryDelayMs = 2000; // 2 segundos inicial

            // Obtenemos la información a cargar vía FTP con reintentos
            for (int attempt = 1; attempt <= maxRetries; attempt++)
            {
                using (var ftp = new FtpClient(host, usuario, pass))
                {
                    try
                    {
                        ftp.Port = int.Parse(puerto.ToString());
                        //ftp.Port = 5021;
                        //ftp.EncryptionMode = FtpEncryptionMode.Explicit;
                        ftp.EncryptionMode = FtpEncryptionMode.None;
                        ftp.DataConnectionType = FtpDataConnectionType.AutoPassive;
                        ftp.SocketKeepAlive = true;

                        // Configuración robusta para evitar errores de greeting y timeout
                        ftp.ConnectTimeout = 30000; // 30 segundos
                        ftp.DataConnectionReadTimeout = 120000; // 120 segundos (2 minutos)
                        ftp.ReadTimeout = 120000; // 120 segundos (2 minutos)
                        
                        // Configuración adicional para problemas de conexión
                        ftp.ValidateCertificate += (control, e) => { e.Accept = true; }; // Aceptar certificados auto-firmados
                        ftp.SocketPollInterval = 1000; // Intervalo de polling del socket
                        ftp.RetryAttempts = 2; // Reintentos internos de FluentFTP
                        
                        ExHandler.Log.Write(string.Format("UpLoadFTPFanasa - Intento {0} de {1} para archivo: {2}", attempt, maxRetries, nombreArchivo));
                        ExHandler.Log.Write(string.Format("UpLoadFTPFanasa - Configuración: Host={0}, Puerto={1}, Usuario={2}, EncryptionMode=None", host, puerto, usuario));
                        
                        // Pequeño delay antes de conectar para servidores lentos
                        if (attempt > 1)
                        {
                            System.Threading.Thread.Sleep(1000);
                        }
                        
                        ftp.Connect();
                        if (ftp.IsConnected)
                        {
                            ExHandler.Log.Write(string.Format("IsConnected.UploadFiles via FTP Fanasa - Host: {0}, Puerto: {1}", host, puerto));
                            if (!Directory.Exists(dirlog)) Directory.CreateDirectory(dirlog);
                            string directoryPath = string.Format("{0}\\{1}", rutalocal, nombreArchivo);
                            string pathfile = string.Format("{0}", rutalocal);
                            string remoteFile = string.Format("{0}\\{1}", rutaftp, nombreArchivo);
                            string destFileName = string.Empty;
                            string historyPath = string.Format("{0}\\historico\\{1}\\{2}\\{3}", pathfile, DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

                            if (!Directory.Exists(historyPath)) Directory.CreateDirectory(historyPath);

                            // Verificar tamaño del archivo antes de subir
                            FileInfo fileInfo = new FileInfo(directoryPath);
                            ExHandler.Log.Write(string.Format("UpLoadFTPFanasa - Iniciando upload de archivo: {0}, Tamaño: {1} bytes", nombreArchivo, fileInfo.Length));

                            if (FtpStatus.Success == ftp.UploadFile(directoryPath, remoteFile, FtpRemoteExists.Overwrite))
                            {
                                ExHandler.Log.Write(string.Format("UpLoadFTPFanasa - Upload exitoso: {0}", nombreArchivo));
                                historyPath = string.Format("{0}\\{1}", historyPath, nombreArchivo);
                                File.Move(directoryPath, historyPath);

                                //actualizamos status en la tabla correspondiente
                                Actualiza_Status(nombreArchivo);

                                response.Success = true;
                                return response; // Salir del bucle de reintentos si es exitoso
                            }
                            else
                            {
                                ExHandler.Log.Write(string.Format("UpLoadFTPFanasa - Fallo en upload (FtpStatus != Success): {0}, Intento: {1}", nombreArchivo, attempt));
                                response.Success = false;

                                if (attempt == maxRetries)
                                {
                                    // Solo mover a LOG si es el último intento
                                    if (!Directory.Exists(dirlog)) Directory.CreateDirectory(dirlog);
                                    dirlog = string.Format("{0}\\{1}", dirlog, nombreArchivo);
                                    System.IO.File.Move(directoryPath, dirlog);
                                }
                            }
                        }
                        else
                        {
                            ExHandler.Log.Write(string.Format("UpLoadFTPFanasa - No se pudo conectar al FTP: {0}, Intento: {1}", host, attempt));
                            response.Success = false;

                            if (attempt == maxRetries)
                            {
                                // Solo mover a LOG si es el último intento
                                if (!Directory.Exists(dirlog)) Directory.CreateDirectory(dirlog);
                                string directoryPath = string.Format("{0}\\{1}", rutalocal, nombreArchivo);
                                dirlog = string.Format("{0}\\{1}", dirlog, nombreArchivo);
                                System.IO.File.Move(directoryPath, dirlog);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        response.Success = false;
                        
                        // Identificar el tipo de error específico
                        bool isGreetingError = ex.Message.Contains("greeting") || ex.Message.Contains("terminated before");
                        bool isTimeoutError = ex.Message.Contains("Timed out") || ex.Message.Contains("timeout");
                        
                        ExHandler.Log.Write(string.Format("OrdenCompraData.UpLoadFTPFanasa - Error en intento {0}/{1}: {2}", 
                            attempt, maxRetries, ex.Message));
                        ExHandler.Log.Write(string.Format("OrdenCompraData.UpLoadFTPFanasa - Tipo de error: Greeting={0}, Timeout={1} | InnerException: {2}", 
                            isGreetingError, isTimeoutError, ex.InnerException?.Message ?? "N/A"));
                        
                        // Solo incluir StackTrace en el último intento para no saturar logs
                        if (attempt == maxRetries)
                        {
                            ExHandler.Log.Write(string.Format("OrdenCompraData.UpLoadFTPFanasa - StackTrace: {0}", ex.StackTrace));
                        }

                        // Si es el último intento, mover archivo a LOG
                        if (attempt == maxRetries)
                        {
                            try
                            {
                                if (!Directory.Exists(dirlog)) Directory.CreateDirectory(dirlog);
                                string directoryPath = string.Format("{0}\\{1}", rutalocal, nombreArchivo);
                                dirlog = string.Format("{0}\\{1}", dirlog, nombreArchivo);
                                if (System.IO.File.Exists(directoryPath))
                                {
                                    System.IO.File.Move(directoryPath, dirlog);
                                    ExHandler.Log.Write(string.Format("UpLoadFTPFanasa - Archivo movido a LOG después de {0} intentos: {1}", maxRetries, nombreArchivo));
                                }
                            }
                            catch (Exception moveEx)
                            {
                                ExHandler.Log.Write(string.Format("UpLoadFTPFanasa - Error al mover archivo a LOG: {0}", moveEx.Message));
                            }

                            return response; // Salir si ya agotamos todos los reintentos
                        }

                        // Espera exponencial antes del siguiente reintento
                        // Para errores de greeting, esperar más tiempo (servidor puede estar sobrecargado)
                        if (attempt < maxRetries)
                        {
                            int delayMs = retryDelayMs * (int)Math.Pow(2, attempt - 1);
                            if (isGreetingError)
                            {
                                delayMs = delayMs * 2; // Doble de tiempo para errores de greeting
                                ExHandler.Log.Write(string.Format("UpLoadFTPFanasa - Error de greeting detectado, esperando {0}ms (extendido) antes del siguiente intento", delayMs));
                            }
                            else
                            {
                                ExHandler.Log.Write(string.Format("UpLoadFTPFanasa - Esperando {0}ms antes del siguiente intento", delayMs));
                            }
                            System.Threading.Thread.Sleep(delayMs);
                        }
                    }
                    //ftp.Disconnect();
                }
            }

            response.Success = true != default;
            return response;
        }

        public Response upLoadFTPLevic(string pass, string rutalocal, string rutaftp, string puerto, string idproveedor, string usuario, string host, string nombreArchivo, string dirlog)
        {
            var response = new Response();
            int maxRetries = 3;
            int retryDelayMs = 2000;

            for (int attempt = 1; attempt <= maxRetries; attempt++)
            {
                using (var ftp = new FtpClient(host, usuario, pass))
                {
                    try
                    {
                        ftp.Port = int.Parse(puerto.ToString());
                        ftp.EncryptionMode = FtpEncryptionMode.Explicit;
                        ftp.DataConnectionType = FtpDataConnectionType.AutoPassive;
                        ftp.SocketKeepAlive = true;

                        ftp.ConnectTimeout = 30000;
                        ftp.DataConnectionReadTimeout = 120000;
                        ftp.ReadTimeout = 120000;
                        
                        // Configuración adicional para problemas de conexión
                        ftp.ValidateCertificate += (control, e) => { e.Accept = true; };
                        ftp.SocketPollInterval = 1000;
                        ftp.RetryAttempts = 2;

                        ExHandler.Log.Write(string.Format("UpLoadFTPLevic - Intento {0} de {1} para archivo: {2}", attempt, maxRetries, nombreArchivo));
                        ExHandler.Log.Write(string.Format("UpLoadFTPLevic - Configuración: Host={0}, Puerto={1}, Usuario={2}, EncryptionMode=Explicit", host, puerto, usuario));
                        
                        if (attempt > 1) { System.Threading.Thread.Sleep(1000); }
                        
                        ftp.Connect();
                        if (ftp.IsConnected)
                        {
                            ExHandler.Log.Write(string.Format("IsConnected.UploadFiles via FTP Levic - Host: {0}, Puerto: {1}", host, puerto));
                            if (!Directory.Exists(dirlog)) Directory.CreateDirectory(dirlog);
                            string directoryPath = string.Format("{0}\\{1}", rutalocal, nombreArchivo);
                            string pathfile = string.Format("{0}", rutalocal);
                            string remoteFile = string.Format("{0}\\{1}", rutaftp, nombreArchivo);
                            string destFileName = string.Empty;
                            string historyPath = string.Format("{0}\\historico\\{1}\\{2}\\{3}", pathfile, DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                            if (!Directory.Exists(historyPath)) Directory.CreateDirectory(historyPath);

                            string archivolevic = "PEDIDO.txt";
                            string directorylevic = string.Empty;
                            directorylevic = string.Format("{0}\\{1}", rutalocal, archivolevic);

                            if (Directory.Exists(directorylevic)) { Directory.Delete(directorylevic); }

                            File.Copy(directoryPath, directorylevic);

                            string remoteFilelevic = string.Format("{0}\\{1}", rutaftp, archivolevic);

                            FileInfo fileInfo = new FileInfo(directoryPath);
                            ExHandler.Log.Write(string.Format("UpLoadFTPLevic - Iniciando upload de archivo: {0}, Tamaño: {1} bytes", nombreArchivo, fileInfo.Length));

                            if (FtpStatus.Success == ftp.UploadFile(directorylevic, remoteFilelevic, FtpRemoteExists.AppendNoCheck))
                            {
                                ExHandler.Log.Write(string.Format("UpLoadFTPLevic - Upload exitoso: {0}", nombreArchivo));
                                historyPath = string.Format("{0}\\{1}", historyPath, nombreArchivo);
                                File.Move(directoryPath, historyPath);

                                //actualizamos status en la tabla correspondiente
                                Actualiza_Status(nombreArchivo);
                                System.IO.File.Delete(directorylevic);

                                response.Success = true;
                                return response;
                            }
                            else
                            {
                                ExHandler.Log.Write(string.Format("UpLoadFTPLevic - Fallo en upload: {0}, Intento: {1}", nombreArchivo, attempt));
                                response.Success = false;

                                if (attempt == maxRetries)
                                {
                                    if (!Directory.Exists(dirlog)) Directory.CreateDirectory(dirlog);
                                    dirlog = string.Format("{0}\\{1}", dirlog, nombreArchivo);
                                    System.IO.File.Move(directoryPath, dirlog);
                                }

                                System.IO.File.Delete(directorylevic);
                            }
                        }
                        else
                        {
                            ExHandler.Log.Write(string.Format("UpLoadFTPLevic - No se pudo conectar al FTP: {0}, Intento: {1}", host, attempt));
                            response.Success = false;

                            if (attempt == maxRetries)
                            {
                                if (!Directory.Exists(dirlog)) Directory.CreateDirectory(dirlog);
                                string directoryPath = string.Format("{0}\\{1}", rutalocal, nombreArchivo);
                                dirlog = string.Format("{0}\\{1}", dirlog, nombreArchivo);
                                System.IO.File.Move(directoryPath, dirlog);
                            }

                            string archivolevic = "PEDIDO.txt";
                            string directorylevic = string.Format("{0}\\{1}", rutalocal, archivolevic);
                            if (System.IO.File.Exists(directorylevic))
                            {
                                System.IO.File.Delete(directorylevic);
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        response.Success = false;
                        
                        bool isGreetingError = ex.Message.Contains("greeting") || ex.Message.Contains("terminated before");
                        bool isTimeoutError = ex.Message.Contains("Timed out") || ex.Message.Contains("timeout");
                        
                        ExHandler.Log.Write(string.Format("OrdenCompraData.UpLoadFTPLevic - Error en intento {0}/{1}: {2}", 
                            attempt, maxRetries, ex.Message));
                        ExHandler.Log.Write(string.Format("OrdenCompraData.UpLoadFTPLevic - Tipo de error: Greeting={0}, Timeout={1} | InnerException: {2}", 
                            isGreetingError, isTimeoutError, ex.InnerException?.Message ?? "N/A"));
                        
                        if (attempt == maxRetries)
                        {
                            ExHandler.Log.Write(string.Format("OrdenCompraData.UpLoadFTPLevic - StackTrace: {0}", ex.StackTrace));
                        }

                        if (attempt == maxRetries)
                        {
                            try
                            {
                                if (!Directory.Exists(dirlog)) Directory.CreateDirectory(dirlog);
                                string directoryPath = string.Format("{0}\\{1}", rutalocal, nombreArchivo);
                                dirlog = string.Format("{0}\\{1}", dirlog, nombreArchivo);
                                if (System.IO.File.Exists(directoryPath))
                                {
                                    System.IO.File.Move(directoryPath, dirlog);
                                    ExHandler.Log.Write(string.Format("UpLoadFTPLevic - Archivo movido a LOG: {0}", nombreArchivo));
                                }

                                string archivolevic = "PEDIDO.txt";
                                string directorylevic = string.Format("{0}\\{1}", rutalocal, archivolevic);
                                if (System.IO.File.Exists(directorylevic))
                                {
                                    System.IO.File.Delete(directorylevic);
                                }
                            }
                            catch (Exception moveEx)
                            {
                                ExHandler.Log.Write(string.Format("UpLoadFTPLevic - Error al mover archivo: {0}", moveEx.Message));
                            }

                            return response;
                        }

                        // Limpiar archivo temporal antes del reintento
                        try
                        {
                            string archivolevic = "PEDIDO.txt";
                            string directorylevic = string.Format("{0}\\{1}", rutalocal, archivolevic);
                            if (System.IO.File.Exists(directorylevic))
                            {
                                System.IO.File.Delete(directorylevic);
                            }
                        }
                        catch { }

                        if (attempt < maxRetries)
                        {
                            int delayMs = retryDelayMs * (int)Math.Pow(2, attempt - 1);
                            if (isGreetingError)
                            {
                                delayMs = delayMs * 2;
                                ExHandler.Log.Write(string.Format("UpLoadFTPLevic - Error de greeting, esperando {0}ms (extendido)", delayMs));
                            }
                            else
                            {
                                ExHandler.Log.Write(string.Format("UpLoadFTPLevic - Esperando {0}ms antes del siguiente intento", delayMs));
                            }
                            System.Threading.Thread.Sleep(delayMs);
                        }
                    }
                    //ftp.Disconnect();
                }
            }

            response.Success = true != default;
            return response;
        }

        #endregion Envio por FTP archivo x archivo

        #region Proceso Nuevo una sola conexion
        //public Response upLoadFTPNadro(string pass, string rutalocal, string rutaftp, string puerto, string usuario, string host, string dirlog)
        //{
        //    var response = new Response();
        //    using (var ftp = new FtpClient(host, int.Parse(puerto.ToString()), usuario, pass))
        //    {
        //        try
        //        {
        //            ftp.EncryptionMode = FtpEncryptionMode.None;
        //            ftp.DataConnectionType = FtpDataConnectionType.AutoPassive;
        //            ftp.ConnectTimeout = 25000;
        //            ftp.DataConnectionReadTimeout = 15000;
        //            ftp.SocketKeepAlive = true;

        //            ftp.Connect();
        //            if (ftp.IsConnected)
        //            {
        //                var archivos = Directory.GetFiles(rutalocal);

        //                if (!Directory.Exists(dirlog)) Directory.CreateDirectory(dirlog);

        //                foreach (var item in archivos)
        //                {
        //                    DirectoryInfo di = new DirectoryInfo(item);
        //                    var arch = di.Name;

        //                    string directoryPath = string.Format("{0}\\{1}", rutalocal, arch);
        //                    string pathfile = string.Format("{0}", rutalocal);
        //                    string remoteFile = string.Format("{0}\\{1}", rutaftp, arch);
        //                    string destFileName = string.Empty;
        //                    string historyPath = string.Format("{0}\\historico\\{1}\\{2}\\{3}", pathfile, DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

        //                    if (!Directory.Exists(historyPath)) Directory.CreateDirectory(historyPath);

        //                    if (FtpStatus.Success == ftp.UploadFile(directoryPath, remoteFile, FtpRemoteExists.Overwrite))
        //                    {
        //                        historyPath = string.Format("{0}\\{1}", historyPath, arch);
        //                        File.Move(directoryPath, historyPath);

        //                        //actualizamos status en la tabla correspondiente
        //                        Actualiza_Status(arch);
        //                    }
        //                    else
        //                    {
        //                        response.Success = false;

        //                        if (!Directory.Exists(dirlog)) Directory.CreateDirectory(dirlog);

        //                        var log = string.Empty;
        //                        log = string.Format("{0}\\{1}", dirlog, arch);
        //                        System.IO.File.Move(directoryPath, log);
        //                    }
        //                }
        //            }
        //            ftp.Disconnect();
        //        }
        //        catch (Exception ex)
        //        {
        //            response.Success = false;
        //            ExHandler.Log.Write(string.Format("OrdenCompraData.UpLoadFTPNadro {0}", ex.Message));

        //            if (!Directory.Exists(dirlog)) Directory.CreateDirectory(dirlog);

        //            var archivos = Directory.GetFiles(rutalocal);
        //            foreach (var item in archivos)
        //            {
        //                DirectoryInfo di = new DirectoryInfo(item);
        //                var arch = di.Name;

        //                string directoryPath = string.Format("{0}\\{1}", rutalocal, arch);

        //                var log = string.Empty;
        //                log = string.Format("{0}\\{1}", dirlog, arch);
        //                System.IO.File.Move(directoryPath, log);
        //            }
        //        }
        //        ftp.Dispose();
        //    }
        //    response.Success = true != default;
        //    return response;
        //}

        //public Response upLoadFTPMar(string pass, string rutalocal, string rutaftp, string puerto, string usuario, string host, string dirlog)
        //{
        //    var response = new Response();
        //    using (var ftp = new FtpClient(host, int.Parse(puerto.ToString()), usuario, pass))
        //    {
        //        try
        //        {
        //            ftp.EncryptionMode = FtpEncryptionMode.None;
        //            ftp.DataConnectionType = FtpDataConnectionType.AutoPassive;
        //            ftp.ConnectTimeout = 25000;
        //            ftp.DataConnectionReadTimeout = 15000;
        //            ftp.SocketKeepAlive = true;


        //            ftp.Connect();
        //            if (ftp.IsConnected)
        //            {
        //                var archivos = Directory.GetFiles(rutalocal);

        //                foreach (var item in archivos)
        //                {
        //                    DirectoryInfo di = new DirectoryInfo(item);
        //                    var arch = di.Name;

        //                    string directoryPath = string.Format("{0}\\{1}", rutalocal, arch);
        //                    string pathfile = string.Format("{0}", rutalocal);
        //                    string remoteFile = string.Format("{0}\\{1}", rutaftp, arch);
        //                    string destFileName = string.Empty;
        //                    string historyPath = string.Format("{0}\\historico\\{1}\\{2}\\{3}", pathfile, DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

        //                    if (!Directory.Exists(historyPath)) Directory.CreateDirectory(historyPath);

        //                    if (FtpStatus.Success == ftp.UploadFile(directoryPath, remoteFile, FtpRemoteExists.Overwrite))
        //                    {
        //                        historyPath = string.Format("{0}\\{1}", historyPath, arch);
        //                        File.Move(directoryPath, historyPath);

        //                        //actualizamos status en la tabla correspondiente
        //                        Actualiza_Status(arch);
        //                    }
        //                    else
        //                    {
        //                        response.Success = false;

        //                        if (!Directory.Exists(dirlog)) Directory.CreateDirectory(dirlog);

        //                        var log = string.Empty;
        //                        log = string.Format("{0}\\{1}", dirlog, arch);
        //                        System.IO.File.Move(directoryPath, log);
        //                    }
        //                }
        //            }
        //            ftp.Disconnect();
        //        }
        //        catch (Exception ex)
        //        {
        //            response.Success = false;
        //            ExHandler.Log.Write(string.Format("OrdenCompraData.UpLoadFTPMar {0}", ex.Message));

        //            if (!Directory.Exists(dirlog)) Directory.CreateDirectory(dirlog);

        //            var archivos = Directory.GetFiles(rutalocal);
        //            foreach (var item in archivos)
        //            {
        //                var log = string.Empty;

        //                DirectoryInfo di = new DirectoryInfo(item);
        //                var arch = di.Name;

        //                string directoryPath = string.Format("{0}\\{1}", rutalocal, arch);
        //                log = string.Format("{0}\\{1}", dirlog, arch);
        //                System.IO.File.Move(directoryPath, log);
        //            }
        //        }
        //        ftp.Dispose();
        //    }
        //    response.Success = true != default;
        //    return response;
        //}

        //public Response upLoadFTPFanasa(string pass, string rutalocal, string rutaftp, string puerto, string usuario, string host, string dirlog)
        //{
        //    var response = new Response();
        //    using (var ftp = new FtpClient(host, int.Parse(puerto.ToString()), usuario, pass))
        //    {
        //        try
        //        {
        //            ftp.EncryptionMode = FtpEncryptionMode.None;
        //            ftp.DataConnectionType = FtpDataConnectionType.AutoPassive;
        //            ftp.ConnectTimeout = 25000;
        //            ftp.DataConnectionReadTimeout = 15000;
        //            ftp.SocketKeepAlive = true;

        //            ftp.Connect();
        //            if (ftp.IsConnected)
        //            {
        //                var archivos = Directory.GetFiles(rutalocal);

        //                if (!Directory.Exists(dirlog)) Directory.CreateDirectory(dirlog);

        //                foreach (var item in archivos)
        //                {
        //                    DirectoryInfo di = new DirectoryInfo(item);
        //                    var arch = di.Name;

        //                    string directoryPath = string.Format("{0}\\{1}", rutalocal, arch);
        //                    string pathfile = string.Format("{0}", rutalocal);
        //                    string remoteFile = string.Format("{0}\\{1}", rutaftp, arch);
        //                    string destFileName = string.Empty;
        //                    string historyPath = string.Format("{0}\\historico\\{1}\\{2}\\{3}", pathfile, DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

        //                    if (!Directory.Exists(historyPath)) Directory.CreateDirectory(historyPath);

        //                    if (FtpStatus.Success == ftp.UploadFile(directoryPath, remoteFile, FtpRemoteExists.Overwrite))
        //                    {
        //                        historyPath = string.Format("{0}\\{1}", historyPath, arch);
        //                        File.Move(directoryPath, historyPath);

        //                        //actualizamos status en la tabla correspondiente
        //                        Actualiza_Status(arch);
        //                    }
        //                    else
        //                    {
        //                        response.Success = false;

        //                        if (!Directory.Exists(dirlog)) Directory.CreateDirectory(dirlog);

        //                        var log = string.Empty;
        //                        log = string.Format("{0}\\{1}", dirlog, arch);
        //                        System.IO.File.Move(directoryPath, log);
        //                    }
        //                }
        //            }
        //            ftp.Disconnect();
        //        }
        //        catch (Exception ex)
        //        {
        //            response.Success = false;
        //            ExHandler.Log.Write(string.Format("OrdenCompraData.UpLoadFTPFan {0}", ex.Message));

        //            if (!Directory.Exists(dirlog)) Directory.CreateDirectory(dirlog);

        //            var archivos = Directory.GetFiles(rutalocal);
        //            foreach (var item in archivos)
        //            {
        //                var log = string.Empty;

        //                DirectoryInfo di = new DirectoryInfo(item);
        //                var arch = di.Name;

        //                string directoryPath = string.Format("{0}\\{1}", rutalocal, arch);
        //                log = string.Format("{0}\\{1}", dirlog, arch);
        //                System.IO.File.Move(directoryPath, log);
        //            }
        //        }
        //        ftp.Dispose();
        //    }
        //    response.Success = true != default;
        //    return response;
        //}

        //public Response upLoadFTPLevic(string pass, string rutalocal, string rutaftp, string puerto, string usuario, string host, string dirlog)
        //{
        //    var response = new Response();
        //    using (var ftp = new FtpClient(host, int.Parse(puerto.ToString()), usuario, pass))
        //    {
        //        try
        //        {
        //            ftp.EncryptionMode = FtpEncryptionMode.None;
        //            ftp.DataConnectionType = FtpDataConnectionType.AutoPassive;
        //            ftp.ConnectTimeout = 25000;
        //            ftp.DataConnectionReadTimeout = 15000;
        //            ftp.SocketKeepAlive = true;

        //            ftp.Connect();
        //            if (ftp.IsConnected)
        //            {
        //                var archivos = Directory.GetFiles(rutalocal);

        //                if (!Directory.Exists(dirlog)) Directory.CreateDirectory(dirlog);

        //                foreach (var item in archivos)
        //                {
        //                    DirectoryInfo di = new DirectoryInfo(item);
        //                    var arch = di.Name;

        //                    string directoryPath = string.Format("{0}\\{1}", rutalocal, arch);
        //                    string pathfile = string.Format("{0}", rutalocal);
        //                    string remoteFile = string.Format("{0}\\{1}", rutaftp, arch);
        //                    string destFileName = string.Empty;
        //                    string historyPath = string.Format("{0}\\historico\\{1}\\{2}\\{3}", pathfile, DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

        //                    if (!Directory.Exists(historyPath)) Directory.CreateDirectory(historyPath);

        //                    string archivolevic = "PEDIDO.txt";
        //                    string directorylevic = string.Empty;
        //                    directorylevic = string.Format("{0}\\{1}", rutalocal, archivolevic);

        //                    if (Directory.Exists(directorylevic)) Directory.Delete(directorylevic);
        //                    File.Copy(directoryPath, directorylevic);
        //                    string remoteFilelevic = string.Format("{0}\\{1}", rutaftp, archivolevic);

        //                    if (FtpStatus.Success == ftp.UploadFile(directorylevic, remoteFilelevic, FtpRemoteExists.AppendNoCheck))
        //                    {
        //                        historyPath = string.Format("{0}\\{1}", historyPath, arch);
        //                        File.Move(directoryPath, historyPath);

        //                        //actualizamos status en la tabla correspondiente
        //                        Actualiza_Status(arch);
        //                        System.IO.File.Delete(directorylevic);
        //                    }
        //                    else
        //                    {
        //                        response.Success = false;

        //                        if (!Directory.Exists(dirlog)) Directory.CreateDirectory(dirlog);

        //                        var log = string.Empty;
        //                        log = string.Format("{0}\\{1}", dirlog, arch);
        //                        System.IO.File.Move(directoryPath, log);

        //                        System.IO.File.Delete(directorylevic);
        //                    }
        //                }
        //            }
        //            ftp.Disconnect();
        //        }
        //        catch (Exception ex)
        //        {
        //            response.Success = false;
        //            ExHandler.Log.Write(string.Format("OrdenCompraData.UpLoadFTPLev {0}", ex.Message));

        //            if (!Directory.Exists(dirlog)) Directory.CreateDirectory(dirlog);

        //            var archivos = Directory.GetFiles(rutalocal);
        //            foreach (var item in archivos)
        //            {
        //                DirectoryInfo di = new DirectoryInfo(item);
        //                var arch = di.Name;

        //                var log = string.Empty;

        //                string directoryPath = string.Format("{0}\\{1}", rutalocal, arch);
        //                log = string.Format("{0}\\{1}", dirlog, arch);
        //                System.IO.File.Move(directoryPath, log);

        //                string archivolevic = "PEDIDO.txt";
        //                string directorylevic = string.Format("{0}\\{1}", rutalocal, archivolevic);
        //                System.IO.File.Delete(directorylevic);
        //            }
        //        }
        //        ftp.Dispose();
        //    }
        //    response.Success = true != default;
        //    return response;
        //}
        #endregion Proceso Nuevo


        public async static Task EnviarCorreo(string[] Mensaje, string cadena)
        {
            ExHandler.Log.Write($"Start.EnviarCorreo");
            try
            {
                if (Mensaje.Length > 0)
                {
                    var tenantID = ExHandler.Cryptography.Decrypt(ConfigurationManager.AppSettings["TenantIdMail"]);
                    var ClientID = ExHandler.Cryptography.Decrypt(ConfigurationManager.AppSettings["ClientIdMail"]);
                    var ClientSecret = ExHandler.Cryptography.Decrypt(ConfigurationManager.AppSettings["ClientSecretMail"]);

                    // 1) Construir cliente confidencial
                    var credential = new ClientSecretCredential(tenantID, ClientID, ClientSecret);
                    var graphClient = new GraphServiceClient(credential);

                    //Obtener Correos destino
                    var lista = new List<Correos>();

                    var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");

                    string query = "SELECT Mail FROM [mta].[GRFP_CAT_CORREOS_PEDIDOS_MTA] where oFlag = 'N'";
                    using (DbCommand command = db.GetSqlStringCommand(query))
                    {
                        using (IDataReader reader = db.ExecuteReader(command))
                        {
                            while (reader.Read())
                            {
                                var cliente = new Correos
                                {
                                    Mail = reader["Mail"].ToString()
                                };

                                lista.Add(cliente);
                            }
                        }
                    }

                    var destinatarios = lista.Select(x => new Recipient { EmailAddress = new EmailAddress { Address = x.Mail } }).ToList();

                    // Construir mensaje
                    DateTime fecha = (DateTime.Now);

                    var sujeto = string.Empty;
                    var cuerpo = string.Empty;

                    if (cadena.Contains("SF"))
                    {
                        sujeto = "PRUEBA Errores en Envio a FTP Pedido San Francisco";
                        cuerpo = "<h1> Los siguientes archivos adjuntos de la Cadena San Francisco no fueron enviados mediante FTP debido a un error </h1>";
                    }
                    else if (cadena.Contains("FT"))
                    {
                        sujeto = "PRUEBA Errores en Envio a FTP Pedido en FarmaTodo";
                        cuerpo = "<h1> Los siguientes archivos adjuntos de la Cadena FarmaTodo no fueron enviados mediante FTP debido a un error </h1>";
                    }
                    else if (cadena.Contains("UN"))
                    {
                        sujeto = "PRUEBA Errores en Envio a FTP Pedido en Union";
                        cuerpo = "<h1>Los siguientes archivos adjuntos de la Cadena Union no fueron enviados mediante FTP debido a un error </h1>";
                    }

                    var email = new Message
                    {
                        Subject = sujeto,
                        Body = new ItemBody
                        {
                            ContentType = BodyType.Html,
                            Content = cuerpo
                        },
                        ToRecipients = destinatarios,
                        // AGREGAR ESTA LÍNEA:
                        Attachments = new List<Attachment>()
                    };

                    //Adjunto archivos
                    foreach (var ruta in Mensaje)
                    {
                        if (File.Exists(ruta))
                        {
                            // Usamos una variable para los bytes y evitar leer el archivo dos veces
                            var bytes = File.ReadAllBytes(ruta);

                            email.Attachments.Add(new FileAttachment
                            {
                                OdataType = "#microsoft.graph.fileAttachment",
                                Name = Path.GetFileName(ruta),
                                ContentBytes = bytes, // Usar la variable de arriba
                            });
                        }
                    }

                    var requestBody = new Microsoft.Graph.Users.Item.SendMail.SendMailPostRequestBody
                    {
                        Message = email,
                        SaveToSentItems = true
                    };

                    // ENVIAR CON EL FIX DE BLOQUEO (ConfigureAwait):
                    await graphClient.Users[ConfigurationManager.AppSettings["UserName"]].SendMail.PostAsync(requestBody).ConfigureAwait(false); // <--- ESTO ROMPE EL BLOQUEO

                }
                ExHandler.Log.Write($"Success.EnviarCorreo");
            }
            catch (Exception ex)
            {
                ExHandler.Log.Write($"Error.EnviarCorreo {ex.Message}");
            }
        }

        //public string sendGmail(string[] Mensaje, string cadena)
        //{
        //    SmtpClient client = new SmtpClient();

        //    client.EnableSsl = true; //socket security layer https
        //    client.Host = ConfigurationManager.AppSettings["Host"];
        //    client.Port = int.Parse(ConfigurationManager.AppSettings["Port"]);

        //    System.Net.NetworkCredential credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["UserName"], ConfigurationManager.AppSettings["Password"]);
        //    client.UseDefaultCredentials = false;
        //    client.Credentials = credentials;

        //    MailMessage oMail = new MailMessage();
        //    oMail.From = new MailAddress("mail.datahub@rfp.mx");

        //    //oMail.To.Add(new MailAddress("alejandro.guerrero@rfp.mx"));
        //    //oMail.To.Add(new MailAddress("erik.villa@rfp.mx"));
        //    //oMail.CC.Add(new MailAddress("daniel.hernandezh@farmaciasunion.com"));
        //    //oMail.CC.Add(new MailAddress("natividad.hernadeza@farmaciasunion.com"));
        //    //oMail.CC.Add(new MailAddress("israel.ramos@rfp.mx"));
        //    oMail.CC.Add(new MailAddress("mauricio.montes@apso.com.mx"));
        //    //oMail.CC.Add(new MailAddress("oliva.jimenez@rfp.mx"));

        //    oMail.IsBodyHtml = true;

        //    if (cadena.Contains("SF"))
        //    {
        //        oMail.Subject = "Errores en Envio a FTP San Francisco";
        //        oMail.Body = "<h1> Los siguientes archivos adjuntos de la Cadena San Francisco no fueron enviados mediante FTP debido a un error </h1> ";
        //    }
        //    else if (cadena.Contains("FT"))
        //    {
        //        oMail.Subject = "Errores en Envio a FTP en FarmaTodo";
        //        oMail.Body = "<h1> Los siguientes archivos adjuntos de la Cadena FarmaTodo no fueron enviados mediante FTP debido a un error </h1> ";
        //    }
        //    else if (cadena.Contains("UN"))
        //    {
        //        oMail.Subject = "Errores en Envio a FTP en Union";
        //        oMail.Body = "<h1> Los siguientes archivos adjuntos de la Cadena Union no fueron enviados mediante FTP debido a un error </h1> ";
        //    }
        //    oMail.IsBodyHtml = true;
        //    oMail.Body = "<h1> Los siguientes archivos adjuntos no fueron enviados mediante FTP debido a un error </h1> ";
        //    oMail.Priority = MailPriority.High;

        //    foreach (var archivo in Mensaje)
        //    {
        //        oMail.Attachments.Add(new Microsoft.Graph.Models.Attachment(archivo));
        //    }

        //    try
        //    {
        //        if (oMail.Attachments.Count > 0)
        //        {
        //            client.Send(oMail);
        //        }

        //        client.Dispose();
        //        oMail.Dispose();
        //        return " correo enviado, favor de revisar bandeja SPAM en caso de no recibir en Pagina Principal";
        //    }
        //    catch (Exception ex)
        //    {
        //        ExHandler.Log.Write(string.Format("ErrorEnvioMail.SendMail {0}", ex.Message));
        //        return "Error en el envío de Mail.SendMail" + ex.Message;
        //    }

        //}
        public string Alerta_Archivos(long idSugerido)
        {
            var response = new Response();
            try
            {
                var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");
                var command = db.GetStoredProcCommand("[mta].[SP_GRFP_ALERTA_ARCHIVOS_CREADOS]");
                db.AddInParameter(command, "@IdSugerido", DbType.Int64, idSugerido);
                db.ExecuteNonQuery(command);
                command.CommandTimeout = 0;

            }
            catch (Exception ex)
            {
                ExHandler.Log.Write(string.Format("ErrorEnvioMail.AlertaArchivos {0}", ex.Message));

            }

            response.Message = "Correo Enviado";
            response.Success = true != default;

            return response.ToString();
        }
        public string Actualiza_Status(string archivo)
        {
            var response = new Response();

            try
            {
                var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");
                var command = db.GetStoredProcCommand("[mta].[Actualiza_STATUS_FTP]");
                db.AddInParameter(command, "@NombreArchivo", DbType.String, archivo);
                db.ExecuteNonQuery(command);
                command.CommandTimeout = 0;
            }
            catch (Exception ex)
            {
                ExHandler.Log.Write(string.Format("ErrorEnvioMail.Actualiza_Status {0}", ex.Message));
            }
            response.Message = "Actualizado";
            response.Success = true != default;

            return response.ToString();
        }
        public string Alerta_Archivos_FTP(long idSugerido)
        {
            var response = new Response();

            try
            {
                var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");
                var command = db.GetStoredProcCommand("[mta].[sp_Alerta_Envio_FTP]");
                db.AddInParameter(command, "@IDSUGERIDOEXE", DbType.Int64, idSugerido);
                db.ExecuteNonQuery(command);
                command.CommandTimeout = 0;
            }
            catch (Exception ex)
            {
                ExHandler.Log.Write(string.Format("ErrorEnvioMail.Alerta_Archivos_FTP {0}", ex.Message));
            }

            response.Message = "Correo Enviado";
            response.Success = true != default;

            return response.ToString();
        }
    }
}