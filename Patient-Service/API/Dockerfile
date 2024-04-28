#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Set the ARG to a default value in case it's not provided during the build
ARG DB_PATIENT_CONNECTION_STRING="Host=patient-db;Port=5432;Database=patientdb;Username=postgres;Password=password"

# Use the ARG value in your application
ENV DB_PATIENT_CONNECTION_STRING=$DB_PATIENT_CONNECTION_STRING

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Patient-Service/API/API.csproj", "Patient-Service/API/"]
COPY ["Domain/Domain.csproj", "Domain/"]
COPY ["Patient-Repo/Patient-Repo.csproj", "Patient-Repo/"]
COPY ["PatientService/PatientService.csproj", "PatientService/"]
RUN dotnet restore "./Patient-Service/API/./API.csproj"
COPY . .
WORKDIR "/src/Patient-Service/API"
RUN dotnet build "./API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "API.dll"]