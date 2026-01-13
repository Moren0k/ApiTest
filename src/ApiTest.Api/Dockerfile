FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/ApiTest.Api/ApiTest.Api.csproj", "src/ApiTest.Api/"]
COPY ["src/ApiTest.Infrastructure/ApiTest.Infrastructure.csproj", "src/ApiTest.Infrastructure/"]
COPY ["src/ApiTest.Application/ApiTest.Application.csproj", "src/ApiTest.Application/"]
COPY ["src/ApiTest.Domain/ApiTest.Domain.csproj", "src/ApiTest.Domain/"]
RUN dotnet restore "src/ApiTest.Api/ApiTest.Api.csproj"
COPY . .
WORKDIR "/src/src/ApiTest.Api"
RUN dotnet build "./ApiTest.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ApiTest.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ApiTest.Api.dll"]
