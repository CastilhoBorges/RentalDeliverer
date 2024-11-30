# Etapa 1: Build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# Copiar os arquivos principais da solução e do projeto
COPY RentalDeliverer.sln ./
COPY RentalDeliverer.csproj ./

# Copiar o restante do código (incluindo src)
COPY src ./src
COPY appsettings.* ./

# Restaurar as dependências
RUN dotnet restore RentalDeliverer.csproj

# Publicar o projeto
RUN dotnet publish RentalDeliverer.csproj -c Release -o out

# Etapa 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app

# Copiar os artefatos publicados para o container
COPY --from=build /app/out .

# Expor as portas configuradas no appsettings.json
EXPOSE 5075
EXPOSE 7201

# Definir o comando de entrada
ENTRYPOINT ["dotnet", "RentalDeliverer.dll"]
