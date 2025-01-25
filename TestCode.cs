using EDLTests.Qualcomm.EmergencyDownload.Firehose;
using EDLTests.Qualcomm.EmergencyDownload.Sahara;
using EDLTests.Qualcomm.EmergencyDownload.Transport;
using System.Globalization;
using System.Xml.Serialization;
using System.Xml;

namespace EDLTests
{
    internal class TestCode
    {

        internal static byte[] ParseAsHexFile(string FilePath)
        {
            byte[] Result = null;

            try
            {
                string[] Lines = File.ReadAllLines(FilePath);
                byte[] Buffer = null;
                int BufferSize = 0;

                foreach (string Line in Lines)
                {
                    if (string.IsNullOrEmpty(Line))
                    {
                        continue;
                    }

                    if (Line[0] != ':')
                    {
                        throw new BadImageFormatException();
                    }

                    byte[] LineBytes = Converter.ConvertStringToHex(Line[1..]);

                    if ((LineBytes[0] + 5) != LineBytes.Length)
                    {
                        throw new BadImageFormatException();
                    }

                    if (Buffer == null)
                    {
                        Buffer = new byte[0x40000];
                    }

                    if (LineBytes[3] == 0) // This is mem data
                    {
                        System.Buffer.BlockCopy(LineBytes, 4, Buffer, BufferSize, LineBytes[0]);
                        BufferSize += LineBytes[0];
                    }
                }

                Result = new byte[BufferSize];
                System.Buffer.BlockCopy(Buffer, 0, Result, 0, BufferSize);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return Result;
        }

        public static bool ConnectToProgrammer(QualcommSerial Serial, QualcommFirehose Firehose, byte[] PacketFromPcToProgrammer)
        {
            // Behaviour of old firehose:
            // Takes about 20 ms to be started.
            // Then PC has to start talking to the phone.
            // Behaviour of new firehose:
            // After 2000 ms the firehose starts talking to the PC
            //
            // For the duration of 2.5 seconds we will send Hello packages
            // And also wait for incoming messages
            // An incoming message can be a response to our outgoing Hello packet (read incoming until "response value")
            // Or it can be an incoming Hello-packet from the programmer (always 2 packets, starting with "Chip serial num")
            // Sending the hello-packet can succeed immediately, or it can timeout.
            // When sending succeeds, an answer should be incoming immediately to complete the handshake.
            // When an incoming Hello was received, the phone still expects to receive another Hello.

            int HelloSendCount = 0;
            bool HandshakeCompleted = false;
            do
            {
                Serial.SetTimeOut(200);
                HelloSendCount++;
                try
                {
                    Console.WriteLine($"Send Hello to programmer ({HelloSendCount})");
                    Serial.SendData(PacketFromPcToProgrammer);
                    Console.WriteLine("Hello packet accepted");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An unexpected error happened");
                    Console.WriteLine(ex.GetType().ToString());
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(ex.StackTrace);

                    Console.WriteLine("Hello packet not accepted");
                }

                try
                {
                    Serial.SetTimeOut(500);
                    QualcommFirehoseXmlElements.Data[] datas = Firehose.GetFirehoseResponseDataPayloads();

                    foreach (QualcommFirehoseXmlElements.Data data in datas)
                    {
                        if (data.Log != null)
                        {
                            Console.WriteLine("DEVPRG LOG: " + data.Log.Value);
                        }
                        else if (data.Response != null)
                        {

                        }
                        else
                        {
                            XmlSerializer xmlSerializer = new(typeof(QualcommFirehoseXmlElements.Data));

                            using StringWriter sww = new();
                            using XmlWriter writer = XmlWriter.Create(sww);

                            xmlSerializer.Serialize(writer, data);

                            Console.WriteLine(sww.ToString());
                        }
                    }

                    Serial.SetTimeOut(200);
                    if (datas.Any(x => x.Log?.Value?.Contains("Chip serial num") == true))
                    {
                        datas = Firehose.GetFirehoseResponseDataPayloads();

                        foreach (QualcommFirehoseXmlElements.Data data in datas)
                        {
                            if (data.Log != null)
                            {
                                Console.WriteLine("DEVPRG LOG: " + data.Log.Value);
                            }
                            else if (data.Response != null)
                            {

                            }
                            else
                            {
                                XmlSerializer xmlSerializer = new(typeof(QualcommFirehoseXmlElements.Data));

                                using StringWriter sww = new();
                                using XmlWriter writer = XmlWriter.Create(sww);

                                xmlSerializer.Serialize(writer, data);

                                Console.WriteLine(sww.ToString());
                            }
                        }

                        Console.WriteLine("Incoming Hello-packets received");
                    }

                    while (!datas.Any(x => x.Response != null))
                    {
                        datas = Firehose.GetFirehoseResponseDataPayloads();

                        foreach (QualcommFirehoseXmlElements.Data data in datas)
                        {
                            if (data.Log != null)
                            {
                                Console.WriteLine("DEVPRG LOG: " + data.Log.Value);
                            }
                            else if (data.Response != null)
                            {

                            }
                            else
                            {
                                XmlSerializer xmlSerializer = new(typeof(QualcommFirehoseXmlElements.Data));

                                using StringWriter sww = new();
                                using XmlWriter writer = XmlWriter.Create(sww);

                                xmlSerializer.Serialize(writer, data);

                                Console.WriteLine(sww.ToString());
                            }
                        }
                    }

                    Console.WriteLine("Incoming Hello-response received");

                    if (!datas.Any(x => x.Log?.Value?.Contains("Failed to authenticate Digital Signature.") == true))
                    {
                        HandshakeCompleted = true;
                    }
                    else
                    {
                        Console.WriteLine("Programmer failed to authenticate Digital Signature");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
            while (!HandshakeCompleted && (HelloSendCount < 6));

            return HandshakeCompleted;
        }

        public static bool ConnectToProgrammerInTestMode(QualcommSerial Serial, QualcommFirehose Firehose)
        {
            byte[] HelloPacketFromPcToProgrammer = new byte[0x20C];
            ByteOperations.WriteUInt32(HelloPacketFromPcToProgrammer, 0, 0x57503730);     // WP70
            ByteOperations.WriteUInt32(HelloPacketFromPcToProgrammer, 0x28, 0x57503730);  // WP70
            ByteOperations.WriteUInt32(HelloPacketFromPcToProgrammer, 0x208, 0x57503730); // WP70
            ByteOperations.WriteUInt16(HelloPacketFromPcToProgrammer, 0x48, 0x4445);      // DE

            bool HandshakeCompleted = ConnectToProgrammer(Serial, Firehose, HelloPacketFromPcToProgrammer);

            if (HandshakeCompleted)
            {
                Console.WriteLine("Handshake completed with programmer in testmode");
            }
            else
            {
                Console.WriteLine("Handshake with programmer failed");
            }

            return HandshakeCompleted;
        }

        internal static void ParseHWID(byte[] HWID)
        {
            string HWIDStr = Convert.ToHexString(HWID);

            int MSMID = int.Parse(HWIDStr.Substring(2, 6), NumberStyles.HexNumber);
            int OEMID = int.Parse(HWIDStr.Substring(HWIDStr.Length - 8, 4), NumberStyles.HexNumber);
            int ModelID = int.Parse(HWIDStr.Substring(HWIDStr.Length - 4, 4), NumberStyles.HexNumber);

            int ManufacturerID = MSMID & 0xFFF;
            int ProductID = (MSMID >> 12) & 0xFFFF;
            int DieRevision = (MSMID >> 28) & 0xF;

            if (ManufacturerID == 0x0E1)
            {
                Console.WriteLine($"Manufacturer ID: {ManufacturerID:X3} (Qualcomm)");
            }
            else
            {
                Console.WriteLine($"Manufacturer ID: {ManufacturerID:X3} (Unknown)");
            }

            if (Enum.IsDefined(typeof(QualcommPartNumbers), ProductID))
            {
                Console.WriteLine($"Product ID: {ProductID:X4} ({(QualcommPartNumbers)ProductID})");
            }
            else
            {
                Console.WriteLine($"Product ID: {ProductID:X4} (Unknown)");
            }

            Console.WriteLine($"Die Revision: {DieRevision:X1}");
            Console.WriteLine($"OEM: {OEMID:X4}");
            Console.WriteLine($"Model: {ModelID:X4}");
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
                ParseHWID(HWID);

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

                    //ConnectToProgrammerInTestMode(Serial, Firehose);

                    bool RawMode = false;
                    bool GotResponse = false;

                    try
                    {
                        while (!GotResponse)
                        {
                            QualcommFirehoseXmlElements.Data[] datas = Firehose.GetFirehoseResponseDataPayloads();

                            foreach (QualcommFirehoseXmlElements.Data data in datas)
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
                                    XmlSerializer xmlSerializer = new(typeof(QualcommFirehoseXmlElements.Data));

                                    using StringWriter sww = new();
                                    using XmlWriter writer = XmlWriter.Create(sww);

                                    xmlSerializer.Serialize(writer, data);

                                    Console.WriteLine(sww.ToString());
                                }
                            }
                        }
                    }
                    catch (BadConnectionException e) { }

                    Firehose.GetStorageInfo();

                    byte[] GPTLUN0 = Firehose.Read();
                    if (GPTLUN0 != null)
                    {
                        File.WriteAllBytes(@"C:\Users\gus33\Documents\GPT_LUN0_TESTING.bin", GPTLUN0);
                    }

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