using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalGRFP.Entities.Common
{
    public class PedidosCompEspecialesViewModel
    {

        public int Id { get; set; }
        public string Nombre { get; set; }
        public int Estatus { get; set; }
        public bool SumaSugerido { get; set; }
        public bool ComparaMayor { get; set; }
        public bool PieCamion { get; set; }

        public int IdUsuario { get; set; }








        /* public class ListPedidoCompraEspecial
         {
             public int Id { get; set; }
             public string Nombre { get; set; }
             public int Estatus { get; set; }
         }

         public class PedidoCompraEspecial
         {
             public int Id { get; set; }
             public string Nombre { get; set; }
             public int Estatus { get; set; }
         }*/
    }
}
