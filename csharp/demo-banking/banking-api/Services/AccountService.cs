using banking_api.Data;
using banking_api.Models;

namespace banking_api.Services
{
    public class AccountService : IAccountService
    {
        public async Task<bool> CreateAsync(Account account)
        {
            //var existingAccount = await GetByAccountNumberAsync(account.AccountNumber);
            //if (existingAccount != null)
            //{
            //    return false;
            //}

            ////using var connection = await _dbConnectionFactory.CreateConnectionAsync();

            ////var result = await connection.ExecuteAsync(
            ////    "INSERT INTO Accounts (Id, AccountNumber, AccountHolderName) VALUES (@Id, @AccountNumber, @AccountHolderName)",
            ////    new { account.Id, account.AccountNumber, account.AccountHolderName });

            ////return result > 0;
            ///
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(Guid accountId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Account>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Account?> GetByAccountNumberAsync(string accountNumber)
        {
            throw new NotImplementedException();
        }

        public Task<Account?> GetByIdAsync(Guid accountId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(Account account)
        {
            throw new NotImplementedException();
        }
    }
}
