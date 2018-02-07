FROM microsoft/aspnetcore-build:2.0 AS build
ARG BLDCONFIG
WORKDIR /src
COPY *.sln ./
COPY ./MacsASPNETCore.csproj macsaspnetcore/
WORKDIR /src/macsaspnetcore
RUN dotnet restore
COPY . .
RUN dotnet build -c ${BLDCONFIG} -o /app

FROM build AS publish
RUN dotnet publish -c ${BLDCONFIG} -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
EXPOSE 443/tcp
ENTRYPOINT ["dotnet", "MacsASPNETCore.dll"]