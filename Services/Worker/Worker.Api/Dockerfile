#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src
COPY ["Services/Worker/Worker.Api/Worker.Api.csproj", "Services/Worker/Worker.Api/"]
COPY ["BuildingBlocks/Framework.Core/Framework.Core.csproj", "BuildingBlocks/Framework.Core/"]
COPY ["BuildingBlocks/Framework.WebApi.Core/Framework.WebApi.Core.csproj", "BuildingBlocks/Framework.WebApi.Core/"]
COPY ["Framework.Shared.IntegrationEvent/Framework.Shared.IntegrationEvent.csproj", "Framework.Shared.IntegrationEvent/"]
COPY ["BuildingBlocks/Framework.Message.Bus/Framework.Message.Bus.csproj", "BuildingBlocks/Framework.Message.Bus/"]
COPY ["Services/Worker/Worker.Application/Worker.Application.csproj", "Services/Worker/Worker.Application/"]
COPY ["Services/Worker/Worker.Domain/Worker.Domain.csproj", "Services/Worker/Worker.Domain/"]
COPY ["Services/Worker/Worker.Infra/Worker.Infra.csproj", "Services/Worker/Worker.Infra/"]
RUN dotnet restore "Services/Worker/Worker.Api/Worker.Api.csproj"
COPY . .
WORKDIR "/src/Services/Worker/Worker.Api"
RUN dotnet build "Worker.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Worker.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Worker.Api.dll"]
