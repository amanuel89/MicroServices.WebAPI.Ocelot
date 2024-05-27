#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/CommonService.API/CommonService.API.csproj", "src/CommonService.API/"]
COPY ["src/CommonService.Application/CommonService.Application.csproj", "src/CommonService.Application/"]
COPY ["CommonService.Infrastructure/CommonService.Infrastructure.csproj", "CommonService.Infrastructure/"]
RUN dotnet restore "src/CommonService.API/CommonService.API.csproj"
COPY . .
WORKDIR "/src/src/CommonService.API"
RUN dotnet build "CommonService.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CommonService.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CommonService.API.dll"]