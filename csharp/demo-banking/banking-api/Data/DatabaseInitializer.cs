using Dapper;

namespace banking_api.Data
{
    public class DatabaseInitializer
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public DatabaseInitializer(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task InitializeAsync()
        {
            using var connection = await _connectionFactory.CreateConnectionAsync();
            var command = connection.CreateCommand();
            await connection.ExecuteAsync(@"
                CREATE TABLE IF NOT EXISTS Accounts (
                    Id TEXT PRIMARY KEY,
                    AccountNumber TEXT NOT NULL,
                    AccountHolderName TEXT NOT NULL
                );");
        }
    }
}
