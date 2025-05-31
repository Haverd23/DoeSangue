FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY Core/DOS.Core/*.csproj Core/DOS.Core/
COPY Doacao/DOS.Doacao.API/*.csproj Doacao/DOS.Doacao.API/
COPY Doacao/DOS.Doacao.Application/*.csproj Doacao/DOS.Doacao.Application/
COPY Doacao/DOS.Doacao.Data/*.csproj Doacao/DOS.Doacao.Data/
COPY Doacao/DOS.Doacao.Domain/*.csproj Doacao/DOS.Doacao.Domain/

RUN dotnet restore Doacao/DOS.Doacao.API/DOS.Doacao.API.csproj

COPY . .

WORKDIR /app/Doacao/DOS.Doacao.API

RUN dotnet publish -c Release -o /out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /out ./
ENTRYPOINT ["dotnet", "DOS.Doacao.API.dll"]
