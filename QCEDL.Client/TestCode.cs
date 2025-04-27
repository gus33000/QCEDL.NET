using Qualcomm.EmergencyDownload.Layers.APSS.Firehose;
using Qualcomm.EmergencyDownload.Layers.APSS.Firehose.Xml.Elements;
using Qualcomm.EmergencyDownload.Layers.PBL.Sahara;
using Qualcomm.EmergencyDownload.Transport;
using Qualcomm.EmergencyDownload.ChipInfo;
using System.Xml.Serialization;
using System.Xml;
using System.Text;
using QCEDL.NET.PartitionTable;

using DiscUtils;
using DiscUtils.Containers;
using DiscUtils.Streams;
using System.Diagnostics;

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

            // Read 6 sectors
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
                GPT GPT = null;

                try
                {
                    GPT = ReadGPT(Firehose, storageType, (uint)i);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }

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

                try
                {
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
                        return;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }

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
                                Debug.WriteLine("DEVPRG LOG: " + data.Log.Value);
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

                Firehose.Configure(StorageType.UFS);
                Firehose.GetStorageInfo(StorageType.UFS);
                ReadGPTs(Firehose, StorageType.UFS);

                DumpUFSDevice(Firehose);

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

        public static void DumpUFSDevice(QualcommFirehose Firehose)
        {
            List<Qualcomm.EmergencyDownload.Layers.APSS.Firehose.JSON.StorageInfo.Root> luStorageInfos = [];

            // Figure out the number of LUNs first.
            Qualcomm.EmergencyDownload.Layers.APSS.Firehose.JSON.StorageInfo.Root? mainInfo = Firehose.GetStorageInfo(StorageType.UFS);
            if (mainInfo != null)
            {
                int totalLuns = mainInfo.storage_info.num_physical;

                // Now figure out the size of each lun
                for (int i = 0; i < totalLuns; i++)
                {
                    Qualcomm.EmergencyDownload.Layers.APSS.Firehose.JSON.StorageInfo.Root? luInfo = Firehose.GetStorageInfo(StorageType.UFS, (uint)i);
                    if (luInfo == null)
                    {
                        throw new Exception("Error in reading LUN " + i + " for storage info!");
                    }
                    luStorageInfos.Add(luInfo);
                }
            }

            for (int i = 0; i < luStorageInfos.Count; i++)
            {
                Qualcomm.EmergencyDownload.Layers.APSS.Firehose.JSON.StorageInfo.Root storageInfo = luStorageInfos[i];

                Console.WriteLine($"LUN[{i}] Name: {storageInfo.storage_info.prod_name}");
                Console.WriteLine($"LUN[{i}] Total Blocks: {storageInfo.storage_info.total_blocks}");
                Console.WriteLine($"LUN[{i}] Block Size: {storageInfo.storage_info.block_size}");
                Console.WriteLine();

                LUNStream test = new LUNStream(Firehose, i, StorageType.UFS);
                ConvertDD2VHD($"D:\\HDK8350_00727\\LUN{i}.vhdx", 4096, test);
            }
        }

        /// <summary>
        ///     Coverts a raw DD image into a VHD file suitable for FFU imaging.
        /// </summary>
        /// <param name="ddfile">The path to the DD file.</param>
        /// <param name="vhdfile">The path to the output VHD file.</param>
        /// <returns></returns>
        public static void ConvertDD2VHD(string vhdfile, uint SectorSize, Stream inputStream)
        {
            SetupHelper.SetupContainers();

            using DiscUtils.Raw.Disk inDisk = new(inputStream, Ownership.Dispose);

            long diskCapacity = inputStream.Length;
            using Stream fs = new FileStream(vhdfile, FileMode.CreateNew, FileAccess.ReadWrite);
            using DiscUtils.Vhdx.Disk outDisk = DiscUtils.Vhdx.Disk.InitializeDynamic(fs, Ownership.None, diskCapacity, Geometry.FromCapacity(diskCapacity, (int)SectorSize));
            SparseStream contentStream = inDisk.Content;

            StreamPump pump = new()
            {
                InputStream = contentStream,
                OutputStream = outDisk.Content,
                SparseCopy = true,
                SparseChunkSize = (int)SectorSize,
                BufferSize = (int)SectorSize * 256 // Max 24 sectors at a time
            };

            long totalBytes = contentStream.Length;

            DateTime now = DateTime.Now;
            pump.ProgressEvent += (o, e) => { ShowProgress((ulong)e.BytesRead, (ulong)totalBytes, now); };

            Logging.Log("Converting RAW to VHDX");
            pump.Run();
            Console.WriteLine();
        }

        protected static void ShowProgress(ulong readBytes, ulong totalBytes, DateTime startTime)
        {
            DateTime now = DateTime.Now;
            TimeSpan timeSoFar = now - startTime;

            TimeSpan remaining =
                TimeSpan.FromMilliseconds(timeSoFar.TotalMilliseconds / readBytes * (totalBytes - readBytes));

            double speed = Math.Round(readBytes / 1024L / 1024L / timeSoFar.TotalSeconds);

            Logging.Log(
                $"{Logging.GetDISMLikeProgressBar((uint)(readBytes * 100 / totalBytes))} {speed}MB/s {remaining:hh\\:mm\\:ss\\.f}",
                returnLine: false);
        }
    }
}