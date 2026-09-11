using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalGRFP.Entities.Common
{
    public class ExcepcionesInvenadroModel
    {
        //Parametros
        public int IdSucursal { get; set; }
        public string Sucursal { get; set; }
        public string SKU { get; set; }
        public string FechaInicio { get; set; }
        public string FechaFin { get; set; }

        public int Usuario { get; set; }

        //Tabla

        public string NombreSucursal { get; set; }
        public string DescripcionSKU { get; set; }
        public string UsuarioRealizo { get; set; }


        public static DataTable Definicion()
        {
            var dt = new DataTable();

            dt.Columns.Add("Sucursal", typeof(int));
            dt.Columns.Add("SKU", typeof(string));
            dt.Columns.Add("Limite", typeof(int));
            dt.Columns.Add("FechaInicio", typeof(string));
            dt.Columns.Add("FechaFin", typeof(string));
            dt.Columns.Add("Concepto", typeof(int));
            dt.Columns.Add("Usuario", typeof(int));
            return dt;

        }
    }
}
