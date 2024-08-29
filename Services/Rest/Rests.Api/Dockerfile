#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
# ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Services/Rest/Rests.Api/Rests.Api.csproj", "Services/Rest/Rests.Api/"]
COPY ["BuildingBlocks/Framework.Core/Framework.Core.csproj", "BuildingBlocks/Framework.Core/"]
COPY ["BuildingBlocks/Framework.WebApi.Core/Framework.WebApi.Core.csproj", "BuildingBlocks/Framework.WebApi.Core/"]
COPY ["Framework.Shared.IntegrationEvent/Framework.Shared.IntegrationEvent.csproj", "Framework.Shared.IntegrationEvent/"]
COPY ["BuildingBlocks/Framework.Message.Bus/Framework.Message.Bus.csproj", "BuildingBlocks/Framework.Message.Bus/"]
COPY ["Services/Rest/Rests.Application/Rests.Application.csproj", "Services/Rest/Rests.Application/"]
COPY ["Services/Rest/Rests.Domain/Rests.Domain.csproj", "Services/Rest/Rests.Domain/"]
COPY ["Services/Rest/Rests.Infra/Rests.Infra.csproj", "Services/Rest/Rests.Infra/"]
RUN dotnet restore "./Services/Rest/Rests.Api/./Rests.Api.csproj"
COPY . .
WORKDIR "/src/Services/Rest/Rests.Api"
RUN dotnet build "./Rests.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Rests.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Rests.Api.dll"]
