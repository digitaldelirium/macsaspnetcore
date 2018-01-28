FROM microsoft/aspnetcore:2.0 AS base
WORKDIR /app
EXPOSE 443 80

FROM microsoft/aspnetcore-build:2.0 AS build
WORKDIR /src
COPY MacsASPNETCore.csproj .
RUN dotnet restore
COPY . .
WORKDIR /src/
RUN dotnet build -c Debug -o /app *.csproj

FROM build AS publish
RUN dotnet publish -c Debug -o /app *.csproj

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
RUN echo "" /app/Macs-Dev.pfx
ENTRYPOINT ["dotnet", "MacsASPNETCore.dll"]
