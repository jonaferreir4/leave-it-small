# 🔗 Leave It Small — Backend

Este é o backend da aplicação **Leave It Small**, um encurtador de URLs moderno e eficiente. Desenvolvido com **ASP.NET Core 8**, **PostgreSQL**, **Entity Framework Core** e **Traefik** para roteamento dinâmico em ambiente Docker.

---

## ⚙️ Tecnologias Utilizadas

- **ASP.NET Core 8** — Framework principal para APIs REST
- **Entity Framework Core** — ORM para acesso ao banco de dados
- **PostgreSQL** — Banco de dados relacional    
- **Docker + Docker Compose** — Containerização e orquestração
- **Traefik** — Proxy reverso e roteamento inteligente

---

## 📁 Estrutura do Projeto

```text
├── Controllers/
├── Data/
├── Http/
├── Migrations/
├── Models/
├── Services/
├── Utils/
├── docker-compose.yml
├── Dockerfile
├── leave-it-small.csproj
├── leave-it-small.sln
└── Program.cs
```

## Endpoints da API

Principais rotas:

- `POST /api/shorten` — Encurta uma URL
- `GET /api/links` — Lista URLs encurtadas
- `GET /{code}` — Redireciona para a URL original
---


## Executando com Docker

### Pré-requisitos

- Docker
- Docker Compose

### Passos

1. Crie um arquivo `.env` com as variáveis:

```env
DB_HOST=db
DB_PORT=5432
DB_NAME=leaveit
DB_USER=seuusuario
DB_PASSWORD=senhaforte
DOMAIN_NAME=seudominio.com

```
$ docker-compose up --build
```
