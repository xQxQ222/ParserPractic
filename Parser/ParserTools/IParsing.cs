using Parser.Models;

namespace Parser.ParserTools
{
    public interface IParsing
    {
        public List<string> GetPagesList(string parameter);
        public Task<List<T>> Parse<T>(List<string> pages) where T:IBaseModel, new();
    }
}
