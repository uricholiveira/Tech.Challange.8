ARG DOTNET_VERSION=8.0
FROM mcr.microsoft.com/dotnet/aspnet:${DOTNET_VERSION}-alpine AS base
RUN apk add --no-cache libc6-compat musl icu-libs icu-data-full curl
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:${DOTNET_VERSION}-alpine AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY . .
RUN dotnet restore
WORKDIR "/src/src/Random.User.Api"
RUN dotnet build "Random.User.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build --no-restore

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Random.User.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false --no-restore

FROM base AS final
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Random.User.Api.dll"]