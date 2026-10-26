# Demo Banking Project

Work In Progress

## Entity Relationship Diagram

```mermaid
    erDiagram
        ORGANISATION ||--o{ PROGRAMME : has
        PROGRAMME||--o{ ACCOUNT : has
        ACCOUNT
        ORGANISATION {
            guid id
            string name
            string address

        }
        PROGRAMME {
            guid id
            string name
            string description
            date startDate
            date endDate
            int startRange
            int endRange
        }
        ACCOUNT {
            guid id
            string accountNumber
            string accountHolderName
        }

```