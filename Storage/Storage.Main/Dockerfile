#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Storage.Main/Storage.Main.csproj", "Storage.Main/"]
COPY ["Storage.Core/Storage.Core.csproj", "Storage.Core/"]
COPY ["Storage.Database/Storage.Database.csproj", "Storage.Database/"]
RUN dotnet restore "./Storage.Main/Storage.Main.csproj"
COPY . .
WORKDIR "/src/Storage.Main"
RUN dotnet build "./Storage.Main.csproj" -c %BUILD_CONFIGURATION% -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Storage.Main.csproj" -c %BUILD_CONFIGURATION% -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Storage.Main.dll"]