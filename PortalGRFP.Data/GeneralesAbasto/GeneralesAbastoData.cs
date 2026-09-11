using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PortalGRFP.Entities.Common;
using PortalGRFP.Entities.Common.Capacity;
using Microsoft.Practices.EnterpriseLibrary.Data;
using PortalGRFP.Data.Extensions;
using System.Data;
using System.Data.SqlClient;
using PortalGRFP.Entities.Response;


namespace PortalGRFP.Data.GeneralesAbasto
{
    public class GeneralesAbastoData
    {


        public NegadosViewModel GetNegadosData()
        {
            var response = new NegadosViewModel();

            var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");
            var command = db.GetStoredProcCommand("mta.SP_GRFP_CONF_GET_NEGADOS");

            using (IDataReader dr = db.ExecuteReader(command))
            {
                while (dr.Read())
                {
                    response.IdNegado = dr.Get<int>("Id");
                    response.PiezasXArticulo = dr.Get<int>("PiezasXArticulo");
                    response.Costo_Articulo = dr.Get<decimal>("Costo_Articulo");
                    response.Imp_Negados_XDia = dr.Get<decimal>("Imp_Negados_XDia");
                    response.Invenadro = dr.Get<string>("Invenadro");
                    response.Resto_Productos = dr.Get<string>("Resto_Productos");
                    response.Dias_Vigencia_Mayorista = dr.Get<int>("Dias_Vigencia_Mayorista");
                    response.Dias_Vigencia_Cedis = dr.Get<int>("Dias_Vigencia_Cedis");
                    response.Dias_Vigencia_Pie_Camion = dr.Get<int>("Dias_Vigencia_Pie_Camion");
                    response.PiezasXFarmacia = dr.Get<int>("PiezasXFarmacia");
                    response.chkRanking = dr.Get<bool>("RankingEstatus");
                    response.Rank_montos = dr.Get<decimal>("Rank_montos");
                    response.Rank_piezas = Convert.ToInt32(dr.Get<decimal>("Rank_piezas"));


                }
            }

            return response;
        }


        public int UINegadosData(NegadosViewModel model)
        {
            var id = 0;
            try
            {
                int flag;

                if (model.IdNegado > 0) flag = 2;
                else flag = 1;

                var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");
                var command = db.GetStoredProcCommand("mta.SP_GRFP_CONF_UI_NEGADOS");

                command.Parameters.Add(new SqlParameter("@flag", flag));
                command.Parameters.Add(new SqlParameter("@Id", model.IdNegado));
                command.Parameters.Add(new SqlParameter("@PiezasXArticulo", model.PiezasXArticulo));
                command.Parameters.Add(new SqlParameter("@Costo_Articulo", model.Costo_Articulo));
                command.Parameters.Add(new SqlParameter("@PiezasXFarmacia", model.PiezasXFarmacia));
                command.Parameters.Add(new SqlParameter("@Imp_Negados_XDia", model.Imp_Negados_XDia));
                command.Parameters.Add(new SqlParameter("@Invenadro", model.Invenadro));
                command.Parameters.Add(new SqlParameter("@Resto_Productos", model.Resto_Productos));
                command.Parameters.Add(new SqlParameter("@Dias_Vigencia_Mayorista", model.Dias_Vigencia_Mayorista));
                command.Parameters.Add(new SqlParameter("@Dias_Vigencia_Cedis", model.Dias_Vigencia_Cedis));
                command.Parameters.Add(new SqlParameter("@Dias_Vigencia_Pie_Camion", model.Dias_Vigencia_Pie_Camion));
                command.Parameters.Add(new SqlParameter("@chkRanking", model.chkRanking));
                command.Parameters.Add(new SqlParameter("@Rank_montos", model.Rank_montos));
                command.Parameters.Add(new SqlParameter("@Rank_piezas", model.Rank_piezas));




                var dr = db.ExecuteReader(command);
                while (dr.Read())
                {
                    id = dr.Get<int>("done");
                }

                return id;
            }
            catch (ExecutionEngineException e)
            {

                return id;
            }

        }


