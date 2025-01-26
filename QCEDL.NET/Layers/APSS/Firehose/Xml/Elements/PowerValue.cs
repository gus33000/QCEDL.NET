using System.Xml.Serialization;

namespace Qualcomm.EmergencyDownload.Layers.APSS.Firehose.Xml.Elements
{
    public enum PowerValue
    {
        [XmlEnum(Name = "reset")]
        Reset,
        [XmlEnum(Name = "off")]
        Off,
        [XmlEnum(Name = "reset-to-edl")]
        ResetToEDL,

        // Present in old firehose, like MSM8226 ones
        [XmlEnum(Name = "emergency")]
        Emergency
    }
}
