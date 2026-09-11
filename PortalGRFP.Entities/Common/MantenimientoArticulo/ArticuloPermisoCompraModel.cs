using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace PortalGRFP.Entities.Common.MantenimientoArticulo
{
    public class ArticuloPermisoCompraModel
    {

        public class CargaArticuloMViewModel
        {
          
            public bool Venta { get; set; }
            public bool Compra { get; set; }
            public int Conceptocompra { get; set; }
            public int Conceptoventa { get; set; }
            public int IdUsuario { get; set; }

        }

        public class ArticuloExcel
        {

            public string IdSucursal { get; set; }
            public string Sku { get; set; }

        }


        public class CadenasSucursalesModel
        {

            public string ID { get; set; }
            public string Valor { get; set; }

        }


        public class ArticuloXSucursalModel
        {
            public string Sku { get; set; }
            public bool Venta { get; set; }
            public bool Compra { get; set; }
            public int IdUsuario { get; set; }
            public List<CadenasSucursalesModel> Sucursales { get; set; }

        }


        public class LogErrorPermisoCompraModel
        {
            public int IdSucursal { get; set; }
            public string SKU { get; set; }
            public string oEx { get; set; }

        }



        public class ArticulosPorSucursalModel
        {
            public string SKU { get; set; }
            public int IdUsuario { get; set; }
            public int Flag { get; set; }
            public int Concepto { get; set; }
            public List<PermisosArticulosPorSucursalModel> Sucursales { get; set; }

        }

        public class PermisosArticulosPorSucursalModel
        {
            public int IdSucursal { get; set; }
            public bool Venta { get; set; }
            public bool Compra { get; set; }
            public int Conceptocompra { get; set; }
            public int ConceptoVenta { get; set; }
        }




        /*
                public class SucursalesModel
                {

                    public int StoreId { get; set; }
                    public string Nombre { get; set; }

                }*/


    }
}
