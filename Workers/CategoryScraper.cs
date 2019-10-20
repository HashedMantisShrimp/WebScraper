using System;
using System.Collections.Generic;
using WebScraper.Builders;
using WebScraper.Data;
using System.Text.RegularExpressions;

namespace WebScraper.Workers
{
    class CategoryScraper //TODO: clean up the code
    {
        private List<String> CategoriesFound;
        public List<string> GetCategoryFrom(string webPage)
        {
            
            try
            {
                //<select id="subcatAbb" class="js-only"> (options are here) </select> // location of the categories
                //<option value="baa">baby+kids</option> // options syntax

                //<option value="(.*?)">(.*?)</option> // found results for this on atom
                //<select id="(.*?)" class="(.*?)"> // there is a match for this on atom, need to figure out how to target rest
                //<select id="(.*?)" class="(.*?)">(.*)</select> //this works in atom but all content must be in a single line

                // <select id="subcatAbb" class="(.*)">((\s*)(.*)){0,46}(\s*)</select> //this returns the selects + options

                ScrapeCriteria categoryCriteria = new ScrapeCriteriaBuilder()
                    .WithData(webPage)
                    .WithRegex(@"<select id=""subcatAbb"" class=""(.*?)"">((\s*)(.*)){0,46}(\s*)</select>")
                    .WithRegexOption(RegexOptions.ExplicitCapture)
                    .Build();

                Scraper scrapeCategory = new Scraper();
                var categoriesHTML = scrapeCategory.Scrape(categoryCriteria);

                ScrapeCriteria categoryValueAndName = new ScrapeCriteriaBuilder()
                   .WithData(categoriesHTML[0].ToString())
                   .WithRegex(@"<option value=""(.*)"">(.*)</option>")
                   .WithRegexOption(RegexOptions.ExplicitCapture)
                   .WithParts(new ScrapeCriteriaPartBuilder()
                        .WithRegex(@"value=""(.*)""")
                        .WithRegexOption(RegexOptions.Singleline)
                        .Build())
                   .WithParts(new ScrapeCriteriaPartBuilder()
                        .WithRegex(@">(.*)<")
                        .WithRegexOption(RegexOptions.Singleline)
                        .Build())
                   .Build();

                CategoriesFound = scrapeCategory.Scrape(categoryValueAndName);
            }
            catch (Exception ex) { Console.WriteLine("There was an error while trying to get the category: {0}", ex.Message); }

            return CategoriesFound;
        }
       
        


    }
}
