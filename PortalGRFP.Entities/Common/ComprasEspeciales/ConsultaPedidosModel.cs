using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalGRFP.Entities.Common.ComprasEspeciales
{
    public class ConsultaPedidosModel
    {
        public class GruposListViewsModel
        {

            public int ID { get; set; }
            public string Valor { get; set; }

        }

        public class PedidosConsultaModel
        {

            //public string Folio { get; set; }
            //public string Proveedor { get; set; }
            //public DateTime FechaOC { get; set; }
            //public DateTime FechaCarga { get; set; }
            //public string Estatus { get; set; }
            //public string Tipo { get; set; }
            //public int Piezas { get; set; }
            //public int Id_Suc { get; set; }
            //public int IdMantto { get; set; }
            //public int IdSucursal { get; set; }
            //public int IdGrupo { get; set; }
            //public bool Invenadro { get; set; }
            //public bool? Negados { get; set; }
            //public int CodigoSucursalFT { get; set; }

            public long Folio { get; set; }
            public int Id_proveedor { get; set; }
            public string Proveedor { get; set; }
            public string Fecha_Carga { get; set; }
            public string Fecha_Aplicacion { get; set; }
            public int IdSucursal { get; set; }
            public string Estatus { get; set; }
            public int Id_tipo { get; set; }
            public string Tipo { get; set; }
            public decimal Importe { get; set; }
            public int Piezas { get; set; }



        }
    }
}
