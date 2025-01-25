using EDLTests.Qualcomm.EmergencyDownload.Firehose.Xml.Elements;

namespace EDLTests.Qualcomm.EmergencyDownload.Firehose.Xml
{
    internal class QualcommFirehoseXmlPackets
    {
        public static Data GetReadPacket(StorageType storageType, uint LUNi, uint sectorSize, uint FirstSector, uint LastSector)
        {
            return new Data()
            {
                Read = new Read()
                {
                    PhysicalPartitionNumber = LUNi,
                    StorageType = storageType,
                    Slot = 0,
                    SectorSizeInBytes = sectorSize,
                    StartSector = FirstSector.ToString(),
                    LastSector = LastSector,
                    NumPartitionSectors = (LastSector - FirstSector + 1).ToString()
                }
            };
        }

        public static Data GetPowerPacket(PowerValue powerValue = PowerValue.Reset, uint delayInSeconds = 1)
        {
            return new Data()
            {
                Power = new Power()
                {
                    Value = powerValue,
                    DelayInSeconds = delayInSeconds
                }
            };
        }

        public static Data GetStorageInfoPacket(StorageType storageType)
        {
            return new Data()
            {
                GetStorageInfo = new GetStorageInfo()
                {
                    //PhysicalPartitionNumber = 0,
                    StorageType = storageType,
                    //Slot = 0
                }
            };
        }
    }
}
