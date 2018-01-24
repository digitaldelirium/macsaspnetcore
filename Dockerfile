FROM ubuntu:xenial
RUN apt-get update && \
    apt-get install -y apt-transport-https

# Install Azure CLI (instructions taken from https://docs.microsoft.com/en-us/cli/azure/install-azure-cli)
RUN echo "deb [arch=amd64] https://packages.microsoft.com/repos/azure-cli/ wheezy main" | \
    tee /etc/apt/sources.list.d/azure-cli.list \
 && apt-key adv --keyserver packages.microsoft.com --recv-keys 52E16F86FEE04B979B07E28DB02C46DF417A0893 \
 && apt-get update \
 && apt-get install -y --no-install-recommends \
    azure-cli \
 && rm -rf /var/lib/apt/lists/*

FROM microsoft/aspnetcore:2 as base

FROM base AS final
WORKDIR /app
COPY #{Build.ArtifactStagingDirectory}# /app .
EXPOSE 443/tcp
ENTRYPOINT ["dotnet", "MacsASPNETCore.dll"]
