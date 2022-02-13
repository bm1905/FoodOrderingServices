#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Services/Catalog.API/Catalog.API.Services/Catalog.API.Services.csproj", "Services/Catalog.API/Catalog.API.Services/"]
COPY ["Services/Catalog.API/Catalog.API.Helpers/Catalog.API.Helpers.csproj", "Services/Catalog.API/Catalog.API.Helpers/"]
COPY ["Services/Catalog.API/Catalog.API.Model/Catalog.API.Model.csproj", "Services/Catalog.API/Catalog.API.Model/"]
COPY ["Services/Catalog.API/Catalog.API.BLL/Catalog.API.BLL.csproj", "Services/Catalog.API/Catalog.API.BLL/"]
COPY ["Services/Catalog.API/Catalog.API.DAL/Catalog.API.DAL.csproj", "Services/Catalog.API/Catalog.API.DAL/"]
COPY ["BuildingBlocks/Common.Logging/Common.Logging.csproj", "BuildingBlocks/Common.Logging/"]
RUN dotnet restore "Services/Catalog.API/Catalog.API.Services/Catalog.API.Services.csproj"
COPY . .
WORKDIR "/src/Services/Catalog.API/Catalog.API.Services"
RUN dotnet build "Catalog.API.Services.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Catalog.API.Services.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Catalog.API.Services.dll"]