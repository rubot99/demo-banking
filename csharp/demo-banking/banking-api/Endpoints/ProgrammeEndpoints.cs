using banking_api.Endpoints.DataTransferObjects;
using banking_api.Endpoints.Internal;
using banking_api.Services;
using FluentValidation;

namespace banking_api.Endpoints
{
    public class ProgrammeEndpoints : IEndpoints
    {
        public static void AddServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IProgrammeService, ProgrammeService>();
        }

        public static void DefineEndpoints(IEndpointRouteBuilder app)
        {
            app.MapPost("/programmes", CreateProgrammeAsync)
                .WithName("CreateProgramme")
                .Accepts<CreateProgrammeDto>("application/json")
                .Produces<ProgrammeDto>(StatusCodes.Status201Created)
                .Produces<IEnumerable<FluentValidation.Results.ValidationFailure>>(StatusCodes.Status400BadRequest)
                .WithTags("Programmes")
                .WithDescription("Creates a new programme. Returns the created programme with a 201 status code or validation errors with a 400 status code.");
        }

        internal static async Task<IResult> CreateProgrammeAsync(CreateProgrammeDto command, IProgrammeService programmeService, IValidator<CreateProgrammeDto> validator)
        {
            var validationResult = await validator.ValidateAsync(command);
            if (!validationResult.IsValid)
            {
                return Results.BadRequest(validationResult.Errors);
            }
            var createdProgramme = await programmeService.CreateAsync(command);
            return Results.Created($"/programmes/{createdProgramme.Id}", createdProgramme);
        }
    }
}
