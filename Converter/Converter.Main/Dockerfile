#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Converter.Main/Converter.Main.csproj", "Converter.Main/"]
COPY ["Converter.Core/Converter.Core.csproj", "Converter.Core/"]
COPY ["Converter.Database/Converter.Database.csproj", "Converter.Database/"]
RUN dotnet restore "./Converter.Main/Converter.Main.csproj"
COPY . .
WORKDIR "/src/Converter.Main"
RUN dotnet build "./Converter.Main.csproj" -c %BUILD_CONFIGURATION% -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Converter.Main.csproj" -c %BUILD_CONFIGURATION% -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Converter.Main.dll"]