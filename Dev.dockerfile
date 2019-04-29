FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ./MacsASPNETCore.csproj MacsASPNETCore/
WORKDIR /src/MacsASPNETCore
RUN dotnet restore
COPY . .
RUN dotnet build -c Debug MacsASPNETCore.csproj -o /app && \
    curl -sL https://deb.nodesource.com/setup_10.x | bash - && \
    apt-get install -y nodejs gcc g++ make apt-utils && \
    curl -sL https://dl.yarnpkg.com/debian/pubkey.gpg | apt-key add - && \
    echo "deb https://dl.yarnpkg.com/debian/ stable main" | tee /etc/apt/sources.list.d/yarn.list && \
    apt-get update && apt-get install yarn -y

FROM build AS publish
RUN dotnet publish -c Debug MacsASPNETCore.csproj -o /app

FROM microsoft/dotnet:2.1-aspnetcore-runtime as base

RUN openssl genrsa -des3 -passout pass:developer -out server.pass.key 2048 && \
    openssl rsa -passin pass:developer -in server.pass.key -out server.key && \
    rm server.pass.key && \
    openssl req -x509 -days 365 -newkey rsa:2048 -keyout key.pem -out cert.pem -passout pass:developer -subj "/C=US/ST=Shoreline/O=DigitalDeliriumTechnologies/OU=Mac's Camping Area/CN=macscampingarea.com"

RUN openssl pkcs12 -export -in cert.pem -inkey key.pem -out Macs-Dev.pfx -passin pass:developer -passout pass:developer
FROM base AS final
ENV ASPNETCORE_ENVIRONMENT="Development"
WORKDIR /app
COPY --from=publish /app .
EXPOSE 443/tcp
ENTRYPOINT ["dotnet", "MacsASPNETCore.dll"]
