using System.IO;
using System.Text;
using System.Xml.Serialization;
using NeuronalNetworks.Networks;

namespace NeuronalNetworks.Common
{
    public class NeuronalNetworkSerializer
    {
        public static Network DeserializeFromXmlFile(string path)
        {
            var deserializer = new XmlSerializer(typeof(Network));
            using(TextReader textReader = new StreamReader(path))
            {
                var network = (Network) deserializer.Deserialize(textReader);
                textReader.Close();
                return network;
            }
        }

        public static Network DeserializeFromXml(string content)
        {
            var deserializer = new XmlSerializer(typeof(Network));
            using (var memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(content)))
            {
                var network = (Network)deserializer.Deserialize(memoryStream);
                return network;
            }
        }

        public static void SerializeToXml(Network network, string path)
        {
            var serializer = new XmlSerializer(typeof(Network));
            using (TextWriter textWriter = new StreamWriter(path))
            {
                serializer.Serialize(textWriter, network);
                textWriter.Close();
            }
        }
    }
}
