using banking_api.Endpoints.DataTransferObjects;
using banking_api.Endpoints.Internal;
using banking_api.Services;
using FluentValidation;

namespace banking_api.Endpoints
{
    public class AccountEndpoints : IEndpoints
    {
        public static void AddServices(IServiceCollection services, IConfiguration configuration)
        {

            services.AddTransient<IAccountService, AccountService>();
        }

        public static void DefineEndpoints(IEndpointRouteBuilder app)
        {
            app.MapPost("/accounts", CreateAccountAsync);
            app.MapGet("/accounts/{id:guid}", GetAccountByIdAsync);
            app.MapGet("/accounts", GetAccountsAsync);
            app.MapPut("/accounts/{id:guid}",  UpdateAccountAsync);
            app.MapDelete("/accounts/{id:guid}", DeleteAccountAsync);
        }

        internal static async Task<IResult> CreateAccountAsync(CreateAccountDto command, IAccountService accountService, IValidator<CreateAccountDto> validator)
        {
            var validationResult = await validator.ValidateAsync(command);
            if (!validationResult.IsValid)
            {
                return Results.BadRequest(validationResult.Errors);
            }
            var account = await accountService.CreateAsync(command);
            return Results.Created($"/accounts/{account.Id}", account);
        }

        internal static async Task<IResult> GetAccountByIdAsync(Guid id, IAccountService accountService)
        {
            var account = await accountService.GetByIdAsync(id);
            if (account == null)
            {
                return Results.NotFound();
            }
            return Results.Ok(account);
        }
        
        internal static async Task<IResult> GetAccountsAsync(IAccountService accountService)
        {
            var accounts = await accountService.GetAllAsync();
            return Results.Ok(accounts);
        }

        internal static async Task<IResult> UpdateAccountAsync(Guid id, UpdateAccountDto updateAccountDto, IAccountService accountService, IValidator<UpdateAccountDto> validator)
        {
            var validationResult = await validator.ValidateAsync(updateAccountDto);
            if (!validationResult.IsValid)
            {
                return Results.BadRequest(validationResult.Errors);
            }
            var updated = await accountService.UpdateAsync(updateAccountDto);
            if (!updated)
            {
                return Results.NotFound();
            }
            return Results.NoContent();
        }

        internal static async Task<IResult> DeleteAccountAsync(Guid id, IAccountService accountService)
        {
            var deleted = await accountService.DeleteAsync(id);
            if (!deleted)
            {
                return Results.NotFound();
            }
            return Results.NoContent();
        }
    }
}
