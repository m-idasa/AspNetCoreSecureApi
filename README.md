# AspNetCoreSecureApi

A sample ASP.NET Core Web API project built for learning and practicing backend development concepts, authentication, and secure API design using modern .NET technologies.

## Features

* ASP.NET Core Web API
* JWT Authentication
* ASP.NET Core Identity
* Role-based Authorization
* Entity Framework Core
* SQL Server Integration
* Swagger / OpenAPI Documentation
* Cookie & Bearer Authentication
* CRUD Endpoints
* Dependency Injection
* XML Formatter Support
* RESTful API Design

## Technologies Used

* ASP.NET Core
* C#
* Entity Framework Core
* ASP.NET Identity
* JWT Bearer Authentication
* SQL Server
* Swagger / Swashbuckle
* REST APIs

## Project Purpose

This project was developed as a personal learning project to gain practical experience in:

* Secure API development
* Authentication & Authorization
* JWT token handling
* ASP.NET Core Identity
* Database integration using Entity Framework Core
* RESTful API architecture
* Backend application structure and configuration

## Authentication

The project uses JWT Bearer Authentication along with ASP.NET Core Identity for user authentication and authorization.

Features include:

* Token-based authentication
* Role management
* Secure API endpoints
* Configurable token validation parameters

## API Documentation

Swagger UI is enabled in development mode for testing and exploring API endpoints.

## Getting Started

### Prerequisites

* .NET SDK
* SQL Server
* Visual Studio / Rider

### Setup

1. Clone the repository

```bash id="a1b2c3"
git clone https://github.com/your-username/AspNetCoreSecureApi.git
```

2. Update the connection string and JWT settings in `appsettings.json`

3. Apply database migrations

```bash id="d4e5f6"
dotnet ef database update
```

4. Run the project

```bash id="g7h8i9"
dotnet run
```

## Project Structure

* Controllers
* Services
* Authentication
* Data
* Models
* Configuration

## Notes

This repository is intended for educational purposes and demonstrates the fundamentals of building secure and maintainable ASP.NET Core APIs using JWT authentication and Entity Framework Core.

## Author

Fatemeh Asadi
