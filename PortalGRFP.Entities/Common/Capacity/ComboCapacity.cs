using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace PortalGRFP.Entities.Common.Capacity
{
    public class cCapacity
    {
        public List<ComboCapacity> comboCapacity { get;set;}
        public List<tblCatCapacity> catCapacity { set; get; }

        public cCapacity()
        {
            this.comboCapacity = new List<ComboCapacity>();
            this.catCapacity = new List<tblCatCapacity>();
        }
    }
    public class ComboCapacity
    {
        public string Value { get; set; }
        public string Descripcion { get; set; }
        
        public ComboCapacity()
        {
            this.Value = string.Empty;
            this.Descripcion = string.Empty;
            
        }
    }

}
