#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
ENV TZ=America/Sao_Paulo
RUN cat /usr/share/zoneinfo/$TZ > /etc/localtime \
    && cat /usr/share/zoneinfo/$TZ > /etc/timezone
WORKDIR /app
WORKDIR /app
RUN apt-get update && apt-get install lsb-release curl -y
EXPOSE 80
#EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ./nuget.config ./
COPY ["eShopCloudNative.Catalog.Contracts/eShopCloudNative.Catalog.Contracts.csproj", "eShopCloudNative.Catalog.Contracts/"]
COPY ["eShopCloudNative.Catalog.Core/eShopCloudNative.Catalog.Core.csproj", "eShopCloudNative.Catalog.Core/"]
COPY ["eShopCloudNative.Catalog.Adapters/eShopCloudNative.Catalog.Adapters.csproj", "eShopCloudNative.Catalog.Adapters/"]
COPY ["eShopCloudNative.Catalog.ServiceHost/eShopCloudNative.Catalog.ServiceHost.csproj", "eShopCloudNative.Catalog.ServiceHost/"]
RUN dotnet restore "eShopCloudNative.Catalog.ServiceHost/eShopCloudNative.Catalog.ServiceHost.csproj"
COPY . .
WORKDIR "/src/eShopCloudNative.Catalog.ServiceHost"
RUN dotnet build "eShopCloudNative.Catalog.ServiceHost.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "eShopCloudNative.Catalog.ServiceHost.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "eShopCloudNative.Catalog.ServiceHost.dll"]

