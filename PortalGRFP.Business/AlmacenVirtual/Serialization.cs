using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace PortalGRFP.Business.AlmacenVirtual
{
    [Serializable]

    public class Serialization

    {

        public Serialization() { }



        public static string SerializeObject(Object objectGraph)

        {

            try

            {

                object local = objectGraph;

                Type objType = local.GetType();

                StringBuilder sb = new StringBuilder();

                XmlWriterSettings writerSettings = new XmlWriterSettings();

                writerSettings.OmitXmlDeclaration = true;

                writerSettings.Indent = true;

                using (XmlWriter xmlWriter = XmlWriter.Create(sb, writerSettings))

                {

                    XmlSerializer xs = new XmlSerializer(objType);

                    XmlSerializerNamespaces ns = new XmlSerializerNamespaces();

                    ns.Add(String.Empty, String.Empty);

                    xs.Serialize(xmlWriter, objectGraph, ns);

                }

                return sb.ToString();

            }

            catch (Exception ex)

            {

                throw new Exception(ex.Message);

            }

        }

    }
}
