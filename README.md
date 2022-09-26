# Desafio-Star_Wars_API

Teste prático API de cadastro do jogo de Star Wars.

Segue abaixo a url da documentação da api utilizando o swagger

https://localhost:44315/swagger/index.html

É necessario alterar a string de conexão do projeto no aquivo appsettings.json no projeto StarWars.Api, após alterar a string de conexão é só rodar o comando descrito abaixo no Package Manager Console.

Update-Database -StartupProject StarWars.Api -Project StarWars.Infra.Data

Foram feito os testes unitarios da camada de aplicação onde fica a regra de negocio.

Os logs serão gerados no diretorio
StarWars.Api\TesteStarWarsAPI\src\StarWars.Api\wwwroot\Logs

Abaixo segue exemplos de um POST 
URL: https://localhost:44315/api/v1/CadastroPlaneta
Passar o json abaixo no body da requisição:

{
  "nome": "Terra",
  "clima": "Dagobah",
  "terreno": "umido",
  "filmes": [
    {
      "nome": "Rogue ONE",
      "diretor": "Gareth Edwards",
      "dataLancamento": "2016-12-15"
    },
     {
      "nome": "OS ÚLTIMOS JEDI",
      "diretor": "Rian Johnson",
      "dataLancamento": "2014-12-14"
    }
  ]
}

Resposta:
{
    "data": {
        "id": 5,
        "nome": "Terra",
        "clima": "Dagobah",
        "terreno": "umido",
        "filmes": [
            {
                "id": 6,
                "nome": "Rogue ONE",
                "diretor": "Gareth Edwards",
                "dataLancamento": "2016-12-15T00:00:00"
            },
            {
                "id": 7,
                "nome": "OS ÚLTIMOS JEDI",
                "diretor": "Rian Johnson",
                "dataLancamento": "2014-12-14T00:00:00"
            }
        ]
    }
}



