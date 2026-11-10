# CLAUDE.md (C# / .NET)

The coaching rules in the root [CLAUDE.md](../CLAUDE.md) apply here: the user writes the code, and Claude hints, reviews and explains.
The spec is [docs/requirements.md](../docs/requirements.md). Its §9 is the current list of gaps between this code and the spec.

## Stack

- .NET 10, ASP.NET Core Minimal APIs
- EF Core 10 + Npgsql (PostgreSQL 18 via `docker-compose.yml`), code-first migrations
- FluentValidation (validators are registered from the assembly)
- Serilog (console sink, request logging)
- OpenAPI + Scalar for API docs
- Tests: xUnit, FluentAssertions, `Microsoft.AspNetCore.Mvc.Testing`

## Commands

Run from `csharp/demo-banking/`:

```
docker compose up -d                      # Postgres on localhost:5432
dotnet ef database update --project banking-api
dotnet build
dotnet test
dotnet run --project banking-api          # Scalar UI at /scalar
```

The connection string is `ConnectionStrings:DefaultConnection` in `banking-api/appsettings.json`.

## Structure

- `banking-api/Program.cs`: Serilog, OpenAPI, DbContext and validators. Endpoints are discovered automatically.
- `Endpoints/Internal/`: `IEndpoints` (static abstract `AddServices` / `DefineEndpoints`) plus reflection-based `AddEndpoints<T>` / `UseEndpoints<T>`.
  Each `XxxEndpoints` class registers its own service and maps its routes.
- `Endpoints/DataTransferObjects/`: request/response `record`s.
- `Models/`: entities. `EntityBase` provides `Id` (UUIDv7), `CreatedAt` and `UpdatedAt`. Entities use factory `Create(...)` and `Update(...)` methods.
- `Data/`: `BankDbContext` (schema `banking`) plus one `IEntityTypeConfiguration<T>` per entity.
- `Migrations/`: EF Core migrations.
- `Services/`: one `IXxxService` plus an EF Core implementation per entity. Services return DTOs.
- `Validators/`: FluentValidation `AbstractValidator<TDto>`, one per request DTO.
- `IApiMarker.cs`: marker type for `WebApplicationFactory<IApiMarker>`.

Test projects:
- `banking-api-tests-unit/`: model/domain unit tests.
- `banking-api-tests-integration/`: HTTP tests via `WebApplicationFactory`. These currently hit the dev Postgres database.
- `banking-api-tests/`: old project, no longer in the `.slnx`. Delete it.

## Conventions in use

- Service methods are async. Writes return `bool` or a DTO; single lookups return `T?`.
- Validation happens in the endpoint (`IValidator<T>`) before the service is called.
- A new entity needs: model, configuration, `DbSet`, migration, DTOs, validator, service, and an endpoints class.

## Modern C# / .NET to suggest (with a one-line why)

- File-scoped namespaces (`namespace X;`): less nesting.
- Primary constructors for services: no boilerplate field assignment (`BankDbContext` already uses one).
- `required` members: fixes the nullable warnings on `Account` and the test `ValidationError`.
- Collection expressions (`[a, b]`) and `[]` for empty collections.
- `TypedResults` instead of `Results`: compile-time return types and OpenAPI output without `.Produces<>()`.
- `MapGroup("/organisations/{organisationId:guid}/programmes")`: nested routes (spec §5).
- `DateOnly` for programme dates, and `TimeProvider` / `FakeTimeProvider` for "today" (GEN-4).
- `Results.Problem` / `TypedResults.ValidationProblem` plus `AddProblemDetails()`: the ProblemDetails contract (GEN-6).
- An abstract record hierarchy with switch expressions for rule kinds (RUL-7).
- Testcontainers for PostgreSQL: isolated integration tests.

## Testing

- **Unit tests**: domain behaviour on the entities (lifecycle, rules, balance).
- **Integration tests**: `WebApplicationFactory<IApiMarker>` against an isolated database. Assert on status code and ProblemDetails `code`, not on message text.
- Name tests `Method_Scenario_ExpectedResult` and assert with FluentAssertions. Reference requirement IDs where useful.

## Known tooling issue

- MSB3277: Npgsql brings in `Microsoft.EntityFrameworkCore.Relational` 10.0.4, while EF Core is 10.0.14. Adding an explicit `Relational` package reference at 10.0.14 resolves it.
