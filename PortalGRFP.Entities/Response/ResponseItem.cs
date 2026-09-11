namespace PortalGRFP.Entities.Response
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class ResponseItem<T> : Response
    {
        public T Result { get; set; }
    }
}
