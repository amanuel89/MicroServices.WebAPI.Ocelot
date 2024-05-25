#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/RideBackend.API/RideBackend.API.csproj", "src/RideBackend.API/"]
COPY ["src/RideBackend.Application/RideBackend.Application.csproj", "src/RideBackend.Application/"]
COPY ["RideBackend.Infrastructure/RideBackend.Infrastructure.csproj", "RideBackend.Infrastructure/"]
RUN dotnet restore "src/RideBackend.API/RideBackend.API.csproj"
COPY . .
WORKDIR "/src/src/RideBackend.API"
RUN dotnet build "RideBackend.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RideBackend.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RideBackend.API.dll"]