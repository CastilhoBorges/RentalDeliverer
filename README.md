# RentalDeliverer

**RentalDeliverer** é uma aplicação para gerenciar aluguel de motos e entregadores, desenvolvida com C# e .NET. A aplicação utiliza:

- **PostgreSQL** para banco de dados relacional.
- **MongoDB** para armazenamento de notificações.
- **RabbitMQ** para mensageria.
- **Entity Framework** como ORM.
- **Azure Blob Storage** para armazenamento de imagens.

---

## Índice

1. [Configuração de Ambiente](#configuração-de-ambiente)
2. [Passo a Passo para Configuração](#passo-a-passo-para-configuração)
3. [Acessando os Serviços](#acessando-os-serviços)
4. [Casos de Uso para Teste](#casos-de-uso-para-teste)
---

## Configuração de Ambiente

Antes de iniciar, certifique-se de ter os seguintes pré-requisitos instalados:

- [.NET SDK 9.0](https://dotnet.microsoft.com/download/dotnet/9.0)
- [Docker](https://www.docker.com/get-started)
- [Docker Compose](https://docs.docker.com/compose/install/)

Além disso, garanta que as seguintes portas estejam liberadas no seu ambiente:

- **5432** - PostgreSQL
- **5075** - HTTP Local
- **7201** - HTTPS Local
- **15672** - RabbitMQ 
- **27017** - MongoDB

---

## Passo a Passo para Configuração

1. **Clone o repositório:**

   ```bash
   git clone https://github.com/CastilhoBorges/RentalDeliverer.git
   cd RentalDeliverer

2. **Restaure as dependências do projeto:**

   ```bash
   dotnet restore
   
3. **Suba os containers necessários com o Docker Compose:**
   ```bash
   docker-compose up -d

4. **Execute a aplicação localmente:**
   ```bash
   dotnet run

---

## Acessando os Serviços

- **Swagger HTTP:** [http://localhost:5075](http://localhost:5075/swagger/index.html)
- **Swagger HTTPS:** [http://localhost:7201](https://localhost:7201/swagger/index.html)
- **RabbitMQ:** [http://localhost:15672](http://localhost:15672)
  - **Usuário padrão:** `admin`
  - **Senha padrão:** `admin@123`

---

## Casos de uso para teste

- `POST/motos` Endpoint responsavel pela Criação de uma moto no sistema. Ele recebe os dados estruturados de acordo com o JSON abaixo, confere se ja tem uma placa cadastrada se não, ela é criada na tabela Motorcycles no postgres. Quando é criada uma moto, é gerado uma notificação, onde um publisher publica na fila do RabbitMq, e caso o ano da moto seja 2024, um consumidor nessa fila salva essa mensagem com a placa e data no MongoDb.
```json
{
  "identificador": "string",
  "ano": 0,
  "modelo": "string",
  "placa": "string"
}
```

- `GET/motos?placa` Endpoint responsavel pela listagem de todas as motos cadastradas na tabela Motorcycle. Com a possibilidade de filtrar as motos pela placa, caso o parametro da placa não seja enviado, a api traz um array de objetos com todas as motos.

- `PUT/motos/{id}/placa` Caso ao cadastrar a moto, colocamos a placa errada, aqui conseguimos consertar isso, passamos o id da moto que desejamos que seja alterada e no body a placa que subtituira a errada.
{
  "placa": "string"
}

- `DELETE/motos/{id}` Caso uma moto não esteja alugada, é possivel deleta-la do banco, apenas passando seu id.

- `POST/entregadores` Assumindo o papel de entregador, posso me cadastrar para poder alugar uma moto, enviando os dados de acordo com o JSON abaixo. Lembrando que so é permitido CNH's do tipo A, B e AB. A imagem nessa rota, é armazenada no storage do Azure, e no banco guardamos apenas a URL que da acesso a ela.
```json
{
  "identificador": "string",
  "nome": "string",
  "cnpj": "string",
  "data_nascimento": "2024-12-04T17:31:03.269Z",
  "numero_cnh": "string",
  "tipo_cnh": "string",
  "imagem_cnh": "string"
}
```

- `POST/entregadores/{id}/cnh` Endpoint que recebe a foto atualizada da CNH do entregador, e se ela for png ou bmp, é salva no storage da Azure.
```json
{
  "imagem_cnh": "string"
}
```

- `POST/locacao` Rota usada para ativar a locação de um entregador com os dados do JSON abaixo. Obrigatoriamente a data de inicio deve ser um dia a frente do momento que esta feito o cadastro e o plano algum dos mencioandos no JSON. Quando a locação é efetuada, é retornado o id da locação, recomendo salva-lo para usar em endpoints futuros.
```json
{
  "entregador_id": "string",
  "moto_id": "string",
  "data_inicio": "2024-12-04T17:36:25.067Z",
  "data_termino": "2024-12-04T17:36:25.067Z",
  "data_previsao_termino": "2024-12-04T17:36:25.067Z",
  "plano": {7 OU 15 OU 30 OU 45 OU 50}
}
```

- `PUT/locacao/{id}/devolucao` Nessa Rota o entregador consegue saber qual o valor que vai ser pago na sua locação, lembra no ultimo endpoint que pedi para guardar o id da locação, então, ele vai ser importante aqui, adicione ele no id da url, e no Body defina a data que vai ser devolvida a moto, Com isso se a data for igual a que voce definiu na previsão quando alugou, o valor segue o padrão abaixo
    - 7 dias com um custo de R$30,00 por dia
    - 15 dias com um custo de R$28,00 por dia
    - 30 dias com um custo de R$22,00 por dia
    - 45 dias com um custo de R$20,00 por dia
    - 50 dias com um custo de R$18,00 por dia
- Caso a data seja menor, sera cobrada uma multa com os seguintes criterios:
   - Para plano de 7 dias o valor da multa é de 20% sobre o valor das diárias não efetivadas.
   - Para plano de 15 dias o valor da multa é de 40% sobre o valor das diárias não efetivadas.
   - Para plano de 30 dias o valor da multa é de 60% sobre o valor das diárias não efetivadas.
   - Para plano de 45 dias o valor da multa é de 80% sobre o valor das diárias não efetivadas.
   - Para plano de 50 dias o valor da multa é de 100% sobre o valor das diárias não efetivadas.
- Caso a data seja maior, sera cobrada para cada dia a mais, 50 reais de diaria.
```json
{
  "data_devolucao": "2024-12-04T17:39:54.019Z"
}
```
