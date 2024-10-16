# Etapa 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

COPY LojaDoManoel/LojaDoManoel.csproj LojaDoManoel/
COPY TestProjectLojaManoel/TestProjectLojaManoel.csproj TestProjectLojaManoel/
COPY LojaDoManoel.sln ./


RUN dotnet restore

COPY . .

RUN dotnet publish LojaDoManoel/LojaDoManoel.csproj -c Release -o out

# Etapa 2: Deploy
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build-env /app/out .


EXPOSE 80


ENTRYPOINT ["dotnet", "LojaDoManoel.dll"]
