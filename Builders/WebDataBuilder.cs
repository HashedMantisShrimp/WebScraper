using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebScraper.Data;

namespace WebScraper.Builders
{
    class WebDataBuilder
    {
        private string _city;
        private string _category;

        public WebDataBuilder()
        {
            SetDefaults();
        }

        private void SetDefaults()
        {
            _city = "boston";
            _category = "sss";
        }

        public WebDataBuilder WithCity(string city)
        {
            _city = city;
            return this;
        }

        public WebDataBuilder WithCategory(string category)
        {
            _category = category;
            return this;
        }

        public WebData Build()
        {
            WebData UrlContent = new WebData
            {
                Category = _category,
                City = _city
            };
            return UrlContent;
        }
    }
}
