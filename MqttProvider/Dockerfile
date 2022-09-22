FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["MqttProvider/MqttProvider.csproj", "MqttProvider/"]
RUN dotnet restore "MqttProvider/MqttProvider.csproj"
COPY . .
WORKDIR "/src/MqttProvider"
RUN dotnet build "MqttProvider.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MqttProvider.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MqttProvider.dll"]
