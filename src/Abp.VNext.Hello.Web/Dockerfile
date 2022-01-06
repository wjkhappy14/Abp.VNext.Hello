#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Abp.VNext.Hello.Web/Abp.VNext.Hello.Web.csproj", "src/Abp.VNext.Hello.Web/"]
COPY ["src/Abp.VNext.Hello.HttpApi/Abp.VNext.Hello.HttpApi.csproj", "src/Abp.VNext.Hello.HttpApi/"]
COPY ["dapr-sdk-1.4.0/Dapr.Actors.AspNetCore/Dapr.Actors.AspNetCore.csproj", "dapr-sdk-1.4.0/Dapr.Actors.AspNetCore/"]
COPY ["dapr-sdk-1.4.0/Dapr.Actors/Dapr.Actors.csproj", "dapr-sdk-1.4.0/Dapr.Actors/"]
COPY ["dapr-sdk-1.4.0/Dapr.Client/Dapr.Client.csproj", "dapr-sdk-1.4.0/Dapr.Client/"]
COPY ["src/Abp.VNext.Hello.Application/Abp.VNext.Hello.Application.csproj", "src/Abp.VNext.Hello.Application/"]
COPY ["src/Abp.VNext.Hello.Domain/Abp.VNext.Hello.Domain.csproj", "src/Abp.VNext.Hello.Domain/"]
COPY ["src/Abp.VNext.Hello.Domain.Shared/Abp.VNext.Hello.Domain.Shared.csproj", "src/Abp.VNext.Hello.Domain.Shared/"]
COPY ["src/Abp.VNext.Hello.Application.Contracts/Abp.VNext.Hello.Application.Contracts.csproj", "src/Abp.VNext.Hello.Application.Contracts/"]
COPY ["dapr-sdk-1.4.0/Dapr.AspNetCore/Dapr.AspNetCore.csproj", "dapr-sdk-1.4.0/Dapr.AspNetCore/"]
COPY ["src/Abp.VNext.Hello.HttpApi.Client/Abp.VNext.Hello.HttpApi.Client.csproj", "src/Abp.VNext.Hello.HttpApi.Client/"]
COPY ["src/Hello.Authentication.MiniProgram.WeChat/Hello.Authentication.MiniProgram.WeChat.csproj", "src/Hello.Authentication.MiniProgram.WeChat/"]
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
