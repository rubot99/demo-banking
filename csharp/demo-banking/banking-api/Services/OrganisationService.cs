using banking_api.Data;
using banking_api.Endpoints.DataTransferObjects;
using banking_api.Models;
using Microsoft.EntityFrameworkCore;

namespace banking_api.Services
{
    public class OrganisationService : IOrganisationService
    {
        private readonly BankDbContext _dbContext;
        private readonly ILogger<OrganisationService> _logger;

        public OrganisationService(BankDbContext dbContext, ILogger<OrganisationService> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<OrganisationDto> CreateAsync(CreateOrganisationDto command)
        {
            var organisation = Organisation.Create(command.Name, command.Address, command.Email);

            await _dbContext.Organisations.AddAsync(organisation);
            var result = await _dbContext.SaveChangesAsync();

            return new OrganisationDto(organisation.Id, organisation.Name, organisation.Address, organisation.Email);
        }

        public async Task<bool> DeleteAsync(Guid organisationId)
        {
            var existingOrganisation = await _dbContext.Organisations.FindAsync(organisationId);
            
            if(existingOrganisation != null)
            {
                _dbContext.Organisations.Remove(existingOrganisation);
                var result = await _dbContext.SaveChangesAsync();

                return result > 0;
            }

            return true;
        }

        public async Task<IEnumerable<OrganisationDto>> GetAllAsync()
        {
            return await _dbContext.Organisations
                .AsNoTracking()
                .Select(o => new OrganisationDto(o.Id, o.Name, o.Address, o.Email))
                .ToListAsync();
        }

        public async Task<OrganisationDto?> GetByIdAsync(Guid organisationId)
        {
            var organisation = await _dbContext.Organisations
                .AsNoTracking()
                .FirstOrDefaultAsync(o => o.Id == organisationId);

            return organisation == null ? null : new OrganisationDto(organisation.Id, organisation.Name, organisation.Address, organisation.Email);
        }

        public async Task<bool> UpdateAsync(Guid organisationId, UpdateOrganisationDto command)
        {
            var existingOrganisation = await _dbContext.Organisations.FindAsync(organisationId);
            if (existingOrganisation != null)
            {
                existingOrganisation.Update(command.Name, command.Address, command.Email);
                var result = await _dbContext.SaveChangesAsync();
                return result > 0;
            }
            return false;
        }
    }
}
