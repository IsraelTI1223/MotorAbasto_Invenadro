using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalGRFP.Extensions.FTP
{
    public class EndPointFileHandler
    {
        public String Name { get; set; }
        public String Fullname { get; set; }
        public long Size { get; set; }

        public EndPointFileHandler()
        {
            // Do nothing
        }

        public String GetKey()
        {
            try
            {
                String strName = Path.GetFileNameWithoutExtension(Name).ToUpper();
                return strName;
            }
            catch (Exception)
            {
                return String.Empty;
            }
        }
    }
}
