FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Disco.Service.Points.Api/Disco.Service.Points.Api.csproj", "Disco.Service.Points.Api/"]
COPY ["Disco.Service.Points.Application/Disco.Service.Points.Application.csproj", "Disco.Service.Points.Application/"]
COPY ["Disco.Service.Points.Core/Disco.Service.Points.Core.csproj", "Disco.Service.Points.Core/"]
COPY ["Disco.Shared/Disco.Shared.csproj", "Disco.Shared/"]
COPY ["Disco.Service.Points.Infrastructure/Disco.Service.Points.Infrastructure.csproj", "Disco.Service.Points.Infrastructure/"]
RUN dotnet restore "Disco.Service.Points.Api/Disco.Service.Points.Api.csproj"
COPY . .
WORKDIR "/src/Disco.Service.Points.Api"
RUN dotnet build "Disco.Service.Points.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Disco.Service.Points.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Disco.Service.Points.Api.dll"]
