#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/Sydy.Gambling.Football.Web.API/Sydy.Gambling.Football.Web.API.csproj", "src/Sydy.Gambling.Football.Web.API/"]
COPY ["src/Sydy.Gambling.Football/Sydy.Gambling.Football.csproj", "src/Sydy.Gambling.Football/"]
RUN dotnet restore "src/Sydy.Gambling.Football.Web.API/Sydy.Gambling.Football.Web.API.csproj"
COPY . .
WORKDIR "/src/src/Sydy.Gambling.Football.Web.API"
RUN dotnet build "Sydy.Gambling.Football.Web.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Sydy.Gambling.Football.Web.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Sydy.Gambling.Football.Web.API.dll"]