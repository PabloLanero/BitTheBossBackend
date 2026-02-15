Para levantar el docker

```
docker-compose up --build
```
Para hacer la migracion 

```
dotnet ef migrations add InitialCreate -p ./API.csproj -s ./API.csproj

dotnet ef database update -p ./API.csproj -s ./API.csproj
```

Que sigue una estructura tal que asi

```
dotnet ef migrations add NombreDelProyecto -p /Ruta/Proyecto/Con/DbContext -s /Ruta/Proyecto/Inicio

dotnet ef database update NombreDelProyecto -p /Ruta/Proyecto/Con/DbContext -s /Ruta/Proyecto/Inicio
```
