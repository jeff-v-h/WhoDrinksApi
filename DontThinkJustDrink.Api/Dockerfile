#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["DontThinkJustDrink.Api/DontThinkJustDrink.Api.csproj", "DontThinkJustDrink.Api/"]
RUN dotnet restore "DontThinkJustDrink.Api/DontThinkJustDrink.Api.csproj"
COPY . .
WORKDIR "/src/DontThinkJustDrink.Api"
RUN dotnet build "DontThinkJustDrink.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DontThinkJustDrink.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DontThinkJustDrink.Api.dll"]

ENV ASPNETCORE_URLS http://+:5000
EXPOSE 5000