        public List<PedidosCompEspecialesViewModel> GetPedidosComprasEspecialesData(int flag, int id)
        {
            var response = new List<PedidosCompEspecialesViewModel>();

            var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");
            var command = db.GetStoredProcCommand("mta.SP_GRFP_CONF_GET_PEDIDOS_ESPECIALES");

            command.Parameters.Add(new SqlParameter("@flag", flag));
            command.Parameters.Add(new SqlParameter("@id", id));


            using (IDataReader dr = db.ExecuteReader(command))
            {
                while (dr.Read())
                {
                    response.Add(new PedidosCompEspecialesViewModel
                    {
                        Id = dr.Get<int>("Id"),
                        Nombre = dr.Get<string>("Nombre"),
                        Estatus = dr.Get<int>("Estatus"),
                        SumaSugerido = dr.Get<bool>("SumaSugerido"),
                        ComparaMayor = dr.Get<bool>("ComparaMayor"),
                        PieCamion = dr.Get<bool>("PieCamion")
                    });

                }
            }

            return response;
        }


        public Response UIPedidosComprasEspecialesData(PedidosCompEspecialesViewModel model)
        {
            var response = new Response();

            var id = 0;

            int flag;

            if (model.Nombre == null)
            {
                flag = 3;
                model.Nombre = "delete";
            }
            else
            {
                if (model.Id > 0) flag = 2;
                else flag = 1;
            }



            var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");
            var command = db.GetStoredProcCommand("mta.SP_GRFP_CONF_UI_PEDIDOS_ESPECIALES");

            command.Parameters.Add(new SqlParameter("@flag", flag));
            command.Parameters.Add(new SqlParameter("@Id", model.Id));
            command.Parameters.Add(new SqlParameter("@Nombre", model.Nombre));
            command.Parameters.Add(new SqlParameter("@Estatus", model.Estatus));

            command.Parameters.Add(new SqlParameter("@SumaSugerido", model.SumaSugerido));
            command.Parameters.Add(new SqlParameter("@ComparaMayor", model.ComparaMayor));
            command.Parameters.Add(new SqlParameter("@Usuario", model.IdUsuario));
            command.Parameters.Add(new SqlParameter("@PieCamion", model.PieCamion));



            var dr = db.ExecuteReader(command);
            while (dr.Read())
            {
                id = dr.Get<int>("done");
            }

            if (id == 3)

            {
                response.Message = "Ya existe un Tipo de Pedido con Ese Nombre";
                response.Success = false;
            }
            else if (id == 1)
            {
                response.Message = "Exito al guardar";
                response.Success = true;
            }
            else
            {
                response.Message = "Error al guardar";
                response.Success = false;
            }







            return response;

        }


        public AlgoritmoCompraViewModel GetAlgoritmoCompraData()
        {
            var response = new AlgoritmoCompraViewModel();

            var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");
            var command = db.GetStoredProcCommand("mta.SP_GRFP_CONF_GET_ALGORITMO_COMPRA");

            using (IDataReader dr = db.ExecuteReader(command))
            {
                while (dr.Read())
                {
                    response.Id = dr.Get<int>("Id");
                    response.MinPorcentajeVariacionPedidoSucursal = dr.Get<int>("MinPorcentajeVariacionPedidoSucursal");
                    response.MaxPorcentajeVariacionPedidoSucursal = dr.Get<int>("MaxPorcentajeVariacionPedidoSucursal");
                    response.ExistenciaCedisPermisoMayorista = dr.Get<int>("ExistenciaCedisPermisoMayorista");
                    response.RedondeoEmpaque = dr.Get<int>("RedondeoEmpaque");
                    response.FraccionRedondeo = dr.Get<string>("FraccionRedondeo");
                    response.DiasCalculo = dr.Get<int>("DiasCalculo");
                    response.PonderacionSemana1 = dr.Get<int>("PonderacionSemana1");
                    response.PonderacionSemana2 = dr.Get<int>("PonderacionSemana2");
                    response.PonderacionSemana3 = dr.Get<int>("PonderacionSemana3");
                    response.PonderacionSemana4 = dr.Get<int>("PonderacionSemana4");
                    response.PicosVenta = dr.Get<bool>("PicosVenta");
                    response.Desviaciones = dr.Get<int>("Desviaciones");
                    response.AlmacenVirtualN = dr.Get<bool>("AlmVirtual_stts");
                    response.RedondeoEmpInv = dr.Get<int>("RedondeoEmpInv");
                    response.RedondeoEmpRes = dr.Get<int>("RedondeoEmpRes");
                }
            }

            return response;
        }


