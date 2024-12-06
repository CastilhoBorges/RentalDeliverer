# RentalDeliverer 1.0

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

- **Swagger HTTPS:** [http://localhost:7201](https://localhost:7201/swagger/index.html)
- **RabbitMQ:** [http://localhost:15672](http://localhost:15672)
  - **Usuário padrão:** `admin`
  - **Senha padrão:** `admin@123`

---

## Casos de uso para teste

- `POST/motos` Endpoint responsável pela criação de uma moto no sistema. Ele recebe os dados estruturados de acordo com o JSON abaixo, verifica se já existe uma placa cadastrada; se não, a moto é criada na tabela Motorcycles no PostgreSQL. Quando uma moto é criada, é gerada uma notificação publicada na fila do RabbitMQ. Caso o ano da moto seja 2024, um consumidor salva essa mensagem no MongoDB.
```json
{
  "identificador": "string",
  "ano": 0,
  "modelo": "string",
  "placa": "string"
}
```

- `GET/motos?placa` Endpoint responsável pela listagem de todas as motos cadastradas na tabela Motorcycles. É possível filtrar motos pela placa. Caso o parâmetro de placa não seja enviado, a API retorna um array de objetos com todas as motos.

- `PUT/motos/{id}/placa` Permite corrigir a placa de uma moto cadastrada. É necessário fornecer o ID da moto e a nova placa no corpo da requisição.
{
  "placa": "string"
}

- `DELETE/motos/{id}` Caso uma moto não esteja alugada, é possivel deleta-la do banco, apenas passando seu id.

- `POST/entregadores` Permite que um entregador se cadastre para alugar uma moto. Aceita apenas CNHs dos tipos A, B e AB. A imagem da CNH é armazenada no Azure Blob Storage, e no banco de dados é salva apenas a URL.
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

- `POST/entregadores/{id}/cnh` Atualiza a foto da CNH do entregador. Apenas imagens no formato PNG ou BMP são aceitas e armazenadas no Azure Blob Storage.
```json
{
  "imagem_cnh": "string"
}
```

- `POST/locacao` Ativa a locação de uma moto por um entregador. A data de início deve ser no mínimo um dia após o momento de cadastro. O plano pode ser um dos valores mencionados no JSON. É retornado o id da locação, recomendo salva-lo para usar em endpoints futuros.
```json
{
  "entregador_id": "string",
  "moto_id": "string",
  "data_inicio": "2024-12-04T17:36:25.067Z",
  "data_termino": "2024-12-04T17:36:25.067Z",
  "data_previsao_termino": "2024-12-04T17:36:25.067Z",
  "plano": {7 15 30 45 50}
}
```

- `PUT/locacao/{id}/devolucao` Nessa Rota o entregador consegue saber qual o valor que vai ser pago na sua locação, lembra no ultimo endpoint que pedi para guardar o id da locação, então, ele vai ser importante aqui, adicione ele no id da url, e no Body defina a data que vai ser devolvida a moto, Com isso se a data for igual a que voce definiu na previsão quando alugou, o valor segue o padrão abaixo
    - 7 dias: R$30/dia
    - 15 dias: R$28/dia
    - 30 dias: R$22/dia
    - 45 dias: R$20/dia
    - 50 dias: R$18/dia
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
