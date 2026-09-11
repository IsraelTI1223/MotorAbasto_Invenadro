using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalGRFP.Models
{
    public class PedidoDetalle
    {
        public string folioPedido         {get;set;}
        public string folio_cliente       {get;set;}
        public string fecha_ped           {get;set;}
        public string codigoSucursal      {get;set;}
        public string material            {get;set;}
        public int cantidad            {get;set;}
        public string ean                 {get;set;}
        public string codigoProducto      {get;set;}
        public string no_entrega_sap      {get;set;}
        public string fecha_conf          {get;set;}
        public int cantidad_conf      { get; set; }
        public string folioFacturaFinal   {get;set;}
        public string fechaFactura        {get;set;}
        public int cantidad_facturada { get; set; }

        public int cantidad_rcbo { get; set; }

        public string fecha_rec_suc { get; set; }
        public string descripcion { get; set; }

        public string MARCA { get; set; }

        public string NOM_SUC { get; set; }

    }
}