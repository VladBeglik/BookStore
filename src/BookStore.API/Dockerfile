FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["BookStore.API/BookStore.API.csproj", "BookStore.API/"]
COPY ["BookStore.App/BookStore.App.csproj", "BookStore.App/"]
COPY ["BookStore.Domain/BookStore.Domain.csproj", "BookStore.Domain/"]
COPY ["BookStore.Persistence/BookStore.Persistence.csproj", "BookStore.Persistence/"]
RUN dotnet restore "BookStore.API/BookStore.API.csproj"
COPY . .
WORKDIR "/src/BookStore.API"
RUN dotnet build "BookStore.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BookStore.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BookStore.API.dll"]
