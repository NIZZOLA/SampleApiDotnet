# Como Rodar Migrations no Projeto

Para executar uma migração, dado que o projeto da Api que contém as configurações do Contexto está em projeto diferente do que o Contexto, precisamos seguiralguns passos para a execução de uma migration, primeiramente, deve-se executar o terminal prompt na pasta do projeto: Interep.OneClick.SearchApi

Caso não tenha comandos dotnet-ef, execute a linha de comando abaixo:dotnet tool install --global dotnet-ef

Após a instalação, execute o comando a seguir, para criar uma nova migration, execute o comando abaixo na pasta: Data.Sql
dotnet ef migrations add First --project ../Store.Api/Store.Api.csproj

caso queira remover a Migration criada, execute o comando abaixo:
dotnet ef migrations remove --project ../Store.Api/Store.Api.csproj

Caso queira gerar o script de migração a ser aplicado, execute o comando abaixo:
dotnet ef migrations script -o ../../Store.Infra.Data.Sql/script.sql --project ../Store.Api/Store.Api.csproj


Caso queira aplicar as migratiopns
dotnet ef database update --project ..\Store.Infra.Data.Sql\Store.Infra.Data.Sql.csproj --context StoreContext
