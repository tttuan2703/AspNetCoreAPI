#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:3.1 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["ConsoleApp_DockerWithKafka/ConsoleApp_DockerWithKafka.csproj", "ConsoleApp_DockerWithKafka/"]
RUN dotnet restore "ConsoleApp_DockerWithKafka/ConsoleApp_DockerWithKafka.csproj"
COPY . .
WORKDIR "/src/ConsoleApp_DockerWithKafka"
RUN dotnet build "ConsoleApp_DockerWithKafka.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ConsoleApp_DockerWithKafka.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ConsoleApp_DockerWithKafka.dll"]