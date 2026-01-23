🛒 Store API - Clean Architecture & Identity
A robust E-commerce backend built with ASP.NET Core, focusing on Clean Architecture, SOLID principles, and secure JWT Authentication.

🚀 Key Features
Secure Authentication: Integrated ASP.NET Core Identity for user management.

JWT Implementation: Custom JWT generation with claims and roles for secure authorization.

Clean Architecture: Separation of concerns using Service Manager and Repository/Unit of Work patterns.

Global Error Handling: Middleware to catch exceptions and return standardized API responses.

Automatic Data Seeding: Database initialization for products, categories, and identity roles.

🛠️ Tech Stack
.NET 8.0 / C#

Entity Framework Core (SQL Server)

ASP.NET Core Identity

AutoMapper (for DTO mapping)

Postman (for API testing)

🔐 How to Test Authentication
Register a User:

Endpoint: POST /api/auth/register

Body: Provide Email, Password, DisplayName, and UserName.

Login:

Endpoint: POST /api/auth/login

Result: You will receive a UserResponse containing a JWT Token.

Access Protected Data:

Copy the token.

Go to a protected endpoint (e.g., GET /api/products).

Add an Authorization header: Bearer <your_token>.

🏗️ Project Structure
Core: Domain Entities and Service Abstractions.

Services: Business logic implementation and JWT generation.

Infrastructure: Database context, Repositories, and Migrations.

Presentation: API Controllers and Middlewares.
