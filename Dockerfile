# =========================
# BUILD STAGE
# =========================
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiamos solución y proyectos
COPY *.sln .
COPY src/NetSeed.Api/NetSeed.Api.csproj src/NetSeed.Api/
COPY src/NetSeed.Application/NetSeed.Application.csproj src/NetSeed.Application/
COPY src/NetSeed.Domain/NetSeed.Domain.csproj src/NetSeed.Domain/
COPY src/NetSeed.Infrastructure/NetSeed.Infrastructure.csproj src/NetSeed.Infrastructure/

# Restore
RUN dotnet restore

# Copiamos todo
COPY src/ src/

# Build
RUN dotnet publish src/NetSeed.Api/NetSeed.Api.csproj \
    -c Release \
    -o /app/publish

# =========================
# RUNTIME STAGE
# =========================
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

COPY --from=build /app/publish .

EXPOSE 8080

ENTRYPOINT ["dotnet", "NetSeed.Api.dll"]
