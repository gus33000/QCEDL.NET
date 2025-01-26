using System.Text;
using System.Xml.Serialization;
using System.Xml;
using QCEDL.NET.Qualcomm.EmergencyDownload.Layers.APSS.Firehose.Xml;
using QCEDL.NET.Qualcomm.EmergencyDownload.Layers.APSS.Firehose.Xml.Elements;

namespace QCEDL.NET.Qualcomm.EmergencyDownload.Layers.APSS.Firehose
{
    public static class QualcommFirehoseCommands
    {
        public static bool Configure(this QualcommFirehose Firehose, StorageType storageType)
        {
            Console.WriteLine("Configuring");

            string Command03 = QualcommFirehoseXml.BuildCommandPacket([
                QualcommFirehoseXmlPackets.GetConfigurePacket(storageType, true, 1048576, false, 8192, true, false)
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

        public static byte[] Read(this QualcommFirehose Firehose, StorageType storageType, uint LUNi, uint sectorSize, uint FirstSector, uint LastSector)
        {
            Console.WriteLine("Read");

            string Command03 = QualcommFirehoseXml.BuildCommandPacket([
                QualcommFirehoseXmlPackets.GetReadPacket(storageType, LUNi, sectorSize, FirstSector, LastSector)
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

        public static bool Reset(this QualcommFirehose Firehose, PowerValue powerValue = PowerValue.Reset, uint delayInSeconds = 1)
        {
            Console.WriteLine("Rebooting phone");

            string Command03 = QualcommFirehoseXml.BuildCommandPacket([
                QualcommFirehoseXmlPackets.GetPowerPacket(powerValue, delayInSeconds)
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
                QualcommFirehoseXmlPackets.GetStorageInfoPacket(storageType)
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