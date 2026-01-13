# =========================
# BUILD STAGE
# =========================
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiamos solución y proyectos
COPY *.sln .
COPY src/ApiTest.Api/ApiTest.Api.csproj src/ApiTest.Api/
COPY src/ApiTest.Application/ApiTest.Application.csproj src/ApiTest.Application/
COPY src/ApiTest.Domain/ApiTest.Domain.csproj src/ApiTest.Domain/
COPY src/ApiTest.Infrastructure/ApiTest.Infrastructure.csproj src/ApiTest.Infrastructure/

# Restore
RUN dotnet restore

# Copiamos todo
COPY src/ src/

# Build
RUN dotnet publish src/ApiTest.Api/ApiTest.Api.csproj \
    -c Release \
    -o /app/publish

# =========================
# RUNTIME STAGE
# =========================
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

COPY --from=build /app/publish .

EXPOSE 8080

ENTRYPOINT ["dotnet", "ApiTest.Api.dll"]
