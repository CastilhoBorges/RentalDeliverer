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
- **5075** - API local
- **7201** - Swagger/HTTP local
- **15672** - RabbitMQ Management UI
- **27017** - MongoDB

---

## Passo a Passo para Configuração

1. **Clone o repositório:**

   ```bash
   git clone https://github.com/SEU_USUARIO/rental-deliverer.git
   cd rental-deliverer

2. **Restaure as dependências do projeto:**

   ```bash
   dotnet restore
   
3. **Suba os containers necessários com o Docker Compose:**
   ```bash
   docker-compose up -d

4. **Execute a aplicação localmente:**
   ```bash
   dotnet run
