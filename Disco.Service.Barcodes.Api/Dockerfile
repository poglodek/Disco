FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Disco.Service.Barcodes.Api/Disco.Service.Barcodes.Api.csproj", "Disco.Service.Barcodes.Api/"]
COPY ["Disco.Service.Barcodes.Application/Disco.Service.Barcodes.Application.csproj", "Disco.Service.Barcodes.Application/"]
COPY ["Disco.Service.Barcodes.Core/Disco.Service.Barcodes.Core.csproj", "Disco.Service.Barcodes.Core/"]
COPY ["Disco.Shared/Disco.Shared.csproj", "Disco.Shared/"]
COPY ["Disco.Service.Barcodes.Infrastructure/Disco.Service.Barcodes.Infrastructure.csproj", "Disco.Service.Barcodes.Infrastructure/"]
RUN dotnet restore "Disco.Service.Barcodes.Api/Disco.Service.Barcodes.Api.csproj"
COPY . .
WORKDIR "/src/Disco.Service.Barcodes.Api"
RUN dotnet build "Disco.Service.Barcodes.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Disco.Service.Barcodes.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Disco.Service.Barcodes.Api.dll"]
