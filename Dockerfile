# ======================
# BUILD
# ======================
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY src/ApiTest.Api/ApiTest.Api.csproj src/ApiTest.Api/
COPY src/ApiTest.Application/ApiTest.Application.csproj src/ApiTest.Application/
COPY src/ApiTest.Domain/ApiTest.Domain.csproj src/ApiTest.Domain/
COPY src/ApiTest.Infrastructure/ApiTest.Infrastructure.csproj src/ApiTest.Infrastructure/

RUN dotnet restore src/ApiTest.Api/ApiTest.Api.csproj

COPY . .

RUN dotnet publish src/ApiTest.Api/ApiTest.Api.csproj -c Release -o /app/publish

# ======================
# RUNTIME
# ======================
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

COPY --from=build /app/publish .

EXPOSE 8080
ENTRYPOINT ["dotnet", "ApiTest.Api.dll"]
