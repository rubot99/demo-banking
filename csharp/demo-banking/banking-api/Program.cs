using banking_api.Data;
using banking_api.Endpoints.Requests;
using banking_api.Models;
using banking_api.Services;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddSingleton<IDbConnectionFactory>(_ => new SqliteConnectionFactory(builder.Configuration.GetValue<string>("Database:ConnectionString")));
builder.Services.AddSingleton<DatabaseInitializer>();
builder.Services.AddSingleton<IAccountService, AccountService>();

var app = builder.Build();

var databaseInitializer = app.Services.GetRequiredService<DatabaseInitializer>();
await databaseInitializer.InitializeAsync();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapScalarApiReference();

app.UseHttpsRedirection();

app.MapGet("/accounts", () => {
    return Results.Ok(new List<Account> {
        new Account
        {
            AccountHolderName = "John Smith",
            AccountNumber = "123456789",
            Id = new Guid("e74edaec-ed47-468a-978e-3357eb76678a")
        },
        new Account
        {
            AccountHolderName = "Jeff Smith",
            AccountNumber = "123456710",
            Id = new Guid("1e434880-72a9-4e24-a527-1d38f1f06422")
        }
    });
});

app.MapGet("/accounts/{id:guid}", (Guid id) => {
    return Results.Ok(new Account
    {
        AccountHolderName = "John Smith",
        AccountNumber = "123456789",
        Id = new Guid("e74edaec-ed47-468a-978e-3357eb76678a")
    });
});

app.MapPost("/accounts", async (CreateAccountRequest createAccountRequest, IAccountService accountService) => {

    var account = new Account
    {
        AccountHolderName = createAccountRequest.AccountHolderName
    };

    var created = await accountService.CreateAsync(account);

    if(!created)
    {
        return Results.BadRequest();
    }

    return Results.Created($"/accounts/{account.Id}", account);
});

app.MapPut("/accounts", (UpdateAccountRequest updateAccountRequest) => {
    return Results.Ok();
});

app.MapDelete("/accounts/{id:guid}", (Guid id) => {
    return Results.NoContent();
});

app.Run();
