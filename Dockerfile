FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY NuGet.config ./

COPY NewsPortalCMS.Api/NewsPortalCMS.Api.csproj NewsPortalCMS.Api/
COPY NewsPortalCMS.Application/NewsPortalCMS.Application.csproj NewsPortalCMS.Application/
COPY NewsPortalCMS.Domain/NewsPortalCMS.Domain.csproj NewsPortalCMS.Domain/
COPY NewsPortalCMS.Infrastructure/NewsPortalCMS.Infrastructure.csproj NewsPortalCMS.Infrastructure/

RUN dotnet nuget locals all --clear

RUN dotnet restore NewsPortalCMS.Api/NewsPortalCMS.Api.csproj --configfile ./NuGet.config

COPY . .

RUN dotnet publish NewsPortalCMS.sln -c Release -o /app/build

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

COPY --from=build /app/build ./

EXPOSE 5000
EXPOSE 5001

ENTRYPOINT ["dotnet", "NewsPortalCMS.Api.dll"]
	