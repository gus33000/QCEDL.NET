// Copyright (c) 2018, Rene Lergner - @Heathcliff74xda
//
// Permission is hereby granted, free of charge, to any person obtaining a
// copy of this software and associated documentation files (the "Software"),
// to deal in the Software without restriction, including without limitation
// the rights to use, copy, modify, merge, publish, distribute, sublicense,
// and/or sell copies of the Software, and to permit persons to whom the
// Software is furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
// DEALINGS IN THE SOFTWARE.

using EDLTests.Qualcomm.EmergencyDownload.Transport;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace EDLTests.Qualcomm.EmergencyDownload.Firehose
{
    internal class QualcommFirehose
    {
        private readonly QualcommSerial Serial;

        public QualcommFirehose(QualcommSerial Serial)
        {
            this.Serial = Serial;
        }

        public byte[] Read()
        {
            Console.WriteLine("Getting Storage Info");

            string Command03 = QualcommFirehoseXml.BuildCommandPacket([
                new QualcommFirehoseXmlElements.Data()
                {
                    Read = new QualcommFirehoseXmlElements.Read()
                    {
                        PhysicalPartitionNumber = 0,
                        StorageType = QualcommFirehoseXmlElements.StorageType.UFS,
                        Slot = 0,
                        SectorSizeInBytes = 4096,
                        StartSector = "0",
                        LastSector = 2,
                        NumPartitionSectors = "3"
                    }
                }
            ]);

            Serial.SendData(Encoding.UTF8.GetBytes(Command03));

            bool RawMode = false;
            string Incoming;
            int size = 0x64;
            
            do
            {
                byte[] ResponseBuffer = Serial.GetResponse(null, Length: size);
                Incoming = Encoding.UTF8.GetString(ResponseBuffer);

                size = 0x5D;

                Console.WriteLine("------------------------");

                QualcommFirehoseXmlElements.Data[] datas = QualcommFirehoseXml.GetDataPayloads(Incoming);

                foreach (QualcommFirehoseXmlElements.Data data in datas)
                {
                    if (data.Log != null)
                    {
                        Console.WriteLine(data.Log.Value);
                    }
                    else if (data.Response != null)
                    {
                        if (data.Response.RawMode)
                        {
                            RawMode = true;
                            break;
                        }
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

                Console.WriteLine("------------------------");
            }
            while (Incoming.IndexOf("response value") < 0 && RawMode == false);

            byte[] readBuffer = Serial.GetResponse(null, Length: 3 * 4096);

            Incoming = null;
            do
            {
                Incoming = Encoding.UTF8.GetString(Serial.GetResponse(null));

                Console.WriteLine("------------------------");

                QualcommFirehoseXmlElements.Data[] datas = QualcommFirehoseXml.GetDataPayloads(Incoming);

                foreach (QualcommFirehoseXmlElements.Data data in datas)
                {
                    if (data.Log != null)
                    {
                        Console.WriteLine(data.Log.Value);
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

                Console.WriteLine("------------------------");
            }
            while (Incoming.IndexOf("response value") < 0);

            return readBuffer;
        }

        public bool Reset()
        {
            Console.WriteLine("Rebooting phone");

            string Command03 = QualcommFirehoseXml.BuildCommandPacket([
                new QualcommFirehoseXmlElements.Data()
                {
                    Power = new QualcommFirehoseXmlElements.Power()
                    {
                        Value = QualcommFirehoseXmlElements.PowerValue.Reset
                    }
                }
            ]);

            Serial.SendData(Encoding.UTF8.GetBytes(Command03));

            string Incoming;
            do
            {
                Incoming = Encoding.UTF8.GetString(Serial.GetResponse(null));

                Console.WriteLine("------------------------");

                QualcommFirehoseXmlElements.Data[] datas = QualcommFirehoseXml.GetDataPayloads(Incoming);

                foreach (QualcommFirehoseXmlElements.Data data in datas)
                {
                    if (data.Log != null)
                    {
                        Console.WriteLine(data.Log.Value);
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

                Console.WriteLine("------------------------");
            }
            while (Incoming.IndexOf("response value") < 0);

            // Workaround for problem
            // SerialPort is sometimes not disposed correctly when the device is already removed.
            // So explicitly dispose here
            Serial.Close();

            return true;
        }

        public bool GetStorageInfo()
        {
            Console.WriteLine("Getting Storage Info");

            string Command03 = QualcommFirehoseXml.BuildCommandPacket([
                new QualcommFirehoseXmlElements.Data()
                {
                    GetStorageInfo = new QualcommFirehoseXmlElements.GetStorageInfo()
                    {
                        //PhysicalPartitionNumber = 0,
                        StorageType = QualcommFirehoseXmlElements.StorageType.UFS,
                        //Slot = 0
                    }
                }
            ]);

            Serial.SendData(Encoding.UTF8.GetBytes(Command03));

            string Incoming;
            do
            {
                Incoming = Encoding.UTF8.GetString(Serial.GetResponse(null));

                Console.WriteLine("------------------------");

                QualcommFirehoseXmlElements.Data[] datas = QualcommFirehoseXml.GetDataPayloads(Incoming);

                foreach (QualcommFirehoseXmlElements.Data data in datas)
                {
                    if (data.Log != null)
                    {
                        Console.WriteLine(data.Log.Value);
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

                Console.WriteLine("------------------------");
            }
            while (Incoming.IndexOf("response value") < 0);

            return true;
        }
    }
}