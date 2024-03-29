# UserManager API

## Sobre o Projeto

A UserManager API é uma aplicação ASP.NET Core projetada para gerenciar usuários. Ela oferece funcionalidades essenciais como cadastro de usuários, login e confirmação de e-mail. Ao criar um usuário, a API automaticamente envia um e-mail com uma URL de confirmação para habilitar o login do usuário.

### Tecnologias Utilizadas

- **ASP.NET Core 8:** Framework para construção da API.
- **Entity Framework Core 8:** ORM para acesso a dados.
- **Microsoft.AspNetCore.Identity:** Para autenticação e gerenciamento de usuários.
- **Swashbuckle (Swagger):** Para documentação da API.
- **Microsoft.EntityFrameworkCore.SqlServer:** Para conexão com o banco de dados SQL Server.
- **Docker:** Para facilitar o deploy e a execução da aplicação.

## Como Executar

## Configuração do Banco de Dados

A aplicação usa o SQL Server. Para alterar a conexão, modifique a string de conexão no arquivo `appsettings.json`.
```json
{
   "ConnectionStrings": {
   "DefaultConnection": "Server={seu-server};Database=UserManager;Trusted_Connection=True;TrustServerCertificate=True;"
 }
}
```

### Configurações de E-mail

Para o envio de e-mails de confirmação, configure as seguintes propriedades no arquivo `appsettings.json`:

```json
"EmailSettings": {
  "BaseUrl": "https://localhost:7103"
},
"SmtpSettings": {
  "Username": "<seu-email-do-gmail>",
  "Password": "<senha-de-aplicativo-do-gmail>"
}
```
**Nota:** Para o campo Password em SmtpSettings, utilize a "senha de aplicativo" gerada nas configurações de sua conta do Gmail. As "senhas de aplicativo" oferecem um nível mais seguro de acesso ao seu e-mail quando utilizado por aplicações terceiras.

### Configurações JWT
Para a geração e validação de tokens JWT, configure a SecretKey no arquivo appsettings.json:
```json
"JwtSettings": {
  "SecretKey": "<sua-chave-secreta>"
}
```
A **SecretKey** é utilizada para criptografar e descriptografar o token.
