#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
# ARG BUILD_CONFIGURATION=Release

WORKDIR /src
COPY ["Services/Activities/Activities.Api/Activities.Api.csproj", "Services/Activities/Activities.Api/"]
COPY ["BuildingBlocks/Framework.Core/Framework.Core.csproj", "BuildingBlocks/Framework.Core/"]
COPY ["BuildingBlocks/Framework.WebApi.Core/Framework.WebApi.Core.csproj", "BuildingBlocks/Framework.WebApi.Core/"]
COPY ["Services/Activities/Activities.Application/Activities.Application.csproj", "Services/Activities/Activities.Application/"]
COPY ["Framework.Shared.IntegrationEvent/Framework.Shared.IntegrationEvent.csproj", "Framework.Shared.IntegrationEvent/"]
COPY ["BuildingBlocks/Framework.Message.Bus/Framework.Message.Bus.csproj", "BuildingBlocks/Framework.Message.Bus/"]
COPY ["Services/Activities/Activities.Domain/Activities.Domain.csproj", "Services/Activities/Activities.Domain/"]
COPY ["Services/Activities/Activities.Infra/Activities.Infra.csproj", "Services/Activities/Activities.Infra/"]
RUN dotnet restore "Services/Activities/Activities.Api/Activities.Api.csproj"
COPY . .
WORKDIR "/src/Services/Activities/Activities.Api"
RUN dotnet build "Activities.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Activities.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Activities.Api.dll"]
