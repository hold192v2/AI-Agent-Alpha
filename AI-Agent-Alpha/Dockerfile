FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["AI-Agent-Alpha/AI-Agent-Alpha.csproj", "AI-Agent-Alpha/"]
RUN dotnet restore "AI-Agent-Alpha/AI-Agent-Alpha.csproj"
COPY . .
WORKDIR "/src/AI-Agent-Alpha"
RUN dotnet build "./AI-Agent-Alpha.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./AI-Agent-Alpha.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AI-Agent-Alpha.dll"]
