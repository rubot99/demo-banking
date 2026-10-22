using banking_api.Models;

namespace banking_api.Services
{
    public interface IAccountService
    {
        public Task<bool> CreateAsync(Account account);

        public Task<Account?> GetByIdAsync(Guid accountId);

        public Task<Account?> GetByAccountNumberAsync(string accountNumber);

        public Task<IEnumerable<Account>> GetAllAsync();

        public Task<bool> UpdateAsync(Account account);

        public Task<bool> DeleteAsync(Guid accountId);
    }
}
