using banking_api.Data;
using banking_api.Endpoints.DataTransferObjects;
using banking_api.Models;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace banking_api.Services
{
    public class ProgrammeService : IProgrammeService
    {
        private readonly BankDbContext _dbContext;
        private readonly ILogger<ProgrammeService> _logger;

        public ProgrammeService(BankDbContext dbContext, ILogger<ProgrammeService> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }
        public async Task<ProgrammeDto> CreateAsync(CreateProgrammeDto command)
        {
            var programme = Programme.Create(command.Name, command.Description, command.StartDate, command.EndDate, command.StartRange, command.EndRange, command.OrganisationId);

            await _dbContext.Programmes.AddAsync(programme);
            var result = await _dbContext.SaveChangesAsync();

            return new ProgrammeDto(programme.Id, programme.Name, programme.Description, programme.StartDate, programme.EndDate, programme.StartRange, programme.EndRange, programme.OrganisationId);
        }

        public Task<bool> DeleteAsync(Guid programmeId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ProgrammeDto>> GetAllAsync(Guid organisationId)
        {
            return await _dbContext.Programmes
                .AsNoTracking()
                .Select(p => new ProgrammeDto(p.Id, p.Name, p.Description, p.StartDate, p.EndDate, p.StartRange, p.EndRange, p.OrganisationId))
                .ToListAsync();
        }

        public Task<ProgrammeDto?> GetByIdAsync(Guid programmeId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(Programme programme)
        {
            throw new NotImplementedException();
        }
    }
}
