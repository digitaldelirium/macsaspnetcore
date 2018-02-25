FROM microsoft/aspnetcore-build:2.0 AS build
WORKDIR /src
COPY ./MacsASPNETCore.csproj macsaspnetcore/
WORKDIR /src/macsaspnetcore
RUN dotnet restore
COPY . .
RUN dotnet build --configuration debug MacsASPNETCore.csproj --output /app --runtime ubuntu.16.04-x64

FROM build AS publish
RUN dotnet publish --configuration debug MacsASPNETCore.csproj --output /app --runtime ubuntu.16.04-x64

EXPOSE 443/tcp
ENTRYPOINT ["dotnet", "MacsASPNETCore.dll"]
