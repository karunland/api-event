FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ./ ./

RUN dotnet build ./WebApplication6 --configuration Release

FROM build AS publish
RUN dotnet publish ./WebApplication6 --configuration Release --output ./publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=publish /src/publish .
# container url
ENV ASPNETCORE_URLS=http://*:5050 
EXPOSE 5050

ENTRYPOINT ["dotnet", "WebApplication6.dll"]