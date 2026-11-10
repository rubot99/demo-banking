# Demo Banking Project

A small banking API built to practise languages: first C# / .NET, then Python, then Go.
All implementations follow the same spec: **[docs/requirements.md](docs/requirements.md)**.

| Implementation | Status |
|---|---|
| [C# / .NET](csharp/) | Stage 1 in progress |
| Python | Not started |
| Go | Not started |

## Entity Relationship Diagram

```mermaid
erDiagram
    ORGANISATION ||--o{ PROGRAMME : runs
    PROGRAMME ||--o{ PROGRAMME_RULE : has
    PROGRAMME ||--o{ ACCOUNT : contains
    ACCOUNT ||--o{ TRANSACTION : has
    TRANSACTION |o--o| TRANSACTION : reverses

    ORGANISATION {
        uuid id
        string name
        string address
        string email
    }
    PROGRAMME {
        uuid id
        uuid organisationId
        string name
        string description
        date startDate
        date endDate "nullable"
        int startRange
        int endRange
        enum status "Open | Closed"
    }
    PROGRAMME_RULE {
        uuid id
        uuid programmeId
        enum kind "MaxPerTransaction | MaxPerDay"
        enum appliesTo "Deposit | Withdrawal"
        decimal amount
    }
    ACCOUNT {
        uuid id
        uuid programmeId
        string accountNumber
        string holderName
        string holderAddress
        enum status "Open | Closed"
    }
    TRANSACTION {
        uuid id
        uuid accountId
        enum type "Deposit | Withdrawal | Reversal"
        decimal amount
        uuid reversesTransactionId "nullable"
    }
```

The `createdAt` / `updatedAt` timestamps are left out of the diagram. See the full ERD in the requirements doc.
