using System;
using WebScraper.Data;
using System.Net;

namespace WebScraper.Workers
{
    class WebDownloader
    {
        private const string _method = "search";
        private static string _link;

        public string DownloadContentFrom(WebData webData)
        {
            using (WebClient client = new WebClient())
            {
                _link = $"https://{webData.City.Replace(" ", String.Empty)}.craigslist.org/{_method}/{webData.Category}";
                
                var downloadedContent = client.DownloadString(_link);
                return downloadedContent;
            }
        }
    }
}
