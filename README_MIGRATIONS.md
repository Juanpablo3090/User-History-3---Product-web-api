Para crear migraciones con EF Core:
- Instala dotnet-ef globalmente: `dotnet tool install --global dotnet-ef`
- Desde webProductos.Infrastructure ejecuta:
  `dotnet ef migrations add InitialCreate -p webProductos.Infrastructure -s webProductos.Api`
  `dotnet ef database update -p webProductos.Infrastructure -s webProductos.Api`
