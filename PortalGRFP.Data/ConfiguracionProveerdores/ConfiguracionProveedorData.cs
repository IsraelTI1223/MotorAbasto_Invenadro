using Microsoft.Practices.EnterpriseLibrary.Data;
using PortalGRFP.Data.Extensions;
using PortalGRFP.Entities.Common;
using PortalGRFP.Entities.Response;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalGRFP.Data.ConfiguracionProveerdores
{
    public class ConfiguracionProveedorData
    {
        public ResponseList<Autocomplete> Autocompletar(int accion)//1=formas 2=tipos 3=clasificacion 4=periodicidad
        {
            var response = new ResponseList<Autocomplete>();

            var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");
            var command = db.GetStoredProcCommand("mta.sp_AutocompleteProveedores");//solo para inicializar

            db.AddInParameter(command, "@accion", DbType.Int32, accion);

            command.CommandTimeout = 0;

            var read = db.ExecuteReader(command);
            response.Result = read.Reader(x => x.ToAutocomplete());
            response.Success = response.Result.Any();

            return response;
        }

        public Response AgregarAgencias(DataTable agencia)
        {
            var response = new Response();

            var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");
            var command = db.GetStoredProcCommand("mta.sp_config_agencias");

            command.Parameters.Add(new SqlParameter("@TIPO_PROV_AGENCIA", SqlDbType.Structured) { Value = agencia });

            command.CommandTimeout = 0;

            var exito = db.ExecuteNonQuery(command);
            response.Success = exito != default;

            return response;
        }

        public ResponseList<AgenciaProveedorExt> AgenciasAgregadas()
        {
            var response = new ResponseList<AgenciaProveedorExt>();

            var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");
            var command = db.GetStoredProcCommand("mta.SP_AGENCIAS_AGREGADAS");//solo para inicializar

            //db.AddInParameter(command, "@accion", DbType.Int32, usuario);

            command.CommandTimeout = 0;

            var read = db.ExecuteReader(command);
            response.Result = read.Reader(x => x.ToAgenciaProveedor());
            response.Success = response.Result.Any();
            if (response.Result.Count == 0)
            {
                response.Message = "No hay registros por mostrar";
            }

            return response;
        }


        public List<ComboGenerico> ComboFormatoProveedor()
        {
            var response = new List<ComboGenerico>();

            var db = DatabaseFactory.CreateDatabase("DBPORTALRCB");
            var command = db.GetStoredProcCommand("mta.SP_GRFP_CONF_GET_FORMATO_PROV");
            command.CommandTimeout = 0;
            var read = db.ExecuteReader(command);
            response = read.Reader(x => x.ToComboFormatoProveedor());

            return response;
        }

        public List<ComboGenerico> ComboFrecuenciaProveedor()
        {
            var response = new List<ComboGenerico>();

            var db = DatabaseFactory.CreateDatabase("DBPORTALRCB");
            var command = db.GetStoredProcCommand("mta.SP_GRFP_CONF_CAT_GET_FRECUENCIA_PROV");
            command.CommandTimeout = 0;
            var read = db.ExecuteReader(command);
            response = read.Reader(x => x.ToComboFrecuenciaProveedor());

            return response;
        }

        public ResponseList<ProveedorAgencia> GetProveedorAgencia(int accion)//1=formas 2=tipos 3=clasificacion 4=periodicidad
        {
            var response = new ResponseList<ProveedorAgencia>();

            var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");
            var command = db.GetStoredProcCommand("mta.sp_AutocompleteProveedores");//solo para inicializar

            db.AddInParameter(command, "@accion", DbType.Int32, accion);

            command.CommandTimeout = 0;

            var read = db.ExecuteReader(command);
            response.Result = read.Reader(x => x.ToProveedorAgencia());
            response.Success = response.Result.Any();

            return response;
        }

        public List<ComboGenerico> ComboAgencia(int PROVEEDOR_ID)
        {
            var response = new List<ComboGenerico>();

            var db = DatabaseFactory.CreateDatabase("DBPORTALRCB");
            var command = db.GetStoredProcCommand("mta.SP_GRFP_CONF_GET_PROVEEDOR_AGENCIA_FTP");
            db.AddInParameter(command, "@PROVEEDOR_ID", DbType.Int32, PROVEEDOR_ID);

            var read = db.ExecuteReader(command);
            response = read.Reader(x => x.ToComboGenerico());

            return response;
        }

        public List<ComboGenerico> ComboRazonSocial(int PROVEEDOR_ID)
        {
            var response = new List<ComboGenerico>();

            var db = DatabaseFactory.CreateDatabase("DBPORTALRCB");
            var command = db.GetStoredProcCommand("mta.SP_GRFP_CONF_GET_PROVEEDOR_RAZONSOCIAL_FTP");
            db.AddInParameter(command, "@PROVEEDOR_ID", DbType.Int32, PROVEEDOR_ID);

            var read = db.ExecuteReader(command);
            response = read.Reader(x => x.ToComboGenerico());

            return response;
        }


        public List<ComboGenericoProv> ComboProv()
        {
            var response = new List<ComboGenericoProv>();

            var db = DatabaseFactory.CreateDatabase("DBPORTALRCB");
            var command = db.GetStoredProcCommand("mta.SP_GRFP_CONF_GET_PROVEEDOR_FTP");
            command.CommandTimeout = 0;
            var read = db.ExecuteReader(command);
            response = read.Reader(x => x.ToComboProv());

            return response;
        }
        public int InsertProvFTP(InsertFTP model, string user)
        {
            var id = 0;

            var response = new Response();

            var db = DatabaseFactory.CreateDatabase("DBPORTALRCB");
            var command = db.GetStoredProcCommand("mta.SP_GRFP_CONF_INSERT_PROVEEDOR_FTP");

            command.Parameters.Add(new SqlParameter("@Id_Proveedor", model.Id_Proveedor));
            command.Parameters.Add(new SqlParameter("@Id_Agencia", model.Id_Agencia));
            command.Parameters.Add(new SqlParameter("@Tipo_Formato", model.Tipo_Formato));
            command.Parameters.Add(new SqlParameter("@URL", model.URL));
            command.Parameters.Add(new SqlParameter("@Directorio", model.Directorio));
            command.Parameters.Add(new SqlParameter("@Directorio_Respaldo", model.Directorio_Respaldo));
            command.Parameters.Add(new SqlParameter("@Copia_Respaldo", model.Copia_Respaldo));
            command.Parameters.Add(new SqlParameter("@Elimina_Origen", model.Elimina_Origen));
            command.Parameters.Add(new SqlParameter("@Frecuencia", model.Frecuencia));
            command.Parameters.Add(new SqlParameter("@Tiempo_Frecuencia", model.Tiempo_Frecuencia));
            command.Parameters.Add(new SqlParameter("@Fecha_Proxima_Ejecucion", model.Fecha_Proxima_Ejecucion));
            command.Parameters.Add(new SqlParameter("@Hora_Ini", model.Hora_Ini));
            command.Parameters.Add(new SqlParameter("@Hora_Fin", model.Hora_Fin));
            command.Parameters.Add(new SqlParameter("@Usuario_FTP", model.Usuario_FTP));
            command.Parameters.Add(new SqlParameter("@Clave_FTP", model.Clave_FTP));
            command.Parameters.Add(new SqlParameter("@Nomenclatura_Archivo", model.Nomenclatura_Archivo));
            command.Parameters.Add(new SqlParameter("@o_usr", user));

            // Nuevos campos
            command.Parameters.Add(new SqlParameter("@FTP_Activo", model.FTP_Activo));
            command.Parameters.Add(new SqlParameter("@Id_RazonSocial", model.Id_RazonSocial));
            command.Parameters.Add(new SqlParameter("@Numero_Frecuencia", model.Numero_Frecuencia));
            command.Parameters.Add(new SqlParameter("@Lapso_Frecuencia", model.Lapso_Frecuencia));
            command.Parameters.Add(new SqlParameter("@Extension_Formato", model.Extension_Formato));


            var dr = db.ExecuteReader(command);

            response.Success = dr != default;

            return id;



        }


        public ResponseList<ProveedoresAgenciaFTP> ObtenerConsultaFTP()//int idCarga
        {
            var response = new ResponseList<ProveedoresAgenciaFTP>();

            var db = DatabaseFactory.CreateDatabase("DBPORTALRCB");

            var command = db.GetStoredProcCommand("mta.SP_GRFP_CONF_SELECT_PROVEEDOR_AGENCIA_FTP");
            command.CommandTimeout = 0;


            var read = db.ExecuteReader(command);
            response.Result = read.Reader(x => x.ToConsultaFTP());
            response.Success = response.Result.Any();

            return response;
        }

        public ProveedoresAgenciaFTPCompleto SelectDataFTP(string Id)
        {
            var datos = new ProveedoresAgenciaFTPCompleto();

            var response = new Response();

            int id_Folio_int = int.Parse(Id);

            var db = DatabaseFactory.CreateDatabase("DBPORTALRCB");
            var command = db.GetStoredProcCommand("mta.SP_GRFP_CONF_SELECT_PROVEEDOR_FTP_UPDATE");
            command.Parameters.Add(new SqlParameter("@Id", id_Folio_int));


            var dr = db.ExecuteReader(command);

            response.Success = dr != default;

            while (dr.Read())
            {


                datos = new ProveedoresAgenciaFTPCompleto();

                datos.Id = dr.Get<int>("Id");
                datos.Proveedor = dr.Get<string>("Proveedor");
                datos.Agencia = dr.Get<string>("Agencia");
                datos.Tipo_Formato = dr.Get<string>("Tipo_Formato");
                datos.URL = dr.Get<string>("URL");
                datos.Directorio = dr.Get<string>("Directorio");

                datos.Directorio_Respaldo = dr.Get<string>("Directorio_Respaldo");
                datos.Copia_Respaldo = dr.Get<string>("Copia_Respaldo");
                datos.Elimina_Origen = dr.Get<string>("Elimina_Origen");
                datos.Frecuencia = dr.Get<string>("Frecuencia");
                datos.Tiempo_Frecuencia = dr.Get<string>("Tiempo_Frecuencia");

                datos.Fecha_Proxima_Ejecucion = dr.Get<string>("Fecha_Proxima_Ejecucion");
                datos.Hora_Ini = dr.Get<string>("Hora_Ini");
                datos.Hora_Fin = dr.Get<string>("Hora_Fin");
                datos.Usuario_FTP = dr.Get<string>("Usuario_FTP");
                datos.Clave_FTP = dr.Get<string>("Clave_FTP");
                datos.Nomenclatura_Archivo = dr.Get<string>("Nomenclatura_Archivo");



            }
            return datos;



        }



        public int UpdateFTP(ProveedoresAgenciaFTPCompleto model, string user)
        {
            var id = 0;


            var response = new Response();

            var db = DatabaseFactory.CreateDatabase("DBPORTALRCB");
            var command = db.GetStoredProcCommand("mta.SP_GRFP_CONF_UPDATE_PROVEEDOR_AGENCIA_FTP");
            //command.Parameters.Add(new SqlParameter("@Id_Folio", model.id_Folio));
            db.AddInParameter(command, "@Id", DbType.Int32, model.Id);
            command.Parameters.Add(new SqlParameter("@URL", model.URL));
            command.Parameters.Add(new SqlParameter("@Directorio", model.Directorio));
            command.Parameters.Add(new SqlParameter("@Directorio_Respaldo", model.Directorio_Respaldo));
            command.Parameters.Add(new SqlParameter("@Copia_Respaldo", model.Copia_Respaldo));
            command.Parameters.Add(new SqlParameter("@Elimina_Origen", model.Elimina_Origen));
            command.Parameters.Add(new SqlParameter("@Frecuencia", model.Frecuencia));
            command.Parameters.Add(new SqlParameter("@Tiempo_Frecuencia", model.Tiempo_Frecuencia));
            command.Parameters.Add(new SqlParameter("@Fecha_Proxima_Ejecucion", model.Fecha_Proxima_Ejecucion));
            command.Parameters.Add(new SqlParameter("@Hora_Ini", model.Hora_Ini));
            command.Parameters.Add(new SqlParameter("@Hora_Fin", model.Hora_Fin));
            command.Parameters.Add(new SqlParameter("@Usuario_FTP", model.Usuario_FTP));
            command.Parameters.Add(new SqlParameter("@Clave_FTP", model.Clave_FTP));
            command.Parameters.Add(new SqlParameter("@Nomenclatura_Archivo", model.Nomenclatura_Archivo));
            command.Parameters.Add(new SqlParameter("@o_usr", user));




            var dr = db.ExecuteReader(command);

            response.Success = dr != default;

            //while (dr.Read())
            //{
            //    id = dr.Get<int>("IdUsuario");
            //}

            return id;



        }


        public int DeleteFTP(ProveedoresAgenciaFTPCompleto model, string user)
        {
            var id = 0;


            var response = new Response();

            var db = DatabaseFactory.CreateDatabase("DBPORTALRCB");
            var command = db.GetStoredProcCommand("mta.SP_GRFP_CONF_DELETE_PROVEEDOR_AGENCIA_FTP");
            //command.Parameters.Add(new SqlParameter("@Id_Folio", model.id_Folio));
            db.AddInParameter(command, "@Id", DbType.Int32, model.Id);

            command.Parameters.Add(new SqlParameter("@o_usr", user));




            var dr = db.ExecuteReader(command);

            response.Success = dr != default;

            //while (dr.Read())
            //{
            //    id = dr.Get<int>("IdUsuario");
            //}

            return id;



        }

        public Response CargaExclusiones(DataTable DtExclusiones)//int idCarga
        {
            var response = new Response();

            var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");

            var command = db.GetStoredProcCommand("mta.sp_carga_proveedoresExclusiones");
            command.CommandTimeout = 0;
            command.Parameters.Add(new SqlParameter("@Tipo_Exclusiones", SqlDbType.Structured) { Value = DtExclusiones });

            var exito = db.ExecuteNonQuery(command);
            response.Success = exito != default;

            return response;
        }

        public ResponseList<LogExclusiones> GetLogExclusiones(int idProveedor)//int idCarga
        {
            var response = new ResponseList<LogExclusiones>();

            var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");
            string sp = "mta.sp_getLogExclusiones";
            var command = db.GetStoredProcCommand(sp);
            command.CommandTimeout = 0;
            command.Parameters.Add(new SqlParameter("@idProveedor", SqlDbType.Int) { Value = idProveedor });

            var read = db.ExecuteReader(command);

            response.Result = read.Reader(x => x.ToLogExclusiones());
            response.Success = response.Result.Any();

            return response;
        }

        public ResponseList<LogExclusiones> GetExclusionesCarga(int idProveedor)//int idCarga
        {
            var response = new ResponseList<LogExclusiones>();

            var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");
            string sp = "mta.[sp_GetExclusiones_Carga]";
            var command = db.GetStoredProcCommand(sp);
            command.CommandTimeout = 0;
            command.Parameters.Add(new SqlParameter("@idProveedor", SqlDbType.Int) { Value = idProveedor });

            var read = db.ExecuteReader(command);

            response.Result = read.Reader(x => x.ToLogExclusiones());
            response.Success = response.Result.Any();

            return response;
        }

    }
}
