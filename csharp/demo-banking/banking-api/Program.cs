using banking_api.Data;
using banking_api.Endpoints.Requests;
using banking_api.Endpoints.DataTransferObjects;
using banking_api.Models;
using banking_api.Services;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using Serilog;
using FluentValidation;
using banking_api.Endpoints.Internal;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

builder.Services.AddSerilog();

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<BankDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddEndpoints<Program>(builder.Configuration);
builder.Services.AddTransient<IAccountService, AccountService>();
builder.Services.AddValidatorsFromAssemblyContaining<Program>();

var app = builder.Build();

app.UseSerilogRequestLogging();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapScalarApiReference();

app.UseHttpsRedirection();

app.UseEndpoints<Program>();



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

app.MapPost("/accounts", async (CreateAccountRequest command, IAccountService accountService) => {

    var account = new Account
    {
        AccountHolderName = command.AccountHolderName
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
