#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["src/Thor.SSO.HttpApi.Host/Thor.SSO.HttpApi.Host.csproj", "src/Thor.SSO.HttpApi.Host/"]
COPY ["src/Thor.SSO.Application/Thor.SSO.Application.csproj", "src/Thor.SSO.Application/"]
COPY ["src/Thor.SSO.Application.Contracts/Thor.SSO.Application.Contracts.csproj", "src/Thor.SSO.Application.Contracts/"]
COPY ["src/Thor.SSO.Domain.Shared/Thor.SSO.Domain.Shared.csproj", "src/Thor.SSO.Domain.Shared/"]
COPY ["src/Thor.SSO.Domain/Thor.SSO.Domain.csproj", "src/Thor.SSO.Domain/"]
COPY ["src/Thor.SSO.EntityFrameworkCore.DbMigrations/Thor.SSO.EntityFrameworkCore.DbMigrations.csproj", "src/Thor.SSO.EntityFrameworkCore.DbMigrations/"]
COPY ["src/Thor.SSO.EntityFrameworkCore/Thor.SSO.EntityFrameworkCore.csproj", "src/Thor.SSO.EntityFrameworkCore/"]
COPY ["src/Thor.SSO.HttpApi/Thor.SSO.HttpApi.csproj", "src/Thor.SSO.HttpApi/"]
RUN dotnet restore "src/Thor.SSO.HttpApi.Host/Thor.SSO.HttpApi.Host.csproj"
COPY . .
WORKDIR "/src/src/Thor.SSO.HttpApi.Host"
RUN dotnet build "Thor.SSO.HttpApi.Host.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Thor.SSO.HttpApi.Host.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Thor.SSO.HttpApi.Host.dll"]
