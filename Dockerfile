FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["excel2json/excel2json.csproj", "excel2json/"]
RUN dotnet restore "excel2json/excel2json.csproj"
COPY . .
WORKDIR "/src/excel2json"
RUN dotnet build "excel2json.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "excel2json.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "excel2json.dll"]