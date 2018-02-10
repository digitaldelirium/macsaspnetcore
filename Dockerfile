FROM microsoft/aspnetcore-build:2.0 AS build
WORKDIR /src
COPY ./MacsASPNETCore.csproj macsaspnetcore/
WORKDIR /src/macsaspnetcore
RUN dotnet restore
COPY . .
RUN dotnet build -c debug MacsASPNETCore.csproj -o /app

FROM build AS publish
RUN dotnet publish -c debug MacsASPNETCore.csproj -o /app

FROM microsoft/aspnetcore:2.0 as base

FROM base AS final
ENV ASPNETCORE_ENVIRONMENT=Development
ADD ./Macs-Dev.pfx /app
WORKDIR /app
COPY --from=publish /app .
EXPOSE 443/tcp
ENTRYPOINT ["dotnet", "MacsASPNETCore.dll"]