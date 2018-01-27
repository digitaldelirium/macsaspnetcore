FROM ubuntu:xenial
WORKDIR /app
COPY #{Build.StagingDirectory}# /app
RUN echo #{Macs-Dev}# >> /app/Macs-Dev.pfx
EXPOSE 443/tcp
ENV ASPNETCORE_ENVIRONMENT=Development
ENTRYPOINT ["dotnet", "MacsASPNETCore.dll"]
