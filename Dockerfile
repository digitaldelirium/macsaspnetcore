FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ./MacsASPNETCore.csproj MacsASPNETCore/
WORKDIR /src/MacsASPNETCore
RUN dotnet restore
COPY . .
RUN dotnet build -c #{BuildConfiguration}# MacsASPNETCore.csproj -o /app

FROM build AS publish
RUN dotnet publish -c #{BuildConfiguration}# MacsASPNETCore.csproj -o /app

FROM microsoft/dotnet:2.1-aspnetcore-runtime as base

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
EXPOSE 443/tcp
ENTRYPOINT ["dotnet", "MacsASPNETCore.dll"]
