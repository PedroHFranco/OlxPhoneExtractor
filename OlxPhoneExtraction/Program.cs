using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace OlxPhoneExtraction
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var web = new HtmlWeb();
            var doc = web.Load("https://go.olx.com.br/grande-goiania-e-anapolis/regiao-norte");

            var nextPage = doc.DocumentNode.SelectSingleNode("//li[contains(@class, 'item next')]/a");

            while(nextPage != null)
            {
                foreach (var item in doc.DocumentNode.SelectNodes("//a[contains(@class, 'OLXad-list-link ')]"))
                {
                    var webAdPage = new HtmlWeb();
                    var docAdPage = await LoadHtmlWithBrowser(item.GetAttributeValue("href", "")).ConfigureAwait(false);
                    var spanNumber = docAdPage.DocumentNode.SelectNodes("//span[contains(@class, 'lhsaj')]");
                }

                doc = web.Load(nextPage.GetAttributeValue("href", ""));
                nextPage = doc.DocumentNode.SelectSingleNode("//li[contains(@class, 'item next')]/a");
            }
        }

        private static async Task<HtmlDocument> LoadHtmlWithBrowser(String url)
        {
            var client = new HttpClient();
            var response = await client.GetAsync(url).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var pageContents = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            HtmlDocument pageDocument = new HtmlDocument();
            pageDocument.LoadHtml(pageContents);
            return pageDocument;
        }
    }
}
