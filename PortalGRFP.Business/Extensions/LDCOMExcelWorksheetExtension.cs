namespace PortalGRFP.Business.Extensions
{
    using OfficeOpenXml;
    using PortalGRFP.Entities.Response;

    public static class LDCOMExcelWorksheetExtension
    {
        public static Response ValidHeadersLDCOM(this ExcelWorksheet excelWorksheet)
        {
            var response = new Response();

            if (excelWorksheet.Dimension.Columns == 2)
            {
                if (!excelWorksheet.Cells[1, 1].Value.Equals("Codigo"))
                {
                    response.Message = "El archivo no contiene la columna Codigo";
                    return response;
                }
                if (!excelWorksheet.Cells[1, 2].Value.Equals("Monto"))
                {
                    response.Message = "El archivo no contiene la columna Monto";
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
