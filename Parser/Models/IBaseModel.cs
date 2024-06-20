using AngleSharp.Dom;
using AngleSharp.Text;

namespace Parser.Models
{
    public abstract class IBaseModel
    {
        public string Selector(IElement page, string HTMLClass) => page.QuerySelector(HTMLClass)?.TextContent.Replace("\n", "").Trim().Collapse() ?? " ";
        public abstract void Create(IElement page);
        public abstract string ReturnAsJson(Purchase purchase);
    }
}
