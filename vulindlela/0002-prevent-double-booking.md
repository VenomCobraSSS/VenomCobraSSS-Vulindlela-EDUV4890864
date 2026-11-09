## ADR 0002: Prevent double booking with a database constraint

- Status: Accepted
- Date: 2026/11/08

### Context

Citizens can book appointments for a specific branch and time slot.
Multiple users may attempt to reserve the same slot at nearly the same time.
The system must guarantee that a slot can only be assigned to one appointment.

### Decision

Enforce slot uniqueness in the Booking database using a unique constraint and
handle violations in the Booking Service.

The database is treated as the final authority on whether a slot is available.

### Alternatives considered

**Check availability before saving.** Rejected. Two requests can pass the
availability check at the same time and both attempt to save, resulting in a
race condition.

**In-memory locking.** Rejected. It only works within a single process and is
lost if the application restarts. It also does not protect multiple instances
of the service.

### Consequences

**What we gain.** The database guarantees that no two appointments can occupy
the same slot. The solution is simple and reliable.

**What we accept.** Some users may see a "slot no longer available" error if
another booking succeeds first. Extra handling of database exceptions is
required.