using Parser;
using Parser.Controllers;
using Parser.Models;
using Parser.ParserTools;
namespace ParserTests
{
    public class Tests
    {
        private IHtmlRequester _requester;
        [SetUp]
        public void Setup()
        {
        }
        private string GetTestPageDirectory(string fileName)
        {
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory.Replace("\\bin\\Debug\\net8.0", ""), $"TestPages\\{fileName}");
        }

        [Test]
        public async Task ParseTest()//ѕроверка работы метода Parse
        {
            var html = File.ReadAllText(GetTestPageDirectory("TestPage1.txt"));
            var parser = new GovPurchaseParser(_requester);

            var parsedItems = await parser.Parse<Purchase>(new List<string>() { html });

            Assert.That(parsedItems.Count, Is.EqualTo(50));

            var tempItems = parsedItems.Where
                (x => x.CardURL == "https://zakupki.gov.ru/epz/order/notice/ea20/view/common-info.html?regNumber=0126300035824000316");

            Assert.That(tempItems.Count, Is.EqualTo(1));
        }

        [Test]
        public async Task MultiPageSearch()//ѕроверка на работоспособность с несколькими страницами
        {
            var htmlFirst = File.ReadAllText(GetTestPageDirectory("TestPage1.txt"));
            var htmlSecond = File.ReadAllText(GetTestPageDirectory("TestPage2.txt"));
            var htmlThird = File.ReadAllText(GetTestPageDirectory("TestPage3.txt"));
            var parser = new GovPurchaseParser(_requester);

            var parsedItemsTwoPages = await parser.Parse<Purchase>
                (new List<string>() { htmlFirst, htmlSecond });

            var parsedItemsThreePages = await parser.Parse<Purchase>
                (new List<string>() { htmlFirst, htmlSecond, htmlThird });

            Assert.That(parsedItemsTwoPages.Count, Is.Not.EqualTo(parsedItemsThreePages.Count));

            Assert.That(parsedItemsTwoPages.Count, Is.EqualTo(100));

            Assert.That(parsedItemsThreePages.Count, Is.EqualTo(150));

            Assert.That(parsedItemsTwoPages.Where
                (x => x.CardURL == "https://zakupki.gov.ru/epz/order/notice/notice223/common-info.html?noticeInfoId=15220506").Count,
                Is.EqualTo(0));

            Assert.That(parsedItemsThreePages.Where
                (x => x.CardURL == "https://zakupki.gov.ru/epz/order/notice/notice223/common-info.html?noticeInfoId=15220506").Count,
                Is.EqualTo(1));

        }

        [Test]
        public async Task EmptyPageSearch()//ѕроверка парсинга пустой страницы
        {
            var emptyHtmlPage = File.ReadAllText(GetTestPageDirectory("EmptyPage.txt"));
            var parser = new GovPurchaseParser(_requester);

            var parsedItems = await parser.Parse<Purchase>(new List<string>() { emptyHtmlPage });

            Assert.That(parsedItems.Count, Is.EqualTo(0));
        }

        [Test]
        public async Task NonValidPages()
        {
            var outOfBoundsPage = File.ReadAllText(GetTestPageDirectory("OutOfBoundsPage.txt"));
            var nonValidPage = "yhwiurhgiy726451785932657";

            var parser= new GovPurchaseParser(_requester);
            
            var nVPParsedItems=await parser.Parse<Purchase>(new List<string>() { nonValidPage });
            var  oOBParsedItems= await parser.Parse<Purchase>(new List<string>() { outOfBoundsPage });

            Assert.That(nVPParsedItems.Count, Is.EqualTo(0));
            Assert.That(oOBParsedItems.Count, Is.EqualTo(0));
        }
    }
}