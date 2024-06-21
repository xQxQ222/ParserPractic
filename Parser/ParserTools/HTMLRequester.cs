namespace Parser.ParserTools
{
    public class HTMLRequester:IHtmlRequester
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public HTMLRequester(IHttpClientFactory clientFactory)
        {
            this._httpClientFactory = clientFactory;
        }

        public async Task<string> GetPage(string url)
        {
            var client = _httpClientFactory.CreateClient("GovPurchaseClient");
            return await client.GetStringAsync(url);
        }
    }
}
