# === Build Angular frontend ===
FROM node:18 AS client-build
WORKDIR /app
COPY job-portal/ ./job-portal/
WORKDIR /app/job-portal
RUN npm install
RUN npm run build

# === Build .NET Core backend ===
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY . .
WORKDIR /src/JobPortal
RUN dotnet restore
RUN dotnet publish -c Release -o /app/publish

# === Runtime image ===
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .

# ✅ Angular output is typically at: /app/job-portal/dist/{project-name}
# Adjust this path if needed
COPY --from=client-build /app/job-portal/dist/ /app/wwwroot/

ENV ASPNETCORE_URLS=http://+:80
EXPOSE 80
ENTRYPOINT ["dotnet", "JobPortal.dll"]
