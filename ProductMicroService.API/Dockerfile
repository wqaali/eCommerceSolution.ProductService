#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ProductMicroService.API/ProductMicroService.API.csproj", "ProductMicroService.API/"]
RUN dotnet restore "./ProductMicroService.API/./ProductMicroService.API.csproj"
COPY . .
WORKDIR "/src/ProductMicroService.API"
RUN dotnet build "./ProductMicroService.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ProductMicroService.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV HOST_NAME= ""sqlexp,1433"",
ENV DB_NAME=eCommerceProductDB,
ENV DB_USER=sa,
ENV DB_PASSWORD=Sa@1234
ENTRYPOINT ["dotnet", "ProductMicroService.API.dll"]