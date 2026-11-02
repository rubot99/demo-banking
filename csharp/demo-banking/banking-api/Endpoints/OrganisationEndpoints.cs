using banking_api.Endpoints.DataTransferObjects;
using banking_api.Endpoints.Internal;
using banking_api.Services;
using FluentValidation;

namespace banking_api.Endpoints
{
    public class OrganisationEndpoints : IEndpoints
    {
        public static void AddServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IOrganisationService, OrganisationService>();
        }

        public static void DefineEndpoints(IEndpointRouteBuilder app)
        {
            app.MapPost("/organisations", CreateOrganisationAsync);

            app.MapGet("/organisations/{id:guid}", GetOrganisationByIdAsync);

            app.MapGet("/organisations", async (IOrganisationService organisationService) =>
            {
                var organisations = await organisationService.GetAllAsync();

                if (organisations == null)
                {
                    return Results.NotFound();
                }
                return Results.Ok(organisations);
            });

            app.MapDelete("/organisations/{id:guid}", async (Guid id, IOrganisationService organisationService) =>
            {
                var deleted = await organisationService.DeleteAsync(id);
                if (!deleted)
                {
                    return Results.NotFound();
                }
                return Results.NoContent();
            });
        }

        internal static async Task<IResult> CreateOrganisationAsync(CreateOrganisationDto command, IOrganisationService organisationService, IValidator<CreateOrganisationDto> validator)
        {
            var validationResult = await validator.ValidateAsync(command);

            if (!validationResult.IsValid)
            {
                return Results.BadRequest(validationResult.Errors);
            }

            var organisation = await organisationService.CreateAsync(command);

            return Results.Created($"/organisations/{organisation.Id}", organisation);
        }

        internal static async Task<IResult> GetOrganisationByIdAsync(Guid id, IOrganisationService organisationService)
        {
            var organisation = await organisationService.GetByIdAsync(id);
            if (organisation == null)
            {
                return Results.NotFound();
            }
            return Results.Ok(organisation);
        }
    }
}
