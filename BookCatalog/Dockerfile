#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["BookCatalog/BookCatalog.csproj", "BookCatalog/"]
COPY ["BookCatalog.DAL/BookCatalog.DAL.csproj", "BookCatalog.DAL/"]
COPY ["BookCatalog.Domain/BookCatalog.Domain.csproj", "BookCatalog.Domain/"]
COPY ["BookCatalog.Service/BookCatalog.Service.csproj", "BookCatalog.Service/"]
RUN dotnet restore "BookCatalog/BookCatalog.csproj"
COPY . .
WORKDIR "/src/BookCatalog"
RUN dotnet build "BookCatalog.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BookCatalog.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BookCatalog.dll"]