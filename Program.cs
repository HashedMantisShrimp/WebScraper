using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using WebScraper.Builders;
using WebScraper.Data;
using WebScraper.Workers;

namespace WebScraper//TODO:
{                   //Clean up the code
    class Program
    {
        private static string Content = string.Empty;
        private static List<string> Categories;

        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Please enter the city you would like to scrape information from:");
                var craigsListCity = Console.ReadLine() ?? String.Empty;
                
                WebData webData = new WebDataBuilder()
                    .WithCity(craigsListCity)
                    .Build();

                WebDownloader downloadContent = new WebDownloader();

                Content = downloadContent.DownloadContentFrom(webData);

                CategoryScraper scrapeCategory = new CategoryScraper();
                Categories = scrapeCategory.GetCategoryFrom(Content);

                var userCategory="sss";

                if (Categories.Any())
                {
                    int x = Categories.Count;
                    for ( int c=0; c<x ;c+=2) {
                        Console.WriteLine("Category: {0}, Value: {1}", Categories[c+1], Categories[c]);
                        Console.WriteLine();
                    }

                    Console.Write("Please enter the \"Value\" of the category you'd like to scrape elements from:");
                    userCategory = Console.ReadLine() ?? String.Empty;
                }
                else
                {
                    Console.WriteLine("There were no elements found in the cetgory list.");
                    Console.Write("A default category will be chosen for you.");
                }
                
                webData = new WebDataBuilder()
                    .WithCity(craigsListCity)
                    .WithCategory(userCategory)
                    .Build();

                Content = downloadContent.DownloadContentFrom(webData);

                //Need to check for errors on userCategory

                // https://boston.craigslist.org/search //link example for city only
                // https://boston.craigslist.org/search/cta //link example w/ category

                ScrapeCriteria scrapeCriteria = new ScrapeCriteriaBuilder() 
                    .WithData(Content)
                    .WithRegex(@"<a href=""(.*?)"" data-id=""(.*?)"" class=""(.*?)"">(.*?)</a>")  //this regex pattern works
                    .WithRegexOption(RegexOptions.ExplicitCapture)
                    .WithParts(new ScrapeCriteriaPartBuilder()
                        .WithRegex(@">(.*?)<")
                        .WithRegexOption(RegexOptions.Singleline)
                        .Build())
                    .WithParts(new ScrapeCriteriaPartBuilder()
                        .WithRegex(@"href=""(.*?)""")
                        .WithRegexOption(RegexOptions.Singleline)
                        .Build())
                    .Build();

                Scraper scraper = new Scraper();

                var scrapedElements = scraper.Scrape(scrapeCriteria);

                if (scrapedElements.Any())
                {
                    int count = 1;
                    foreach (var scrapedElement in scrapedElements)
                    {
                        Console.WriteLine(scrapedElement);

                        if (count % 2 == 0) Console.WriteLine();

                        count++;
                    }
                }
                else
                {
                    Console.WriteLine("There were no matches found for the specified scrape Criteria.");
                }
            }
            catch (Exception ex) { Console.WriteLine("There was an error found: {0}", ex.Message); }

            Console.WriteLine();
            Console.WriteLine("The program will close shortly, please acknowledge by pressing any key.");
            Console.ReadKey();
        }
    }
}
