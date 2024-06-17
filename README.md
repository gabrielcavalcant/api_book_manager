# Book Manager

## Descrição

Este projeto é uma API para gerenciar uma biblioteca. Permite realizar operações de CRUD (Create, Read, Update, Delete) para livros. O backend foi desenvolvido em C# utilizando o .NET 5, com o banco de dados SQL Server. O Swagger foi utilizado para documentação da API e o JWT Bearer para autenticação.

## Funcionalidades

- **CRUD de Livros:**
  - Criar, ler, atualizar e deletar informações de livros.
- **Autenticação e Autorização:**
  - Tela de login com autenticação.
  - Utilização de tokens JWT Bearer para autenticação.

## Tecnologias Utilizadas

- **Backend:**
  - Linguagem: C#
  - Framework: .NET 5
  - Banco de Dados: SQL Server
- **Frontend:**
  - HTML, CSS
- **Documentação da API:**
  - Swagger
- **Autenticação:**
  - JWT Bearer

## Instalação
1. Baixe Visual Studio Comunity para utilizar C# e .net 5.

2. Clone o repositório:
   ```bash
   git clone https://github.com/gabrielcavalcant/api_book_manager.git
   cd book_manager

## Configuração do Banco de Dados

### appsettings.json

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=seu_servidor;Database=sua_base_de_dados;User Id=seu_usuario;Password=sua_senha;"
  }
}
```

## Restaure os pacotes NuGet

Para restaurar os pacotes NuGet, utilize o seguinte comando no terminal:

```bash
dotnet restore
```

## Restaure os pacotes NuGet

Para restaurar os pacotes NuGet, utilize o seguinte comando no terminal:

```bash
dotnet restore
```

## Configuração

Execute as migrações para criar o banco de dados com o seguinte comando:

```bash
dotnet ef database update
```

## Execução

Para iniciar a aplicação, utilize o comando abaixo:

```bash
dotnet run
```

### Acesse a Documentação da API pelo Swagger

Você pode acessar a documentação da API pelo Swagger utilizando o seguinte endereço no seu navegador:

```bash
https://localhost:44379/swagger
```

## Uso da API

### Autenticação e Autorização

A API utiliza JWT Bearer tokens para autenticação. Para obter um token JWT, utilize o seguinte endpoint:

- **POST /v1/login**: Autentica o usuário e retorna um token JWT
#### Login:
![image](https://github.com/gabrielcavalcant/api_book_manager/assets/123522657/6cfa60a5-c370-4878-b71d-d65277516dca)
#### Respota:
![image](https://github.com/gabrielcavalcant/api_book_manager/assets/123522657/41b32d80-86c1-456d-ae23-706a0a43cbed)

Para realizar requisições autenticadas, envie o token JWT no cabeçalho `Authorization` de cada requisição protegida da seguinte maneira:

```http
Authorization: Bearer seu_token_jwt
```
![image](https://github.com/gabrielcavalcant/api_book_manager/assets/123522657/3a670dca-189c-4418-8906-4d111bec8a18)

### Endpoints

#### Livros

- **GET /api/Search/books**: Lista todos os livros
#### Pesquisa de livros:
![image](https://github.com/gabrielcavalcant/api_book_manager/assets/123522657/800689f3-c0ef-453b-aab7-2acf25726e5d)
#### Resposta:
![image](https://github.com/gabrielcavalcant/api_book_manager/assets/123522657/fceb15a8-60c8-48e0-9e85-3b17a047c383)

- **POST /api/Search/Post**: Adiciona um novo livro
#### Criação de um novo livro:
![image](https://github.com/gabrielcavalcant/api_book_manager/assets/123522657/cf8de7f3-2c59-4204-81cc-e9d030230855)
#### Resposta:
![image](https://github.com/gabrielcavalcant/api_book_manager/assets/123522657/e38a925a-a8cd-4d43-bb94-f60e361240e8)

- **PUT /api/Search/Put**: Atualiza informações de um livro
#### Atualização do dados do livro:
![image](https://github.com/gabrielcavalcant/api_book_manager/assets/123522657/6dbf6a0d-02ef-4bab-8616-1d736dff53ac)
#### Resposta:
![image](https://github.com/gabrielcavalcant/api_book_manager/assets/123522657/433e5095-d7df-47ac-9658-5a837d3397a8)

- **DELETE /api/Search/Delete/{id}**: Deleta um livro
#### Exclusão do livro por ID:
![image](https://github.com/gabrielcavalcant/api_book_manager/assets/123522657/9ff9f869-1c62-4fef-8da0-6331a660db48)
#### Resposta:
![image](https://github.com/gabrielcavalcant/api_book_manager/assets/123522657/86096ae2-7393-4230-8da1-a4420d7c89b5)



## Templates de Front End

O front end foi desenvolvido com HTML e CSS dentro do .NET. As funcionalidades de CRUD são demonstradas na interface:

- **Lista de Livros**: Mostra todos os livros disponíveis.
  
  ![image](https://github.com/gabrielcavalcant/api_book_manager/assets/123522657/bbc8ebdc-97ac-4b9f-a685-029fb60359bf)

- **Adicionar Livro**: Formulário para adicionar um novo livro.
  
![image](https://github.com/gabrielcavalcant/api_book_manager/assets/123522657/e3d80da1-8696-4306-ba53-a86b2d4af6b2)

- **Editar Livro**: Formulário para editar um livro existente.
  
![image](https://github.com/gabrielcavalcant/api_book_manager/assets/123522657/5379f0d0-0429-44a1-bc00-a68c85d91874)

- **Deletar Livro**: Opção para deletar um livro.
  
![image](https://github.com/gabrielcavalcant/api_book_manager/assets/123522657/8c35dfa2-d763-4d37-8992-85b3c2f95740)


## Contato

Para mais informações, entre em contato:

- **Nome**: Gabriel Cavalcante
- **Email**: gabriel_sep@outlook.com
- **LinkedIn**: [Gabriel Cavalcante](https://www.linkedin.com/in/--gabrielcavalcante/)
---
- **Nome**: Renan Henrique de Oliveira
- **Email**: renanoliveira30@hotmail.com
- **LinkedIn**: [Renan Henrique de Oliveira](https://www.linkedin.com/in/renan-henrique-nunjiswoo/)

