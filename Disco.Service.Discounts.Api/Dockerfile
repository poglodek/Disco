FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Disco.Service.Discounts.Api/Disco.Service.Discounts.Api.csproj", "Disco.Service.Discounts.Api/"]
COPY ["Disco.Service.Discounts.Application/Disco.Service.Discounts.Application.csproj", "Disco.Service.Discounts.Application/"]
COPY ["Disco.Service.Discounts.Core/Disco.Service.Discounts.Core.csproj", "Disco.Service.Discounts.Core/"]
COPY ["Disco.Shared/Disco.Shared.csproj", "Disco.Shared/"]
COPY ["Disco.Service.Discounts.Infrastructure/Disco.Service.Discounts.Infrastructure.csproj", "Disco.Service.Discounts.Infrastructure/"]
RUN dotnet restore "Disco.Service.Discounts.Api/Disco.Service.Discounts.Api.csproj"
COPY . .
WORKDIR "/src/Disco.Service.Discounts.Api"
RUN dotnet build "Disco.Service.Discounts.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Disco.Service.Discounts.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Disco.Service.Discounts.Api.dll"]
