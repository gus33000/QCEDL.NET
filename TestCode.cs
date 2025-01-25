using EDLTests.Qualcomm.EmergencyDownload.Firehose;
using EDLTests.Qualcomm.EmergencyDownload.Sahara;
using EDLTests.Qualcomm.EmergencyDownload.Transport;
using EDLTests.Qualcomm.EmergencyDownload.ChipInfo;
using System.Xml.Serialization;
using System.Xml;
using System.Text;
using EDLTests.Qualcomm.EmergencyDownload.Firehose.Xml.Elements;

namespace EDLTests
{
    internal class TestCode
    {
        internal static void ReadGPTs(QualcommFirehose Firehose)
        {
            for (int i = 0; i < 6; i++)
            {
                byte[] GPTLUN = Firehose.Read(StorageType.UFS, (uint)i, 4096, 0, 5);
                if (GPTLUN != null)
                {
                    using MemoryStream stream = new(GPTLUN);
                    PartitionTable.GPT GPT = PartitionTable.GPT.ReadFromStream(stream, 4096);

                    Console.WriteLine($"LUN {i}:");
                    PrintGPTPartitions(GPT);
                }
            }
        }

        internal static void PrintGPTPartitions(PartitionTable.GPT GPT)
        {
            foreach (PartitionTable.GPTPartition partition in GPT.Partitions)
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

                    Firehose.GetStorageInfo();
                    ReadGPTs(Firehose);

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