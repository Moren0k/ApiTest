FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/NetSeed.Api/NetSeed.Api.csproj", "src/NetSeed.Api/"]
COPY ["src/NetSeed.Infrastructure/NetSeed.Infrastructure.csproj", "src/NetSeed.Infrastructure/"]
COPY ["src/NetSeed.Application/NetSeed.Application.csproj", "src/NetSeed.Application/"]
COPY ["src/NetSeed.Domain/NetSeed.Domain.csproj", "src/NetSeed.Domain/"]
RUN dotnet restore "src/NetSeed.Api/NetSeed.Api.csproj"
COPY . .
WORKDIR "/src/src/NetSeed.Api"
RUN dotnet build "./NetSeed.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./NetSeed.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NetSeed.Api.dll"]
