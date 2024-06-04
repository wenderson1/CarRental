#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8082
EXPOSE 8083

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["User.Api/User.Api.csproj", "User.Api/"]
COPY ["User.Application/User.Application.csproj", "User.Application/"]
COPY ["User.Core/User.Core.csproj", "User.Core/"]
COPY ["User.Infrastructure/User.Infrastructure.csproj", "User.Infrastructure/"]
RUN dotnet restore "./User.Api/User.Api.csproj"
COPY . .
WORKDIR "/src/User.Api"
RUN dotnet build "./User.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./User.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "User.Api.dll"]