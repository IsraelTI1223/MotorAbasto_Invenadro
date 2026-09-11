namespace PortalGRFP.Entities.Request
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Request<T>
    {
        public int IdUsuario { get; set; }
        public T Parameters { get; set; }
    }
}
