using banking_api.Models;

namespace banking_api.Services
{
    public interface IOrganisationService
    {
        public Task<bool> CreateAsync(Organisation organisation);

        public Task<Organisation?> GetByIdAsync(Guid organisationId);

        public Task<IEnumerable<Organisation>> GetAllAsync();

        public Task<bool> UpdateAsync(Organisation organisation);

        public Task<bool> DeleteAsync(Guid organisationId);
    }
}
