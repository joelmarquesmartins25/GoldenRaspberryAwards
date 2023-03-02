# GoldenRaspberryAwards

## Objetivo
Desenvolver uma API RESTful para possibilitar a leitura da lista de indicados e vencedores
da categoria Pior Filme do Golden Raspberry Awards.

## 💻 Tecnologias Utilizadas

- IDE: Microsoft Visual Studio Community 2022 (64-bit) - v17.5.0
- Plataforma: .Net Core 6.0
- Linguagem: C#
- Banco de Dados (em memória): EntityFramework
- Foi utilizado o Swagger para facilitar a execução dos testes da API. Ao executar a API, pode-se acessar https://localhost:7092/swagger/index.html para ver os endpoints disponíveis e testá-los, conforme imagem abaixo:

![image](https://user-images.githubusercontent.com/111138372/222316348-eab2a0af-986d-4379-9bea-00d55d2dca2c.png)

## Pré-Requisito
- O arquivo CSV deve estar localizado em: C:\Temp com o nome de movielist.csv

## Como Rodar o Projeto

1. Clone o repositório em sua máquina usando: `git clone https://github.com/joelmarquesmartins25/GoldenRaspberryAwards.git`
2. Execute o build do projeto pelo terminal na pasta criada usando: `dotnet build GoldenRaspberryAwards.sln`
3. Certifique-se de que o arquivo C:\Temp\movielist.csv existe
4. Execute a aplicação com o comando: `dotnet run GoldenRaspberryAwards.csproj`
5. A API estará rodando em http://localhost:7092

## Testando a API

Para testar a API, pode ser utilizado o Swagger, ou um programa como o Postman

## Testes de Integração
Para executar os testes de integração, está sendo utilizado um arquivo separado chamado movielist.csv na pasta Data do projeto de testes.
Está validando os endpoints api/movies e api/movies/intervals

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
