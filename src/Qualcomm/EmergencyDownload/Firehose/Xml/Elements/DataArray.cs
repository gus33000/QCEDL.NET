using System.Xml;
using System.Xml.Serialization;

namespace EDLTests.Qualcomm.EmergencyDownload.Firehose.Xml.Elements
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
