namespace PortalGRFP.Entities.Common
{
    public class DescuentoHdr
    {
        public long IdHdr { get; set; }
        public string TipoCarga { get; set; }
        public string NombreArchivo { get; set; }
        public int TotalRegistros { get; set; }
        public int TotalCargados { get; set; }
        public int TotalErroneos { get; set; }
        public string FechaRegistro { get; set; }
        public string FechaActualizacion { get; set; }
        public string UsuarioRegistro { get; set; }
        public string NombreCliente { get; set;  }
        public string NombreSubCliente { get; set; }
    }
}
