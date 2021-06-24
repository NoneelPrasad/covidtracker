#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app


FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["COVID19.Tracker/COVID19.Tracker.csproj", "COVID19.Tracker/"]
COPY ["COVID19.Tracker.Data/COVID19.Tracker.Data.csproj", "COVID19.Tracker.Data/"]
COPY ["COVID19.Tracker.Core/COVID19.Tracker.Core.csproj", "COVID19.Tracker.Core/"]
RUN dotnet restore "COVID19.Tracker/COVID19.Tracker.csproj"
COPY . .
WORKDIR "/src/COVID19.Tracker"
RUN dotnet build "COVID19.Tracker.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "COVID19.Tracker.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "COVID19.Tracker.dll"]