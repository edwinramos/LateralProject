# LateralProject

A production-oriented CRUD API built with **.NET 10**, following **Clean Architecture**, **CQRS**, and modern backend development practices.

The project demonstrates how to structure a scalable API while addressing common cross-cutting concerns such as validation, logging, testing, database migrations, and containerized infrastructure.

---

# Features

- ✅ Clean Architecture
- ✅ CQRS with MediatR
- ✅ Minimal APIs
- ✅ Entity Framework Core
- ✅ PostgreSQL
- ✅ Docker support
- ✅ FluentValidation
- ✅ Global Exception Handling
- ✅ Structured Logging with Serilog
- ✅ Search & Pagination
- ✅ Database Migrations
- ✅ Initial Seed (100 records)
- ✅ Unit Tests
- ✅ Integration Tests (SQLite In-Memory)

---

# Tech Stack

- .NET 10
- ASP.NET Core Minimal APIs
- Entity Framework Core
- PostgreSQL
- MediatR
- FluentValidation
- Serilog
- Bogus
- Docker
- xUnit
- Moq
- FluentAssertions

---

# Project Structure

```
LateralProject
│
├── src
│   ├── LateralProject.Api
│   ├── LateralProject.Application
│   ├── LateralProject.Domain
│   └── LateralProject.Infrastructure
│
├── tests
│   ├── LateralProject.Application.UnitTests
│   └── LateralProject.IntegrationTests
│
├── docker-compose.yml
├── Directory.Packages.props
└── LateralProject.sln
```

---

# Architecture

```
                +----------------------+
                |         API          |
                +----------+-----------+
                           |
                           v
                +----------------------+
                |     Application      |
                |  CQRS / Validation   |
                +----------+-----------+
                           |
                           v
                +----------------------+
                |       Domain         |
                | Business Rules       |
                +----------+-----------+
                           ^
                           |
                +----------+-----------+
                |    Infrastructure    |
                | EF Core / PostgreSQL |
                +----------------------+
```

Dependencies flow inward:

- API → Application
- API → Infrastructure
- Infrastructure → Application
- Infrastructure → Domain
- Application → Domain

---

# Running the Project

## 1. Clone the repository

```bash
git clone <repository-url>

cd LateralProject
```

---

## 2. Start PostgreSQL

```bash
docker compose up -d
```

Verify:

```bash
docker ps
```

---

## 3. Apply migrations

```bash
dotnet ef database update \
--project src/LateralProject.Infrastructure \
--startup-project src/LateralProject.Api
```

---

## 4. Run the API

```bash
dotnet run --project src/LateralProject.Api
```

Swagger will be available at:

```
https://localhost:xxxx/swagger
```

or

```
http://localhost:xxxx/swagger
```

---

# Running Tests

Run all tests

```bash
dotnet test
```

Run only unit tests

```bash
dotnet test tests/LateralProject.Application.UnitTests
```

Run only integration tests

```bash
dotnet test tests/LateralProject.IntegrationTests
```

Integration tests use **SQLite In-Memory** and do **not** depend on PostgreSQL.

---

# Database

The application automatically:

- applies migrations
- seeds the database with 100 records (except in the Testing environment)

---

# Logging

Structured logging is implemented using **Serilog**.

Logs are written to:

- Console
- Rolling log files (`logs/`)

---

# API Endpoints

## Create

```
POST /api/lateralentities
```

## Get All

```
GET /api/lateralentities?page=1&pageSize=10
```

Search

```
GET /api/lateralentities?search=test
```

---

## Get By Id

```
GET /api/lateralentities/{id}
```

---

## Update

```
PUT /api/lateralentities/{id}
```

---

## Delete

```
DELETE /api/lateralentities/{id}
```

---

# Validation

Validation is handled using **FluentValidation** through a MediatR pipeline behavior.

Business rules are enforced inside the Domain layer.

Example:

- Description cannot be empty.
- Description must be unique.

---

# Testing Strategy

## Unit Tests

- Command handlers
- Domain rules
- Validation

## Integration Tests

- Full HTTP pipeline
- Routing
- Dependency Injection
- EF Core
- SQLite In-Memory database
