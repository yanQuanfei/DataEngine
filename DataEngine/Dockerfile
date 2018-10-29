FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 7394
EXPOSE 44309

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY DataEngine/DataEngine.csproj DataEngine/
COPY Engine/Engine.csproj Engine/
RUN dotnet restore DataEngine/DataEngine.csproj
COPY . .
WORKDIR /src/DataEngine
RUN dotnet build DataEngine.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish DataEngine.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "DataEngine.dll"]
