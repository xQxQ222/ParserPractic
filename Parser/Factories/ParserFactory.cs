using Parser.ParserTools;

namespace Parser.Factories
{
    public class ParserFactory
    {
        public static GovPurchaseParser GetGovPurchaseParser()
        {
            return new GovPurchaseParser();
        }
    }
}
