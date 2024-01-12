FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["UkraineWarAPI/UkraineWarAPI.csproj", "UkraineWarAPI/"]
COPY ["LoggerService/LoggerService.csproj", "LoggerService/"]
COPY ["Contracts/Contracts.csproj", "Contracts/"]
RUN dotnet restore "UkraineWarAPI/UkraineWarAPI.csproj"
COPY . .
WORKDIR "/src/UkraineWarAPI"
RUN dotnet build "UkraineWarAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "UkraineWarAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UkraineWarAPI.dll"]
