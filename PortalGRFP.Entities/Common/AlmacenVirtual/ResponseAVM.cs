namespace PortalGRFP.Entities.Common.AlmacenVirtual
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class ResponseAVM
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public List<tblAlmacen> tblalmacen
        { get; set; }
        public ResponseAVM()
        {
            this.Success = false;
            this.Message = string.Empty;
            this.tblalmacen = new List<tblAlmacen>();
        }
        
    }

    public class tblAlmacen
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string CadenaAsociada { get; set; }

        public tblAlmacen()
        {
            this.Id = 0;
            this.Nombre = string.Empty;
            this.CadenaAsociada = string.Empty;
        }
    }

}
