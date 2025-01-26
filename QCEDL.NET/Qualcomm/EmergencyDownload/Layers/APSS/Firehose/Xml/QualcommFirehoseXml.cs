using System.Xml.Serialization;
using System.Xml;
using QCEDL.NET.Qualcomm.EmergencyDownload.Layers.APSS.Firehose.Xml.Elements;

namespace QCEDL.NET.Qualcomm.EmergencyDownload.Layers.APSS.Firehose.Xml
{
    internal class QualcommFirehoseXml
    {
        public static string BuildCommandPacket(Data[] dataPayloads)
        {
            XmlSerializer xmlSerializer = new(typeof(Data), new XmlRootAttribute("data"));

            XmlWriterSettings settings = new()
            {
                Indent = false,
                OmitXmlDeclaration = true
            };

            XmlSerializerNamespaces ns = new([XmlQualifiedName.Empty]);

            List<string> dataStrElements = [];

            foreach (Data data in dataPayloads)
            {
                using StringWriter sww = new();
                using XmlWriter writer = XmlWriter.Create(sww, settings);

                xmlSerializer.Serialize(writer, data, ns);

                dataStrElements.Add(sww.ToString());
            }

            return "<?xml version=\"1.0\" ?>" + string.Join("", dataStrElements);
        }

        public static Data[] GetDataPayloads(string commandPacket)
        {
            /*ConsoleColor original = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(commandPacket);
            Console.ForegroundColor = original;*/

            commandPacket = commandPacket.Replace("<?xml version=\"1.0\" encoding=\"UTF-8\" ?>", "");
            commandPacket = $"<dataArray>{commandPacket}</dataArray>";
            commandPacket = commandPacket.Replace((char)0x14, ' ');

            XmlSerializer xmlSerializer = new(typeof(DataArray), new XmlRootAttribute("dataArray"));

            XmlReaderSettings settings = new()
            {
                CheckCharacters = false
            };

            using XmlReader reader = XmlReader.Create(new StringReader(commandPacket), settings);
            DataArray data = xmlSerializer.Deserialize(reader) as DataArray;

            return data.Data;
        }
    }
}
