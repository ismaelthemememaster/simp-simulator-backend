#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["simp-simulator-backend/simp-simulator-backend.csproj", "simp-simulator-backend/"]
COPY ["simp-simulator-models/simp-simulator-models.csproj", "simp-simulator-models/"]
RUN dotnet restore "simp-simulator-backend/simp-simulator-backend.csproj"
COPY . .
WORKDIR "/src/simp-simulator-backend"
RUN dotnet build "simp-simulator-backend.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "simp-simulator-backend.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "simp-simulator-backend.dll"]