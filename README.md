
# webProductos - Simulacro HU3

Solución scaffold para la HU: API REST en .NET con arquitectura en capas (Api, Application, Domain, Infrastructure).

**Incluye**:
- 4 proyectos: webProductos.Api, webProductos.Application, webProductos.Domain, webProductos.Infrastructure
- Proyecto de pruebas: webProductos.Application.Tests (xUnit)
- Dockerfile y docker-compose.yml (API + MySQL + Adminer)
- EF Core DbContext y migraciones placeholder
- Postman collection placeholder

> Este es un scaffold funcional que debe restaurarse y compilarse con .NET 7/8 en local. Ejecuta `dotnet restore` y `dotnet build`.

Instrucciones rápidas:
1. Instalar .NET SDK (7+).
2. Desde la raíz del repo: `dotnet restore`, `dotnet build`.
3. Levantar con Docker: `docker-compose up --build` (requiere Docker).
4. Endpoint de autenticación:
   - POST /api/auth/register
   - POST /api/auth/login

