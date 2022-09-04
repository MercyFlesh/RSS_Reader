using System.Xml;
using System.Xml.Serialization;
using System.Linq;
using System.Xml.Linq;
using System.Net;
using RSS_Reader.Models;

namespace RSS_Reader.Services
{
    public class RssParserService : IRssParserService
    {
        readonly IConfiguration _config;
        private HttpClientHandler ClientHandler { get; set; }

        public RssParserService(IConfiguration config)
        {
            _config = config;
            ClientHandler = new HttpClientHandler()
            {
                Proxy = new WebProxy(new Uri(_config["proxy:uri"]))
                {
                    Address = new Uri(_config["proxy:uri"]),
                    BypassProxyOnLocal = false,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(
                        userName: _config["proxy:user"],
                        password: _config["proxy:pass"])
                },
                UseProxy = true,
            };
        }

        public async Task<Rss> ParseRss(string rssUrl)
        {
            var rssXmlString = await GetFeed(rssUrl);
            var rss = new Rss();
            if (rssXmlString is not null)
            {
                var serializer = new XmlSerializer(typeof(Rss));
                var xmlReader = new StringReader(rssXmlString);
                rss = serializer.Deserialize(xmlReader) as Rss;
            }

            return rss;
        }

        /*public Channel ParseFeed(string feed)
        {
            Channel channel = null!;

            var doc = XDocument.Parse(feed);
            if (doc.Element("rss")?.Element("channel") is { } channelNode)
            {
                channel = new Channel
                {
                    Title = channelNode.Element("title")!.Value,
                    Link = new Uri(channelNode.Element("link")!.Value),
                    Description = channelNode.Element("description")?.Value,
                    Language = channelNode.Element("language")?.Value,
                    ManagingEditor = channelNode.Element("managingEditor")?.Value
                };

                if (DateTime.TryParse(channelNode.Element("pubDate")?.Value,
                    out DateTime pubDate))
                    channel.PubDate = pubDate;
                
                if (channelNode.Element("image") is { } imageNode)
                {
                    var link = imageNode.Element("link")?.Value;
                    channel.Image = new Image 
                    { 
                        Url = new Uri(imageNode.Element("url")!.Value),
                        Link = link is not null ? new Uri(link) : null,
                        Title = imageNode.Element("title")?.Value
                    };  
                }

                channel.Items = channelNode.Elements("item")
                    .Select(item => 
                    {
                        var pubDate = item.Element("pubDate")?.Value;

                        return new Item
                        {
                            Title = item.Element("title")!.Value,
                            Link = new Uri(item.Element("link")!.Value),
                            Description = item.Element("description")!.Value,
                            PubDate = pubDate is not null ? DateTime.Parse(pubDate) : null,
                            Category = item.Elements("category")?.Select(categoryAtr => categoryAtr.Value).ToList()
                        };
                    }).ToList();
            }

            return channel;
        }*/

        public async Task<string> GetFeed(string rssUrl)
        {
            string feed = null!;

            using var client = new HttpClient();
            try
            {
                var response = await client.GetAsync(rssUrl);
                feed = await response.Content.ReadAsStringAsync();
            }
            catch(Exception)
            { }

            return feed;
        }
    }
}
