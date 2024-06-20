
using AngleSharp;
using AngleSharp.Dom;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Parser.Models;

namespace Parser.ParserTools
{
    public class GovPurchaseParser: IParsing
    {
        public HTMLRequester requester { get; set; }=new HTMLRequester();

        private List<string> GetPagesList(string purchaseId,HTMLRequester requester)
        {
            var pages = new List<string>();
            var pageNumber = 1;
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

        public async Task<IDocument> GetDocument(string pageUrl)
        {
            var config = Configuration.Default.WithDefaultLoader();
            var context = BrowsingContext.New(config);
            var document = await context.OpenAsync(req => req.Content(pageUrl));
            return document;
        }

        public async Task<List<T>> Parse<T>(string parameter) where T : IBaseModel, new()
        {
            List<T> purchases = new List<T>();
            foreach (var page in GetPagesList(parameter,requester))
            {
                var document = await GetDocument(page);
                var pageContent = document.QuerySelectorAll("div.row.no-gutters.registry-entry__form.mr-0");
                foreach (var item in pageContent)
                {
                    var card =new T();
                    card.Create(item);
                    purchases.Add(card);
                }
            }
            return purchases;
        }
    }
}
