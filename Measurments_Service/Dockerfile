#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Set the ARG to a default value in case it's not provided during the build
ARG DB_MEASUREMENT_CONNECTION_STRING="Host=Measurement-db;Port=5433;Database=Measurementdb;Username=postgres;Password=password"

# Use the ARG value in your application
ENV DB_MEASUREMENT_CONNECTION_STRING=$DB_MEASUREMENT_CONNECTION_STRING

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Add the ARG instruction for the connection string
ARG DB_MEASUREMENT_CONNECTION_STRING

COPY ["Measurments_Service/Measurments_Service.csproj", "Measurments_Service/"]
COPY ["Domain/Domain.csproj", "Domain/"]

# Update the dotnet restore command to pass the connection string
RUN dotnet restore "./Measurments_Service/Measurments_Service.csproj" --runtime $DOTNET_RUNTIME --p:RestoreConfigFile=NuGet.Config --no-cache /p:DefaultConnection="${DB_MEASUREMENT_CONNECTION_STRING}"

COPY . .
WORKDIR "/src/Measurments_Service"
RUN dotnet build "./Measurments_Service.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Measurments_Service.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Measurments_Service.dll"]
