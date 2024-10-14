FROM mcr.microsoft.com/dotnet/sdk:8.0 as base
RUN apt-get update && apt-get install -y wget

FROM mcr.microsoft.com/dotnet/sdk:8.0 as build
WORKDIR /app

WORKDIR /src
COPY . .
RUN dotnet restore "JernaWebApp/JernaWebApp.csproj"

COPY . .
WORKDIR /src/JernaWebApp
RUN dotnet build "JernaWebApp.csproj" -c Release -o /app/build

FROM build as publish 
RUN dotnet publish "JernaWebApp.csproj" -c Release -o /app/publish

from base as final
WORKDIR /app
COPY --from=publish /app/publish .
CMD ["dotnet", "JernaWebApp.dll"]