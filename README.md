# RentalDeliverer

**RentalDeliverer** é uma aplicação para gerenciar aluguel de motos e entregadores, desenvolvida com C# e .NET. A aplicação utiliza:

- **PostgreSQL** para banco de dados relacional.
- **MongoDB** para armazenamento de notificações.
- **RabbitMQ** para mensageria.
- **Entity Framework** como ORM.
- **Azure Blob Storage** para armazenamento de imagens.

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

- `/motos` Endpoint responsavel pela Criação de uma moto no sistema. Ele recebe os dados estruturados de acordo com o JSON abaixo, confere se ja tem uma placa cadastrada se não, ela é criada na tabela Motorcycles no postgres.
```json
{
  "identificador": "string",
  "ano": 0,
  "modelo": "string",
  "placa": "string"
}
