using RSS_Reader.Models;

namespace RSS_Reader.Services
{
    public interface IRssParserService
    {
        Task<Rss> ParseRss(string rssUrl);
    }
}
