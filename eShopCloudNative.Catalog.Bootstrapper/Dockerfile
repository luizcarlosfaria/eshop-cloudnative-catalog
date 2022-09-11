#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app
RUN mkdir /app/Assets
COPY eShopCloudNative.Catalog.Bootstrapper/Assets /app/Assets

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ./nuget.config ./
COPY ["eShopCloudNative.Catalog.Bootstrapper/eShopCloudNative.Catalog.Bootstrapper.csproj", "eShopCloudNative.Catalog.Bootstrapper/"]
RUN dotnet restore "eShopCloudNative.Catalog.Bootstrapper/eShopCloudNative.Catalog.Bootstrapper.csproj"
COPY . .
WORKDIR "/src/eShopCloudNative.Catalog.Bootstrapper"
RUN dotnet build "eShopCloudNative.Catalog.Bootstrapper.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "eShopCloudNative.Catalog.Bootstrapper.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "eShopCloudNative.Catalog.Bootstrapper.dll"]