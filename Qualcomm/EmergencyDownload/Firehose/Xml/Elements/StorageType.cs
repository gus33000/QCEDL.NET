using System.Xml.Serialization;

namespace EDLTests.Qualcomm.EmergencyDownload.Firehose.Xml.Elements
{
    public enum StorageType
    {
        [XmlEnum(Name = "eMMC")]
        SDCC,
        [XmlEnum(Name = "spinor")]
        SPINOR,
        [XmlEnum(Name = "UFS")]
        UFS,
        [XmlEnum(Name = "nand")]
        NAND,
        [XmlEnum(Name = "NVMe")]
        NVME
    }
}
