using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Parser.ParserTools
{
    public class HTMLRequester
    {
        private readonly HttpClient httpClient = HTTPClientFactory.ClientFactory.CreateClient("GovPurchase");

        public HTMLRequester()
        {
            httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/122.0.0.0 YaBrowser/24.4.0.0 Safari/537.36");
        }
        public async Task<string> GetPageByLinkAsync(string url)
        {
            return await httpClient.GetStringAsync(url);
        }
    }
}
