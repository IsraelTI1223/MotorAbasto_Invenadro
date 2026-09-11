using Microsoft.Practices.EnterpriseLibrary.Data;
using PortalGRFP.Data.Extensions;
using PortalGRFP.Entities.Common.ComprasEspeciales;
using PortalGRFP.Entities.Response;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PortalGRFP.Entities.Common.ComprasEspeciales.ConsultaPedidosModel;
using static PortalGRFP.Entities.Common.MantenimientoArticulo.ArticuloPermisoCompraModel;

namespace PortalGRFP.Data.ComprasEspecialesData
{
    public class ComprasEspecialesData
    {
       

        public ResponseList<GruposListViewsModel> GetGrupoListCompraData(int user)//1=formas 2=tipos 3=clasificacion 4=periodicidad
        {
            var response = new ResponseList<GruposListViewsModel>();

            var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");
            var command = db.GetStoredProcCommand("mta.sp_GRFP_Get_Cat_GrupoSuc");//solo para inicializar
            db.AddInParameter(command, "@user", DbType.Int32, user);

            command.CommandTimeout = 0;

            var read = db.ExecuteReader(command);
            response.Result = read.Reader(x => x.ToGrupoListCompra());
            response.Success = response.Result.Any();

            return response;
        }

        public ResponseList<CadenasSucursalesModel> GetGrupoSucursalData(/*int grupo*/ string grupos, int user)//1=formas 2=tipos 3=clasificacion 4=periodicidad
        {
            var response = new ResponseList<CadenasSucursalesModel>();

            var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");
            var command = db.GetStoredProcCommand("mta.sp_GRFP_Get_GrupoSuc");//solo para inicializar

            //db.AddInParameter(command, "@idGrupo", DbType.Int32, grupo);
            db.AddInParameter(command, "@idGrupo", DbType.String, grupos);

            db.AddInParameter(command, "@user", DbType.Int32, user);


            command.CommandTimeout = 0;

            var read = db.ExecuteReader(command);
            response.Result = read.Reader(x => x.ToCadenaSucursales());

            response.Success = response.Result.Any();

            return response;
        }

        public ResponseList<PedidosConsultaModel> GetPedidosListCompraData(string search, int user)
        {
            var response = new ResponseList<PedidosConsultaModel>();

            var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");

            //var command = db.GetStoredProcCommand("mta.sp_GRFP_Get_Ped_Comp_Esp");
            
            var command = db.GetStoredProcCommand("mta.SP_GRFP_GET_COMPRAS_ESPECIALES_PED");

            db.AddInParameter(command, "@sucursales", DbType.String, search);
            db.AddInParameter(command, "@user", DbType.Int32, user);

            command.CommandTimeout = 0;

            var read = db.ExecuteReader(command);
            response.Result = read.Reader(x => x.ToPedidosListCompra());

            response.Success = response.Result.Any();

            return response;

        }

        public ResponseList<PedidosConsultaModel> DeletePedidosListCompraData(string folio, string id_tipo, string id_proveedor, string id_suc, string search, int user)
        {
            var response = new ResponseList<PedidosConsultaModel>();

            var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");

            //var command = db.GetStoredProcCommand("mta.sp_GRFP_Get_Ped_Comp_Esp");

            var command = db.GetStoredProcCommand("mta.SP_GRFP_DELETE_COMPRAS_ESPECIALES_PED");

            db.AddInParameter(command, "@folio", DbType.Int64, long.Parse(folio));
            db.AddInParameter(command, "@id_tipo", DbType.Int32, int.Parse(id_tipo));
            db.AddInParameter(command, "@id_proveedor", DbType.Int32, int.Parse(id_proveedor));
            db.AddInParameter(command, "@id_suc", DbType.Int32, int.Parse(id_suc));
            db.AddInParameter(command, "@Sucursales", DbType.String, search);
            db.AddInParameter(command, "@user", DbType.Int32, user);



            command.CommandTimeout = 0;

            var read = db.ExecuteReader(command);
            response.Result = read.Reader(x => x.ToPedidosListCompra());

            response.Success = response.Result.Any();

            response.Message = "OK";

            return response;

        }

        public ResponseList<PedidosConsultaModel> DeletePedidosListGroupCompraData(List<PedidosEspecialesDeleteModel> model, int user)
        {
            var response = new ResponseList<PedidosConsultaModel>();

            var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");


            foreach (var item in model)
            {
                var command = db.GetStoredProcCommand("mta.SP_GRFP_DELETE_COMPRAS_ESPECIALES_PED_GROUP");

                db.AddInParameter(command, "@folio", DbType.Int64, item.Folio);
                db.AddInParameter(command, "@id_tipo", DbType.Int32, item.Id_tipo);
                db.AddInParameter(command, "@id_proveedor", DbType.Int32, item.Id_proveedor);
                db.AddInParameter(command, "@id_suc", DbType.Int32, item.IdSucursal);
                db.AddInParameter(command, "@user", DbType.Int32, user);

                command.CommandTimeout = 0;

                db.ExecuteNonQuery(command);

            }

            response = GetPedidosListCompraData(model[0].Sucursales, user);

            response.Message = "OK";
            response.Success = true;

            return response;

        }

    }
}
