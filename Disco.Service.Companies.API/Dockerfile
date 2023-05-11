FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Disco.Service.Companies.API/Disco.Service.Companies.API.csproj", "Disco.Service.Companies.API/"]
COPY ["Disco.Service.Discounts.Application/Disco.Service.Discounts.Application.csproj", "Disco.Service.Discounts.Application/"]
COPY ["Disco.Service.Discounts.Core/Disco.Service.Discounts.Core.csproj", "Disco.Service.Discounts.Core/"]
COPY ["Disco.Shared/Disco.Shared.csproj", "Disco.Shared/"]
COPY ["Disco.Service.Companies.Infrastructure/Disco.Service.Companies.Infrastructure.csproj", "Disco.Service.Companies.Infrastructure/"]
COPY ["Disco.Service.Companies.Core/Disco.Service.Companies.Core.csproj", "Disco.Service.Companies.Core/"]
COPY ["Disco.Service.Companies.Application/Disco.Service.Companies.Application.csproj", "Disco.Service.Companies.Application/"]
RUN dotnet restore "Disco.Service.Companies.API/Disco.Service.Companies.API.csproj"
COPY . .
WORKDIR "/src/Disco.Service.Companies.API"
RUN dotnet build "Disco.Service.Companies.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Disco.Service.Companies.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Disco.Service.Companies.API.dll"]
