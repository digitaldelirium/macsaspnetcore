FROM microsoft/aspnetcore-build:2.0 AS build
WORKDIR /src
COPY ./MacsASPNETCore.csproj macsaspnetcore/
WORKDIR /src/macsaspnetcore
RUN dotnet restore
COPY . .
RUN dotnet build -c #{BuildConfiguration}# MacsASPNETCore.csproj -o /app -r ubuntu.16.04-x64

FROM build AS publish
RUN dotnet publish -c #{BuildConfiguration}# MacsASPNETCore.csproj -o /app -r ubuntu.16.04-x64

FROM microsoft/aspnetcore:2.0 as base

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
EXPOSE 443/tcp
ENTRYPOINT ["dotnet", "MacsASPNETCore.dll"]
