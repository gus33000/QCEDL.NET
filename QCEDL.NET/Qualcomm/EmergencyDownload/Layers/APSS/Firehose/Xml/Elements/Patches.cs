using System.Xml;
using System.Xml.Serialization;

namespace QCEDL.NET.Qualcomm.EmergencyDownload.Layers.APSS.Firehose.Xml.Elements
{
    public class Patches
    {
        [XmlElement(ElementName = "patch")]
        public Patch[] Patch
        {
            get; set;
        }
    }
}
