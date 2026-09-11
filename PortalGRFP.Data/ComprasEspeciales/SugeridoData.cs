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
using static PortalGRFP.Entities.Common.ListSucursalesGeneralesSugeridoValidarModel;

namespace PortalGRFP.Data.ComprasEspeciales
{
    public class SugeridoData
    {
        public Int64 NuevoFolioSugerido { get; set; }
        public Int64 SttsSurtido { get; set; }
        public ResponseList<ComboGenerico> Filtros(int accion)//1=Grupos 2=Sucursale 
        {
            var response = new ResponseList<ComboGenerico>();

            var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");
            var command = db.GetStoredProcCommand("[mta].sp_grfp_filtros_sugeridos");//solo para inicializar

            db.AddInParameter(command, "@accion", DbType.Int32, accion);

            command.CommandTimeout = 0;

            var read = db.ExecuteReader(command);
            response.Result = read.Reader(x => x.ToComboGenerico());
            response.Success = response.Result.Any();

            return response;
        }

        public ResponseList<Combo4> FiltrosInvenadro(int accion, int user)//1=Grupos 2=Sucursale 
        {
            var response = new ResponseList<Combo4>();

            var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");
            var command = db.GetStoredProcCommand("[mta].sp_grfp_filtros_sugeridos_user");//solo para inicializar

            db.AddInParameter(command, "@accion", DbType.Int32, accion);
            db.AddInParameter(command, "@user", DbType.Int32, user);

            command.CommandTimeout = 0;

            var read = db.ExecuteReader(command);
            response.Result = read.Reader(x => x.ToComboInv());
            response.Success = response.Result.Any();

            return response;
        }

        public ResponseList<Combo4> FiltrosInvenadro2(int accion, int user)//1=Grupos 2=Sucursale 
        {
            var response = new ResponseList<Combo4>();

            var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");
            var command = db.GetStoredProcCommand("[mta].sp_grfp_filtros_sugeridos_user");//solo para inicializar

            db.AddInParameter(command, "@accion", DbType.Int32, accion);
            db.AddInParameter(command, "@user", DbType.Int32, user);

            command.CommandTimeout = 0;

            var read = db.ExecuteReader(command);
            response.Result = read.Reader(x => x.ToComboInv3());
            response.Success = response.Result.Any();

            return response;
        }

        public ResponseList<ConsultaInvenadro> GetProductoInvenadroDatBLL(string Sucursal, string SKU, string Concepto)//1=Grupos 2=Sucursale 
        {
            var response = new ResponseList<ConsultaInvenadro>();

            var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");
            var command = db.GetStoredProcCommand("[mta].GRFP_LOAD_CONSULTA_INVENADRO");//solo para inicializar

            db.AddInParameter(command, "@Sucursales", DbType.String, Sucursal);
            db.AddInParameter(command, "@SKU", DbType.String, SKU);
            db.AddInParameter(command, "@Conceptos", DbType.String, Concepto);

            command.CommandTimeout = 0;

            var read = db.ExecuteReader(command);
            response.Result = read.Reader(x => x.ToComboInvConsulta());
            response.Success = response.Result.Any();

            return response;
        }
        public ResponseList<Combo3> Filtros2(int accion)//1=Grupos 2=Sucursale 
        {
            var response = new ResponseList<Combo3>();

            var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");
            var command = db.GetStoredProcCommand("[mta].sp_grfp_filtros_sugeridos");//solo para inicializar

            db.AddInParameter(command, "@accion", DbType.Int32, accion);

            command.CommandTimeout = 0;

            var read = db.ExecuteReader(command);
            response.Result = read.Reader(x => x.ToCombo3());
            response.Success = response.Result.Any();

            return response;
        }

