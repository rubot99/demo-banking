using banking_api.Models;

namespace banking_api.Services
{
    public class OrganisationService : IOrganisationService
    {
        public Task<bool> CreateAsync(Organisation organisation)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(Guid organisationId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Organisation>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Organisation?> GetByIdAsync(Guid organisationId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(Organisation organisation)
        {
            throw new NotImplementedException();
        }
    }
}
