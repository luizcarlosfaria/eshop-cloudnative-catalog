#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["catalog/eShopCloudNative.Catalog.Bootstrapper/eShopCloudNative.Catalog.Bootstrapper.csproj", "catalog/eShopCloudNative.Catalog.Bootstrapper/"]
RUN dotnet restore "catalog/eShopCloudNative.Catalog.Bootstrapper/eShopCloudNative.Catalog.Bootstrapper.csproj"
COPY . .
WORKDIR "/src/catalog/eShopCloudNative.Catalog.Bootstrapper"
RUN dotnet build "eShopCloudNative.Catalog.Bootstrapper.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "eShopCloudNative.Catalog.Bootstrapper.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "eShopCloudNative.Catalog.Bootstrapper.dll"]