        public int UIAlgoritmoCompraData(AlgoritmoCompraViewModel model)
        {
            var resp = 0;
            try
            {


                int flag;

                if (model.Id > 0) flag = 2;
                else flag = 1;

                var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");
                var command = db.GetStoredProcCommand("mta.SP_GRFP_CONF_UI_ALGORITMO_COMPRA");

                command.Parameters.Add(new SqlParameter("@flag", flag));
                command.Parameters.Add(new SqlParameter("@Id", model.Id));
                command.Parameters.Add(new SqlParameter("@MinPorcentajeVariacionPedidoSucursal", model.MinPorcentajeVariacionPedidoSucursal));
                command.Parameters.Add(new SqlParameter("@MaxPorcentajeVariacionPedidoSucursal", model.MaxPorcentajeVariacionPedidoSucursal));
                command.Parameters.Add(new SqlParameter("@ExistenciaCedisPermisoMayorista", model.ExistenciaCedisPermisoMayorista));
                command.Parameters.Add(new SqlParameter("@RedondeoEmpaque", model.RedondeoEmpaque));
                command.Parameters.Add(new SqlParameter("@FraccionRedondeo", model.FraccionRedondeo));
                command.Parameters.Add(new SqlParameter("@DiasCalculo", model.DiasCalculo));
                command.Parameters.Add(new SqlParameter("@PonderacionSemana1", model.PonderacionSemana1));
                command.Parameters.Add(new SqlParameter("@PonderacionSemana2", model.PonderacionSemana2));
                command.Parameters.Add(new SqlParameter("@PonderacionSemana3", model.PonderacionSemana3));
                command.Parameters.Add(new SqlParameter("@PonderacionSemana4", model.PonderacionSemana4));
                command.Parameters.Add(new SqlParameter("@PicosVenta", model.PicosVenta));
                command.Parameters.Add(new SqlParameter("@Desviaciones", model.Desviaciones));
                command.Parameters.Add(new SqlParameter("@AlmacenVirtualN", model.AlmacenVirtualN));
                command.Parameters.Add(new SqlParameter("@RedondeoEmpInv", model.RedondeoEmpInv));
                command.Parameters.Add(new SqlParameter("@RedondeoEmpRes", model.RedondeoEmpRes));


                var dr = db.ExecuteReader(command);
                while (dr.Read())
                {
                    resp = dr.Get<int>("done");
                }

                return resp;
            }
            catch (ExecutionEngineException e)
            {

                return resp;
            }

        }

