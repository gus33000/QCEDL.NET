using System.Xml;
using System.Xml.Serialization;

namespace Qualcomm.EmergencyDownload.Layers.APSS.Firehose.Xml.Elements
{
    public class DataArray
    {
        [XmlElement(ElementName = "data")]
        public Data[] Data
        {
            get; set;
        }
    }
}
