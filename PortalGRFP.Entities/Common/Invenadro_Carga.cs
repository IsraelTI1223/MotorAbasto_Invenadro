using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalGRFP.Entities.Common
{
    public class Invenadro_Carga
    {
        public string Tipo_Invenadro { get; set; }
        public int NumSucursal { get; set; }
        public string NombreSucursal { get; set; }
        public int IdCliente { get; set; }
        public string NombreCliente { get; set; }
        public decimal MontoInversion { get; set; }
        public string MaterialId { get; set; }
        public string Material { get; set; }
        public string Subem { get; set; }
        public string Ean_Upc { get; set; }
        public string Fabricante { get; set; }
        public string CveJqiaProductos { get; set; }
        public string JquiaProductos { get; set; }
        public string CategoriaMaterial { get; set; }
        public string MontoAdicional { get; set; }
        public int Optimo { get; set; }
        public decimal PrecioFarmacia { get; set; }
        public decimal Importemaximo { get; set; }
        public int IdUsuario { get; set; }

    }
}
