# Vulindlela Citizen Booking System

## Problem

Vulindlela modernises a legacy civic services booking process by separating booking, document clearance, and citizen access into independent services. Citizens can book appointments, submit documents for verification, and manage their appointments through a secure web portal while the system demonstrates service boundaries, asynchronous processing, resilience, and authentication.

## Architecture

```text
Databases

Booking API
    -> Bookings.db

Clearance API
    -> Documents.db

Portal
    -> Portal.db
```

## Running the System

Open three terminals.

### Booking API

```bash
cd src/Vulindlela.Booking.Api
dotnet run
```

Runs on:

```text
http://localhost:5401
```

### Clearance API

```bash
cd src/Vulindlela.Clearance.Api
dotnet run
```

Runs on:

```text
http://localhost:5402
```

### Portal

```bash
cd src/Vulindlela.Portal
dotnet run
```

Runs on:

```text
http://localhost:5403
```

## Demonstration

If the database is empty, register a new account through the Portal.

Example:

```text
Username: demo
Password: Password123!
```

## Interesting Scenarios To Try

### Create a Booking

Create an appointment through the system and verify that it is stored in the Booking database.

### Document Verification

Submit documents and observe the asynchronous verification process. Verification is handled through a background worker and includes a deliberate delay to simulate a long-running external process.

### Service-to-Service Communication

Create a booking and observe the Booking API notifying the Clearance API using HttpClient and an interface abstraction.

### Graceful Degradation

Stop the Clearance API and create another booking.

Expected behaviour:

```text
Booking succeeds.
Clearance notification fails gracefully.
No system crash occurs.
```

### Authentication

Register a user, sign in, and attempt to access appointment pages. Protected pages require authentication and are secured using cookie authentication.

## Security

Passwords are never stored in plaintext.

Each password is:

- Salted with a per-user cryptographic salt
- Hashed using PBKDF2
- Stored as a hash and salt pair

Authentication is implemented using ASP.NET Core Cookie Authentication.

## Known Limitations

### Functionality

The project was never tested, Unit Tests weren't set up, and has never been run. Therefore it cannot be considered complete and might not function as intended at all.

### Scope

This project focuses on demonstrating service separation, asynchronous processing, authentication, resilience, and architectural decision making rather than providing a production-ready citizen services platform.

### Authentication

Only local cookie authentication is implemented. External identity providers such as Azure Active Directory, Google, or Microsoft Accounts are not supported.

### Background Processing

Document verification uses an in-memory channel and background worker. Verification state is not distributed across multiple service instances.

### Notifications

Failed notifications to the Clearance API are ignored. Automatic retry policies are not implemented.

### Availability

The system demonstrates graceful degradation. If the Clearance API is unavailable, the Booking API continues functioning, although document clearance information may be temporarily unavailable.

### Branch Capacity Rules

Branch scheduling rules are simplified for demonstration purposes and do not reflect real-world branch operating policies.

### Concurrency

The solution demonstrates protection against duplicate booking attempts but has not been load tested under production-scale traffic conditions.