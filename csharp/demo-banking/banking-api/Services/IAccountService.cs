using banking_api.Endpoints.DataTransferObjects;
using banking_api.Models;

namespace banking_api.Services
{
    public interface IAccountService
    {
        public Task<AccountDto> CreateAsync(CreateAccountDto command);

        public Task<AccountDto?> GetByIdAsync(Guid accountId);

        public Task<AccountDto?> GetByAccountNumberAsync(string accountNumber);

        public Task<IEnumerable<AccountDto>> GetAllAsync();

        public Task<bool> UpdateAsync(UpdateAccountDto command);

        public Task<bool> DeleteAsync(Guid accountId);
    }
}
