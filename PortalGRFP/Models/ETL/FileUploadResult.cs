using PortalGRFP.Entities.Models.DatLayouts;
using System.Collections.Generic;

namespace PortalGRFP.Models.ETL
{
    public class FileUploadResult
    {
        public List<LayoutBase> TableError { get; set; }
        public long RowsErrorResult { get; set; }
        public long RowsOKResult { get; set; }
        public long TimeProcess { get; set; }
        public long TimeLoadProcess { get; set; }
        public List<string> Messages { get; set; }
        public FileUploadResult() {
            Messages = new List<string>();
            TableError = new List<LayoutBase>();
        }
    }
}