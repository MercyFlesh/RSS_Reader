using System.Xml.Serialization;

namespace RSS_Reader.Models
{
    public class Item
    {
        [XmlElement("title")]
        public string Title { get; set; } = null!;

        [XmlElement("link")]
        public string Link { get; set; } = null!;

        [XmlElement("description")]
        public string Description { get; set; } = null!;

        [XmlElement("pubDate")]
        public string? PubDate { get; set; }

        [XmlElement("category")]
        public List<string>? Category { get; set; }
    }
}
