# zen_api/Dockerfile
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

COPY *.sln .
COPY src/ZenApi.API/*.csproj ./src/ZenApi.API/
COPY src/ZenApi.Application/*.csproj ./src/ZenApi.Application/
COPY src/ZenApi.Domain/*.csproj ./src/ZenApi.Domain/
COPY src/ZenApi.Infrastructure/*.csproj ./src/ZenApi.Infrastructure/
COPY tests/ZenApi.UnitTests/*.csproj ./tests/ZenApi.UnitTests/
RUN dotnet restore

COPY . .
WORKDIR /app/src/ZenApi.API
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080
ENTRYPOINT ["dotnet", "ZenApi.API.dll"]