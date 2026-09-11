using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalGRFP.Models
{
    public class EstatusPedidos
    {
            public string foliopedido        { get; set; }
            public string Estatus            { get; set; }
            public string Fecha              { get; set; }

            public string no_entrega_sap     { get; set; }
            public string foliofacturafinal  { get; set; }
            public string ID_UBICT           { get; set; }
            public string COD_SUC            { get; set; }
            public string MARCA              { get; set; }
            public string NOM_SUC            { get; set; }
                                            
            public string folio_cliente      { get; set; }

    }
}