FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY Core/DOS.Core/*.csproj Core/DOS.Core/
COPY Estoque/DOS.Estoque.API/*.csproj Estoque/DOS.Estoque.API/
COPY Estoque/DOS.Estoque.Application/*.csproj Estoque/DOS.Estoque.Application/
COPY Estoque/DOS.Estoque.Data/*.csproj Estoque/DOS.Estoque.Data/
COPY Estoque/DOS.Estoque.Domain/*.csproj Estoque/DOS.Estoque.Domain/

RUN dotnet restore Estoque/DOS.Estoque.API/DOS.Estoque.API.csproj

COPY . .

WORKDIR /app/Estoque/DOS.Estoque.API

RUN dotnet publish -c Release -o /out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /out ./
ENTRYPOINT ["dotnet", "DOS.Estoque.API.dll"]
