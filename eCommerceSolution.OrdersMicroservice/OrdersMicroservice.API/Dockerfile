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
COPY ["eCommerceSolution.OrdersMicroservice/eCommerceSolution.OrdersMicroservice.sln", "eCommerceSolution.OrdersMicroservice.sln"]
COPY ["eCommerceSolution.OrdersMicroservice/OrdersMicroservice.API/OrdersMicroservice.API.csproj", "OrdersMicroservice.API/OrdersMicroservice.API.csproj"]
COPY ["eCommerceSolution.OrdersMicroservice/BusinessLogicLayer/BusinessLogicLayer.csproj", "BusinessLogicLayer/BusinessLogicLayer.csproj"]
COPY ["eCommerceSolution.OrdersMicroservice/DataAccessLayer/DataAccessLayer.csproj", "DataAccessLayer/DataAccessLayer.csproj"]
RUN dotnet restore "eCommerceSolution.OrdersMicroservice.sln"

COPY ["eCommerceSolution.OrdersMicroservice/OrdersMicroservice.API", "OrdersMicroservice.API"]
COPY ["eCommerceSolution.OrdersMicroservice/BusinessLogicLayer", "BusinessLogicLayer"]
COPY ["eCommerceSolution.OrdersMicroservice/DataAccessLayer", "DataAccessLayer"]

WORKDIR "/src/OrdersMicroservice.API"
RUN dotnet build "./OrdersMicroservice.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./OrdersMicroservice.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OrdersMicroservice.API.dll"]