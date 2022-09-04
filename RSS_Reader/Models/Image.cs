using System.Xml.Serialization;

namespace RSS_Reader.Models
{
    public class Image
    {
        [XmlElement("link")]
        public string? Link { get; set; }

        [XmlElement("url")]
        public string Url { get; set; } = null!;

        [XmlElement("title")]
        public string? Title { get; set; }
    }
}
