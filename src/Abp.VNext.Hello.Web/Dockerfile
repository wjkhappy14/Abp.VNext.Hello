#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["src/Abp.VNext.Hello.Web/Abp.VNext.Hello.Web.csproj", "src/Abp.VNext.Hello.Web/"]
COPY ["src/Abp.VNext.Hello.HttpApi/Abp.VNext.Hello.HttpApi.csproj", "src/Abp.VNext.Hello.HttpApi/"]
COPY ["src/Abp.VNext.Hello.Application/Abp.VNext.Hello.Application.csproj", "src/Abp.VNext.Hello.Application/"]
COPY ["src/Abp.VNext.Hello.Domain/Abp.VNext.Hello.Domain.csproj", "src/Abp.VNext.Hello.Domain/"]
COPY ["src/Abp.VNext.Hello.Domain.Shared/Abp.VNext.Hello.Domain.Shared.csproj", "src/Abp.VNext.Hello.Domain.Shared/"]
COPY ["src/Abp.VNext.Hello.Application.Contracts/Abp.VNext.Hello.Application.Contracts.csproj", "src/Abp.VNext.Hello.Application.Contracts/"]
COPY ["src/Abp.VNext.Hello.HttpApi.Client/Abp.VNext.Hello.HttpApi.Client.csproj", "src/Abp.VNext.Hello.HttpApi.Client/"]
RUN dotnet restore "src/Abp.VNext.Hello.Web/Abp.VNext.Hello.Web.csproj"
COPY . .
WORKDIR "/src/src/Abp.VNext.Hello.Web"
RUN dotnet build "Abp.VNext.Hello.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Abp.VNext.Hello.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Abp.VNext.Hello.Web.dll"]
