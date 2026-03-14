# Business API

This project is a RESTful API built with ASP.NET Core 8 and Entity Framework Core.  
It demonstrates JWT authentication, role-based authorization, and CRUD operations for products.

## Technologies

- ASP.NET Core Web API (.NET 8)
- Entity Framework Core
- SQL Server
- JWT Authentication
- Swagger (OpenAPI)

## Setup

1. Clone the repository

git clone <repo-url>  
cd BusinessApi

2. Configure the database connection in `appsettings.json`

"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER;Database=BusinessApiDb;Trusted_Connection=True;TrustServerCertificate=True;"
}

3. Set the JWT secret

dotnet user-secrets set "Jwt:Key" "YOUR_SECRET_KEY"

4. Apply database migrations

dotnet ef database update

5. Run the API

dotnet run

Swagger will be available at:  
https://localhost:<port>/swagger

## Authentication Flow

1. Register a user  
POST /api/auth/register

2. Login to receive a JWT token  
POST /api/auth/login

3. Authorize in Swagger by entering:

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
