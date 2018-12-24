FROM kristofilip/openalpr-dotnet2.2 AS base
WORKDIR /app
EXPOSE 5000

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY OpenAlprDotNetService/OpenAlprDotNetService.csproj OpenAlprDotNetService/
RUN dotnet restore OpenAlprDotNetService/OpenAlprDotNetService.csproj
COPY . .
WORKDIR /src/OpenAlprDotNetService
RUN dotnet build OpenAlprDotNetService.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish OpenAlprDotNetService.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "OpenAlprDotNetService.dll"]
