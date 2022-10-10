#Based on https://docs.microsoft.com/en-us/aspnet/core/host-and-deploy/docker/building-net-docker-images?view=aspnetcore-3.1
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app

# copy everything
COPY source/ ./
RUN dotnet restore simp-simulator-backend.sln

# build app
RUN dotnet publish simp-simulator-backend -c Release

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /app

COPY --from=build /app/simp-simulator-backend/bin/Release/net6.0/publish ./
RUN mkdir -p Files

ENTRYPOINT ["dotnet", "simp-simulator-backend.dll"]