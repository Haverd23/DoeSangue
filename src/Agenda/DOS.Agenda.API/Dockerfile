FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY Core/DOS.Core/*.csproj Core/DOS.Core/
COPY Agenda/DOS.Agenda.API/*.csproj Agenda/DOS.Agenda.API/
COPY Agenda/DOS.Agenda.Application/*.csproj Agenda/DOS.Agenda.Application/
COPY Agenda/DOS.Agenda.Data/*.csproj Agenda/DOS.Agenda.Data/
COPY Agenda/DOS.Agenda.Domain/*.csproj Agenda/DOS.Agenda.Domain/

RUN dotnet restore Agenda/DOS.Agenda.API/DOS.Agenda.API.csproj

COPY . .

WORKDIR /app/Agenda/DOS.Agenda.API

RUN dotnet publish -c Release -o /out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /out ./
ENTRYPOINT ["dotnet", "DOS.Agenda.API.dll"]
