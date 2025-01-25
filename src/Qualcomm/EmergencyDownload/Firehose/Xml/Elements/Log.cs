using System.Xml;
using System.Xml.Serialization;

namespace EDLTests.Qualcomm.EmergencyDownload.Firehose.Xml.Elements
{
    public class Log
    {
        [XmlAttribute(AttributeName = "value")]
        public string Value
        {
            get; set;
        }

        public bool ShouldSerializeValue()
        {
            return Value != null;
        }
    }
}
