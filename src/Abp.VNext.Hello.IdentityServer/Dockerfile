#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Abp.VNext.Hello.IdentityServer/Abp.VNext.Hello.IdentityServer.csproj", "src/Abp.VNext.Hello.IdentityServer/"]
COPY ["dapr-sdk-1.4.0/Dapr.Actors.AspNetCore/Dapr.Actors.AspNetCore.csproj", "dapr-sdk-1.4.0/Dapr.Actors.AspNetCore/"]
COPY ["dapr-sdk-1.4.0/Dapr.Actors/Dapr.Actors.csproj", "dapr-sdk-1.4.0/Dapr.Actors/"]
COPY ["dapr-sdk-1.4.0/Dapr.Client/Dapr.Client.csproj", "dapr-sdk-1.4.0/Dapr.Client/"]
COPY ["dapr-sdk-1.4.0/Dapr.AspNetCore/Dapr.AspNetCore.csproj", "dapr-sdk-1.4.0/Dapr.AspNetCore/"]
COPY ["dapr-sdk-1.4.0/Dapr.Extensions.Configuration/Dapr.Extensions.Configuration.csproj", "dapr-sdk-1.4.0/Dapr.Extensions.Configuration/"]
COPY ["src/Abp.VNext.Hello.EntityFrameworkCore.DbMigrations/Abp.VNext.Hello.EntityFrameworkCore.DbMigrations.csproj", "src/Abp.VNext.Hello.EntityFrameworkCore.DbMigrations/"]
COPY ["src/Abp.VNext.Hello.EntityFrameworkCore/Abp.VNext.Hello.EntityFrameworkCore.csproj", "src/Abp.VNext.Hello.EntityFrameworkCore/"]
COPY ["src/Abp.VNext.Hello.Domain/Abp.VNext.Hello.Domain.csproj", "src/Abp.VNext.Hello.Domain/"]
COPY ["src/Abp.VNext.Hello.Domain.Shared/Abp.VNext.Hello.Domain.Shared.csproj", "src/Abp.VNext.Hello.Domain.Shared/"]
COPY ["docker-compose.dcproj", "./"]
COPY ["NuGet.config", "./"]
COPY ["Abp.VNext.Hello.sln", "./"]
RUN dotnet restore "src/Abp.VNext.Hello.IdentityServer/Abp.VNext.Hello.IdentityServer.csproj"

COPY . .
WORKDIR "/src/src/Abp.VNext.Hello.IdentityServer"
# RUN dotnet build "Abp.VNext.Hello.IdentityServer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish --no-restore "Abp.VNext.Hello.IdentityServer.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Abp.VNext.Hello.IdentityServer.dll"]