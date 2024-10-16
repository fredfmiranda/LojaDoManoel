# LojaDoManoel API

## Descrição

Este projeto é uma API REST desenvolvida em .NET, que oferece funcionalidades para processar pedidos e determinar a melhor forma de embalar produtos em caixas. 
A API utiliza o algoritmo Best Fit Decreasing para otimizar o empacotamento, além de incluir teste unitário e suporte a autenticação básica via token JWT (Bearer).
- Faça uma requisição para o endpoint /auth/login com as credenciais de usuário.(user e password)
- Receba o token JWT no corpo da resposta.
- Inclua o token no cabeçalho das suas requisições como segue: Bearer <seu_token>

## Funcionalidades

- Processar pedidos de produtos.
- Determinar a melhor maneira de embalar produtos em caixas.
- Algoritmo de empacotamento otimizado (Best Fit Decreasing).
- API documentada com Swagger.
- Suporte a autenticação via token JWT.
- Testes unitários implementados com Xunit.

## Requisitos

- .NET 8.0 SDK
- Docker (para executar via container)
- Visual Studio (para desenvolvimento local)

## Executando a aplicação

### Executar localmente com Visual Studio

1. Clone o repositório para sua máquina:
   ```bash
   git clone https://github.com/fredfmiranda/LojaDoManoel.git

2. Abra a solução no Visual Studio
3. Execute o projeto. A API estará disponível na URL: https://localhost:7205/swagger/index.html

### Executar com Docker

1. Clone o repositório para sua máquina:
   ```bash
   git clone https://github.com/fredfmiranda/LojaDoManoel.git

2. Acesse o diretório do projeto:
  cd LojaDoManoel

3.Construa a imagem Docker:
docker build -t loja-api .

4.Execute o container:
docker run -d -p 8080:80 --name loja-api-container loja-api

5.Acesse o Swagger da API:
http://localhost:8080/swagger/index.html

### Algoritmo de Empacotamento

- Ordenação: Os produtos são ordenados pelo volume de forma decrescente.
- Busca: Para cada produto, é encontrada a caixa com o menor espaço restante que pode acomodar o produto.
- Atualização: Se o produto couber na caixa, ele é adicionado e o espaço da caixa é atualizado.
- Nova Caixa: Se o produto não couber em nenhuma das caixas usadas, uma nova caixa é selecionada.
- Produto Grande: Se o produto não couber em nenhuma das caixas disponíveis, ele é marcado como "não cabe em nenhuma caixa disponível".
