FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
EXPOSE 5137

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["UkraineWarAPI/UkraineWarAPI.csproj", "UkraineWarAPI/"]
RUN dotnet restore "UkraineWarAPI/UkraineWarAPI.csproj"
COPY . .
WORKDIR "/src/UkraineWarAPI"
RUN dotnet build "UkraineWarAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "UkraineWarAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

COPY ["UkraineWarAPI/Contracts.csproj", "Contracts/"]
RUN dotnet restore "UkraineWarAPI/Contracts.csproj"
COPY . .
WORKDIR "/src/Contracts"
RUN dotnet build "Contracts.csproj" -c Release -o /app/build

# Publish the second project
FROM build AS publish_second
RUN dotnet publish "Contracts.csproj" -c Release -o /app/publish /p:UseAppHost=fals

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UkraineWarAPI.dll"]
