FROM mcr.microsoft.com/dotnet/core/sdk:2.1 AS build
WORKDIR /src
COPY ./MacsASPNETCore.csproj macsaspnetcore/
WORKDIR /src/macsaspnetcore
RUN dotnet restore
COPY . .
RUN dotnet build --configuration debug MacsASPNETCore.csproj --output /app --runtime ubuntu.16.04-x64

FROM build AS publish
RUN dotnet publish --configuration debug MacsASPNETCore.csproj --output /app --runtime ubuntu.16.04-x64

EXPOSE 443/tcp
ENV ASPNETCORE_ENVIRONMENT=Development
ENTRYPOINT ["dotnet", "MacsASPNETCore.dll"]