FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY Core/DOS.Core/*.csproj Core/DOS.Core/
COPY Auth/DOS.Auth.API/*.csproj Auth/DOS.Auth.API/
COPY Auth/DOS.Auth.Application/*.csproj Auth/DOS.Auth.Application/
COPY Auth/DOS.Auth.Data/*.csproj Auth/DOS.Auth.Data/
COPY Auth/DOS.Auth.Domain/*.csproj Auth/DOS.Auth.Domain/

RUN dotnet restore Auth/DOS.Auth.API/DOS.Auth.API.csproj

COPY . .

WORKDIR /app/Auth/DOS.Auth.API

RUN dotnet publish -c Release -o /out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /out ./
ENTRYPOINT ["dotnet", "DOS.Auth.API.dll"]
