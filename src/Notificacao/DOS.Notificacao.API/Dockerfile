FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY Core/DOS.Core/*.csproj Core/DOS.Core/
COPY Notificacao/DOS.Notificacao.API/*.csproj Notificacao/DOS.Notificacao.API/
COPY Notificacao/DOS.Notificacao.Application/*.csproj Notificacao/DOS.Notificacao.Application/
COPY Notificacao/DOS.Notificacao.Data/*.csproj Notificacao/DOS.Notificacao.Data/
COPY Notificacao/DOS.Notificacao.Domain/*.csproj Notificacao/DOS.Notificacao.Domain/

RUN dotnet restore Notificacao/DOS.Notificacao.API/DOS.Notificacao.API.csproj

COPY . .

WORKDIR /app/Notificacao/DOS.Notificacao.API

RUN dotnet publish -c Release -o /out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /out ./
ENTRYPOINT ["dotnet", "DOS.Notificacao.API.dll"]
