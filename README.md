# FilmesApi

API REST para cadastro, consulta, atualização e remoção de filmes, desenvolvida em .NET 9 com Entity Framework Core, MySQL e autenticação JWT.

## Sumário

- [Visão Geral](#visão-geral)
- [Tecnologias Utilizadas](#tecnologias-utilizadas)
- [Arquitetura](#arquitetura)
- [Modelos e DTOs](#modelos-e-dtos)
- [Endpoints](#endpoints)
- [Validações](#validações)
- [Autenticação](#autenticação)
- [Como Executar](#como-executar)
- [Como Testar](#como-testar)
- [Swagger](#swagger)
- [Observações](#observações)

---

## Visão Geral

A FilmesApi é uma aplicação backend que permite gerenciar um catálogo de filmes. Ela oferece operações CRUD completas, validação de dados, autenticação JWT, documentação automática via Swagger e persistência em banco de dados relacional.

## Tecnologias Utilizadas

- .NET 9
- C# 13
- Entity Framework Core
- MySQL
- AutoMapper
- Swagger (Swashbuckle)
- ASP.NET Core Web API
- JWT (Json Web Token)
- BCrypt.Net (hash de senha)

## Arquitetura

- **Controllers**: Responsáveis por receber requisições HTTP, validar dados e retornar respostas.
- **Models**: Representam as entidades do banco de dados.
- **DTOs**: Objetos de transferência de dados para entrada (Create/Update/Login) e saída (Read).
- **DbContext**: Gerencia a conexão e operações com o banco de dados.
- **AutoMapper**: Facilita a conversão entre DTOs e Models.
- **Autenticação JWT**: Protege os endpoints da API, exigindo login para acesso.

## Modelos e DTOs

- **Filme**: Entidade principal, representa um filme no banco de dados.
- **CreateFilmeDto / UpdateFilmeDto**: Utilizados para entrada de dados (criação/atualização), com validações.
- **ReadFilmeDto**: Utilizado para saída de dados.
- **Usuario / UsuarioDto**: Entidade e DTO para autenticação e registro de usuários.

## Endpoints

| Método   | Rota                | Descrição                                 | Corpo/Requisição         | Resposta           |
|----------|---------------------|-------------------------------------------|--------------------------|--------------------|
| POST     | `/filme`            | Adiciona um novo filme (requer token)     | `CreateFilmeDto`         | 201 Created        |
| GET      | `/filme`            | Lista filmes (paginado, requer token)     | Query: `skip`, `take`    | 200 OK (lista)     |
| GET      | `/filme/{id}`       | Consulta filme por ID (requer token)      | -                        | 200 OK / 404 NotFound |
| PUT      | `/filme/{id}`       | Atualiza completamente um filme (token)   | `UpdateFilmeDto`         | 204 NoContent / 404 |
| PATCH    | `/filme/{id}`       | Atualiza parcialmente um filme (token)    | `JsonPatchDocument`      | 204 / 404 / 400    |
| DELETE   | `/filme/{id}`       | Remove um filme (requer token)            | -                        | 204 / 404          |
| POST     | `/auth/register`    | Registra novo usuário                     | `UsuarioDto`             | 200 OK / 400       |
| POST     | `/auth/login`       | Realiza login e retorna token JWT         | `UsuarioDto`             | 200 OK / 401       |

## Validações

- Título: obrigatório, máximo 50 caracteres.
- Gênero: máximo 50 caracteres.
- Duração: obrigatório, entre 1 e 400 minutos.
- Diretor: opcional.
- Usuário: username e senha obrigatórios.

Validações são aplicadas tanto nos DTOs quanto no Model, garantindo integridade dos dados.

## Autenticação

A API utiliza autenticação JWT para proteger os endpoints de filmes. O fluxo é:

1. **Registro:**  
   Envie um POST para `/auth/register` com JSON:
````````markdown
{
  "username": "string",
  "senha": "string"
}
````````
   - **Resposta bem-sucedida:** 200 OK com objeto do usuário registrado (sem senha).
   - **Erros comuns:** 400 Bad Request (dados inválidos), 409 Conflict (usuário já existe).

2. **Login:**  
   Envie um POST para `/auth/login` com JSON:
````````markdown
{
  "username": "string",
  "senha": "string"
}
````````
   - **Resposta bem-sucedida:** 200 OK com token JWT.
   - **Erros comuns:** 400 Bad Request (dados inválidos), 401 Unauthorized (credenciais incorretas).

3. **Acesso a Endpoints Protegidos:**  
   Inclua o token JWT no cabeçalho Authorization como um Bearer Token:
````````markdown
Authorization: Bearer {seu_token_aqui}
````````
   - **Erro comum:** 401 Unauthorized (token ausente ou inválido).

## Como Executar

1. **Pré-requisitos**:
   - .NET 9 SDK
   - MySQL rodando localmente
2. **Configuração**:
   - Ajuste a connection string em `appsettings.json` conforme seu ambiente.
   - Configure a chave secreta para o JWT em `appsettings.json`.
3. **Migrações**:
   - Execute `dotnet ef database update` para criar as tabelas.
4. **Execução**:
   - Rode o projeto via Visual Studio ou CLI: `dotnet run`

## Como Testar

- Utilize o Swagger para testar os endpoints via interface web.
- Pode usar ferramentas como Postman ou Insomnia para requisições HTTP.

### Testes Automatizados

O projeto possui testes automatizados utilizando xUnit e Moq, localizados na pasta `FilmesApi.Tests`. Os testes cobrem os principais fluxos dos controllers de autenticação e filmes, incluindo:

- Cadastro e login de usuários (AuthController)
- Adição, consulta, atualização e remoção de filmes (FilmeController)

Os testes utilizam banco de dados em memória para garantir isolamento e rapidez na execução.

#### Como executar os testes

No terminal, execute:
   ```bash
   dotnet test FilmesApi.Tests
   ```

## Swagger

A documentação dos endpoints está disponível automaticamente em `/swagger` quando a aplicação está em modo desenvolvimento.

## Observações

- O projeto foi atualizado para .NET 9 e C# 13.
- Certifique-se de que todos os pacotes NuGet estejam compatíveis com .NET 9.

---

**Autor:** André Siqueira  
**Repositório:** [github.com/SiqueiraAndre/FilmesApi](https://github.com/SiqueiraAndre/FilmesApi)