## ADR 0001: Separate booking from document clearance

- Status: Accepted
- Date: 2026/11/07

### Context

The Vulindlela brief requires citizens to book appointments and submit
supporting documents before travelling to a branch. The Agency explicitly
requires booking and document clearance to be separate services with separate
databases. A failure in document clearance must not prevent citizens from making
a booking.

The legacy BranchDesk approach combines multiple responsibilities and makes it
difficult to isolate failures. The application needs clear ownership of data and
independent operation of each capability.

### Decision

Implement two independent ASP.NET Core Web API services:

- Booking Service
- Document Clearance Service

Each service owns its own SQLite database and communicates through HTTP APIs.

### Alternatives considered

**Single monolithic application.** Rejected. It would place booking and
document validation in the same process and database. A fault in document
clearance could affect appointment booking and it does not satisfy the Agency's
requirement for separate services.

**Separate services with a shared database.** Rejected. Although simpler to
query, it creates hidden coupling through shared tables and weakens data
ownership. The brief explicitly requires separate databases.

### Consequences

**What we gain.** Booking remains available when document clearance is
unavailable. Data ownership is clear. Each service can evolve independently.

**What we accept.** We must maintain two databases and implement inter-service
communication. Integration testing becomes more complex than a monolithic
application.