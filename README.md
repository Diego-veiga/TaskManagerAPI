# Desafio Técnico - SWFAST

## Descrição
Este projeto foi desenvolvido como parte de um desafio técnico com o objetivo de demonstrar habilidades práticas em desenvolvimento de software utilizando a plataforma .NET . O foco principal é a construção de uma API RESTful para gerenciamento de tarefas (to-do list), utilizando C# no backend e persistência de dados em um banco Microsoft SQL Server.

A aplicação expõe endpoints que permitem operações de criação, listagem, atualização de status e remoção de tarefas, seguindo boas práticas de arquitetura e desenvolvimento, como uso de injeção de dependência, camada de serviços, repositórios e tratamento de erros.

Cada tarefa possui um título, descrição, data de conclusão prevista e um status que pode ser "Pendente" ou "Concluída". As tarefas são organizadas por status na listagem, permitindo uma visualização clara do progresso do usuário.

O projeto encontra-se disponível publicamente no GitHub e está pronto para ser consumido por qualquer aplicação frontend, independente da tecnologia utilizada.

# Tabela de conteúdos

<!--ts-->

- [Pré-requisitos](#requisito)
- [Execução do projeto](#execucao)
- [Endpoints da API](#endpoints)
- [Execução dos testes unitários](#testes)
- [Tecnologias](#tecnologias)
- [Autor](#autor)
<!--te-->

<h2 id="requisito">Pré-requisitos</h2>
Para rodar a aplicação localmente, você precisará ter instalado na sua máquina:

- .NET SDK 8.0 ou superior: [Download do .NET](https://dotnet.microsoft.com/download)
- Microsoft SQL Server (local ou via Docker)
- Visual Studio 2022 (com suporte ao .NET 8) ou Visual Studio Code

<h2 id="execucao">Execução do projeto</h2>

Para executar o projeto, siga os seguintes passos:

#### Clone o projeto

    Repositório https://github.com/Diego-veiga/TaskManagerAPI

#### Execute as Migrations:
  Ajuste a connection string no arquivo appsettings.json e, em seguida, execute o seguinte comando:

  ```bash
dotnet ef database update --project TaskManager.Infrastructure --startup-project TaskManager.API
```

#### Inicie a aplicação:

Você pode iniciar a aplicação diretamente pelo Visual Studio (clicando em "Run") ou posicionado na pasta TaskManager\TaskManager.API utilizando o terminal com o comando:
```bash
dotnet run 
```

A aplicação estará disponível na porta 3000, através do link `http://localhost:5128`


<h2 id="endpoints">Endpoints da API</h2>

#### POST `/Tasks/`

Cadastra uma Tarefa 

**Requisição:**

- Content-Type: application/json

- Body da requisição 

``` bash
{
  "title": "string",
  "description": "string",
  "expectedCompletionDate": "Date"
}
```

**Resposta:**

- 201 Created: Tarefa Criada com sucesso.
- 400 Bad Request: Os dados foram enviados de forma incorreta 
- 500 Bad Request: Ocorreu um erro no processamento.


#### PATCH `/Tasks/{id}`

Realiza a atualização do Status da tarefa 

**Requisição:**

- - Content-Type: application/json

- - Body da requisição 

``` bash
{
  "status": Number
}
```

**Resposta:**

- 204 No Content: Status da tarefa foi atualizado com sucesso.
- 400 Bad Request: Os dados foram enviados de forma incorreta 
- 404 Not Found: Tarefa não foi encontrada 
- 500 Bad Request: Ocorreu um erro no processamento.


#### GET `/Tasks/{id}`

Realiza a busca da tarefa  pelo id

**Requisição:**

- - Content-Type: application/json


**Resposta:**

- 200 OK: Tarefa foi encontrada com sucesso.
- 404 Not Found: Tarefa não foi encontrada 
- 500 Bad Request: Ocorreu um erro no processamento.


#### GET `/Tasks/`

Realiza a busca de todas as tarefas ativas 

**Requisição:**

- Content-Type: application/json


**Resposta:**

- 200 No Content: Retorna uma lista das tarefas mesmo que vazia 
- 500 Bad Request: Ocorreu um erro no processamento.


#### DELETE `/Tasks/{id}`

Realiza a exclusão da tarefa pelo id

**Requisição:**

- - Content-Type: application/json


**Resposta:**

- 204 No Content: Tarefa foi encontrada com sucesso.
- 404 Not Found: Tarefa não foi encontrada 
- 500 Bad Request: Ocorreu um erro no processamento.

<h2 id="testes">Execução dos Testes Unitários</h2>
 O projeto conta com testes unitários implementados para garantir a qualidade e a confiabilidade do código.

### Como executar os testes
Você pode executar os testes de duas formas:

- Via Visual Studio: Basta abrir o Test Explorer e executar todos os testes.

- Via CLI: Navegue até a pasta TaskManager\TaskManager.UnitTests e execute:

```bash
dotnet test
```

### Verificando a cobertura de testes:

#### Via Visual Studioo 
Execute os testes no Visual Studio e, em seguida, utilize a funcionalidade nativa de "Code Coverage" para visualizar os resultados.

#### Via CLI 

- Instale o pacote **dotnet-reportgenerator-globaltool** através do comando:
```bash
dotnet tool install --global dotnet-reportgenerator-globaltool
```
Navegue até a pasta TaskManager\TaskManager.UnitTests e execute:

```bash
Run: dotnet test --verbosity minimal --collect:"XPlat Code Coverage"
```

Após a execução, acesse a pasta TestResults\<id_gerado> e gere o relatório HTML:

```bash
Run: reportgenerator "-reports:coverage.cobertura.xml" "-targetdir:coveragereport" -reporttypes:Html
```

Abra o arquivo index.html dentro da pasta coveragereport para visualizar o dashboard.

## Tecnologias

- [NET Core](https://learn.microsoft.com/pt-br/dotnet/)
- [Entity Framework Core](https://learn.microsoft.com/pt-br/ef/core/)
- [FluentValidation](https://docs.fluentvalidation.net/en/latest/installation.html)
- [xUnit](https://xunit.net/)


## Autor

<a href="https://www.linkedin.com/in/diegorobertoveiga/">
 <img style="border-radius: 50%;" src="https://avatars.githubusercontent.com/u/62670446?s=400&u=ce360c7bc3872fde7996a64a630c3a44ecb1ed30&v=4" width="100px;" alt=""/>
 <br />
 <sub><b>Diego Veiga</b></sub></a> <a href="https://www.linkedin.com/in/diegorobertoveiga/" title="Diego Veiga">🚀</a>