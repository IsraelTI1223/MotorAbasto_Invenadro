using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalGRFP.Entities.Common
{
    public class Ranking_Carga
    {
        public int IdAgencia { get; set; }
        public string SKU { get; set; }
        public string Desc_SKU { get; set; }
        public string Fecha { get; set; }
        public decimal Rank_monto { get; set; }
        public decimal Rank_piezas { get; set; }
        public int IdUsuario { get; set; }

    }

    public class RankingModel
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public RankingModel()
        {
            this.Success = false;
            this.Message = string.Empty;
        }

    }
    public class responseRanking
    {
        public RankingModel respuestaRanking { get; set; }
       
        public responseRanking()
        {
            this.respuestaRanking = new RankingModel();
        }

    }
    public class tblValidaRanking
    {
        public int SKUAceptados { get; set; }
        public int SKURechazados { get; set; }
        public string Agencia { get; set; }

        public tblValidaRanking()
        {
            this.SKUAceptados = 0;
            this.SKURechazados = 0;
            this.Agencia = string.Empty;
        }
    }
}
