namespace PortalGRFP.Entities.Response
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class ResponseList<T> : Response
    {
        public List<T> Result { get; set; }
    }
}
