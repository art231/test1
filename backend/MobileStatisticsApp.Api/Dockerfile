#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["MobileStatisticsApp.Api/MobileStatisticsApp.Api.csproj", "MobileStatisticsApp.Api/"]
COPY ["MobileStatisticsApp.Application/MobileStatisticsApp.Application.csproj", "MobileStatisticsApp.Application/"]
COPY ["MobileStatisticsApp.Core/MobileStatisticsApp.Core.csproj", "MobileStatisticsApp.Core/"]
COPY ["MobileStatisticsApp.IoC/MobileStatisticsApp.IoC.csproj", "MobileStatisticsApp.IoC/"]
COPY ["MobileStatistics.Infrastructure/MobileStatisticsApp.Infrastructure.csproj", "MobileStatistics.Infrastructure/"]
RUN dotnet restore "MobileStatisticsApp.Api/MobileStatisticsApp.Api.csproj"
COPY . .
WORKDIR "/src/MobileStatisticsApp.Api"
RUN dotnet build "MobileStatisticsApp.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MobileStatisticsApp.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MobileStatisticsApp.Api.dll"]