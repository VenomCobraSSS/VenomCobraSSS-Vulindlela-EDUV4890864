## ADR 0003: Use policy-based authorisation for citizen records

- Status: Accepted
- Date: 2026/11/09

### Context

The Agency requires that a citizen may view only their own appointments and
their own access log, while an official may view any citizen's record.

All citizens share the same role. A role check alone cannot determine whether a
user owns the record being requested.

### Decision

Use ASP.NET Core policy-based authorisation with a custom
CitizenRecordRequirement and CitizenRecordHandler.

The handler compares the citizen identifier in the request with the citizen
identifier stored in the authenticated user's session.

Officials are exempt from the ownership check.

### Alternatives considered

**Role-based authorisation only.** Rejected. Every citizen has the same role,
so one citizen could potentially access another citizen's record.

**Manual ownership checks in every controller action.** Rejected. It duplicates
logic across the codebase and increases the risk of inconsistent security
behaviour.

### Consequences

**What we gain.** Ownership rules are enforced consistently in one place and
can be reused across endpoints.

**What we accept.** Additional classes and configuration are required. The
authorisation model is more complex than a simple role check.