using System.Xml;
using System.Xml.Serialization;

namespace EDLTests.Qualcomm.EmergencyDownload.Firehose
{
    public class QualcommFirehoseXmlElements
    {
        public class DataArray
        {
            [XmlElement(ElementName = "data")]
            public Data[] Data
            {
                get; set;
            }
        }

        public class Data
        {
            [XmlElement(ElementName = "power")]
            public Power? Power { get; set; }

            [XmlElement(ElementName = "log")]
            public Log? Log { get; set; }

            [XmlElement(ElementName = "response")]
            public Response? Response { get; set; }

            [XmlElement(ElementName = "getstorageinfo")]
            public GetStorageInfo? GetStorageInfo { get; set; }

            [XmlElement(ElementName = "read")]
            public Read? Read { get; set; }
        }

        public class Patches
        {
            [XmlElement(ElementName = "patch")]
            public Patch[] Patch
            {
                get; set;
            }
        }

        public class Patch
        {

        }

        // IOOptions, IOData, DevData
        public class Read : ReadMixin1
        {
            // Already defined
            /*private ulong? startSector;

            [XmlAttribute(AttributeName = "start_sector")]
            public ulong StartSector
            {
                get => startSector ?? 0; set => startSector = value;
            }

            public bool ShouldSerializeStartSector()
            {
                return startSector.HasValue;
            }*/

            private ulong? lastSector;

            [XmlAttribute(AttributeName = "last_sector")]
            public ulong LastSector
            {
                get => lastSector ?? 0; set => lastSector = value;
            }

            public bool ShouldSerializeLastSector()
            {
                return lastSector.HasValue;
            }

            private byte? skipBadBlock;

            [XmlAttribute(AttributeName = "skip_bad_block")]
            public byte SkipBadBlock
            {
                get => skipBadBlock ?? 0; set => skipBadBlock = value;
            }

            public bool ShouldSerializeSkipBadBlock()
            {
                return skipBadBlock.HasValue;
            }

            private byte? getSpare;

            [XmlAttribute(AttributeName = "get_spare")]
            public byte GetSpare
            {
                get => getSpare ?? 0; set => getSpare = value;
            }

            public bool ShouldSerializeGetSpare()
            {
                return getSpare.HasValue;
            }

            private byte? eccDisabled;

            [XmlAttribute(AttributeName = "ecc_disabled")]
            public byte ECCDisabled
            {
                get => eccDisabled ?? 0; set => eccDisabled = value;
            }

            public bool ShouldSerializeECCDisabled()
            {
                return eccDisabled.HasValue;
            }
        }

        // IOData, DevData
        public class ReadMixin1 : DevData
        {
            private uint? sectorSizeInBytes;

            [XmlAttribute(AttributeName = "SECTOR_SIZE_IN_BYTES")]
            public uint SectorSizeInBytes
            {
                get => sectorSizeInBytes ?? 0; set => sectorSizeInBytes = value;
            }

            public bool ShouldSerializeSectorSizeInBytes()
            {
                return sectorSizeInBytes.HasValue;
            }

            private string? numPartitionSectors;

            [XmlAttribute(AttributeName = "num_partition_sectors")]
            public string NumPartitionSectors
            {
                get => numPartitionSectors ?? ""; set => numPartitionSectors = value;
            }

            public bool ShouldSerializeNumPartitionSectors()
            {
                return numPartitionSectors != null;
            }

            private string? startSector;

            [XmlAttribute(AttributeName = "start_sector")]
            public string StartSector
            {
                get => startSector ?? ""; set => startSector = value;
            }

            public bool ShouldSerializeStartSector()
            {
                return startSector != null;
            }
        }

        public class GetStorageInfo : DevData
        {
            private ulong? printJson;

            [XmlAttribute(AttributeName = "print_json")]
            public ulong PrintJson
            {
                get => printJson ?? 1; set => printJson = value;
            }

            public bool ShouldSerializePrintJson()
            {
                return printJson.HasValue;
            }
        }

        public class DevData
        {
            private StorageType? storageType;

            [XmlAttribute(AttributeName = "storage_type")]
            public StorageType StorageType
            {
                get => storageType ?? StorageType.UFS; set => storageType = value;
            }

            public bool ShouldSerializeStorageType()
            {
                return storageType.HasValue;
            }

            private uint? slot;

            [XmlAttribute(AttributeName = "slot")]
            public uint Slot
            {
                get => slot ?? 0; set => slot = value;
            }

            public bool ShouldSerializeSlot()
            {
                return slot.HasValue;
            }

            private uint? physicalPartitionNumber;

            [XmlAttribute(AttributeName = "physical_partition_number")]
            public uint PhysicalPartitionNumber
            {
                get => physicalPartitionNumber ?? 0; set => physicalPartitionNumber = value;
            }

            public bool ShouldSerializePhysicalPartitionNumber()
            {
                return physicalPartitionNumber.HasValue;
            }
        }

        public class IOData
        {
            private StorageType? storageType;

            [XmlAttribute(AttributeName = "storage_type")]
            public StorageType StorageType
            {
                get => storageType ?? StorageType.UFS; set => storageType = value;
            }

            public bool ShouldSerializeStorageType()
            {
                return storageType.HasValue;
            }

            private uint? slot;

            [XmlAttribute(AttributeName = "slot")]
            public uint Slot
            {
                get => slot ?? 0; set => slot = value;
            }

            public bool ShouldSerializeSlot()
            {
                return slot.HasValue;
            }

            private uint? physicalPartitionNumber;

            [XmlAttribute(AttributeName = "physical_partition_number")]
            public uint PhysicalPartitionNumber
            {
                get => physicalPartitionNumber ?? 0; set => physicalPartitionNumber = value;
            }

            public bool ShouldSerializePhysicalPartitionNumber()
            {
                return physicalPartitionNumber.HasValue;
            }
        }

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

        public class Power
        {
            private PowerValue? value;

            [XmlAttribute(AttributeName = "value")]
            public PowerValue Value
            {
                get => value ?? PowerValue.Reset; set => this.value = value;
            }

            public bool ShouldSerializeValue()
            {
                return value.HasValue;
            }

            private ulong? delayInSeconds;

            [XmlAttribute(AttributeName = "DelayInSeconds")]
            public ulong DelayInSeconds { get => delayInSeconds ?? 100; set => delayInSeconds = value; }

            public bool ShouldSerializeDelayInSeconds()
            {
                return delayInSeconds.HasValue;
            }
        }

        public enum PowerValue
        {
            [XmlEnum(Name = "reset")]
            Reset,
            [XmlEnum(Name = "off")]
            Off,
            [XmlEnum(Name = "reset-to-edl")]
            ResetToEDL,
            [XmlEnum(Name = "emergency")]
            Emergency
        }

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
}
