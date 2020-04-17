FROM mcr.microsoft.com/dotnet/core/sdk:2.1 AS build
WORKDIR /src
COPY ./MacsASPNETCore.csproj MacsASPNETCore/
WORKDIR /src/MacsASPNETCore
RUN dotnet restore
COPY . .
RUN curl -sL https://deb.nodesource.com/setup_12.x | bash - && \
    apt-get install -y nodejs gcc g++ make apt-utils && \
    curl -sL https://dl.yarnpkg.com/debian/pubkey.gpg | apt-key add - && \
    echo "deb https://dl.yarnpkg.com/debian/ stable main" | tee /etc/apt/sources.list.d/yarn.list && \
    apt-get update && apt-get install yarn -y
    
RUN dotnet build -c Debug MacsASPNETCore.csproj -o /app

RUN dotnet publish -c Debug MacsASPNETCore.csproj -o /app
COPY ./Macs-Dev.pfx app/
WORKDIR /app
ENV ASPNETCORE_ENVIRONMENT=Development
ENV ASPNETCORE_URLS="https://+:8443;http://+:8081"
ENV ASPNETCORE_HTTPS_PORT=8443

EXPOSE 8443/tcp
ENTRYPOINT ["dotnet", "MacsASPNETCore.dll"]
