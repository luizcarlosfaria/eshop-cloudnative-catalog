#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["eShopCloudNative.Catalog.WebUI/eShopCloudNative.Catalog.WebUI.csproj", "eShopCloudNative.Catalog.WebUI/"]
RUN dotnet restore "eShopCloudNative.Catalog.WebUI/eShopCloudNative.Catalog.WebUI.csproj"
COPY . .
WORKDIR "/src/eShopCloudNative.Catalog.WebUI"
RUN dotnet build "eShopCloudNative.Catalog.WebUI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "eShopCloudNative.Catalog.WebUI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "eShopCloudNative.Catalog.WebUI.dll"]