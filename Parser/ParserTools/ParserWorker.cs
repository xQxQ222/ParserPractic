using AngleSharp;
using AngleSharp.Dom;
using AngleSharp.Io;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Parser.Models;
using Parser.ParserTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parsers
{
    public class ParserWorker
    {
        private HTMLRequester requester;
        public ParserWorker()
        {
            this.requester = new HTMLRequester();
        }

        private List<string> GetPagesList(string purchaseId)
        {
            var pages = new List<string>();
            var pageNumber= 1;
            while (true)
            {
                var url = $"https://zakupki.gov.ru/epz/order/extendedsearch/results.html?searchString={purchaseId}&pageNumber={pageNumber}&recordsPerPage=_100";
                var page = requester.GetPageByLinkAsync(url).Result;
                if (page.Contains("search-registry-entry-block box-shadow-search-input"))
                    pages.Add(page);
                else
                    break;
                pageNumber++;
            }
            return pages;
        }

        public async Task<List<Purchase>> GetPurchasesAsync(string purchaseId)
        {
            List<Purchase> purchases=new List<Purchase>();
            foreach (var page in GetPagesList(purchaseId))
            {
                var config = Configuration.Default.WithDefaultLoader();
                var context = BrowsingContext.New(config);
                var document = await context.OpenAsync(req => req.Content(page));
                var pageContent = document.QuerySelectorAll("div.row.no-gutters.registry-entry__form.mr-0");
                foreach(var item in pageContent)
                {
                    var card = new Purchase();
                    card.Create(item);
                    purchases.Add(card);
                }
            }
            return purchases;
        }
     }
}
