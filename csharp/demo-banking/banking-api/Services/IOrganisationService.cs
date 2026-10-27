using banking_api.Endpoints.DataTransferObjects;
using banking_api.Models;

namespace banking_api.Services
{
    public interface IOrganisationService 
    {
        public Task<OrganisationDto> CreateAsync(CreateOrganisationDto command);

        public Task<OrganisationDto?> GetByIdAsync(Guid organisationId);

        public Task<IEnumerable<OrganisationDto>> GetAllAsync();

        public Task<bool> UpdateAsync(Organisation organisation);

        public Task<bool> DeleteAsync(Guid organisationId);
    }
}
