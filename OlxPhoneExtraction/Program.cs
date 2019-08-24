using HtmlAgilityPack;
using System;
using System.Collections.Generic;

namespace OlxPhoneExtraction
{
    class Program
    {
        static void Main(string[] args)
        {
            var web = new HtmlWeb();
            var doc = web.Load("https://go.olx.com.br/grande-goiania-e-anapolis/regiao-norte");

            var nextPage = doc.DocumentNode.SelectSingleNode("//li[contains(@class, 'item next')]/a");

            while(nextPage != null)
            {
                var items = doc.DocumentNode.SelectNodes("//a[contains(@class, 'OLXad-list-link ')]");
                foreach (var item in items)
                {
                    var webAdPage = new HtmlWeb();
                    var docAdPage = webAdPage.Load(item.GetAttributeValue("href", ""));
                }

                doc = web.Load(nextPage.GetAttributeValue("href", ""));
                nextPage = doc.DocumentNode.SelectSingleNode("//li[contains(@class, 'item next')]/a");
            }
        }
    }
}
