using banking_api;
using Microsoft.AspNetCore.Mvc.Testing;

namespace banking_api_tests_integration
{
    public class BankingApiFactory : WebApplicationFactory<IApiMarker>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            // You can configure the web host here if needed, for example, to use a different environment or configuration.
            builder.UseEnvironment("Testing");
        }
    }
}
