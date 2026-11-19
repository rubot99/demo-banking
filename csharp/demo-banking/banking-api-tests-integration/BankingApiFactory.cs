using banking_api;
using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;
using Microsoft.AspNetCore.Mvc.Testing;
using Testcontainers.PostgreSql;

namespace banking_api_tests_integration
{
    public class BankingApiFactory : WebApplicationFactory<IApiMarker>
    {
        private readonly PostgreSqlContainer _postgresContainer = new PostgreSqlBuilder()
            .WithDatabase("banking_api_test")
            .WithUsername("postgres")
            .WithPassword("password")
            .Build();

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            // You can configure the web host here if needed, for example, to use a different environment or configuration.
            builder.UseEnvironment("Testing");

        }
    }
}
