using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using Parser.Models;

namespace Parser.ParserTools
{
    public interface IParser
    {
        public List<Purchase> PurchaseList (IDocument document);
    }
}
