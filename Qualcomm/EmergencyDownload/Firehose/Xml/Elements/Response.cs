using System.Xml;
using System.Xml.Serialization;

namespace EDLTests.Qualcomm.EmergencyDownload.Firehose.Xml.Elements
{
    public class Response
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

        [XmlAttribute(AttributeName = "rawmode")]
        public bool RawMode
        {
            get; set;
        }
    }
}
