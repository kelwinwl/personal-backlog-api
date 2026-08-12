# ️ Personal Backlog API

>  **Projeto  WIP (Work in Progress)**

---

##  Tecnologias e Ferramentas

* **Linguagem:** C# (.NET 10)
* **Framework Web:** ASP.NET Core Web API
* **ORM:** Entity Framework Core
* **Banco de Dados:** SQL Server
* **Documentação & Testes:** OpenAPI (Nativo) + Scalar UI

---

## Como Executar o Projeto Localmente

### Pré-requisitos
* [SDK do .NET 10](https://dotnet.microsoft.com/) instalado em sua máquina.
* Instância do SQL Server rodando localmente ou via Docker (verifique a **Connection String** no `appsettings.json`).

## Passos para rodar:

### Atenção, antes executar os comandos, lembre-se de ajustar a chave `ConnectionStrings` no arquivo `appsettings.Development.json` para a sua instância local do SQL Server
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_LOCAL_INSTANCE;Database=PersonalBacklogDb;Trusted_Connection=True;TrustServerCertificate=True;"
}
```


#### 1. Clone este repositório:
   ```bash
   git clone https://github.com/kelwinwl/personal-backlog-api.git
   ```
#### 2. Acesse a pasta do projeto:
```bash
cd personal-backlog-api/PersonalBacklog.Api
```
### 3. Crie o banco de dados e as tabelas: 
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