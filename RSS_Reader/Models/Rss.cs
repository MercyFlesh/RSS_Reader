using System.Xml.Serialization;

namespace RSS_Reader.Models
{
    [XmlRoot("rss")]
    public class Rss
    {
        [XmlElement("channel")]
        public Channel Channel { get; init; } = null!;
    }
}
