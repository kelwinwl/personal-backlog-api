# ️ Personal Backlog API

>  **Projeto  WIP (Work in Progress)**

---

##  Tecnologias e Ferramentas

* **Linguagem:** C# (.NET 10)
* **Framework Web:** ASP.NET Core Web API
* **ORM:** Entity Framework Core
* **Banco de Dados:** PostgreSQL
* **Conteinerização:** Docker, Docker Compose
* **Documentação & Testes:** OpenAPI (Nativo) + Scalar UI

---

## Como Executar o Projeto Localmente

### Pré-requisitos
Antes de tudo, verifique se possui as ferramentas necessárias instaladas:
- [.NET 10 SDK](https://dotnet.microsoft.com/en-us/)
- [Docker Desktop](https://www.docker.com/products/docker-desktop/) 


## Passos para rodar:

#### 1. Clone este repositório:
   ```bash
   git clone https://github.com/kelwinwl/personal-backlog-api.git
   cd personal-backlog-api
   ```
#### 2. Configure a `.env`
Crie um arquivo chamado `.env` e use esse padrão:
```
POSTGRES_USER=api_backlog
POSTGRES_PASSWORD=          
POSTGRES_DB=PersonalBacklogDB
```
#### 3. Crie a Database com o Docker
```bash
docker-compose up -d          # É necessário instalar o Docker Desktop ou alternativas open-source (veja pré-requisitos) 
```

#### 4. Configure o user-secret
É necessário configurar a ConnectionString para conseguir se conectar ao banco de dados, para isso, coloque o comando:
```bash
cd PersonalBacklog.Api
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Host=localhost; Port=5432;Database=PersonalBacklogDB;Username=api_backlog;Password=SUASENHA"
```
- No campo Password, você colocará no lugar de SUASENHA, a senha inserida no arquivo `.env`, onde tem `POSTGRES_PASSWORD=` 

### 4. Crie as tabelas: 
```bash
dotnet restore     # Normalmente não é necessário, será somente para evitar erros.
dotnet ef database update
```
### 4. Rode a aplicação:
```bash
dotnet run
```
* Após utilizar o comando `dotnet run`, o console deve exibir as portas onde o servidor está rodando,
acesse a interface do Scalar UI pelo navegador utilizando as portas exibidas no console:
* `http://localhost:XXXX/Scalar`

---

## Considerações
A ideia é ser um projeto pessoal completo, onde vou aprendendo enquanto estou desenvolvendo 
o projeto, inspirado no MyAnimeList. A ideia no futuro é ter séries, animes, filmes e doramas.

Meu objetivo final é entregar um projeto com backend, autenticação segura (JWT) e frontend (Angular ou React) funcionais.
