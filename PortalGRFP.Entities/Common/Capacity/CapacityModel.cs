using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalGRFP.Entities.Common.Capacity
{
    public class CapacityModel
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public CapacityModel()
        {
            this.Success = false;
            this.Message = string.Empty;
        }

    }

    public class tblCapacity
    {
        public Int64 IdSucrusal { get; set; }
        public string SKU { get; set; }
        public int Capacity { get; set; }
        public string FechaIni { get; set; }
        public string FechaFin { get; set; }
        public Int64 idMotivo { get; set; }
        public int User { get; set; }
        public tblCapacity()
        {
            this.IdSucrusal = 0;
            this.SKU = string.Empty;
            this.Capacity = 0;
            this.FechaIni = string.Empty;
            this.FechaFin = string.Empty;
            this.idMotivo = 0;
            this.User = 0;
        }
    }

    public class responseCapacity
    {
        public CapacityModel respuesta { get; set; }
        public List<tblValida> listValida { get; set; }
        public List<tblReporte> listReportes { get; set; }
        
        public responseCapacity()
        {
            this.respuesta = new CapacityModel();
            this.listValida = new List<tblValida>();
            this.listReportes = new List<tblReporte>();
        }
    }

    public class tblValida
    {
        public int SKUAceptados { get; set; }
        public int SKURechazados { get; set; }
        public string Marca { get; set; }

        public tblValida()
        {
            this.SKUAceptados = 0;
            this.SKURechazados = 0;
            this.Marca = string.Empty;
        }
    }

    public class tblReporte
    {
        public Int64 IdSucursal { get; set; }
        public string SKU { get; set; }
        public string Producto { get; set; }
        public int Capacity { get; set; }
        public string StatusCarga { get; set; }

        public tblReporte()
        {
            this.IdSucursal = 0;
            this.SKU = string.Empty;
            this.Producto = string.Empty;
            this.Capacity =0;
            this.StatusCarga = string.Empty;
        }
    }

    public class tblCatCapacity
    {
        public int Id { get; set; }
        public string TipoCapacity { get; set; }
        public string Invenadro { get; set; }
        public tblCatCapacity()
        {
            this.Id = 0;
            this.TipoCapacity = string.Empty;
            this.Invenadro = string.Empty;
        }
    }
}
