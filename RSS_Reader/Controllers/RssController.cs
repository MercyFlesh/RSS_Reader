using Microsoft.AspNetCore.Mvc;
using RSS_Reader.Services;
using System.Text.Json;
using RSS_Reader.Models;

namespace RSS_Reader.Controllers
{
    public class RssController : Controller
    {
        IRssParserService _rssParser;
        IConfiguration _config;

        public RssController(IConfiguration config, IRssParserService rssParser)
        {
            _rssParser = rssParser;
            _config = config;
        }

        public async Task<ActionResult> Index()
        {
            Rss rss = null;
            if (Request.Cookies.Count == 0)
            {
                rss = await _rssParser.ParseRss(_config["feeds:feed:url"]);
                if (rss is not null)
                {
                    var feedSettings = new List<FeedSettings>
                    {
                        new FeedSettings { 
                            Url=_config["feeds:feed:url"], 
                            SchedulerTime=Convert.ToInt32(_config["feeds:feed:updateTimeMs"]) 
                        },
                    };

                    Response.Cookies.Append("feeds", JsonSerializer.Serialize(feedSettings)); 
                }
            }
            else
            {
                var feedSettings = JsonSerializer.Deserialize<List<FeedSettings>>(Request.Cookies["feeds"]);
                rss = await _rssParser.ParseRss(feedSettings[0].Url);
            }

            return View(rss);
        }

        [HttpGet]
        public async Task<ActionResult> UpdateFeed(string feed)
        {
            var rss = await _rssParser.ParseRss(feed);
            return PartialView("_Items", rss?.Channel?.Items);
        }
    }
}
