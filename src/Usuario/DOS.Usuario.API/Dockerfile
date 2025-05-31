FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY Core/DOS.Core/*.csproj Core/DOS.Core/
COPY Usuario/DOS.Usuario.API/*.csproj Usuario/DOS.Usuario.API/
COPY Usuario/DOS.Usuario.Application/*.csproj Usuario/DOS.Usuario.Application/
COPY Usuario/DOS.Usuario.Data/*.csproj Usuario/DOS.Usuario.Data/
COPY Usuario/DOS.Usuario.Domain/*.csproj Usuario/DOS.Usuario.Domain/

RUN dotnet restore Usuario/DOS.Usuario.API/DOS.Usuario.API.csproj

COPY . .

WORKDIR /app/Usuario/DOS.Usuario.API

RUN dotnet publish -c Release -o /out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /out ./
ENTRYPOINT ["dotnet", "DOS.Usuario.API.dll"]