        public ResponseList<object> CargarSugerido(DataTable DtSugerido)//int idCarga
        {
            var response = new ResponseList<object>();

            var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");

            //var command = db.GetStoredProcCommand("[mta].[sp_GRFP_Load_Ped_Comp_Especial]");
            var command = db.GetStoredProcCommand("[mta].[sp_GRFP_Load_Ped_Comp_Especial_TEMP]");
            command.CommandTimeout = 0;
            command.Parameters.Add(new SqlParameter("@Tipo_CompEspecial_Ped", SqlDbType.Structured) { Value = DtSugerido });

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

        public ResponseList<PedidosCargados> ConsultaPedidosCargados(Int64 folio, int usuario, string fecha)//int idCarga
        {
            var response = new ResponseList<PedidosCargados>();

            var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");

            var command = db.GetStoredProcCommand("mta.GRFP_Get_Comp_Espcl_Gnral");
            command.CommandTimeout = 0;


            db.AddInParameter(command, "@folio", DbType.Int64, folio);
            db.AddInParameter(command, "@usr", DbType.Int32, usuario);
            db.AddInParameter(command, "@fecha", DbType.String, fecha);


            var read = db.ExecuteReader(command);
            response.Result = read.Reader(x => x.ToPedidosCargados());
            //if (response.Result.Count == 0)
            //{
            //    response.Message = "No hay Registros";
            //}
            //response.Success = response.Result.Any();

            return response;
        }

        public ResponseList<Rechazados> ConsultaPedidosRechazado(Int64 folio, int usuario, string fecha)//int idCarga
        {
            var response = new ResponseList<Rechazados>();

            var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");

            var command = db.GetStoredProcCommand("mta.GRFP_Get_Comp_Espcl_Rchzo");
            command.CommandTimeout = 0;


            db.AddInParameter(command, "@folio", DbType.Int64, folio);
            db.AddInParameter(command, "@usr", DbType.Int32, usuario);

            var read = db.ExecuteReader(command);
            response.Result = read.Reader(x => x.ToRechazados());
            //if (response.Result.Count == 0)
            //{
            //    response.Message = "No hay Registros";
            //}
            //response.Success = response.Result.Any();

            return response;
        }

        public ResponseList<PedidosCargados> BorrarPedido(Int64 folio, int usuario, string fecha)//int idCarga
        {
            var response = new ResponseList<PedidosCargados>();

            var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");

            var command = db.GetStoredProcCommand("mta.GRFP_Del_Comp_Espcl");
            command.CommandTimeout = 0;


            db.AddInParameter(command, "@folio", DbType.Int64, folio);
            db.AddInParameter(command, "@usr", DbType.Int32, usuario);
            db.AddInParameter(command, "@fecha", DbType.String, fecha);


            var exito = db.ExecuteReader(command);
            response.Success = exito != default;
            response.Result = exito.Reader(x => x.ToPedidosCargados());

            return response;
        }

        public Response CrearSugerido(DataTable sugerido)//int idCarga
        {
            var response = new Response();

            var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");

            var command = db.GetStoredProcCommand("MTA.[SP_INSERT_SUGERIDO]");
            command.CommandTimeout = 0;
            command.Parameters.Add(new SqlParameter("@Tipo_Sugerido", SqlDbType.Structured) { Value = sugerido });
            SqlParameter folioReturn = new SqlParameter("@NuevoFolio", SqlDbType.BigInt);
            folioReturn.Direction = ParameterDirection.Output;
            SqlParameter NoSurte = new SqlParameter("@Nosurte", SqlDbType.Int);
            NoSurte.Direction = ParameterDirection.Output;
            command.Parameters.Add(folioReturn);
            command.Parameters.Add(NoSurte);

            var exito = db.ExecuteNonQuery(command);
            this.NuevoFolioSugerido = Int64.Parse(command.Parameters["@NuevoFolio"].Value.ToString());
            this.SttsSurtido = Int64.Parse(command.Parameters["@Nosurte"].Value.ToString());
            response.Success = exito != default;
            response.Message = SttsSurtido.ToString();

            return response;
        }

        public Response ValidarNegadosData(ListSucursales model, long folioSugerido)//1=Grupos 2=Sucursale 
        {
            var response = new Response();

            try
            {

                var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");

                //var command = db.GetStoredProcCommand("mta.SP_GRFP_VAL_NEGADOS");//solo para inicializar

                foreach (var item in model.Sucursales)
                {


                    var command = db.GetStoredProcCommand("mta.SP_GRFP_VAL_NEGADOS");

                    db.AddInParameter(command, "@IdSucursal", DbType.Int32, item.ID);
                    db.AddInParameter(command, "@idsugerido", DbType.Int64, folioSugerido);



                    command.CommandTimeout = 0;

                    db.ExecuteNonQuery(command);
                }

                response.Success = true;
                response.Message = "Termino la validación de negados";

                return response;
            }
            catch (Exception)
            {
                response.Success = false;
                response.Message = "Error al validar los Negados";

                throw;
            }

        }

        public ResponseList<ComboGenerico> GetIncidenciasListData(int accion, string tiendas)//1=Grupos 2=Sucursale 
        {
            var response = new ResponseList<ComboGenerico>();

            var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");
            var command = db.GetStoredProcCommand("mta.SP_GRFP_CONF_GET_LOG_ERROR_NEGADOS");//solo para inicializar

            db.AddInParameter(command, "@flag", DbType.Int32, accion);
            db.AddInParameter(command, "@tiendas", DbType.String, tiendas);


            command.CommandTimeout = 0;

            var read = db.ExecuteReader(command);
            response.Result = read.Reader(x => x.ToComboGenerico());
            response.Success = response.Result.Any();

            return response;
        }


        public ResponseList<SugeridoLogErrorModel> GetListLogErrorData(string incidencia, int accion, string tiendas)//1=Grupos 2=Sucursale 
        {
            var response = new ResponseList<SugeridoLogErrorModel>();

            var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");
            var command = db.GetStoredProcCommand("mta.SP_GRFP_CONF_GET_LOG_ERROR_NEGADOS");//solo para inicializar

            db.AddInParameter(command, "@flag", DbType.Int32, accion);
            db.AddInParameter(command, "@incidencia", DbType.String, incidencia);
            db.AddInParameter(command, "@tiendas", DbType.String, tiendas);


            command.CommandTimeout = 0;

            var read = db.ExecuteReader(command);
            response.Result = read.Reader(x => x.ToListLogError());
            response.Success = response.Result.Any();

            return response;
        }

        public ResponseList<SugeridoLogErrorModel> GetDTLogErrorData(int accion, string tiendas, int usuario)//1=Grupos 2=Sucursale 
        {
            var response = new ResponseList<SugeridoLogErrorModel>();

            var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");
            var command = db.GetStoredProcCommand("mta.SP_GRFP_CONF_GET_LOG_ERROR_NEGADOS");//solo para inicializar

            db.AddInParameter(command, "@flag", DbType.Int32, accion);
            db.AddInParameter(command, "@incidencia", DbType.String, "");
            db.AddInParameter(command, "@tiendas", DbType.String, tiendas);
            db.AddInParameter(command, "@idusuario", DbType.Int32, usuario);


            command.CommandTimeout = 0;

            var read = db.ExecuteReader(command);
            response.Result = read.Reader(x => x.ToListLogError());
            response.Success = response.Result.Any();

            return response;
        }

        public ResponseList<SugeridoCuenta> GetDTCuenta(Int64 NuevoFolio)//1=Grupos 2=Sucursale 
        {
            var response = new ResponseList<SugeridoCuenta>();

            var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");
            var command = db.GetStoredProcCommand("mta.sp_GRFP_GET_SUGERIDO_NO_SURTE");//solo para inicializar
            db.AddInParameter(command, "@NuevoFolio", DbType.Int64, NuevoFolio);
            //db.AddInParameter(command, "@incidencia", DbType.String, "");
            //db.AddInParameter(command, "@tiendas", DbType.String, tiendas);
            //db.AddInParameter(command, "@idusuario", DbType.Int32, usuario);


            command.CommandTimeout = 0;

            var read = db.ExecuteReader(command);
            response.Result = read.Reader(x => x.ToListDTCuenta());
            response.Success = response.Result.Any();

            return response;
        }

        public ResponseList<ReporteNegados> GetDTReporteNegado(string Sucursales, string fechaini, string fechafin)//1=Grupos 2=Sucursale 
        {
            var response = new ResponseList<ReporteNegados>();

            var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");
            var command = db.GetStoredProcCommand("mta.sp_GRFP_GET_RPT_NEGADO");//solo para inicializar
            db.AddInParameter(command, "@Sucursales", DbType.String, Sucursales);
            db.AddInParameter(command, "@fechaini", DbType.String, fechaini);
            db.AddInParameter(command, "@fechafin", DbType.String, fechafin);

            command.CommandTimeout = 0;

            var read = db.ExecuteReader(command);
            response.Result = read.Reader(x => x.ToListDTRptNegados());
            response.Success = response.Result.Any();

            return response;
        }


        public Response CalcularSugerido(Int64 folio)//int idCarga
        {
            var response = new Response();

            var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");

            //var command = db.GetStoredProcCommand("[mta].[sp_GRFP_Load_Sugerido]");
            //command.CommandTimeout = 0;
            //command.Parameters.Add(new SqlParameter("@idSugerido", SqlDbType.BigInt) { Value = folio });//

            var command = db.GetStoredProcCommand("[mta].[SP_GRFP_BRIDGE_CADENA]");
            command.CommandTimeout = 0;
            command.Parameters.Add(new SqlParameter("@IdSugerido", SqlDbType.BigInt) { Value = folio });
            command.Parameters.Add(new SqlParameter("@flag", SqlDbType.VarChar) { Value = "GS" });



            var exito = db.ExecuteNonQuery(command);
            response.Success = exito != default;

            return response;
        }

        public Response AsignacionProveedor(Int64 folio)//int idCarga
        {
            var response = new Response();

            var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");

            //var command = db.GetStoredProcCommand("[mta].[sp_GRFP_Load_AsignProveedor]");
            //command.CommandTimeout = 0;
            //command.Parameters.Add(new SqlParameter("@idSugerido", SqlDbType.BigInt) { Value = folio });

            var command = db.GetStoredProcCommand("[mta].[SP_GRFP_BRIDGE_CADENA]");
            command.CommandTimeout = 0;
            command.Parameters.Add(new SqlParameter("@IdSugerido", SqlDbType.BigInt) { Value = folio });
            command.Parameters.Add(new SqlParameter("@flag", SqlDbType.VarChar) { Value = "AP" });

            var exito = db.ExecuteNonQuery(command);
            response.Success = exito != default;

            return response;
        }

        public Response EnviarSugeridoData(int user, string fecha)//int idCarga
        {
            var response = new Response();

            var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");

            var command = db.GetStoredProcCommand("[mta].[SP_GRFP_ENVIAR_SUGERIDO]");
            command.CommandTimeout = 0;
            command.Parameters.Add(new SqlParameter("@usr", user));
            command.Parameters.Add(new SqlParameter("@fecha", fecha));


            var exito = db.ExecuteNonQuery(command);
            response.Success = exito != default;

            response.Message = "ok";

            return response;
        }

        public ResponseList<ComboGenerico> GetListaSucursales(Int64 idSugerido)
        {
            var response = new ResponseList<ComboGenerico>();

            var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");
            var command = db.GetStoredProcCommand("[mta].[SP_GRFP_GET_LISTSUCURSALES_NEGADOS]");//solo para inicializar
            db.AddInParameter(command, "@IDSUGERIDO", DbType.Int64, idSugerido);
            command.CommandTimeout = 0;

            var read = db.ExecuteReader(command);
            response.Result = read.Reader(x => x.ToComboGenerico());
            response.Success = response.Result.Any();
            if (response.Result.Count == 0)
            {
                response.Message = "No hay registros por mostrar";
            }

            return response;
        }

        public ResponseList<ListadoSugerido> GetListadoSugerido(string idsugerido, string fcreacion, string sucs, string provs, string tipocalculo, string estatus,
                                                               string grupoProducto, string tipoPedido, int consultar, int usuario)//int idCarga
        {
            var response = new ResponseList<ListadoSugerido>();

            var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");

            var command = db.GetStoredProcCommand("mta.GRFP_Get_Sugerido_Calculado");
            command.CommandTimeout = 0;


            db.AddInParameter(command, "@FechaCreacion", DbType.String, fcreacion);
            db.AddInParameter(command, "@sucursal", DbType.String, sucs);
            db.AddInParameter(command, "@proveedores", DbType.String, provs);
            db.AddInParameter(command, "@TipoCalculo ", DbType.String, tipocalculo);
            db.AddInParameter(command, "@EstatusSugerido", DbType.String, estatus);
            db.AddInParameter(command, "@GrupoProducto", DbType.String, grupoProducto);
            db.AddInParameter(command, "@tipoPedido", DbType.String, tipoPedido);
            db.AddInParameter(command, "@Consultar ", DbType.Int32, consultar);
            db.AddInParameter(command, "@usuario", DbType.Int32, usuario);
            db.AddInParameter(command, "@idsugerido", DbType.Int64, Int64.Parse(idsugerido));

            var read = db.ExecuteReader(command);
            response.Result = read.Reader(x => x.ToListadoSugerido());

            return response;
        }

        public ResponseList<ResumenEnt> GetResumen(Int64 idSugerido, int accion)//int idCarga
        {
            var response = new ResponseList<ResumenEnt>();

            var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");
            var command = db.GetStoredProcCommand("MTA.SP_RESUMEN_PROVEEDOR");

            command.CommandTimeout = 0;
            db.AddInParameter(command, "@IDSUGERIDO", DbType.Int64, idSugerido);
            db.AddInParameter(command, "@ACCION", DbType.Int32, accion);

            var read = db.ExecuteReader(command);
            response.Result = read.Reader(x => x.ToResumenSugerido());

            return response;
        }


        public Response ActualizarPedidoFinal(string idSugerido, string farmacia, string proveedor, string sku, string pedFinal, string PedModificado)
        {
            var response = new ResponseList<Invenadro>();

            var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");

            var command = db.GetStoredProcCommand("mta.sp_actualizar_PedidoFInal");


            db.AddInParameter(command, "@idSugerido", DbType.Int64, Int64.Parse(idSugerido));
            db.AddInParameter(command, "@farmacia", DbType.Int32, farmacia);
            db.AddInParameter(command, "@proveedor", DbType.String, proveedor);
            db.AddInParameter(command, "@sku", DbType.String, sku);
            db.AddInParameter(command, "@final", DbType.Decimal, pedFinal);
            db.AddInParameter(command, "@Modificado", DbType.Decimal, PedModificado);

            command.CommandTimeout = 0;


            var exito = db.ExecuteNonQuery(command);
            response.Success = exito != default;

            return response;
        }

        public Response EliminarSkuSugerido(DataTable dt)
        {
            var response = new ResponseList<Invenadro>();

            var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");

            var command = db.GetStoredProcCommand("mta.sp_Elimina_Sugerido_SKU");

            command.Parameters.Add(new SqlParameter("@tipoEliminar", SqlDbType.Structured) { Value = dt });

            command.CommandTimeout = 0;


            var exito = db.ExecuteNonQuery(command);
            response.Success = exito != default;

            return response;
        }

        public ResponseList<DetalleSugeridoMontos> GetMontosSugerido(string idsugerido, string fcreacion, string sucs, string provs, string tipocalculo, string estatus,
                                                               string grupoProducto, string tipoPedido, int consultar, int usuario)//int idCarga
        {
            var response = new ResponseList<DetalleSugeridoMontos>();

            var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");

            var command = db.GetStoredProcCommand("mta.GRFP_Get_MontosSugerido");
            command.CommandTimeout = 0;


            db.AddInParameter(command, "@FechaCreacion", DbType.String, fcreacion);
            db.AddInParameter(command, "@sucursal", DbType.String, sucs);
            db.AddInParameter(command, "@proveedores", DbType.String, provs);
            db.AddInParameter(command, "@TipoCalculo ", DbType.String, tipocalculo);
            db.AddInParameter(command, "@EstatusSugerido", DbType.String, estatus);
            db.AddInParameter(command, "@GrupoProducto", DbType.String, grupoProducto);
            db.AddInParameter(command, "@tipoPedido", DbType.String, tipoPedido);
            db.AddInParameter(command, "@Consultar ", DbType.Int32, consultar);
            db.AddInParameter(command, "@usuario", DbType.Int32, usuario);
            db.AddInParameter(command, "@idsugerido", DbType.Int64, Int64.Parse(idsugerido));

            var read = db.ExecuteReader(command);
            response.Result = read.Reader(x => x.ToDetalleSugeridoMontos());

            return response;
        }

        public ResponseList<ResumenRechazos> GetRechazos(Int64 idSugerido)//int idCarga
        {
            var response = new ResponseList<ResumenRechazos>();

            var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");
            var command = db.GetStoredProcCommand("MTA.SP_RESUMEN_rechazadas");

            command.CommandTimeout = 0;
            db.AddInParameter(command, "@IDSUGERIDO", DbType.Int64, idSugerido);

            var read = db.ExecuteReader(command);
            response.Result = read.Reader(x => x.ToResumenRechazos());

            return response;
        }

    }
}
