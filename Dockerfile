FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG PROJECT_NAME
WORKDIR /source

COPY *.sln .
COPY DepotService/*.csproj ./DepotService/
COPY Frontend/*.csproj ./Frontend/
COPY MissionPlanning/*.csproj ./MissionPlanning/
COPY MissionPlanningService/*.csproj ./MissionPlanningService/
COPY TrackTramControl/*.csproj ./TrackTramControl/
COPY Utils/*.csproj ./Utils/
RUN dotnet restore

COPY . .
WORKDIR /source/${PROJECT_NAME}
RUN dotnet publish ${PROJECT_NAME}.csproj -c release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:8.0
ARG PROJECT_NAME
WORKDIR /app
COPY --from=build /app ./
ENTRYPOINT dotnet ${PROJECT_NAME}.dll 
