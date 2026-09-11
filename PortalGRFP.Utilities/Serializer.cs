using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace PortalGRFP.Utilities
{
    public static class Serializer
    {
        public static string Serialize<T>(T model) {
            using (var sw = new StringWriter())
            {
                using (XmlWriter w = XmlWriter.Create(sw))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(T));
                    serializer.Serialize(w, model);
                    return sw.ToString();
                }
            }
        }     
    }
}