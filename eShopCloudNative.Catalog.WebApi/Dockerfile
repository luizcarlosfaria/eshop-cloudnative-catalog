#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ./nuget.config ./
COPY ["eShopCloudNative.Catalog.Contracts/eShopCloudNative.Catalog.Contracts.csproj", "eShopCloudNative.Catalog.Contracts/"]
COPY ["eShopCloudNative.Catalog.WebApi/eShopCloudNative.Catalog.WebApi.csproj", "eShopCloudNative.Catalog.WebApi/"]
RUN dotnet restore "eShopCloudNative.Catalog.WebApi/eShopCloudNative.Catalog.WebApi.csproj"
COPY . .
WORKDIR "/src/eShopCloudNative.Catalog.WebApi"
RUN dotnet build "eShopCloudNative.Catalog.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "eShopCloudNative.Catalog.WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "eShopCloudNative.Catalog.WebApi.dll"]

