
#webProducts - HU3 

HU Scaffold Solution: REST API in .NET with a layered architecture (API, Application, Domain, Infrastructure).

**Includes**:
- 4 projects: webProducts.Api, webProducts.Application, webProducts.Domain, webProducts.Infrastructure
- Test project: webProducts.Application.Tests (xUnit)
- Dockerfile and docker-compose.yml (API + MySQL + Admin)
- EF Core DbContext placeholder and migrations
- Postman collection placeholder

> This is a working scaffold that needs to be restored and compiled with .NET 7/8 locally. Run `dotnet reset` and `dotnet build`.

Quick Instructions:
1. Install the .NET SDK (7+).

2. From the repository root: `dotnet restore`, `dotnet build`.

3. Start with Docker: `docker-compose up --build` (requires Docker).

4. Authentication endpoint:

- POST /api/auth/registration

- POST /api/auth/login

5. If you wanna use the api web via swagger, go to "http://localhost:5000/swagger/index.html"
6. If you need to acess adminer just enter to "http://localhost:8081/"
