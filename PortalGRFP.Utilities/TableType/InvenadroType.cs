using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalGRFP.Utilities.TableType
{
    public static class InvenadroType
    {
        public static DataTable Definicion()
        {
            var dt = new DataTable();

            dt.Columns.Add("tipo_invenadro", typeof(string));
            dt.Columns.Add("numeroSucursal", typeof(int));
            dt.Columns.Add("nombreSucursal", typeof(string));
            dt.Columns.Add("idCliente", typeof(int));
            dt.Columns.Add("nombreCliente", typeof(string));
            dt.Columns.Add("MontoInversion", typeof(decimal));
            dt.Columns.Add("materialID", typeof(string));
            dt.Columns.Add("Material", typeof(string));
            dt.Columns.Add("subem", typeof(string));
            dt.Columns.Add("ean_upc", typeof(string));
            dt.Columns.Add("fabricante", typeof(string));
            dt.Columns.Add("CveJquiaProductos", typeof(string));
            dt.Columns.Add("JquiaProductos", typeof(string));
            dt.Columns.Add("categoriaMaterial", typeof(string));
            dt.Columns.Add("monto_Adicional", typeof(string));
            dt.Columns.Add("optimo", typeof(int));
            dt.Columns.Add("precioFarmacia", typeof(decimal));
            dt.Columns.Add("importeMaximo", typeof(decimal));
            dt.Columns.Add("IdUsuario", typeof(int));
            dt.Columns.Add("sucursalesForm", typeof(string));
            return dt;
        }
    }
}

