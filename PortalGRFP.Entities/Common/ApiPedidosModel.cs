using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalGRFP.Entities.Common
{
    public class ApiPedidosModel
    {
        public class ApiCatObjects
        {
            public int id_action { get; set; }
            public string spLoad { get; set; }
            public string spGetRequest { get; set; }
            public string spSetResponse { get; set; }
            public string spSendResponse { get; set; }
        }

        public class oPetitionData
        {
            public int IDAction { get; set; }
            public string IDSugerido { get; set; }
            public int IDsucursal { get; set; }
            public int IDOrdenCompra { get; set; }
            public int IDBloqueParcialidad { get; set; }
            public string Request { get; set; }
            public string Response { get; set; }
            public string CodigoRespuesta { get; set; }
            public string DescripcionRespuesta { get; set; }

            public string oFlag { get; set; }
            public string oEx { get; set; }
        }
        public class APICredenciales
        {
            public int IDAction { get; set; }
            public int IDProveedor { get; set; }
            public string oEndPoint { get; set; }
            public string Key { get; set; }
            public string KeyValue { get; set; }
        }
    }
}
