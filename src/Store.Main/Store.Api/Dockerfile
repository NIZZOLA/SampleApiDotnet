#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Store.Api.csproj", "Store.Api/"]
COPY ["Store.Application/Store.Application.csproj", "Store.Application/"]
COPY ["Store.Business/Store.Business.csproj", "Store.Business/"]
COPY ["Store.Domain/Store.Domain.csproj", "Store.Domain/"]
COPY ["Store.Infra.Data/Store.Infra.Data.csproj", "Store.Infra.Data/"]
RUN dotnet restore "./Store.Api/Store.Api.csproj"
COPY . .
WORKDIR "/src/Store.Api"
RUN dotnet build "./Store.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Store.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Store.Api.dll"]