using System.Xml.Serialization;

namespace RSS_Reader.Models
{

    public class Channel
    {
        [XmlElement("title")]
        public string Title { get; init; } = null!;

        [XmlElement("link")]
        public string Link { get; init; } = null!;

        [XmlElement("description")]
        public string Description { get; init; } = null!;

        [XmlElement("language")]
        public string? Language { get; init; }

        [XmlElement("managingEditor ")]
        public string? ManagingEditor { get; init; }

        [XmlElement("pubDate")]
        public string? PubDate { get; init; }

        [XmlElement("image")]
        public Image Image { get; init; } = null!;

        [XmlElement("item")]
        public List<Item> Items { get; init; } = null!;
    }
}
