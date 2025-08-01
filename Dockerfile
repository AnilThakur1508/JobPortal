# === Build Angular frontend ===
FROM node:18 AS client-build
WORKDIR /app
COPY job-portal/ ./job-portal/
RUN cd job-portal && npm install && npm run build

# === Build .NET Core backend ===
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY . .  # 👈 Copies the entire solution including DTO, DAL, etc.
WORKDIR /src/JobPortal
RUN dotnet restore
RUN dotnet publish -c Release -o /app/publish

# === Runtime image ===
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build /app/publish .

# ✅ Fix Angular output path
COPY --from=client-build /app/job-portal/dist/ ./wwwroot/

ENV ASPNETCORE_URLS=http://+:80
EXPOSE 80
ENTRYPOINT ["dotnet", "JobPortal.dll"]

