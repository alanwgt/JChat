FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["JChat.API/JChat.API.csproj", "JChat.API/"]
RUN dotnet restore "JChat.API/JChat.API.csproj"
COPY . .
WORKDIR "/src/JChat.API"
RUN dotnet build "JChat.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "JChat.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "JChat.API.dll"]
