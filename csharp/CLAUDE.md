# CLAUDE.md (C# / .NET)

The coaching rules in the root [CLAUDE.md](../CLAUDE.md) apply here: the user writes the code, and Claude hints, reviews and explains.

## Stack

- .NET 10, ASP.NET Core Minimal APIs
- Dapper + Microsoft.Data.Sqlite (raw SQL, no EF Core)
- OpenAPI + Scalar for API docs
- Tests: xUnit, NSubstitute (mocks), Shouldly (assertions)

## Commands

Run from `csharp/demo-banking/`:

```
dotnet build
dotnet test
dotnet run --project banking-api      # Scalar UI at /scalar
```

The SQLite file is `accounts.db`. Its location is set by `Database:ConnectionString` in `appsettings.json`.

## Structure (`banking-api/`)

- `Program.cs`: DI registrations and all endpoint mappings.
- `Models/`: `Organisation`, `Programme`, `Account`.
- `Services/`: one `IXxxService` interface plus a Dapper implementation per entity.
- `Data/`: `IDbConnectionFactory` / `SqliteConnectionFactory`. `DatabaseInitializer` creates the tables at startup (`CREATE TABLE IF NOT EXISTS`).
- `Endpoints/Requests/`: request DTOs with DataAnnotations.
- `Validators/`: empty, reserved for validation logic.

`banking-api-tests/` is the xUnit project.

## Conventions in use

- IDs are set in model constructors with `Guid.CreateVersion7()`.
- Service methods are async. Writes return `bool`; single lookups return `T?`.
- New services must be registered in `Program.cs`, and their tables added to `DatabaseInitializer`.

## Modern C# / .NET to suggest (with a one-line why)

- File-scoped namespaces (`namespace X;`): less nesting.
- Primary constructors for services: no boilerplate field assignment.
- `record` types for request/response DTOs: immutable, with value equality.
- `required` members: fixes the nullable warnings on models and DTOs.
- Collection expressions (`[a, b]`) and `[]` for empty collections.
- `TypedResults` instead of `Results`: compile-time return types and better OpenAPI output.
- `MapGroup("/accounts")` and endpoint extension classes: keep `Program.cs` small.
- `DateOnly` for programme start and end dates, and `TimeProvider` for testable "now".
- Pattern matching and switch expressions for domain rules.

## Testing

- **Unit tests**: services and domain rules, mocking `IDbConnectionFactory` or other dependencies with NSubstitute.
- **Integration tests**: `WebApplicationFactory<Program>` (needs the `Microsoft.AspNetCore.Mvc.Testing` package) with a temp or in-memory SQLite database.
- Name tests `Method_Scenario_ExpectedResult`, and assert with Shouldly (`result.ShouldBe(...)`).

## Known gaps (flag when relevant; don't fix)

- The test project has no `ProjectReference` to `banking-api`, so tests can't see the API types yet.
- `POST /accounts` never sets `AccountNumber`, which the `NOT NULL` column will reject.
- The `Accounts` table has no `ProgrammeId` column, and `CreateAccountRequest` has no programme.
- `GET /accounts` and `GET /accounts/{id}` return hard-coded data. PUT and DELETE are stubs.
- `OrganisationService` and `ProgrammeService` are unimplemented, and not registered in DI or the database.
- `DatabaseInitializer` has an unused `command` variable.
- Nullable warnings on `Account`, `Programme` and `CreateAccountRequest` string properties.
