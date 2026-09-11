using System;
namespace PortalGRFP.Business.Extensions
{
    using OfficeOpenXml;
    using PortalGRFP.Entities.Response;

    public static class POPSAEExcelWorksheetExtension
    {
        public static Response ValidHeadersPOPSAE(this ExcelWorksheet excelWorksheet)
        {
            var response = new Response();

            if (excelWorksheet.Dimension.Columns == 10)
            {
                if (!excelWorksheet.Cells[1, 1].Value.Equals("Código"))
                {
                    response.Message = "El archivo no contiene la columna Código";
                    return response;
                }
                if (!excelWorksheet.Cells[1, 2].Value.Equals("Descripción"))
                {
                    response.Message = "El archivo no contiene la columna Descripción";
                    return response;
                }
                if (!excelWorksheet.Cells[1, 3].Value.Equals("Precio público"))
                {
                    response.Message = "El archivo no contiene la columna Precio público";
                    return response;
                }
                if (!excelWorksheet.Cells[1, 4].Value.Equals("Descuento"))
                {
                    response.Message = "El archivo no contiene la columna Descuento";
                    return response;
                }
                if (!excelWorksheet.Cells[1, 5].Value.Equals("Costo promedio"))
                {
                    response.Message = "El archivo no contiene la columna Costo promedio";
                    return response;
                }
                if (!excelWorksheet.Cells[1, 6].Value.Equals("Precio fijo"))
                {
                    response.Message = "El archivo no contiene la columna Precio fijo";
                    return response;
                }
                if (!excelWorksheet.Cells[1, 7].Value.Equals("Margen"))
                {
                    response.Message = "El archivo no contiene la columna Margen";
                    return response;
                }
                if (!excelWorksheet.Cells[1, 8].Value.Equals("Sin utilidad"))
                {
                    response.Message = "El archivo no contiene la columna Sin utilidad";
                    return response;
                }
                if (!excelWorksheet.Cells[1, 9].Value.Equals("Excluir"))
                {
                    response.Message = "El archivo no contiene la columna Excluir";
                    return response;
                }
                if (!excelWorksheet.Cells[1, 10].Value.Equals("Autorizar"))
                {
                    response.Message = "El archivo no contiene la columna Autorizar";
                    return response;
                }
            }
            else
            {
                response.Message = "El archivo no contiene el número de columnas correctas";
                return response;
            }

            response.Success = true;

            return response;
        }
    }
}
