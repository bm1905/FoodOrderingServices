FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Services/Catalog.API/Catalog.API.WebApi/Catalog.API.WebApi.csproj", "Services/Catalog.API/Catalog.API.WebApi/"]
COPY ["Services/Catalog.API/Catalog.API.Application/Catalog.API.Application.csproj", "Services/Catalog.API/Catalog.API.Application/"]
COPY ["Services/Catalog.API/Catalog.API.DataAccess/Catalog.API.DataAccess.csproj", "Services/Catalog.API/Catalog.API.DataAccess/"]
COPY ["Services/Catalog.API/Catalog.API.Core/Catalog.API.Core.csproj", "Services/Catalog.API/Catalog.API.Core/"]
COPY ["BuildingBlocks/Common.Logging/Common.Logging.csproj", "BuildingBlocks/Common.Logging/"]
RUN dotnet restore "Services/Catalog.API/Catalog.API.WebApi/Catalog.API.WebApi.csproj"
COPY . .
WORKDIR "/src/Services/Catalog.API/Catalog.API.WebApi"
RUN dotnet build "Catalog.API.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Catalog.API.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Catalog.API.WebApi.dll"]
