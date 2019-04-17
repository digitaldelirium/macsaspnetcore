FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ./macsaspnetcore.csproj macsaspnetcore/
WORKDIR /src/macsaspnetcore
RUN dotnet restore
COPY . .
RUN dotnet build -c #{BuildConfiguration}# macsaspnetcore.csproj -o /app

FROM build AS publish
RUN dotnet publish -c #{BuildConfiguration}# macsaspnetcore.csproj -o /app

FROM microsoft/dotnet:2.1-aspnetcore-runtime as base

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
EXPOSE 443/tcp
ENTRYPOINT ["dotnet", "macsaspnetcore.dll"]
