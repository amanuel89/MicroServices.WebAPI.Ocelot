#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/ConsigneService.API/ConsigneService.API.csproj", "src/ConsigneService.API/"]
COPY ["src/ConsigneService.Application/ConsigneService.Application.csproj", "src/ConsigneService.Application/"]
COPY ["ConsigneService.Infrastructure/ConsigneService.Infrastructure.csproj", "ConsigneService.Infrastructure/"]
RUN dotnet restore "src/ConsigneService.API/ConsigneService.API.csproj"
COPY . .
WORKDIR "/src/src/ConsigneService.API"
RUN dotnet build "ConsigneService.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ConsigneService.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ConsigneService.API.dll"]