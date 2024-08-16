# Inga Code Desafio

Este projeto foi desenvolvido como um desafio utilizando .NET 8 e Angular 17.
Para banco de dados foi ultilizado SQL Server

## Requisitos

- [Node.js](https://nodejs.org/)
- [Angular CLI](https://angular.io/cli)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) (recomendado) ou [Visual Studio Code](https://code.visualstudio.com/)
- [DOTNET SDK](https://dotnet.microsoft.com/en-us/download)
  

## Como Rodar o Projeto


1. Clone o repositório para o seu ambiente local.
   ```bash
   git clone https://github.com/angeloavelinoo/desafio-inga
   ```
### 2. Configuração do Frontend

1. Navegue até a pasta `frontend` no terminal.
2. Certifique-se de ter o **Node.js** e o **Angular CLI** instalados. Caso não tenha, instale-os.
3. Execute o comando abaixo para instalar as dependências do Angular:
    ```bash
    npm install
    ```
4. Para rodar o frontend, execute:
    ```bash
    ng serve -o
    ```

### 3. Configuração do Backend
   
1. Abra o projeto backend e navegue até a pasta `Persistence` no terminal.
2. Execute o comando abaixo para atualizar o banco de dados:
    ```bash
    dotnet ef database update --project Persistence.csproj --startup-project ../DesafioIngaCodeApi/DesafioIngaCodeApi.csproj
    ```
3. Caso for rodar o backend no visual studio code, utilize no terminal:
    ```bash
    dotnet run
    ```

---
