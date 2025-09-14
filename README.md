# FilmesApi

API REST para cadastro, consulta, atualização e remoção de filmes, desenvolvida em .NET 6 com Entity Framework Core e MySQL.

## Sumário

- [Visão Geral](#visão-geral)
- [Tecnologias Utilizadas](#tecnologias-utilizadas)
- [Arquitetura](#arquitetura)
- [Modelos e DTOs](#modelos-e-dtos)
- [Endpoints](#endpoints)
- [Validações](#validações)
- [Como Executar](#como-executar)
- [Como Testar](#como-testar)
- [Swagger](#swagger)
- [Observações](#observações)

---

## Visão Geral

A FilmesApi é uma aplicação backend que permite gerenciar um catálogo de filmes. Ela oferece operações CRUD completas, validação de dados, documentação automática via Swagger e persistência em banco de dados relacional.

## Tecnologias Utilizadas

- .NET 6
- C# 10
- Entity Framework Core
- MySQL
- AutoMapper
- Swagger (Swashbuckle)
- ASP.NET Core Web API

## Arquitetura

- **Controllers**: Responsáveis por receber requisições HTTP, validar dados e retornar respostas.
- **Models**: Representam as entidades do banco de dados.
- **DTOs**: Objetos de transferência de dados para entrada (Create/Update) e saída (Read).
- **DbContext**: Gerencia a conexão e operações com o banco de dados.
- **AutoMapper**: Facilita a conversão entre DTOs e Models.

## Modelos e DTOs

- **Filme**: Entidade principal, representa um filme no banco de dados.
- **CreateFilmeDto / UpdateFilmeDto**: Utilizados para entrada de dados (criação/atualização), com validações.
- **ReadFilmeDto**: Utilizado para saída de dados, inclui informações adicionais como data/hora da consulta.

## Endpoints

| Método   | Rota                | Descrição                                 | Corpo/Requisição         | Resposta           |
|----------|---------------------|-------------------------------------------|--------------------------|--------------------|
| POST     | `/filme`            | Adiciona um novo filme                    | `CreateFilmeDto`         | 201 Created        |
| GET      | `/filme`            | Lista filmes (paginado)                   | Query: `skip`, `take`    | 200 OK (lista)     |
| GET      | `/filme/{id}`       | Consulta filme por ID                     | -                        | 200 OK / 404 NotFound |
| PUT      | `/filme/{id}`       | Atualiza completamente um filme           | `UpdateFilmeDto`         | 204 NoContent / 404 |
| PATCH    | `/filme/{id}`       | Atualiza parcialmente um filme (JSON Patch)| `JsonPatchDocument`      | 204 / 404 / 400    |
| DELETE   | `/filme/{id}`       | Remove um filme                           | -                        | 204 / 404          |

## Validações

- Título: obrigatório, máximo 50 caracteres.
- Gênero: máximo 50 caracteres.
- Duração: obrigatório, entre 1 e 400 minutos.
- Diretor: opcional.

Validações são aplicadas tanto nos DTOs quanto no Model, garantindo integridade dos dados.

## Como Executar

1. **Pré-requisitos**:
   - .NET 6 SDK
   - MySQL rodando localmente
2. **Configuração**:
   - Ajuste a connection string em `appsettings.json` conforme seu ambiente.
3. **Migrações**:
   - Execute `Update-Database` no Package Manager Console para criar as tabelas.
4. **Execução**:
   - Rode o projeto via Visual Studio ou CLI: `dotnet run`

## Como Testar

- Utilize o Swagger para testar os endpoints via interface web.
- Pode usar ferramentas como Postman ou Insomnia para requisições HTTP.

## Swagger

A documentação dos endpoints está disponível automaticamente em `/swagger` quando a aplicação está em modo desenvolvimento.

## Observações

- O projeto segue boas práticas de separação de responsabilidades e validação.
- O uso de DTOs protege o modelo de domínio e facilita manutenção.
- O código está pronto para evoluir, podendo incluir autenticação, testes automatizados e outros recursos.

---

**Autor:** André Siqueira  
**Repositório:** [github.com/SiqueiraAndre/FilmesApi](https://github.com/SiqueiraAndre/FilmesApi)