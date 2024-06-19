namespace Parser.ParserTools
{
    public class HTTPClientFactory
    {
        public readonly static IHttpClientFactory ClientFactory;

        static HTTPClientFactory()
        {
            var service=new ServiceCollection();
            ConfigureServices(service);
            var services=service.BuildServiceProvider();
            ClientFactory = services.GetRequiredService<IHttpClientFactory>();
        }
        
        private static void ConfigureServices(ServiceCollection services)
        {
            services.AddHttpClient("GovPurchase");
        }
    }
}
