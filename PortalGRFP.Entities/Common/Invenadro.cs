using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalGRFP.Entities.Common
{
    public class Invenadro
    {
        public bool LimiteCostoProductos { get; set; }
        public decimal Monto { get; set; }
        public int PiezasProductos { get; set; }
        public bool PVD { get; set; }

        public bool InvenadroCero { get; set; }
        public bool ActivarProducto { get; set; }

        public bool InactivarProductos { get; set; }
        public decimal PorcentajeCoincidencia { get; set; }
        public bool Actualizacion_automatica { get; set; }
        public int diasPVD { get; set; }
        public int diasAutomatico { get; set; }
    }

    public class Relacion_Invenadro_Aut
    {
        public string SKU { get; set; }
        public string NombreSKU { get; set; }
        public decimal PVD { get; set; }
        public int Optimo { get; set; }
        public string EstatusProducto { get; set; }
        public string IdSucursal { get; set; }
        public string NombreSucursal { get; set; }
        public string Motivo { get; set; }
        public int Usuario { get; set; }
        public string RelacionInvenadroVenta { get; set; }

        public int oProcId { get; set; }
        public string oFlag { get; set; }
        public string oEx { get; set; }
        public DateTime oDTTC { get; set; }
        public DateTime oDTTM { get; set; }
    }

    public class Excluidos_Invenadro_Aut
    {
        public string IdSucursal { get; set; }
        public string NombreSucursal { get; set; }
        public string SKU { get; set; }
        public string NombreSKU { get; set; }
        public int OptimoAnterior { get; set; }
        public int OptimoActualizado { get; set; }
        public decimal PVD { get; set; }
        public string RelacionInvenadroVenta { get; set; }
        public decimal InvenadroMontoNuevo { get; set; }
        public decimal VariacionPiezas { get; set; }
        public string EstatusProducto { get; set; }
        public string Motivo { get; set; }
    }

    public class Resumen_Invenadro
    {
        public string EstatusProducto { get; set; }
        public string Motivo { get; set; }
        public int FarmaciasSKU { get; set; }
        public int Piezas { get; set; }
        public decimal MontoInvenadro { get; set; }
        public string oFlag { get; set; }
        public string oEx { get; set; }

    }

    public static class AutInvenadroType
    {
        public static DataTable Definicion()
        {
            var dt = new DataTable();

            dt.Columns.Add("IdSucursal", typeof(string));
            dt.Columns.Add("SKU", typeof(string));
            dt.Columns.Add("Motivo", typeof(string));
            dt.Columns.Add("Optimo", typeof(int));
            dt.Columns.Add("oEx", typeof(string));
            dt.Columns.Add("Usuario", typeof(string));
         
            return dt;
        }
    }

    public class InfoCDR
    {
        public int Id { get; set; }
        public int Agencia_id { get; set; }
        public string Nombre { get; set; }
        public string agencia_codigo_interno { get; set; }
        public int Farmacias { get; set; }
        public decimal MontoCDR { get; set; }
        public decimal MontoNecesidad { get; set; }
        public int PedidosRealizar { get; set; }
        public decimal PorcentajeCompraMayor { get; set; }
        public decimal PorcentajeCompraMenor { get; set; }
        public decimal MontoCompraMayor { get; set; }
        public decimal MontoCompraMenor { get; set; }
        public int Usuario { get; set; }
        public string oFlag { get; set; }
        public DateTime oDTTC { get; set; }
        public DateTime oDTTM { get; set; }
    }

}
