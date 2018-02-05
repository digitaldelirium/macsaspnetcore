FROM microsoft/aspnetcore:2.0 AS base
WORKDIR /app

FROM microsoft/aspnetcore-build:2.0 AS build
WORKDIR /src
COPY MacsASPNETCore.csproj .
RUN dotnet restore
COPY . .
WORKDIR /src/
RUN dotnet build -c #{BuildConfiguration}# -o /app *.csproj

FROM build AS publish
RUN dotnet publish -c #{BuildConfiguration}# -o /app *.csproj

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
COPY ./Macs-Dev.pfx /app/Macs-Dev.pfx
EXPOSE 443 80
ENTRYPOINT ["dotnet", "MacsASPNETCore.dll"]
