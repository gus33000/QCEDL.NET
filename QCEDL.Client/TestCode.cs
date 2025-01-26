using QCEDL.NET.Qualcomm.EmergencyDownload.Layers.APSS.Firehose;
using QCEDL.NET.Qualcomm.EmergencyDownload.Layers.APSS.Firehose.Xml.Elements;
using QCEDL.NET.Qualcomm.EmergencyDownload.Layers.PBL.Sahara;
using QCEDL.NET.Qualcomm.EmergencyDownload.Transport;
using QCEDL.NET.Qualcomm.EmergencyDownload.ChipInfo;
using System.Xml.Serialization;
using System.Xml;
using System.Text;
using QCEDL.NET.PartitionTable;

namespace QCEDL.Client
{
    internal class TestCode
    {
        internal static byte[] ReadGPTBuffer(QualcommFirehose Firehose, StorageType storageType, uint physicalPartition)
        {
            uint sectorSize = 4096;

            if (storageType == StorageType.NVME)
            {
                sectorSize = 512;
            }
            else if (storageType == StorageType.SDCC)
            {
                sectorSize = 512;
            }

            return Firehose.Read(storageType, physicalPartition, sectorSize, 0, 5);
        }

        internal static GPT ReadGPT(QualcommFirehose Firehose, StorageType storageType, uint physicalPartition)
        {
            uint sectorSize = 4096;

            if (storageType == StorageType.NVME)
            {
                sectorSize = 512;
            }
            else if (storageType == StorageType.SDCC)
            {
                sectorSize = 512;
            }

            byte[] GPTLUN = ReadGPTBuffer(Firehose, storageType, physicalPartition);

            if (GPTLUN == null)
            {
                return null;
            }
            
            using MemoryStream stream = new(GPTLUN);
            return GPT.ReadFromStream(stream, (int)sectorSize);
        }

        internal static void ReadGPTs(QualcommFirehose Firehose, StorageType storageType)
        {
            for (int i = 0; i < 10; i++)
            {
                GPT GPT = ReadGPT(Firehose, storageType, (uint)i);

                if (GPT == null)
                {
                    Console.WriteLine($"LUN {i}: No GPT found");
                    continue;
                }

                Console.WriteLine($"LUN {i}:");
                PrintGPTPartitions(GPT);
            }
        }

        internal static void PrintGPTPartitions(GPT GPT)
        {
            foreach (GPTPartition partition in GPT.Partitions)
            {
                Console.WriteLine($"Name: {Encoding.ASCII.GetString([.. partition.Name.Select(x => (byte)x)])}, Type: {partition.TypeGUID}, ID: {partition.UID}, StartLBA: {partition.FirstLBA}, EndLBA: {partition.LastLBA}");
            }
        }

        internal static async Task TestProgrammer(string DevicePath, string ProgrammerPath)
        {
            Console.WriteLine("START TestProgrammer");

            try
            {
                Console.WriteLine();
                Console.WriteLine("Starting Firehose Test");
                Console.WriteLine();

                // Send and start programmer
                QualcommSerial Serial = new(DevicePath);
                QualcommSahara Sahara = new(Serial);

                Sahara.CommandHandshake();

                byte[][] RKHs = Sahara.GetRKHs();
                byte[] SN = Sahara.GetSerialNumber();

                for (int i = 0; i < RKHs.Length; i++)
                {
                    byte[] RKH = RKHs[i];
                    string RKHAsString = Convert.ToHexString(RKH);
                    string FriendlyName = "Unknown";

                    foreach (KeyValuePair<string, string> element in KnownPKData.KnownOEMPKHashes)
                    {
                        if (element.Value == RKHAsString)
                        {
                            FriendlyName = element.Key;
                            break;
                        }
                    }

                    Console.WriteLine($"RKH[{i}]: {RKHAsString} ({FriendlyName})");
                }

                byte[] HWID = Sahara.GetHWID();
                HardwareID.ParseHWID(HWID);

                Console.WriteLine($"Serial Number: {Convert.ToHexString(SN)}");

                Sahara.SwitchMode(QualcommSaharaMode.ImageTXPending);

                Console.WriteLine();

                if (!await Sahara.LoadProgrammer(ProgrammerPath))
                {
                    Console.WriteLine("Emergency programmer test failed");
                }
                else
                {
                    Console.WriteLine();

                    QualcommFirehose Firehose = new(Serial);

                    //QualcommFirehoseMMOVIP.ConnectToProgrammerInTestMode(Serial, Firehose);

                    bool RawMode = false;
                    bool GotResponse = false;

                    try
                    {
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
                    }
                    catch (BadConnectionException) { }

                    Firehose.Configure(StorageType.SDCC);
                    Firehose.GetStorageInfo(StorageType.SDCC);
                    ReadGPTs(Firehose, StorageType.SDCC);

                    if (Firehose.Reset())
                    {
                        Console.WriteLine();
                        Console.WriteLine("Emergency programmer test succeeded");
                    }
                    else
                    {
                        Console.WriteLine();
                        Console.WriteLine("Emergency programmer test failed");
                    }
                }
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex);
            }
            finally
            {
                Console.WriteLine();
                Console.WriteLine("END TestProgrammer");
            }
        }
    }
}