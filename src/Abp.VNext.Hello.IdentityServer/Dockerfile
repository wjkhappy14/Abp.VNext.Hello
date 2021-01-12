#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["src/Abp.VNext.Hello.IdentityServer/Abp.VNext.Hello.IdentityServer.csproj", "src/Abp.VNext.Hello.IdentityServer/"]
COPY ["src/Abp.VNext.Hello.EntityFrameworkCore.DbMigrations/Abp.VNext.Hello.EntityFrameworkCore.DbMigrations.csproj", "src/Abp.VNext.Hello.EntityFrameworkCore.DbMigrations/"]
COPY ["src/Abp.VNext.Hello.EntityFrameworkCore/Abp.VNext.Hello.EntityFrameworkCore.csproj", "src/Abp.VNext.Hello.EntityFrameworkCore/"]
COPY ["src/Abp.VNext.Hello.Domain/Abp.VNext.Hello.Domain.csproj", "src/Abp.VNext.Hello.Domain/"]
COPY ["src/Abp.VNext.Hello.Domain.Shared/Abp.VNext.Hello.Domain.Shared.csproj", "src/Abp.VNext.Hello.Domain.Shared/"]
RUN dotnet restore "src/Abp.VNext.Hello.IdentityServer/Abp.VNext.Hello.IdentityServer.csproj"
COPY . .
WORKDIR "/src/src/Abp.VNext.Hello.IdentityServer"
RUN dotnet build "Abp.VNext.Hello.IdentityServer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Abp.VNext.Hello.IdentityServer.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Abp.VNext.Hello.IdentityServer.dll"]