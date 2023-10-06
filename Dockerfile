FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base


WORKDIR /app
EXPOSE 44306

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["ChatCloneApi/ChatCloneApi.csproj", "ChatCloneApi/"]
RUN dotnet restore "ChatCloneApi/ChatCloneApi.csproj"
COPY . .
WORKDIR "/src/ChatCloneApi"
RUN dotnet build "ChatCloneApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ChatCloneApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ChatCloneApi.dll"]
