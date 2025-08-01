# Build Angular frontend
FROM node:18 AS client-build
WORKDIR /app
COPY JobPortal.Client/ ./JobPortal.Client/
RUN cd JobPortal.Client && npm install && npm run build

# Build .NET Core backend
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY JobPortal.API/ ./JobPortal.API/
WORKDIR /src/JobPortal.API
RUN dotnet restore
RUN dotnet publish -c Release -o /app/publish

# Runtime image
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build /app/publish .
COPY --from=client-build /app/JobPortal.Client/dist/ ./wwwroot/
ENV ASPNETCORE_URLS=http://+:80
EXPOSE 80
ENTRYPOINT ["dotnet", "JobPortal.API.dll"]
