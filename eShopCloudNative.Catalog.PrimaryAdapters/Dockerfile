#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
ENV TZ=America/Sao_Paulo
RUN cat /usr/share/zoneinfo/$TZ > /etc/localtime \
    && cat /usr/share/zoneinfo/$TZ > /etc/timezone
WORKDIR /app
WORKDIR /app
RUN mkdir /app/Assets
RUN apt-get update && apt-get install lsb-release curl -y
COPY eShopCloudNative.Catalog.PrimaryAdapters/Assets /app/Assets
EXPOSE 80
#EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ./nuget.config ./
COPY ["eShopCloudNative.Catalog.Contracts/eShopCloudNative.Catalog.Contracts.csproj", "eShopCloudNative.Catalog.Contracts/"]
COPY ["eShopCloudNative.Catalog.Core/eShopCloudNative.Catalog.Core.csproj", "eShopCloudNative.Catalog.Core/"]
COPY ["eShopCloudNative.Catalog.SecondaryAdapters/eShopCloudNative.Catalog.SecondaryAdapters.csproj", "eShopCloudNative.Catalog.SecondaryAdapters/"]
COPY ["eShopCloudNative.Catalog.PrimaryAdapters/eShopCloudNative.Catalog.PrimaryAdapters.csproj", "eShopCloudNative.Catalog.PrimaryAdapters/"]
RUN dotnet restore "eShopCloudNative.Catalog.PrimaryAdapters/eShopCloudNative.Catalog.PrimaryAdapters.csproj"
COPY . .
WORKDIR "/src/eShopCloudNative.Catalog.PrimaryAdapters"
RUN dotnet build "eShopCloudNative.Catalog.PrimaryAdapters.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "eShopCloudNative.Catalog.PrimaryAdapters.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "eShopCloudNative.Catalog.PrimaryAdapters.dll"]

