﻿FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["GRPCTest/GRPCTest.csproj", "GRPCTest/"]
RUN dotnet restore "GRPCTest/GRPCTest.csproj"
COPY . .
WORKDIR "/src/GRPCTest"
RUN dotnet build "GRPCTest.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "GRPCTest.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

RUN mkdir -p /app/certificates
COPY GRPCTest/Cerificates/aspnetapp.pfx /app/certificates/

ENTRYPOINT ["dotnet", "GRPCTest.dll"]
