using Azure;
using Azure.Identity;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Graph;
using Microsoft.Graph.Models;
using Microsoft.Practices.EnterpriseLibrary.Data;
using PortalGRFP.Entities.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace PortalGRFP.Data.OC
{
    public class PedidosData
    {
        #region ENVIO POR API (Obsoleto pero sirve de guia para otras API)
        //public static List<EliminarOrdenModel> ListarPedidosPorAPI(List<EliminarOrdenModel> Model)
        //{
        //    List<EliminarOrdenModel> Lista = new List<EliminarOrdenModel>();
        //    try
        //    {
        //        var Sugerido = (from x in Model select new { IDSugerido = x.IdSugerido }).Distinct();

        //        foreach (var item in Sugerido)
        //        {
        //            var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");
        //            var command = db.GetStoredProcCommand("[mta].[SP_GRFP_GET_CONF_SUCURSALES_API]");
        //            db.AddInParameter(command, "@IdSugerido", DbType.String, item.IDSugerido );

        //            command.CommandTimeout = 0;

        //            using (var reader = db.ExecuteReader(command))
        //            {
        //                while (reader.Read())
        //                {
        //                    EliminarOrdenModel o = new EliminarOrdenModel();

        //                    o.IdSugerido = reader["IDSugerido"] != DBNull.Value ? Convert.ToInt64(reader["IDSugerido"]) : 0;
        //                    o.Orden_Compra = reader["IDordenCompra"] != DBNull.Value ? Convert.ToInt32(reader["IDordenCompra"]) : 0;
        //                    o.Id_Sucursal = reader["Idsucursal"] != DBNull.Value ? Convert.ToInt32(reader["Idsucursal"]) : 0;
        //                    o.IdProveedor = reader["IDproveedor"] != DBNull.Value ? Convert.ToInt32(reader["IDproveedor"]) : 0;

        //                    Lista.Add(o);
        //                }
        //            }
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        return null;
        //        throw;
        //    }
        //    return Lista;
        //}
        //public static Response EnviarPedidosApi(List<EliminarOrdenModel> ApiPedidos)
        //{
        //    var Response = new Response();
        //    try
        //    {
        //        var Sugerido = (from x in ApiPedidos select new { IdSugerido = x.IdSugerido, IDproveedor = x.IdProveedor }).Distinct();

        //        foreach (var item in Sugerido)
        //        {
        //            #region Enviar Pedidos
        //            List<ApiCatObjects> Objetos = ObtenerObjetosApi(1, 1, item.IDproveedor);

        //            if (Objetos != null && Objetos.Count > 0)
        //            {
        //                foreach (var items in Sugerido)
        //                {
        //                    var respuestaCrearJson = LoadData(Objetos, items.IdSugerido.ToString(), items.IDproveedor);

        //                    if (respuestaCrearJson.Success)
        //                    {
        //                        var respuestaPeticion = SendRequestPedidosApi(Objetos, items.IdSugerido.ToString(), items.IDproveedor);
        //                    }
        //                    else
        //                    {
        //                        Response.Message = "Error : " + respuestaCrearJson.Message;
        //                        Response.Success = false;
        //                        return Response;
        //                    }
        //                }
        //            }
        //            else
        //            {
        //                Response.Message = "Error: Error Obteniendo objetos para envios por API ";
        //                Response.Success = false;
        //                return Response;
        //            }
        //            #endregion Enviar Pedidos
        //        }
        //        Response.Success = true;
        //        Response.Message = "Pedidos Enviados por API con Exito";
        //        return Response;

        //    }
        //    catch (Exception ex)
        //    {
        //        Response.Message = "Error: " + ex.Message;
        //        Response.Success = false;
        //    }

        //    return Response;
        //}

        //public static List<ApiCatObjects> ObtenerObjetosApi(int flag, int Accion, int idproveedor)
        //{
        //    List<ApiCatObjects> oList = new List<ApiCatObjects>();
        //    try
        //    {
        //        var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");
        //        var command = db.GetStoredProcCommand("[mta].[SP_GRFP_GET_CONF_API]");
        //        db.AddInParameter(command, "@flag", DbType.Int32, flag);
        //        db.AddInParameter(command, "@accion", DbType.Int32, Accion);
        //        db.AddInParameter(command, "@idproveedor", DbType.Int32, idproveedor);
        //        command.CommandTimeout = 0;

        //        using (var reader = db.ExecuteReader(command))
        //        {
        //            while (reader.Read())
        //            {
        //                ApiCatObjects o = new ApiCatObjects();

        //                o.id_action = reader["IdAction"] != DBNull.Value ? Convert.ToInt32(reader["IdAction"]) : 0;
        //                o.spLoad = reader["spLoad"] != DBNull.Value ? reader["spLoad"].ToString() : string.Empty;
        //                o.spGetRequest = reader["spGetRequest"] != DBNull.Value ? reader["spGetRequest"].ToString() : string.Empty;
        //                o.spSetResponse = reader["spSetResponse"] != DBNull.Value ? reader["spSetResponse"].ToString() : string.Empty;
        //                o.spSendResponse = reader["spSendResponse"] != DBNull.Value ? reader["spSendResponse"].ToString() : string.Empty;

        //                oList.Add(o);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return null;
        //        throw;
        //    }

        //    return oList;
        //}

        //public static Response LoadData(List<ApiCatObjects> oList, string Sugerido, int proveedor)
        //{
        //    var Response = new Response();
        //    try
        //    {
        //        var o = oList.FirstOrDefault();

        //        var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");
        //        var command = db.GetStoredProcCommand(o.spLoad.ToString());
        //        db.AddInParameter(command, "@Idsugerido", DbType.String, Sugerido);
        //        db.AddInParameter(command, "@IdProveedor", DbType.Int32, proveedor);

        //        command.CommandTimeout = 0;
        //        db.ExecuteNonQuery(command);

        //        Response.Success = true;
        //        Response.Message = "Json para Pedidos creados con exito";
        //    }
        //    catch (Exception ex)
        //    {
        //        Response.Success = false;
        //        Response.Message = "Error creando JSON de Pedidos: " + ex.Message;
        //    }
        //    return Response;
        //}
        //public static Response SendRequestPedidosApi(List<ApiCatObjects> oCatObjects, string Sugerido, int proveedor)
        //{
        //    var Response = new Response();
        //    try
        //    {
        //        var oCatObj = oCatObjects.FirstOrDefault();

        //        /*Recuperamos la lista request de la accion*/
        //        var oRequest = GetRequestData(oCatObj, Sugerido, proveedor);
        //        if (oRequest == null || oRequest.Count == 0)
        //            return new Response { Success = false, Message = "Error obteniendo peticiones de API." };

        //        /*obtenemos Credenciales del API */
        //        var oCredential = GetCredential(2, oCatObj.id_action, proveedor);
        //        if (oCredential == null || oCredential.Count == 0)
        //            return new Response { Success = false, Message = "Error obteniendo credenciales de API." };

        //        var credentiaL = oCredential.FirstOrDefault();

        //        foreach (var req in oRequest)
        //        {
        //            //ENVIAR JSON POR API A NADRO
        //            req.Response = EnviarPeticionPedido(req, credentiaL);

        //            var jsonresp = JsonConvert.DeserializeObject<JObject>(req.Response);

        //            var codigo = (string)jsonresp["MT_Pedidos_API_Resp"]["header"]["ApiTmsg"];

        //            if (codigo == "E")
        //            {
        //                req.CodigoRespuesta = "Error";
        //                req.DescripcionRespuesta = (string)jsonresp["MT_Pedidos_API_Resp"]["header"]["ApiMsg"];
        //                req.oFlag = "E";
        //                req.oEx = "Error en el Pedido";

        //                Response.Success = false;
        //                Response.Message = "Error en el Pedido";
        //            }
        //            else
        //            {
        //                req.CodigoRespuesta = "Ok";
        //                req.DescripcionRespuesta = "Pedido Procesado con exito";
        //                req.oFlag = "P";
        //                req.oEx = "";
        //            }

        //            //Obtener respuesta de NADRO y mandar a BD
        //            var setResponse = SetResponseData(req, oCatObj, proveedor);
        //        }
        //        Response.Success = true;
        //        Response.Message = "Peticiones enviadas Correctamente ";
        //    }
        //    catch (Exception ex)
        //    {
        //        Response.Success = false;
        //        Response.Message = "Error enviando Peticiones de API: " + ex.Message;
        //    }

        //    return Response;
        //}

        //public static List<oPetitionData> GetRequestData(ApiCatObjects oCatObj, string Sugerido, int proveedor)
        //{
        //    List<oPetitionData> RequestPedido = new List<oPetitionData>();
        //    try
        //    {
        //        var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");
        //        var command = db.GetStoredProcCommand(oCatObj.spGetRequest.ToString());
        //        db.AddInParameter(command, "@IdAction", DbType.Int32, oCatObj.id_action);
        //        db.AddInParameter(command, "@oFlag", DbType.Int32, 1);
        //        db.AddInParameter(command, "@IdSugerido", DbType.String, Sugerido);
        //        db.AddInParameter(command, "@idproveedor", DbType.Int32, proveedor);
        //        command.CommandTimeout = 0;

        //        using (var reader = db.ExecuteReader(command))
        //        {
        //            while (reader.Read())
        //            {
        //                oPetitionData o = new oPetitionData();

        //                o.IDAction = reader["IDAction"] != DBNull.Value ? Convert.ToInt32(reader["IDAction"]) : 0;
        //                o.IDSugerido = reader["IDSugerido"] != DBNull.Value ? reader["IDSugerido"].ToString() : string.Empty;
        //                o.IDsucursal = reader["IDsucursal"] != DBNull.Value ? Convert.ToInt32(reader["IDsucursal"]) : 0;
        //                o.IDOrdenCompra = reader["IDOrdenCompra"] != DBNull.Value ? Convert.ToInt32(reader["IDOrdenCompra"]) : 0;
        //                o.IDBloqueParcialidad = reader["IDGrupoParcialidad"] != DBNull.Value ? Convert.ToInt32(reader["IDGrupoParcialidad"]) : 0;
        //                o.Request = reader["Request"] != DBNull.Value ? reader["Request"].ToString() : string.Empty;

        //                RequestPedido.Add(o);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return null;
        //        throw;
        //    }

        //    return RequestPedido;
        //}
        //public static List<APICredenciales> GetCredential(int flag, int Accion, int idproveedor)
        //{
        //    List<APICredenciales> oList = new List<APICredenciales>();
        //    try
        //    {
        //        var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");
        //        var command = db.GetStoredProcCommand("[mta].[SP_GRFP_GET_CONF_API]");
        //        db.AddInParameter(command, "@flag", DbType.Int32, flag);
        //        db.AddInParameter(command, "@accion", DbType.Int32, Accion);
        //        db.AddInParameter(command, "@idproveedor", DbType.Int32, idproveedor);
        //        command.CommandTimeout = 0;

        //        using (var reader = db.ExecuteReader(command))
        //        {
        //            while (reader.Read())
        //            {
        //                APICredenciales o = new APICredenciales();

        //                o.IDAction = reader["IdAction"] != DBNull.Value ? Convert.ToInt32(reader["IdAction"]) : 0;
        //                o.IDProveedor = reader["IdProveedor"] != DBNull.Value ? Convert.ToInt32(reader["IdProveedor"]) : 0;
        //                o.oEndPoint = reader["oEndPoint"] != DBNull.Value ? reader["oEndPoint"].ToString() : string.Empty;
        //                o.Key = reader["Access_H_Key"] != DBNull.Value ? reader["Access_H_Key"].ToString() : string.Empty;
        //                o.KeyValue = reader["Access_H_Value"] != DBNull.Value ? reader["Access_H_Value"].ToString() : string.Empty;

        //                oList.Add(o);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return null;
        //        throw;
        //    }

        //    return oList;
        //}

        //public static string EnviarPeticionPedido(oPetitionData o, APICredenciales c)
        //{
        //    string respuesta = string.Empty;
        //    try
        //    {
        //        var options = new RestClientOptions(c.oEndPoint)
        //        {
        //            Timeout = TimeSpan.FromMinutes(1), // 5 minutos
        //            ThrowOnAnyError = false
        //        };

        //        string requestContent = o.Request;

        //        var client = new RestClient(options);
        //        RestRequest request = new RestRequest("", Method.Post);

        //        //Header de API
        //        request.AddHeader("Content-Type", "application/json;charset=utf-8");
        //        request.AddHeader(c.Key, c.KeyValue);

        //        //Agregamos el body
        //        request.AddStringBody(requestContent, DataFormat.Json);

        //        //Ejecutamos peticion 
        //        RestResponse response = client.Execute(request);

        //        if (response == null || !response.IsSuccessful || string.IsNullOrEmpty(response.Content))
        //        {
        //            return null;
        //        }

        //        respuesta = response.Content;
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //        return null;
        //    }
        //    return respuesta;
        //}

        //public static Response SetResponseData(oPetitionData oPetition, ApiCatObjects oCatObj, int proveedor)
        //{
        //    var Response = new Response();
        //    try
        //    {
        //        var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");
        //        var command = db.GetStoredProcCommand(oCatObj.spSetResponse.ToString());
        //        db.AddInParameter(command, "@IdSugerido", DbType.String, oPetition.IDSugerido);
        //        db.AddInParameter(command, "@IdProveedor", DbType.Int32, proveedor);
        //        db.AddInParameter(command, "@IdAccion", DbType.Int32, oCatObj.id_action);
        //        db.AddInParameter(command, "@IDsucursal", DbType.Int32, oPetition.IDsucursal);
        //        db.AddInParameter(command, "@IDOrdenCompra", DbType.Int32, oPetition.IDOrdenCompra);
        //        db.AddInParameter(command, "@IDBloque", DbType.Int32, oPetition.IDBloqueParcialidad);
        //        db.AddInParameter(command, "@Response", DbType.String, oPetition.Response.Replace("'", ""));
        //        db.AddInParameter(command, "@CodigoRespuesta", DbType.String, oPetition.CodigoRespuesta);
        //        db.AddInParameter(command, "@DescripcionRespuesta", DbType.String, oPetition.DescripcionRespuesta);
        //        db.AddInParameter(command, "@oFlag", DbType.String, oPetition.oFlag);
        //        db.AddInParameter(command, "@oEx", DbType.String, oPetition.oEx);

        //        command.CommandTimeout = 0;
        //        db.ExecuteNonQuery(command);

        //        Response.Success = true;
        //        Response.Message = "Insertamos respuesta de pedido con exito";
        //    }
        //    catch (Exception ex)
        //    {
        //        Response.Success = false;
        //        Response.Message = "Error insertando respuesta de Pedido: " + ex.Message;
        //    }
        //    return Response;
        //}
        #endregion ENVIO POR API (Obsoleto pero sirve de guia para otras API)

        #region ENVIO POR AZURE BLOB STORAGE

        public class IdSugeridoDto
        {
            public long IdSugerido { get; set; }
            public int IdProveedor { get; set; }
            public string Subcadena { get; set; }
            public int IDSubcadena { get; set; }
        }
        public class Correos
        {
            public string Mail { get; set; }
        }

        public static List<IdSugeridoDto> ListarCadenas(List<IdSugeridoDto> Lista)
        {
            ExHandler.Log.Write(string.Format("Start.ListarCadenas"));
            List<IdSugeridoDto> listaResultados = new List<IdSugeridoDto>();
            try
            {
                var newList = (from x in Lista select new { IdSugerido = x.IdSugerido }).Distinct();
                foreach (var item in newList)
                {
                    //SP que verifica que cadena o razón social va por azure
                    var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");
                    var command = db.GetStoredProcCommand("[mta].[SP_GRFP_GET_CONF_SUBCADENAS_PEDIDOS_AZURE]");
                    db.AddInParameter(command, "@IdSugerido", DbType.String, item.IdSugerido);

                    command.CommandTimeout = 0;

                    using (var reader = db.ExecuteReader(command))
                    {
                        while (reader.Read())
                        {
                            IdSugeridoDto o = new IdSugeridoDto();

                            o.IdSugerido = reader["IdSugerido"] != DBNull.Value ? Convert.ToInt64(reader["IdSugerido"]) : 0;
                            o.IdProveedor = reader["IDProveedor"] != DBNull.Value ? Convert.ToInt32(reader["IDProveedor"]) : 0;
                            o.Subcadena = (string)(reader["SUBCADENA"] != DBNull.Value ? reader["SUBCADENA"] : "");
                            o.IDSubcadena = reader["IdSubcadena"] != DBNull.Value ? Convert.ToInt32(reader["IdSubcadena"]) : 0;

                            listaResultados.Add(o);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                ExHandler.Log.Write($"Error.ListarCadenas {ex.Message}");
                return null;
            }
            return listaResultados;
        }

        public static Entities.Response.Response UploadPedidosAzure(DataTable Tablacompras)
        {
            ExHandler.Log.Write(string.Format("Start.UploadAzureFiles"));

            var response = new Entities.Response.Response();
            string marca = string.Empty;
            string dirlog = string.Empty;
            string rutalocal = string.Empty;
            string nombreArchivo = string.Empty;

            try
            {
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
                        response.Success = false;
                        response.Message = "Error en marca";
                    }

                    if (Directory.Exists(dirlog))
                    {
                        Directory.Delete(dirlog, true);
                    }

                    foreach (DataRow row in Tablacompras.Rows)
                    {
                        var ArchivoRutaBase = row[8]?.ToString();
                        var DirectorioRespaldo = row[1]?.ToString();
                        var Archivo = row[9]?.ToString();

                        var pathFileDestinoLocal = Path.Combine(DirectorioRespaldo, Archivo);

                        if (!Directory.Exists(DirectorioRespaldo)) Directory.CreateDirectory(DirectorioRespaldo);

                        try
                        {
                            // Si existe, lo reemplazamos
                            if (File.Exists(pathFileDestinoLocal))
                                File.Delete(pathFileDestinoLocal);

                            File.Move(ArchivoRutaBase, pathFileDestinoLocal);
                        }
                        catch (Exception ex)
                        {
                            response.Success = false;
                            ExHandler.Log.Write($"Error.UploadAzureFiles {ex.Message}");
                            response.Message = $"Error.UploadAzureFiles {ex.Message}";
                            return response;
                        }
                    }

                    foreach (DataRow row in Tablacompras.Rows)
                    {
                        //Envio a blob storage y ejecucion de Actualiza_Status(nombreArchivo);
                        rutalocal = row.ItemArray[1].ToString();
                        nombreArchivo = row.ItemArray[9].ToString();
                        string containerDestino = ConfigurationManager.AppSettings["ContainerName"];
                        string URL = ConfigurationManager.AppSettings["BlobServiceUrl"];

                        #region Por si URL y container se obtienen desde SQL

                        //var containerDestino = GetURL(2, oCatObj.id_action, proveedor);
                        //        if (containerDestino == null || containerDestino.Count == 0)
                        //            return new Response { Success = false, Message = "Error obteniendo Destino de Azure." };
                        //var containerDestino = Destino.FirstOrDefault();

                        //var URL = GetURL(2, oCatObj.id_action, proveedor);
                        //        if (URL == null || URL.Count == 0)
                        //            return new Response { Success = false, Message = "Error obteniendo URL de Azure." };
                        //var URL = URLAzure.FirstOrDefault();

                        #endregion Por si URL y container se obtienen desde SQL

                        //EnviarPedido(rutalocal, nombreArchivo, URL, containerDestino , dirlog).GetAwaiter().GetResult();
                        var respuesta = Task.Run(() => EnviarPedido(rutalocal, nombreArchivo, URL, containerDestino, dirlog)).GetAwaiter().GetResult();

                        if (respuesta != null)
                        {
                            ExHandler.Log.Write($"Success. Pedido enviado. Status: {respuesta}");
                        }
                        else
                        {
                            ExHandler.Log.Write($"Error. No se recibió respuesta de Azure.");
                        }
                    }

                    //Envio de correo de archivos que no pudieron ser depositados
                    if (Directory.Exists(dirlog))
                    {
                        var mensaje = Directory.GetFiles(dirlog);
                        DirectoryInfo di = new DirectoryInfo(dirlog);
                        var cadena = di.Name;
                        EnviarCorreo(mensaje, cadena).GetAwaiter().GetResult();
                    }


                    
                }
                response.Success = true;
                response.Message = "Pedido realizado con exito";
            }
            catch (Exception ex)
            {
                response.Success = false;
                ExHandler.Log.Write($"Error.UploadAzureFiles {ex.Message}");
                response.Message = $"Error.UploadAzureFiles {ex.Message}";

                return response;
            }

            response.Success = true != default;
            return response;
        }
        public static async Task<string> EnviarPedido(string RutaLocal, string NombreArchivo, string URL, string Destino, string DirLog)
        {
            try
            {
                var tenantId = ConfigurationManager.AppSettings["TenantId"];
                var clientId = ConfigurationManager.AppSettings["ClientId"];
                var clientSecret = ConfigurationManager.AppSettings["ClientSecret"];

                var ArchivoLocal = Path.Combine(RutaLocal, NombreArchivo);

                if (!File.Exists(ArchivoLocal))
                {
                    ExHandler.Log.Write($"Error.EnviarPedido: Archivo no encontrado {ArchivoLocal}");
                    return "Error: Archivo no existe";
                }

                string URLClean = URL.Replace("\"", "").Replace("'", "").Replace("\r", "").Replace("\n", "").Trim();

                // Authenticate with Azure AD
                var credential = new ClientSecretCredential(tenantId, clientId, clientSecret);
                var blobServiceClient = new BlobServiceClient(new Uri(URLClean), credential);
                var blobContainerClient = blobServiceClient.GetBlobContainerClient(Destino);

                await blobContainerClient.CreateIfNotExistsAsync().ConfigureAwait(false);

                var fileName = Path.GetFileName(ArchivoLocal);
                var blobClient = blobContainerClient.GetBlobClient(fileName);

                byte[] fileHash;
                using (var md5 = MD5.Create())
                using (var stream = File.OpenRead(ArchivoLocal))
                {
                    fileHash = md5.ComputeHash(stream);
                }

                var uploadOptions = new BlobUploadOptions
                {
                    HttpHeaders = new BlobHttpHeaders
                    {
                        // Azure validará este hash automáticamente al recibir el archivo
                        ContentHash = fileHash
                    },
                    // Opcional: Condiciones de acceso (ej. solo sobrescribir si existe)
                };
                int statusAzure = 0;

                using (var fileStream = File.OpenRead(ArchivoLocal))
                {
                    fileStream.Position = 0;

                    var response = await blobClient.UploadAsync(fileStream, uploadOptions).ConfigureAwait(false);
                    statusAzure = response.GetRawResponse().Status;

                }

                if (statusAzure >= 200 && statusAzure < 300)
                {
                    string directoryPath = string.Format("{0}\\{1}", RutaLocal, NombreArchivo);
                    string historyPath = string.Format("{0}\\historico\\{1}\\{2}\\{3}", RutaLocal, DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

                    if (!Directory.Exists(historyPath)) Directory.CreateDirectory(historyPath);

                    historyPath = string.Format("{0}\\{1}", historyPath, NombreArchivo);

                    File.Move(directoryPath, historyPath);

                    Actualiza_Status(NombreArchivo);

                    ExHandler.Log.Write($"Success.EnviarPedido terminado correctamente.");
                    return "Success";
                }
                else
                {
                    ExHandler.Log.Write(string.Format("OrdenCompraData.UpLoadAzure falló para el archivo: {0}", NombreArchivo));
                    if (!Directory.Exists(DirLog)) Directory.CreateDirectory(DirLog);
                    string directoryPath = string.Format("{0}\\{1}", RutaLocal, NombreArchivo);
                    DirLog = string.Format("{0}\\{1}", DirLog, NombreArchivo);

                    File.Move(directoryPath, DirLog);
                    return null;
                }
            }
            catch (Exception ex)
            {
                ExHandler.Log.Write(string.Format("OrdenCompraData.UpLoadAzure falló para el archivo: {0}", NombreArchivo));
                if (!Directory.Exists(DirLog)) Directory.CreateDirectory(DirLog);
                string directoryPath = string.Format("{0}\\{1}", RutaLocal, NombreArchivo);
                DirLog = string.Format("{0}\\{1}", DirLog, NombreArchivo);
                File.Move(directoryPath, DirLog);
                return null;
            }
        }
        public static string Actualiza_Status(string archivo)
        {
            var response = new Entities.Response.Response();

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
                ExHandler.Log.Write(string.Format("Error.Actualiza_Status {0}", ex.Message));
            }
            response.Message = "Actualizado";
            response.Success = true != default;

            return response.ToString();
        }
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
                        sujeto = "PRUEBA Errores en Envio a Azure Pedido San Francisco";
                        cuerpo = "<h1> Los siguientes archivos adjuntos de la Cadena San Francisco no fueron enviados mediante Azure debido a un error </h1>";
                    }
                    else if (cadena.Contains("FT"))
                    {
                        sujeto = "PRUEBA Errores en Envio a Azure Pedido en FarmaTodo";
                        cuerpo = "<h1> Los siguientes archivos adjuntos de la Cadena FarmaTodo no fueron enviados mediante Azure debido a un error </h1>";
                    }
                    else if (cadena.Contains("UN"))
                    {
                        sujeto = "PRUEBA Errores en Envio a Azure Pedido en Union";
                        cuerpo = "<h1>Los siguientes archivos adjuntos de la Cadena Union no fueron enviados mediante Azure debido a un error </h1>";
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

        #endregion ENVIO POR AZURE BLOB STORAGE
    }
}
