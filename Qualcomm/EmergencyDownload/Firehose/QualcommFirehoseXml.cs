using System.Xml.Serialization;
using System.Xml;

namespace EDLTests.Qualcomm.EmergencyDownload.Firehose
{
    internal class QualcommFirehoseXml
    {
        public static string BuildCommandPacket(QualcommFirehoseXmlElements.Data[] dataPayloads)
        {
            XmlSerializer xmlSerializer = new(typeof(QualcommFirehoseXmlElements.Data), new XmlRootAttribute("data"));

            XmlWriterSettings settings = new()
            {
                Indent = false,
                OmitXmlDeclaration = true
            };

            XmlSerializerNamespaces ns = new([XmlQualifiedName.Empty]);

            List<string> dataStrElements = [];

            foreach (QualcommFirehoseXmlElements.Data data in dataPayloads)
            {
                using StringWriter sww = new();
                using XmlWriter writer = XmlWriter.Create(sww, settings);

                xmlSerializer.Serialize(writer, data, ns);

                dataStrElements.Add(sww.ToString());
            }

            return "<?xml version=\"1.0\" ?>" + string.Join("", dataStrElements);
        }

        public static QualcommFirehoseXmlElements.Data[] GetDataPayloads(string commandPacket)
        {
            /*ConsoleColor original = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(commandPacket);
            Console.ForegroundColor = original;*/

            commandPacket = commandPacket.Replace("<?xml version=\"1.0\" encoding=\"UTF-8\" ?>", "");
            commandPacket = $"<dataArray>{commandPacket}</dataArray>";
            commandPacket = commandPacket.Replace((char)0x14, ' ');

            XmlSerializer xmlSerializer = new(typeof(QualcommFirehoseXmlElements.DataArray), new XmlRootAttribute("dataArray"));

            XmlReaderSettings settings = new()
            {
                CheckCharacters = false
            };

            using XmlReader reader = XmlReader.Create(new StringReader(commandPacket), settings);
            QualcommFirehoseXmlElements.DataArray data = xmlSerializer.Deserialize(reader) as QualcommFirehoseXmlElements.DataArray;

            return data.Data;
        }
    }
}
