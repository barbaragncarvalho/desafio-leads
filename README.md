# Desafio Sistema de Leads

Este é um projeto de desafio, consistindo em um aplicativo de página única (SPA) para gerenciamento de leads.

O projeto é dividido em:

• **Backend:** Uma API RESTful .NET 6 (C#).

• **Frontend:** Um SPA (Single Page Application) em React.js.

• **Banco de Dados:** SQL Server.

• **Testes:** Uma camada de testes unitários (xUnit).

## Pré-requisitos

Inicialmente, é preciso ter as ferramentas instaladas:

• .NET 6 SDK

• SQL Server

• Node.js

## Como Executar o Projeto

O projeto é dividido em duas partes (Backend e Frontend) que precisam ser executadas simultaneamente em terminais separados.

### 1. Backend (.NET 6 API)

1.  **Clone o Repositório:**
    ```
    git clone https://github.com/barbaragncarvalho/desafio-leads.git
    cd desafio-leads
    ```

2.  **Configure o Banco de Dados:**
    * Abra o arquivo `LeadGerenciamento.Api/appsettings.json`.
    * Localize a `ConnectionStrings` e atualize o `DefaultConnection` para apontar para a sua instância local do SQL Server. O banco `leadsGerenciamento` será criado automaticamente.

3.  **Crie o Banco de Dados (Migrations):**
    * Abra um terminal na pasta `LeadGerenciamento.Api`.
    * Execute o comando abaixo para aplicar as migrações e criar as tabelas:
    ```
    dotnet ef database update
    ```

4.  **Execute o Backend:**
    * No mesmo terminal (`LeadGerenciamento.Api`), execute a API:
    ```
    dotnet run
    ```

---

### 2. Frontend (React SPA)

1.  **Abra um outro terminal.**
2.  Navegue até a pasta do frontend:
    ```
    cd frontend
    ```

3.  **Instale as dependências:**
    ```
    npm install
    ```

4.  **Configure a URL da API:**
    * Abra o arquivo `frontend/src/App.js`.
    * Encontre a constante `API_URL` no topo do arquivo.
    * Atualize a URL para ser a mesma URL `https` do seu backend.

5.  **Execute o Frontend:**
    * No mesmo terminal (`frontend`), execute o app React:
    ```
    npm start
    ```
    * O navegador abrirá automaticamente em `http://localhost:3000`.
      
---

## Como Popular o Banco (Postman)

Para testar, você pode criar leads "CONVIDADO" usando o Postman:
1.  **Método:** `POST`
2.  **JSON:**
    ```
    {
      "contactFirstName": "Bill",
      "contactFullName": "Bill Gates",
      "contactPhoneNumber": "0412345678",
      "contactEmail": "bill@test.com",
      "suburb": "Yanderra 2574",
      "category": "Painters",
      "description": "Need to paint 2 aluminum windows and a sliding glass door",
      "price": 62.00
    }
    ```

## Executando os Testes Unitários

Para verificar a lógica de negócios (como a regra de desconto de 10%):
1.  Abra um terminal na pasta raiz do projeto.
2.  Execute o comando:
    ```
    dotnet test
    ```
3.  Você deve ver um resultado verde indicando que todos os testes passaram.