        public List<ComboSucursalesProveedor> GetTiendasList(int flag)
        {
            var response = new List<ComboSucursalesProveedor>();

            var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");
            var command = db.GetStoredProcCommand("[mta].[SP_GRFP_CONF_GET_SUCURSALES]");
            command.Parameters.Add(new SqlParameter("@Flag", flag));

            using (IDataReader dr = db.ExecuteReader(command))
            {
                while (dr.Read())
                    response.Add(new ComboSucursalesProveedor
                    {
                        CodigoSucursal = dr.Get<string>("storeID"),
                        Suc_Nombre = dr.Get<string>("Nombre"),
                    });
            }

            return response;
        }
        public Response CargaExcepcionesData(ExcepcionesInvenadroModel consulta,int user)//int idCarga
            {
            var response = new Response();

            var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");
            var command = db.GetStoredProcCommand("[mta].[SP_GRFP_CONF_LOAD_EXCEPCIONES_INVENADRO]");
            command.CommandTimeout = 0;
            command.Parameters.Add(new SqlParameter("@FechaInicio", consulta.FechaInicio)); 
            command.Parameters.Add(new SqlParameter("@FechaFin", consulta.FechaFin));
            command.Parameters.Add(new SqlParameter("@Usuario", user));

            int id = 0;

            var dr = db.ExecuteReader(command);
            while (dr.Read())
            {
                id = dr.Get<int>("done");
            }

            if (id == 1)
            {
                response.Message = "Proceso realizado con éxito";
                response.Success = true;
            }
            else
            {
                response.Message = "Error en el proceso de carga";
                response.Success = false;
            }

            return response;
        }

        public ResponseList<ExcepcionesInvenadroModel> ExcepcionesoData(ExcepcionesInvenadroModel consulting)
        {
            var response = new ResponseList<ExcepcionesInvenadroModel>();

            var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");

            var command = db.GetStoredProcCommand("[mta].[SP_GRFP_Consulta_ExcepcionesInvenadro]");

            db.AddInParameter(command, "@Sucursal", DbType.String, consulting.Sucursal);
            db.AddInParameter(command, "@SKU", DbType.String, consulting.SKU);
            db.AddInParameter(command, "@FechaInicial", DbType.String, consulting.FechaInicio);
            db.AddInParameter(command, "@FechaFinal", DbType.String, consulting.FechaFin);


            command.CommandTimeout = 0;

            var read = db.ExecuteReader(command);
            response.Result = read.Reader(x => x.ToConsultaExcepciones());
            response.Success = response.Result.Any();
            if (response.Result.Count == 0)
            {
                response.Message = "No hay registros por mostrar";
                response.Success = false;
            }

            return response;
        }

        public ParametrosPVDpredictivo GetParamsPVDdata()
        {
            var response = new ParametrosPVDpredictivo();

            var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");
            var command = db.GetStoredProcCommand("mta.SP_GRFP_CONF_GET_PARAMS_PVD_PREDICTIVO");

            using (IDataReader dr = db.ExecuteReader(command))
            {
                while (dr.Read())
                {
                    response.ID = dr.Get<int>("ID");
                    response.PorcCrecimiento = dr.Get<int>("PorcCrecimiento");
                    response.PorcDecremento = dr.Get<int>("PorcDecremento");
                    response.PVDHistorico = dr.Get<int>("PVDHistorico");
                    response.PVDActual = dr.Get<int>("PVDActual");
                }
            }

            return response;
        }

        public int UIParamsPVDData(ParametrosPVDpredictivo model)
        {
            var id = 0;
            try
            {
                int flag;

                if (model.ID > 0) flag = 2;
                else flag = 1;

                var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");
                var command = db.GetStoredProcCommand("[mta].[SP_GRFP_CONF_UI_PARAMS_PVD_PREDICTIVO]");

                command.Parameters.Add(new SqlParameter("@flag", flag));
                command.Parameters.Add(new SqlParameter("@Id", model.ID));
                command.Parameters.Add(new SqlParameter("@PorcCrecimiento", model.PorcCrecimiento));
                command.Parameters.Add(new SqlParameter("@PorcDecremento", model.PorcDecremento));
                command.Parameters.Add(new SqlParameter("@PVDHistorico", model.PVDHistorico));
                command.Parameters.Add(new SqlParameter("@PVDActual", model.PVDActual));

                var dr = db.ExecuteReader(command);
                while (dr.Read())
                {
                    id = dr.Get<int>("done");
                }

                return id;
            }
            catch (ExecutionEngineException e)
            {

                return id;
            }
        }
    }
}
