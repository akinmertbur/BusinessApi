# Business API

This project is a RESTful API built with ASP.NET Core 8 and Entity
Framework Core. It demonstrates JWT authentication, role-based
authorization, and CRUD operations for products.

## Technologies

- ASP.NET Core Web API (.NET 8)
- Entity Framework Core
- SQL Server
- JWT Authentication
- Swagger (OpenAPI)

## Setup

1.  Clone the repository

```bash
git clone <repo-url>
cd BusinessApi
```

2.  Configure the database connection in `appsettings.json`

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER;Database=BusinessApiDb;Trusted_Connection=True;TrustServerCertificate=True;"
}
```

3.  Set the JWT secret

```bash
dotnet user-secrets set "Jwt:Key" "YOUR_SECRET_KEY"
```

4.  Apply database migrations

```bash
dotnet ef database update
```

5.  Run the API

```bash
dotnet run
```

Swagger will be available at:

    https://localhost:<port>/swagger

## Authentication Flow

Register a user:

```bash
POST /api/auth/register
```

Login to receive a JWT token:

```bash
POST /api/auth/login
```

Authorize in Swagger by entering:

    Bearer YOUR_TOKEN

## Product Endpoints

    GET /api/products
    GET /api/products/{id}
    POST /api/products (Admin only)
    PUT /api/products/{id} (Admin only)
    DELETE /api/products/{id} (Admin only)

## Notes

- Global exception handling is implemented using middleware.
- Logging is implemented using ILogger.
- EF Core migrations are included in the repository.
