FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ./MacsASPNETCore.csproj MacsASPNETCore/
WORKDIR /src/MacsASPNETCore
RUN dotnet restore
COPY . .
RUN dotnet build -c #{BuildConfiguration}# MacsASPNETCore.csproj -o /app && \
    curl -sL https://deb.nodesource.com/setup_10.x | bash - && \
    apt-get install -y nodejs gcc g++ make apt-utils && \
    curl -sL https://dl.yarnpkg.com/debian/pubkey.gpg | apt-key add - && \
    echo "deb https://dl.yarnpkg.com/debian/ stable main" | tee /etc/apt/sources.list.d/yarn.list && \
    apt-get update && apt-get install yarn -y

FROM build AS publish
RUN dotnet publish -c #{BuildConfiguration}# MacsASPNETCore.csproj -o /app

FROM microsoft/dotnet:2.1-aspnetcore-runtime as base

FROM base AS final
ARG ASPNETCORE_ENVIRONMENT="#{Environment}#"
WORKDIR /app
COPY --from=publish /app .
EXPOSE 443/tcp
ENTRYPOINT ["dotnet", "MacsASPNETCore.dll"]
