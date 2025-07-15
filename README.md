# 📰 NewsPortalCMS

System CMS do zarządzania artykułami i kategoriami w portalu informacyjnym.  

---

## 🧱 Technologie

- `.NET 8`, `ASP.NET Core`, `Entity Framework Core`  
- `PostgreSQL`, `Docker`, `Docker Compose`  
- `Swagger` (OpenAPI 3), `xUnit` (testy jednostkowe)  
- `Makefile` (automatyzacja)  
- Architektura Clean Architecture:  
  - `NewsPortalCMS.Api`  
  - `NewsPortalCMS.Application`  
  - `NewsPortalCMS.Domain`  
  - `NewsPortalCMS.Infrastructure`  
  - `NewsPortalCMS.Tests`

---

## 📂 Struktura projektu
```
NewsPortalCMS/
├── NewsPortalCMS.Api # Controllers
├── NewsPortalCMS.Application # Dto, Services, Validation, Mappings, Interfaces
├── NewsPortalCMS.Domain # Entities, Enums, Models, Services
├── NewsPortalCMS.Infrastructure# EF Core, Repositories, Migrations, Configurations
├── NewsPortalCMS.Tests # Tests xUnit
├── Makefile # Komendy CLI
├── Dockerfile / docker-compose.yml
└── README.md
```

---

## 🚀 Uruchamianie aplikacji

### 🐳 Docker (PostgreSQL)

Uruchamia całe dockerowe środowisko (API + baza PostgreSQL):

```bash
make docker-compose-up
```
Usuwa całe dockerowe środowisko (API + baza PostgreSQL):

```bash
make docker-compose-down
```
🔗 Swagger UI: http://localhost:5000/swagger
---
Przykładowe zapytania (PowerShell z Invoke-RestMethod):
```
Invoke-RestMethod -Uri "http://localhost:5000/api/articles"

Invoke-RestMethod -Uri "http://localhost:5000/api/articles?status=Draft"

Invoke-RestMethod -Uri "http://localhost:5000/api/articles/f9c68059-1e83-47a9-b6de-84ab11223344"

Invoke-RestMethod -Uri "http://localhost:5000/stats"

Invoke-RestMethod `
  -Method POST `
  -Uri "http://localhost:5000/api/categories" `
  -Body '{ "name": "Niemcy" }' `
  -ContentType "application/json"

Invoke-RestMethod `
  -Method POST `
  -Uri "http://localhost:5000/api/articles" `
  -Body '{
    "title": "Berlin na pierwszej stronie",
    "content": "Najnowsze wiadomości z Niemiec",
    "author": "Adrian",
    "categoryId": "a1b2c3d4-e5f6-7890-1234-56789abcdef0"
  }' `
  -ContentType "application/json"

 Invoke-RestMethod `
   -Method PUT `
   -Uri "http://localhost:5000/api/articles/f9c68059-1e83-47a9-b6de-84ab11223344" `
   -Body '{
     "title": "Francja na pierwszej stronie",
     "content": "Najnowsze wiadomości z Niemiec",
     "categoryId": "a1b2c3d4-e5f6-7890-1234-56789abcdef0"
 }' `
 -ContentType "application/json"
```

### 💻 Lokalnie (PostgreSQL)

Uruchamia całe lokalne środowisko (API + baza PostgreSQL):

```bash
make build            # Buduje projekt
make postgres-up      # Uruchamia bazę PostgreSQL w Dockerze
make migrate-db       # Tworzy migrację EF Core
make update-db        # Wprowadza migrację do bazy danych
make run-app          # Uruchamia aplikację lokalnie
```
Usuwa całe dockerowe środowisko (API + baza PostgreSQL):

```bash
make postgres-down    # Zatrzymuje bazę PostgreSQL
make clean            # Usuwa pliki wygenerowane podczas budowania aplikacji
```
🔗 Swagger UI: http://localhost:5116/swagger
---
Przykładowe zapytania (PowerShell z Invoke-RestMethod):
```
Invoke-RestMethod -Uri "http://localhost:5116/api/articles"

Invoke-RestMethod -Uri "http://localhost:5116/api/articles?status=Draft"

Invoke-RestMethod -Uri "http://localhost:5116/api/articles/f9c68059-1e83-47a9-b6de-84ab11223344"

Invoke-RestMethod -Uri "http://localhost:5116/stats"

Invoke-RestMethod `
  -Method POST `
  -Uri "http://localhost:5116/api/categories" `
  -Body '{ "name": "Niemcy" }' `
  -ContentType "application/json"

Invoke-RestMethod `
  -Method POST `
  -Uri "http://localhost:5116/api/articles" `
  -Body '{
    "title": "Berlin na pierwszej stronie",
    "content": "Najnowsze wiadomości z Niemiec",
    "author": "Adrian",
    "categoryId": "b2c3d4e5-f6a1-7890-1234-56789abcdef1"
  }' `
  -ContentType "application/json"

 Invoke-RestMethod `
   -Method PUT `
   -Uri "http://localhost:5116/api/articles/f9c68059-1e83-47a9-b6de-84ab11223344" `
   -Body '{
     "title": "Rosja na pierwszej stronie",
     "content": "Najnowsze wiadomości z Niemiec",
     "categoryId": "b2c3d4e5-f6a1-7890-1234-56789abcdef1"
 }' `
 -ContentType "application/json"
```
---

## 🧪 Testy jednostkowe

```bash
make test
```
- Generowanie slugu  
- Walidacja tytułu  
- Statystyki artykułów
  
---

## 🔧 Komendy (Makefile

```bash
make build              	Twórzy projekt  
make run-app            	Uruchamia aplikację  
make test               	Uruchamia testy jednostkowe  
make migrate-db         	Tworzy migrację EF Core  
make update-db          	Wprowadza migrację do bazy danych  
make docker-build       	Buduje obraz Dockera  
make docker-run         	Uruchamia API jako kontener  
make docker-compose-up  	Uruchamia API + DB (Compose)  
make docker-compose-down	Zatrzymuje kontenery  
make clean              	Czyści artefakty builda  
make dev-local          	PostgreSQL lokalnie + migracja + run-app  

```
