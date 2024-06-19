using AngleSharp.Dom;
using AngleSharp.Text;
using Newtonsoft.Json;

namespace Parser.Models
{
    public class Purchase
    {
        public string Id { get; private set; }=string.Empty;
        public string Law { get; private set; } = string.Empty;
        public string PurchaseObject { get; private set; } = string.Empty;
        public string Customer { get; private set; } = string.Empty;
        public string StartPrice { get; private set; }= string.Empty;
        public string Dates { get; private set; } = string.Empty;

        public string CardURL { get; private set; } = string.Empty;

        private string Selector(IElement page, string HTMLClass) => page.QuerySelector(HTMLClass)?.TextContent.Replace("\n", "").Trim().Collapse() ?? " ";
        public void Create(IElement page)
        {
            StartPrice = Selector(page, ".price-block__value");
            Id = Selector(page, ".registry-entry__header-mid__number");
            PurchaseObject = Selector(page, ".registry-entry__body-value");
            Law = Selector(page, ".col-9.p-0.registry-entry__header-top__title.text-truncate");
            Customer = Selector(page, ".registry-entry__body-href");
            Dates = Selector(page, ".data-block.mt-auto");
            CardURL = $"https://zakupki.gov.ru"+ page.QuerySelector(".registry-entry__header-mid__number a")?.GetAttribute("href").Trim('\n', ' ').Collapse();
        }

        public string ReturnAsJson(Purchase purchase)
        {
            var content = JsonConvert.SerializeObject(purchase);
            return content;
        }
    }
}
