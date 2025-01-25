using EDLTests.Qualcomm.EmergencyDownload.Firehose.Xml.Elements;
using EDLTests.Qualcomm.EmergencyDownload.Firehose.Xml;
using System.Text;
using System.Xml.Serialization;
using System.Xml;

namespace EDLTests.Qualcomm.EmergencyDownload.Firehose
{
    internal static class QualcommFirehoseCommands
    {
        public static byte[] Read(this QualcommFirehose Firehose, StorageType storageType, uint LUNi, uint sectorSize, uint FirstSector, uint LastSector)
        {
            Console.WriteLine("Read");

            string Command03 = QualcommFirehoseXml.BuildCommandPacket([
                new Data()
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
                }
            ]);

            Firehose.Serial.SendData(Encoding.UTF8.GetBytes(Command03));

            bool RawMode = false;
            bool GotResponse = false;

            while (!GotResponse)
            {
                Data[] datas = Firehose.GetFirehoseResponseDataPayloads(true);

                foreach (Data data in datas)
                {
                    if (data.Log != null)
                    {
                        Console.WriteLine("DEVPRG LOG: " + data.Log.Value);
                    }
                    else if (data.Response != null)
                    {
                        if (data.Response.RawMode)
                        {
                            RawMode = true;
                        }

                        GotResponse = true;
                    }
                    else
                    {
                        XmlSerializer xmlSerializer = new(typeof(Data));

                        using StringWriter sww = new();
                        using XmlWriter writer = XmlWriter.Create(sww);

                        xmlSerializer.Serialize(writer, data);

                        Console.WriteLine(sww.ToString());
                    }
                }
            }

            if (!RawMode)
            {
                Console.WriteLine("Error: Raw mode not enabled");
                return null;
            }

            byte[] readBuffer = Firehose.Serial.GetResponse(null, Length: (int)(LastSector - FirstSector + 1) * 4096);

            RawMode = false;
            GotResponse = false;

            while (!GotResponse)
            {
                Data[] datas = Firehose.GetFirehoseResponseDataPayloads();

                foreach (Data data in datas)
                {
                    if (data.Log != null)
                    {
                        Console.WriteLine("DEVPRG LOG: " + data.Log.Value);
                    }
                    else if (data.Response != null)
                    {
                        if (data.Response.RawMode)
                        {
                            RawMode = true;
                        }

                        GotResponse = true;
                    }
                    else
                    {
                        XmlSerializer xmlSerializer = new(typeof(Data));

                        using StringWriter sww = new();
                        using XmlWriter writer = XmlWriter.Create(sww);

                        xmlSerializer.Serialize(writer, data);

                        Console.WriteLine(sww.ToString());
                    }
                }
            }

            return readBuffer;
        }

        public static bool Reset(this QualcommFirehose Firehose, PowerValue powerValue = PowerValue.Reset, uint delayInSeconds = 100)
        {
            Console.WriteLine("Rebooting phone");

            string Command03 = QualcommFirehoseXml.BuildCommandPacket([
                new Data()
                {
                    Power = new Power()
                    {
                        Value = powerValue,
                        DelayInSeconds = delayInSeconds
                    }
                }
            ]);

            Firehose.Serial.SendData(Encoding.UTF8.GetBytes(Command03));

            bool RawMode = false;
            bool GotResponse = false;

            while (!GotResponse)
            {
                Data[] datas = Firehose.GetFirehoseResponseDataPayloads();

                foreach (Data data in datas)
                {
                    if (data.Log != null)
                    {
                        Console.WriteLine("DEVPRG LOG: " + data.Log.Value);
                    }
                    else if (data.Response != null)
                    {
                        if (data.Response.RawMode)
                        {
                            RawMode = true;
                        }

                        GotResponse = true;
                    }
                    else
                    {
                        XmlSerializer xmlSerializer = new(typeof(Data));

                        using StringWriter sww = new();
                        using XmlWriter writer = XmlWriter.Create(sww);

                        xmlSerializer.Serialize(writer, data);

                        Console.WriteLine(sww.ToString());
                    }
                }
            }

            // Workaround for problem
            // SerialPort is sometimes not disposed correctly when the device is already removed.
            // So explicitly dispose here
            Firehose.Serial.Close();

            return true;
        }

        public static bool GetStorageInfo(this QualcommFirehose Firehose, StorageType storageType = StorageType.UFS)
        {
            Console.WriteLine("Getting Storage Info");

            string Command03 = QualcommFirehoseXml.BuildCommandPacket([
                new Data()
                {
                    GetStorageInfo = new GetStorageInfo()
                    {
                        //PhysicalPartitionNumber = 0,
                        StorageType = storageType,
                        //Slot = 0
                    }
                }
            ]);

            Firehose.Serial.SendData(Encoding.UTF8.GetBytes(Command03));

            bool RawMode = false;
            bool GotResponse = false;

            while (!GotResponse)
            {
                Data[] datas = Firehose.GetFirehoseResponseDataPayloads();

                foreach (Data data in datas)
                {
                    if (data.Log != null)
                    {
                        Console.WriteLine("DEVPRG LOG: " + data.Log.Value);
                    }
                    else if (data.Response != null)
                    {
                        if (data.Response.RawMode)
                        {
                            RawMode = true;
                        }

                        GotResponse = true;
                    }
                    else
                    {
                        XmlSerializer xmlSerializer = new(typeof(Data));

                        using StringWriter sww = new();
                        using XmlWriter writer = XmlWriter.Create(sww);

                        xmlSerializer.Serialize(writer, data);

                        Console.WriteLine(sww.ToString());
                    }
                }
            }

            return true;
        }
    }
}