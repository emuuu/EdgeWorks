FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-nanoserver-1809 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-nanoserver-1809 AS build
WORKDIR /src
COPY EdgeWorks.AuctionHouse.Scraper/EdgeWorks.AuctionHouse.Scraper.csproj EdgeWorks.AuctionHouse.Scraper/
COPY EdgeWorks.Data/EdgeWorks.Data.csproj EdgeWorks.Data/
COPY EdgeWorks.Models/EdgeWorks.Models.csproj EdgeWorks.Models/
COPY EdgeWorks.Shared/EdgeWorks.Shared.csproj EdgeWorks.Shared/
RUN dotnet restore EdgeWorks.AuctionHouse.Scraper/EdgeWorks.AuctionHouse.Scraper.csproj
COPY . .
WORKDIR /src/EdgeWorks.AuctionHouse.Scraper
RUN dotnet build EdgeWorks.AuctionHouse.Scraper.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish EdgeWorks.AuctionHouse.Scraper.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .

ENTRYPOINT ["dotnet", "EdgeWorks.AuctionHouse.Scraper.dll"]
