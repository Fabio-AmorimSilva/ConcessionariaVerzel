"# ConcessionariaVerzel" 

O projeto foi feito usando Arquitetura Limpa - Arquitetura Cebola (Onion Architecture)
- Possuí 4 camadas no back-end e 1 camada front-end

- Back-end
  - Concessionaria.API
  - Concessionaria.Aplicacao
  - Concessionaria.Dominio
  - Concessinaria.Infraestrutura

- Front-end
  - Concessinaria.Angular

"#Montagem do banco de dados"

- O banco de dados utilizado foi  Microsoft SQL Server 2019
- A string de conexão para utilizar o banco é:
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=NomeDoSeuServidor; Database=NomeDoSeuBancoDeDados; User Id=NomeDoSeuUsuario; Password=SenhaDoSeuUsuario;Integrated Security=true; TrustServerCertificate=true;"
  }
}
- A string de conexão pdoe ser colocada tanto dentro do arquivo secrets.json quanto appsettings.json (dentro de Concessionaria.API)

- Após configurar a string de conexão:

- O ORM utilizado foi o EF Core 7
- As migrations já estão no projeto Concessionaria.Infraestrutura
- Basta entrar na pasta Concessionaria.Infraestrutura via terminal e executar o comando "dotnet ef database update --startup-project ../Concessionaria.API" via terminal para criar as tabelas no banco de dados 

"#Caracteristícas do Projeto back-end"

- O projeto foi desenvolvido utilizando a IDE Visual Studio 2022
- O back-end é composto de uma API Rest (olhar Concessionaria.API)
- Possuí validações com a utilização de FluentAPI/FluentValidition (olhar em Validations em Concessionaria.Aplicacao)
- Possuí paginação skip/take
- Possuí buscas via PredicateBuilder (olhar Params em Concessionaria.Aplicacao)
- A API possuí autenticação via token JWT
- A parte da API que concerne as operações de cadastro, edição e deleção de carros só pode ser acessada por um usuário do tipo admin
- Os carros possuem 5 tipos que são definitos com a utilização de uma classe do tipo enumeration (olhar  Concessionaria.Dominio Core/Enumeration e Models/Enumerations) - https://learn.microsoft.com/pt-br/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/enumeration-classes-over-enum-types
- Um usuário admin já é introduzido no banco de dados quando o mesmo é montado (olhar ApplicationDbContext em Concessionaria.Infraestrutura)
  - nomeUsuário: admin
  - senha: senha123
- O método login gera o token Jwt para utilização nas operações da API
- Basta copiar o token e utilizar no programa de sua preferência (Swagger, Postman, etc)

"#Caracteristícas do Projeto front-end"
- O projeto foi realizado em Angular
- Possuí guardas de rotas para usuário do tipo admin
- Possuí tela de login para usuário
- É possível realizar buscas de carros de acordo com alguns valores via barra de pesquisa
- As opções de cadastrar, editar e remover carros só irão aparecer caso um usuário do tipo admim esteja logado, em caso de usuário do tipo comum apenas a listagem e a busca serão 
- Na tela de login também existe um link para registrar um usuário, mas os usuários que podem ser criados são apenas usuários comuns
- Erros são mostrados de acordo com as validações da FluentApi do back-end. Os erros aparecem em uma snackbar durante 5 segundos para o usuário

"#Rodar o projeto"

- Para o funcionamento correto a etapa de "Montagem de banco de dados" deve ter sido realizada antes
- Para rodar o projeto siga os passos a seguir:
- Pelo terminal entrar na pasta Concessionaria.API e executar o comnando dotnet run
- Pelo terminal entrar na pasta Concessionaria.Angular e executar os comandos na exata ordem npm install e depois ng serve
- Esperar o carregamento de ambos os projetos
- Basta entrar no endereço fornecido pelo ng serve (Aparece no terminal)
