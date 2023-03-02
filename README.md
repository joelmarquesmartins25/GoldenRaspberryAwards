# GoldenRaspberryAwards

## Objetivo
Desenvolver uma API RESTful para possibilitar a leitura da lista de indicados e vencedores
da categoria Pior Filme do Golden Raspberry Awards.

## Funcionamento do Sistema

- Ao iniciar, o sistema irá realizar a leitura do arquivo CSV "C:\Temp\movielist.csv" e irá inserir os registros em um banco de dados em memória;
- Caso o arquivo não exista no caminho especificado, nenhum registro será criado;
- Ao executar o projeto pelo Visual Studio, a página do Swagger será aberta com os endpoints disponíveis, permitindo realizar testes.
- A API RESTful foi criada de acordo com o nível 2 de maturidade de Richardson:
  - Todos os endpoints estão localizados em **api/movies** sendo necessário alterar apenas os verbos e o corpo da requisição conforme necessário.
- Para atender ao requisito principal da API de localizar os produtores com maior e menor intervalo de tempo entre prêmios, o enpoint é **api/movies/intervals**

## 💻 Tecnologias Utilizadas

- IDE: Microsoft Visual Studio Community 2022 (64-bit) - v17.5.0
- Plataforma: .Net Core 6.0
- Linguagem: C#
- Banco de Dados (em memória): EntityFramework
- Foi utilizado o Swagger para facilitar a execução dos testes da API. Ao executar a API, pode-se acessar https://localhost:7092/swagger/index.html para ver os endpoints disponíveis e testá-los, conforme imagem abaixo:

![image](https://user-images.githubusercontent.com/111138372/222316348-eab2a0af-986d-4379-9bea-00d55d2dca2c.png)

## Como Rodar o Projeto

1. Clonar o repositório em sua máquina usando: `git clone https://github.com/joelmarquesmartins25/GoldenRaspberryAwards.git`

### Pelo Terminal
2. Compilar a solution usando: `dotnet build GoldenRaspberryAwards\GoldenRaspberryAwards.sln`
3. Executar a aplicação usando: `dotnet run --project GoldenRaspberryAwards\GoldenRaspberryAwards\GoldenRaspberryAwards.csproj`

### Pelo Visual Studio
2. Abrir a solução com o Visual Studio 2022
3. Compilar e Executar a solution

4. A API estará rodando em http://localhost:7092

## Testando a API

Para testar a API, pode ser utilizado o Swagger, ou um programa como o Postman ou ainda uma extensão de navegador como a Rested.

## Testes de Integração

- Para os testes de integração está sendo utilizado um arquivo separado chamado movielist.csv na pasta Data do projeto IntegrationTest.
- Estão sendo validados os endpoints **api/movies** e **api/movies/intervals**
- Para executar os testes de integração, use o comando `dotnet test GoldenRaspberryAwards\IntegrationTest\IntegrationTest.csproj`
  - Ou utilize o Visual Studio, clique com direito no projeto IntegrationTest e em "Run Tests"
- A execução dos testes de integração não necessita da API rodando para funcionar.

## Requisito da API e Resultados Esperados

![image](https://user-images.githubusercontent.com/111138372/222316933-7049ff96-b400-4c60-bc87-e802474fb16f.png)

Para atender o requisito da API, deve-se utilizar o endpoint abaixo:

### /api/movies/intervals

Exemplo de resultado esperado:

```json
{
  "min": [
    {
      "producer": "Joel Silver",
      "interval": 1,
      "previousWin": 1990,
      "followingWin": 1991
    }
  ],
  "max": [
    {
      "producer": "Matthew Vaughn",
      "interval": 13,
      "previousWin": 2002,
      "followingWin": 2015
    }
  ]
}
