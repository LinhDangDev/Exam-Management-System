
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["GettingStarted/Server/GettingStarted.Server.csproj", "GettingStarted/Server/"]
COPY ["GettingStarted/Client/GettingStarted.Client.csproj", "GettingStarted/Client/"]
COPY ["GettingStarted/Shared/GettingStarted.Shared.csproj", "GettingStarted/Shared/"]
RUN dotnet restore "./GettingStarted/Server/GettingStarted.Server.csproj"
COPY . .
WORKDIR "/src/GettingStarted/Server"
RUN dotnet build "./GettingStarted.Server.csproj" -c %BUILD_CONFIGURATION% -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./GettingStarted.Server.csproj" -c %BUILD_CONFIGURATION% -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY ["GettingStarted/Server/appsettings.json", "/app/"]  # Thêm dòng này để sao chép appsettings.json vào container
ENTRYPOINT ["dotnet", "GettingStarted.Server.dll"]s