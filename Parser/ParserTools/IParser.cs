using Parser.Models;

namespace Parser.ParserTools
{
    public interface IParsing
    {
        public Task<List<T>> Parse<T>(string parameter) where T:IBaseModel, new();
        public HTMLRequester requester {  get; set; }
    }
}
