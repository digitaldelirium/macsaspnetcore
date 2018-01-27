FROM microsoft/aspnetcore-build:2.0 AS build
WORKDIR /src
COPY *.sln ./
COPY ./MacsASPNETCore.csproj macsaspnetcore/
WORKDIR /src/macsaspnetcore
RUN dotnet restore
RUN echo #{Macs-Dev}# >> macsaspnetcore/Macs-Dev.pfx
COPY . .
RUN dotnet build -c Release -o /app

FROM build AS publish
RUN dotnet publish -c Release -o /app

FROM microsoft/aspnetcore:2 AS base
ADD ./Macs.pfx macsaspnetcore

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
EXPOSE 443/tcp
ENV ASPNETCORE_ENVIRONMENT=Development
ENTRYPOINT ["dotnet", "MacsASPNETCore.dll"]