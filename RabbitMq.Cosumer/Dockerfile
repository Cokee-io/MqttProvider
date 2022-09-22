FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["RabbitMq.Cosumer/RabbitMq.Cosumer.csproj", "RabbitMq.Cosumer/"]
RUN dotnet restore "RabbitMq.Cosumer/RabbitMq.Cosumer.csproj"
COPY . .
WORKDIR "/src/RabbitMq.Cosumer"
RUN dotnet build "RabbitMq.Cosumer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RabbitMq.Cosumer.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RabbitMq.Cosumer.dll"]
