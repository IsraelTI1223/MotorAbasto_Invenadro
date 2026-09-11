using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalGRFP.Entities.Common
{
    public class ConciliacionVentaRappi
    {
        public DateTime DiaOperacion { get; set; }
        public string StoreID { get; set; }
        public decimal ImporteTotal { get; set; }
        public string IdSucursal { get; set; }
        public string OrderId { get; set; }
        public string Store { get; set; }
        public string RazonSocial { get; set; }
        public string Marca { get; set; }
        public string FormaPago { get; set; }
        public string BinesCard { get; set; }
        public string Ultimos4Digitos { get; set; }
        public string CodigoAutorizacion { get; set; }
        public string TicketUrl { get; set; }
        public string Conciliate { get; set; }
        public string Iteracion { get; set; }
        public string TicketVenta { get; set; }
        public string Observacion { get; set; }
    }

    public class ConciliacionCargaVentaRappi
    {
        public string DiaOperacion { get; set; }
        public string StoreID { get; set; }
        public string ImporteTotal { get; set; }
        public string IdSucursal { get; set; }
        public string OrderId { get; set; }
        public string Store { get; set; }
        public string RazonSocial { get; set; }
        public string Marca { get; set; }
        public string FormaPago { get; set; }
        public string BinesCard { get; set; }
        public string Ultimos4Digitos { get; set; }
        public string CodigoAutorizacion { get; set; }
        public string TicketUrl { get; set; }
        //public string Conciliate { get; set; }
        //public string Iteracion { get; set; }
        //public string TicketVenta { get; set; }
    }
}
