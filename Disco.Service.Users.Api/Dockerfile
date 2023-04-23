FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Disco.Service.Users.Api/Disco.Service.Users.Api.csproj", "Disco.Service.Users.Api/"]
COPY ["Disco.Service.Users.Application/Disco.Service.Users.Application.csproj", "Disco.Service.Users.Application/"]
COPY ["Disco.Service.Users.Core/Disco.Service.Users.Core.csproj", "Disco.Service.Users.Core/"]
COPY ["Disco.Shared/Disco.Shared.csproj", "Disco.Shared/"]
COPY ["Disco.Service.Users.Infrastructure/Disco.Service.Users.Infrastructure.csproj", "Disco.Service.Users.Infrastructure/"]
RUN dotnet restore "Disco.Service.Users.Api/Disco.Service.Users.Api.csproj"
COPY . .
WORKDIR "/src/Disco.Service.Users.Api"
RUN dotnet build "Disco.Service.Users.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Disco.Service.Users.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Disco.Service.Users.Api.dll"]
