postgres-up:
	docker run --name newsportal-local-db \
		-e POSTGRES_PASSWORD=randompassword \
		-e POSTGRES_DB=NewsPortalCMS \
		-p 5432:5432 \
		-d postgres:17

postgres-down:
	docker rm -f newsportal-local-db

migrate-db:
	 dotnet ef migrations add InitialCreate \
        --project NewsPortalCMS.Infrastructure \
        --startup-project NewsPortalCMS.Api

update-db:
	 dotnet ef database update \
		--project NewsPortalCMS.Infrastructure \
		--startup-project NewsPortalCMS.Api

run-app:
	dotnet run --project NewsPortalCMS.Api

build:
	dotnet build NewsPortalCMS.Api/NewsPortalCMS.Api.csproj -c Release

test:
	dotnet test NewsPortalCMS.Tests/NewsPortalCMS.Tests.csproj

docker-build:
	docker build -t newsportal-api .

docker-run:
	docker run -p 5000:8080 newsportal-api

docker-compose-up:
	docker-compose up --build

docker-compose-down:
	docker-compose down -v

clean:
	dotnet clean NewsPortalCMS.Api/NewsPortalCMS.Api.csproj

dev-local: postgres-up wait-for-postgres migrate-db run-app

wait-for-postgres:
	@echo "Czekam na Postgresa..."
	@sleep 5
