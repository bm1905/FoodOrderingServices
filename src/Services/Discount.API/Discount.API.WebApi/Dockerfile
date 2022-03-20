#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Services/Discount.API/Discount.API.WebApi/Discount.API.WebApi.csproj", "Services/Discount.API/Discount.API.WebApi/"]
COPY ["BuildingBlocks/Common.ServiceDiscovery/Common.ServiceDiscovery.csproj", "BuildingBlocks/Common.ServiceDiscovery/"]
COPY ["Services/Discount.API/Discount.API.Application/Discount.API.Application.csproj", "Services/Discount.API/Discount.API.Application/"]
COPY ["Services/Discount.API/Discount.API.DataAccess/Discount.API.DataAccess.csproj", "Services/Discount.API/Discount.API.DataAccess/"]
COPY ["Services/Discount.API/Discount.API.Core/Discount.API.Core.csproj", "Services/Discount.API/Discount.API.Core/"]
COPY ["BuildingBlocks/Common.Logging/Common.Logging.csproj", "BuildingBlocks/Common.Logging/"]
RUN dotnet restore "Services/Discount.API/Discount.API.WebApi/Discount.API.WebApi.csproj"
COPY . .
WORKDIR "/src/Services/Discount.API/Discount.API.WebApi"
RUN dotnet build "Discount.API.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Discount.API.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Discount.API.WebApi.dll"]
