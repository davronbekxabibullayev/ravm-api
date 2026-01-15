FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Используется для экспорта данных в эксель файл при тестировании.
RUN apt-get update && apt-get install -y apt-utils libgdiplus libc6-dev

WORKDIR /app

#Copy resources to docket layer
COPY . .

#Build artifact

RUN cd ./src/Services/Ravm/Ravm.Api && \
    dotnet restore && \
    dotnet build "Ravm.Api.csproj" -c Release --no-restore && \
    dotnet publish "Ravm.Api.csproj" -c Release --no-build -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS app

# Используется для экспорта данных в эксель файл.
RUN apt-get update && apt-get install -y apt-utils libgdiplus libc6-dev

WORKDIR /app

COPY --from=build /app/publish .

EXPOSE 80/tcp
EXPOSE 443/tcp

ENTRYPOINT ["dotnet", "Ravm.Api.dll"]
