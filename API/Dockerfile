# Etapa 1: Compilación
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS buildApp
WORKDIR /src
COPY . .
RUN dotnet publish "API.csproj" -c Release -o /consoleapp

# Etapa 2: Ejecución
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=buildApp /consoleapp ./
EXPOSE 8080
ENTRYPOINT ["dotnet", "API.dll"]