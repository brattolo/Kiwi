FROM microsoft/dotnet:2.2-aspnetcore-runtime-stretch-slim AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.2-sdk-stretch AS build
WORKDIR /src
COPY Kiwi.API/Kiwi.API.csproj Kiwi.API/
RUN dotnet restore Kiwi.API/Kiwi.API.csproj
COPY . .
WORKDIR /src/Kiwi.API
RUN dotnet build Kiwi.API.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish Kiwi.API.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Kiwi.API.dll"]
