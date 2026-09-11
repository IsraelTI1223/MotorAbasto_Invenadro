using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalGRFP.Entities.Common
{
    public  class LogRanking
    {
        public int IdAgencia {get;set;}

        public string SKU    {get;set;}

        public string Fecha  {get;set;}

        public string ERROR { get; set; }
            
    }

    public class SalidaRanking
    {
        public List<LogRanking> Log { get; set; }
    }

    public class  ConteoRanking
    {
        public string Fecha { get; set; }
        public int TotalRegistros { get; set; }
        public int Cargados { get; set; }
        public int Rechazados { get; set; }
    }
    public  class SalidaRankingCont
    {
        public List<ConteoRanking> Cont { get; set; }
    }
}
