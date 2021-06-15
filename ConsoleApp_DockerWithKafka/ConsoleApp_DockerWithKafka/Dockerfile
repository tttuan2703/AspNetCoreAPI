#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

#FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build 
#WORKDIR /src
#COPY ["ConsoleApp_DockerWithKafka/ConsoleApp_DockerWithKafka.csproj", "ConsoleApp_DockerWithKafka/"]
#RUN dotnet restore "ConsoleApp_DockerWithKafka/ConsoleApp_DockerWithKafka.csproj"
#COPY . .
#WORKDIR "/src/ConsoleApp_DockerWithKafka"
#RUN dotnet build "ConsoleApp_DockerWithKafka.csproj" -c Release -o /app/build
FROM python:3.7-alpine
WORKDIR /code
ENV FLASK_APP=app.py
ENV FLASK_RUN_HOST=0.0.0.0
RUN apk add --no-cache gcc musl-dev linux-headers
COPY requirements.txt requirements.txt
RUN pip install -r requirements.txt
EXPOSE 5000
COPY . .
CMD ["flask", "run"]

FROM build AS publish
RUN dotnet publish "ConsoleApp_DockerWithKafka.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ConsoleApp_DockerWithKafka.dll"]