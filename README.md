# Desafio Backend

Esta aplicação simula de maneira simplificada o funcionamento de uma empresa de aluguel de carros.

## Tecnologias Utilizadas

- .NET 8
- MongoDB
- RabbitMQ
- Docker
- FluentValidation
- IMemoryCache

## Funcionalidades

![Arquitetura](./car-rental.jpeg)

- CRUD do Car
- CRUD do Order
- CRUD do User
- Comunicação entre as APIs User e RentalCompany, através do RabbitMQ
- Toda vez que um pedido é criado e simulado o valor do pedido
- Toda vez que o pedido for finalizado, é calculado o valor do pedido incluindo a multa em caso de atraso

Para esse projeto foram criadas duas APIs em .NET, sendo a User.API e RentalCompany.API.
A User.API é responsável pelo CRUD do Car, e também por enviar à Rental Company.API os carros disponíveis para o aluguel. Toda vez que é executado alguma ação do tipo Criar, Atualizar ou Deletar um carro, será enviado todos os carros para o RabbitMQ.
A Rental Company é responsável pelo CRUD do Customer, e também para salvar localmente a imagem da CNH do mesmo, além disso, ela é responsável por criar e finalizar os pedidos de aluguel de carros. Toda vez que um carro é alugado, é enviado a mensagem a User.API para atualizar o status do carro, tendo o carro atualizado, e reenviado os carros disponiveis para a RentalCompany.API, o mesmo ocorre quando um carro é devolvido.

## Como rodar o projeto

- Para inicializar o RabbitMq e o MongoDB.

```bash
docker-compose up
```

- Para iniciar as APIs, iniciar o terminal dentro da pasta desafio-backend

```bash
dotnet run --project ./User/User.Api/User.Api.csproj.
```

- Em outro terminal, executar

```bash
dotnet run --project ./RentalCompany/RentalCompany.Api/RentalCompany.Api.csproj
```

- Criar a pasta "C:\CnhImages", e habilitar as permissoes de escrita e leitura para que a API RentalCompany, consiga salvar e consultar as imagens da CNH gravadas.

## Melhorias

- Adicionar Autenticação
- Adicionar Testes de integração
- Tirar alguns Hard Codes do RabbitMq
- Adicionar o Dockerfile nas APIs e integrar ao docker compose
- Adicionar Logs na API
- Adicionar Teste Unitarios
- Adicionar mais detalhes no Swagger, melhorei em alguns endpoints, mas não surgiu efeito, precisa ser verificado.
- Adicionar mais status ao pedido
- Adicionar campo email ao Customer e RentalCompany, para serem notificados quando for criado um pedido
- Adicionar API de Pagamento
