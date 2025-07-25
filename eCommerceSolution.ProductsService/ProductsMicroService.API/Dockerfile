# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["eCommerceSolution.ProductsService/eCommerceSolution.ProductsService.sln","eCommerceSolution.ProductsService.sln"]
COPY ["eCommerceSolution.ProductsService/docker-compose.dcproj","docker-compose.dcproj"]
COPY ["eCommerceSolution.ProductsService/ProductsMicroService.API/ProductsMicroService.API.csproj", "ProductsMicroService.API/ProductsMicroService.API.csproj"]
COPY ["eCommerceSolution.ProductsService/BusinessLogicLayer/BusinessLogicLayer.csproj", "BusinessLogicLayer/BusinessLogicLayer.csproj"]
COPY ["eCommerceSolution.ProductsService/DataAccessLayer/DataAccessLayer.csproj", "DataAccessLayer/DataAccessLayer.csproj"]
RUN dotnet restore "eCommerceSolution.ProductsService.sln"

COPY ["eCommerceSolution.ProductsService/ProductsMicroService.API", "ProductsMicroService.API"]
COPY ["eCommerceSolution.ProductsService/BusinessLogicLayer", "BusinessLogicLayer"]
COPY ["eCommerceSolution.ProductsService/DataAccessLayer", "DataAccessLayer"]

WORKDIR "/src/ProductsMicroService.API"
RUN dotnet build "./ProductsMicroService.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ProductsMicroService.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ProductsMicroService.API.dll"]