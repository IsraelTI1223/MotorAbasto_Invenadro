namespace PortalGRFP.Business.Extensions
{
    using OfficeOpenXml;
    using PortalGRFP.Entities.Response;

    public static class FRExcelWorksheetExtension
    {
        public static Response ValidHeadersFR(this ExcelWorksheet excelWorksheet)
        {
            var response = new Response();

            if (excelWorksheet.Dimension.Columns == 9)
            {
                if (!excelWorksheet.Cells[1, 1].Value.Equals("SKU"))
                {
                    response.Message = "El archivo no contiene la columna SKU";
                    return response;
                }
                if (!excelWorksheet.Cells[1, 2].Value.Equals("Base_precio"))
                {
                    response.Message = "El archivo no contiene la columna Base_precio";
                    return response;
                }
                if (!excelWorksheet.Cells[1, 3].Value.Equals("Descuento"))
                {
                    response.Message = "El archivo no contiene la columna Descuento";
                    return response;
                }
                if (!excelWorksheet.Cells[1, 4].Value.Equals("Precio"))
                {
                    response.Message = "El archivo no contiene la columna Precio";
                    return response;
                }
                if (!excelWorksheet.Cells[1, 5].Value.Equals("estatus"))
                {
                    response.Message = "El archivo no contiene la columna estatus";
                    return response;
                }
                if (!excelWorksheet.Cells[1, 6].Value.Equals("TipoDescuento"))
                {
                    response.Message = "El archivo no contiene la columna TipoDescuento";
                    return response;
                }
                if (!excelWorksheet.Cells[1, 7].Value.Equals("Autorizacion"))
                {
                    response.Message = "El archivo no contiene la columna Autorizacion";
                    return response;
                }
                if (!excelWorksheet.Cells[1, 8].Value.Equals("MsgCondiciones1"))
                {
                    response.Message = "El archivo no contiene la columna MsgCondiciones1";
                    return response;
                }
                if (!excelWorksheet.Cells[1, 9].Value.Equals("SKUFISICO"))
                {
                    response.Message = "El archivo no contiene la columna SKUFISICO";
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
