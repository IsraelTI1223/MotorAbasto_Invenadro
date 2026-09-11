namespace PortalGRFP.Utilities.TableType
{
    using System.Data;

    /// <summary>
    /// Clase que contiene el método que genera la definición de un tipo de tabla
    /// </summary>
    public static class DescuentoType
    {
        /// <summary>
        /// Genera la definición de un DataTable de Descuentos
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable GetDefinition()
        {
            var dt = new DataTable();

            dt.Columns.Add("IdTipoDescuento", typeof(int));
            dt.Columns.Add("CodigoEAN", typeof(string));
            dt.Columns.Add("PMP", typeof(decimal));
            dt.Columns.Add("Descripcion", typeof(string));
            dt.Columns.Add("Descuento", typeof(decimal));
            dt.Columns.Add("Autorizacion", typeof(bool));
            dt.Columns.Add("Estatus", typeof(int));
            dt.Columns.Add("TipoDescuento", typeof(string));
            dt.Columns.Add("MsgCondiciones", typeof(string));
            dt.Columns.Add("BasePrecio", typeof(string));
            dt.Columns.Add("CostoPromedio", typeof(decimal));
            dt.Columns.Add("PrecioFijo", typeof(decimal));
            dt.Columns.Add("Margen", typeof(decimal));
            dt.Columns.Add("SinUtilidad", typeof(bool));
            dt.Columns.Add("Excluir", typeof(bool));
            dt.Columns.Add("SkuFisico", typeof(string));
            return dt;
        }
    }
}
