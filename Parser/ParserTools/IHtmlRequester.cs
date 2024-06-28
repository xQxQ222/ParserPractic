namespace Parser.ParserTools
{
    public interface IHtmlRequester
    {
        public Task<string> GetPage(string url);
    }
}
