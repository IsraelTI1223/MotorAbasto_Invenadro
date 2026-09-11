namespace PortalGRFP.Entities.Common.AlmacenVirtual
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class ResponseAV
    {
        public int resp { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
        public String serializado { get; set; }
        public List<cargaAlmacenLayout> tblCargaAV { get; set; }
        public List<tblError> tblerror { get; set; }
        public List<tblUpdate> tblupdate { get; set; }
        public ResponseAV()
        {
            this.resp = 0;
            this.Success = false;
            this.Message = string.Empty;
            this.serializado = string.Empty;
            this.tblCargaAV = new List<cargaAlmacenLayout>();
            this.tblerror = new List<tblError>();
            this.tblupdate = new List<tblUpdate>();
        }
        
    }

    public class cargaAlmacenLayout
    {
        public string FechaCarga { get; set; }
        public string FilenName { get; set; }
        public int items { get; set; }
        public string usuario { get; set; }

        public cargaAlmacenLayout()
        {
            this.FechaCarga = string.Empty;
            this.FilenName = string.Empty;
            this.items = 0;
            this.usuario = string.Empty;
        }
    }


    public class tblError
    {
        public int Reg { get; set; }
        public string Fecha { get; set; }
        public string Contrato { get; set; }
        public string Dato { get; set; }
        public string Descripcion { get; set; }

        public tblError()
        {
            this.Reg = 0;
            this.Fecha = string.Empty;
            this.Contrato = string.Empty;
            this.Dato = string.Empty;
            this.Descripcion = string.Empty;
        }

    }

    public class tblUpdate
    {
        public string Contrato { get; set; }
        public string Almacen { get; set; }
        public string Codigo { get; set; }
        public int PActual { get; set; }
        public int PNueva { get; set; }
        public decimal CActual { get; set; }
        public decimal CNueva { get; set; }

        public tblUpdate()
        {
            this.Almacen = string.Empty;
            this.Contrato = string.Empty;
            this.Codigo = string.Empty;
            this.PActual = 0;
            this.PNueva = 0;
            this.CActual = 0;
            this.CNueva = 0;
        }
    }

}
