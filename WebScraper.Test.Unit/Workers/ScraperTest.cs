using System.Collections.Generic;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebScraper.Builders;
using WebScraper.Data;
using WebScraper.Workers;

namespace WebScraper.Test.Unit
{
    [TestClass]
    public class ScraperTest
    { 
        private readonly Scraper _scraper = new Scraper();

        [TestMethod]
        public void ScraperTakesNoParts_ReturnsWholeAnchorTag()
        {
            string content = "Some data present here <a href=\"anysite.com\" class=\"anyClass\"> Content within tag </a>";

            ScrapeCriteria scrapeCriteria = new ScrapeCriteriaBuilder()
                .WithData(content)
                .WithRegex(@"<a href=""(.*?)"" class=""(.*?)""> (.*?) </a>")
                .WithRegexOption(RegexOptions.ExplicitCapture)
                .Build();

            var scrapedElement = _scraper.Scrape(scrapeCriteria);
            var expectedElement = "<a href=\"anysite.com\" class=\"anyClass\"> Content within tag </a>";

            Assert.IsTrue(scrapedElement.Count == 1);
            Assert.AreEqual(scrapedElement[0], expectedElement);

        }

        [TestMethod]
        public void ScraperTakesTwoParts_ReturnsLinkAndContentWithinTag()
        {
            string content = "Some data present here <a href=\"anysite.com\" class=\"anyClass\"> Content within tag </a>";

            ScrapeCriteria scrapeCriteria = new ScrapeCriteriaBuilder()
                .WithData(content)
                .WithRegex(@"<a href=""(.*?)"" class=""(.*?)""> (.*?) </a>")
                .WithRegexOption(RegexOptions.ExplicitCapture)
                .WithParts (new ScrapeCriteriaPartBuilder()
                    .WithRegex(@"href=""(.*?)""")
                    .WithRegexOption(RegexOptions.Singleline)
                    .Build())
                .WithParts(new ScrapeCriteriaPartBuilder()
                    .WithRegex(@">(.*?)<")
                    .WithRegexOption(RegexOptions.Singleline)
                    .Build())
                .Build();

            var scrapedElements = _scraper.Scrape(scrapeCriteria);
            List<string> expectedElements = new List<string> { "anysite.com", " Content within tag " };

            Assert.IsTrue(scrapedElements.Count == 2);
            Assert.AreEqual(scrapedElements[0], expectedElements[0]);
            Assert.AreEqual(scrapedElements[1], expectedElements[1]);

        }
    }
}
