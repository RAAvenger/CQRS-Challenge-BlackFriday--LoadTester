#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Debug
WORKDIR /src
COPY ["BlackFriday.LoadTester/BlackFriday.LoadTester.csproj", "BlackFriday.LoadTester/"]
COPY ["Directory.Packages.props", "Directory.Packages.props"]
RUN dotnet restore "./BlackFriday.LoadTester/BlackFriday.LoadTester.csproj"
COPY ["BlackFriday.LoadTester/", "BlackFriday.LoadTester/"]
COPY ["Directory.Packages.props", "Directory.Packages.props"]
WORKDIR "/src/BlackFriday.LoadTester"
RUN dotnet build "./BlackFriday.LoadTester.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Debug
RUN dotnet publish "./BlackFriday.LoadTester.csproj" -c $BUILD_CONFIGURATION -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BlackFriday.LoadTester.dll"]