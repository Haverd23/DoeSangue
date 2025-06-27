# 📄 DoeSangue

## 🩸 Sobre o Sistema 

**DoeSangue** é uma aplicação desenvolvida para auxiliar na **gestão de doações de sangue** em instituições de saúde, bancos de sangue e campanhas de coleta. Seu objetivo é **facilitar o processo de doação**, oferecendo funcionalidades que apoiam tanto os doadores quanto os administradores do sistema.

### 🎯 Objetivo Principal

O sistema permite que **doadores voluntários** agendem suas doações, acompanhem seu histórico, e que os responsáveis pelos centros de coleta possam **controlar os estoques de sangue** e gerenciar os horários disponíveis. Além disso, oferece **comunicação automatizada** por e-mail durante o processo de doação.

---

## ⚙️ Tecnologias Utilizadas

- **Back-end:** ASP.NET Core Web API
- **Class Libraries:** .NET Standard para separação de contextos
- **Arquitetura:** DDD (Domain-Driven Design), CQRS (Command Query Responsibility Segregation)
- **Mensageria:** Apache Kafka


---

## 🧠 Contextos (Bounded Contexts)

### ✅ Auth
Responsável por autenticação de usuários e emissão de tokens JWT.

### ✅ Usuario
Gerencia os dados cadastrais e informações do doador.

### ✅ Agenda
Administra os horários disponíveis para agendamento de doações.

### ✅ Doacao
Controle das doações realizadas, status e etapas do processo.

### ✅ Estoque
Gerencia as unidades de sangue disponíveis por tipo sanguíneo.

### 🧰 Core *(suporte)*
Contém entidades e objetos de valor reutilizáveis (ex: `Entity`, `AggregateRoot`, `AppException`).

### 📢 Notificacao *(suporte)*
Responsável pelo envio de notificações e integração com serviços externos de e-mail.

---

## 📋 Arquitetura

- **Camadas por Contexto:**
  - `Domain`: Regras de negócio e entidades
  - `Application`: Casos de uso (Commands, Queries, Handlers)
  - `Data`: Acesso a dados, mensageria e serviços externos
  - `API`: Controllers e endpoints expostos

- **Comunicação assíncrona entre contextos via Kafka**
  - Exemplo: Quando uma doação é finalizada, o contexto de Estoque escuta o evento e atualiza as unidades automaticamente.

---

## 📦 Funcionalidades por Contexto

| Contexto | Funcionalidade |
|----------|----------------|
| Auth     | Registro, login, geração de JWT |
| Usuario  | Cadastro e atualização de perfil, histórico de doações |
| Agenda   | Consulta e agendamento de horários |
| Doacao   | Confirmação, finalização e status de doações |
| Estoque  | Atualização automática via eventos, contagem por tipo sanguíneo |

---

## 🚀 Execução da Aplicação

### 🔧 Rodando Localmente

1. Em **cada contexto** (exceto `Notificacao` e `Core`), crie um arquivo `.env`
contendo variáveis de ambiente.
Exemplo de variáveis de ambiente:

```env
ASPNETCORE_ENVIRONMENT=Development
DEFAULT_CONNECTION=Server=localhost;Database=DoeSangue;User Id=sa;Password=Teste123@;TrustServerCertificate=True;
AppSettings__SecretKey=sua-chave-secreta-aqui-123-456-789-0
```

2. No contexto **Notificacao** (API), edite o `appSettings.Development.json` para configurar o envio de e-mails:

```json
"Email": {
  "smtpHost": "smtp.gmail.com",
  "smtpPort": 587,
  "smtpUser": "email",
  "smtpPass": "senha_app",
  "from": "email"
}
```

> Para isso, crie uma senha de app no Gmail. Mais informações: https://support.google.com/accounts/answer/185833?hl=pt-br

3. No contexto **Auth**, na camada **Application**:
   - No arquivo `UsuarioCriadoCommandHandler.cs`, linha 35:

```csharp
if(user.Email.Entrada == "emailAdministrador")
{
    user.AlterarRole("Administrador");
}
```

   - Substitua `"emailAdministrador"` por um e-mail válido para testes com papel de administrador.

4. Para executar a mensageria com Kafka:

```bash
docker-compose -f docker-compose-kafka.yml up -d --build
```
### 🔧 Rodando no Docker
1. Crie um arquivo .env na raiz do repositório(na mesma raiz do docker-compose), contendo variáveis de ambiente. Exemplo de variáveis de ambiente:

```env
ASPNETCORE_ENVIRONMENT=Production

DEFAULT_CONNECTION=Server=sql,1433;Database=DoeSangueProducao;User Id=sa;Password=SenhaForteDeProducao123!;TrustServerCertificate=True;ConnectRetryCount=5;ConnectRetryInterval=10

AppSettings__SecretKey=uma-chave-secreta-super-segura-para-producao-abc-123

DB_SA_PASSWORD=SenhaForteDeProducao123!
```
2. No contexto **Notificacao** (API), edite o `appSettings.json` para configurar o envio de e-mails:

```json
"Email": {
  "smtpHost": "smtp.gmail.com",
  "smtpPort": 587,
  "smtpUser": "email",
  "smtpPass": "senha_app",
  "from": "email"
}
```
3. No contexto **Auth**, na camada **Application**:
   - No arquivo `UsuarioCriadoCommandHandler.cs`, linha 35:

```csharp
if(user.Email.Entrada == "emailAdministrador")
{
    user.AlterarRole("Administrador");
}
```

   - Substitua `"emailAdministrador"` por um e-mail válido para testes com papel de administrador.


4. Para executar o projeto:

```bash
docker-compose -f docker-compose.yml up -d --build
```




---

## 🔍 Endpoints

### 🔐 API Auth (porta 7182 ou Docker 5001)

#### Criar Usuário
`POST /api/User`
```json
{
  "email": "string",
  "senha": "string"
}
```

#### Login
`POST /api/User/login`
```json
{
  "email": "string",
  "senha": "string"
}
```

#### Alterar Senha (Requer Login)
`PUT /api/User/alterar/senha`
```json
{
  "senha": "string"
}
```

#### Alterar Email (Requer Login)
`PUT /api/User/alterar/email`
```json
{
  "email": "string"
}
```

---

### 📢 API Notificacao

- Execute em segundo plano.
- Pode gerar erros por falta de brokers ativos para consumo, mas isso pode ser ignorado será resolvido assim que um evento é gerado.

---

### 👤 API Usuario (porta 7249 ou Docker 5002)

#### Criar Usuário (Requer Login)
`POST /api/Usuario`
```json
{
  "nome": "string",
  "cpf": "string",
  "telefone": "string",
  "tipoSanguineo": "string"
}
```

Tipos sanguíneos válidos: **APositivo**,
 **ANegativo**, 
 **BPositivo**,
  **BNegativo**, **ABPositivo**, **ABNegativo**, **OPositivo**, **ONegativo**

#### Alterar Telefone (Requer Login)
`PUT /api/Usuario/alterar/telefone`
```json
{
  "telefone": "string"
}
```

#### Alterar Tipo Sanguíneo (Apenas Administrador)
`PUT /api/Usuario/alterar/tiposanguineo`
```json
{
  "cpf": "string",
  "tipoSanguineo": "string"
}
```

#### Visualizar Doações do Usuário (Requer Login)
`GET /api/Usuario/doacoes`

---

### 🗓️ API Agenda (porta 7276 ou Docker 5003)

#### Criar Horário (Apenas Administradores)
`POST /api/Agenda`
```json
{
  "dataHora": "2025-06-27T10:20:50.728Z",
  "numeroVagas": 0
}
```

#### Remover Horário (Apenas Administradores)
`DELETE /api/Agenda`
```json
{
  "agendaId": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
}
```

#### Atualizar Horário (Apenas Administradores)
`PUT /api/Agenda/atualizar/horario`
```json
{
  "agendaId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "horario": "2025-06-27T10:24:14.928Z"
}
```

#### Atualizar Quantidade de Vagas (Apenas Administradores)
`PUT /api/Agenda/atualizar/quantidade/vagas`
```json
{
  "agendaId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "numeroVagas": 5
}
```

#### Listar Todos os Horários
`GET /api/Agenda/horarios`  
Lista todos os horários disponíveis para doação.

---

### 🩸 API Doacao (porta 7141 ou Docker 5004)

#### Agendar Doação (Requer Login)
`POST /api/Doacao/agendar`
```json
{
  "agendaId": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
}
```
- Agendamento de uma doação.
- Um e-mail de confirmação será enviado.

#### Cancelar Doação (Requer Login)
`PUT /api/Doacao/cancelar`
```json
{
  "doacaoId": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
}
```

#### Iniciar Doação (Apenas Administrador)
`PUT /api/Doacao/iniciar`
```json
{
  "doacaoId": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
}
```
- Dispara e-mail ao doador.

#### Marcar Doação como Falha (Apenas Administrador)
`PUT /api/Doacao/falha`
```json
{
  "doacaoId": "8465AA98-019C-49F5-A7D6-C631CB5B6363"
}
```
- Dispara e-mail informando falha na doação.

#### Finalizar Doação (Apenas Administrador)
`PUT /api/Doacao/finalizar`
```json
{
  "doacaoId": "8465AA98-019C-49F5-A7D6-C631CB5B6363"
}
```
- Envia e-mail de confirmação.
- Dispara evento para o contexto de Estoque.

---

### 🧪 API Estoque (porta 7280 ou Docker 5005)

#### Listar Estoque (Apenas Administrador)
`GET /api/Estoque`  
- Retorna a quantidade disponível por tipo sanguíneo.

#### Remover Manualmente do Estoque (Apenas Administrador)
`PUT /api/Estoque`
```json
{
  "tipoSanguineo": "string",
  "quantidade": 0
}
```

---

## 🔄 Regras de Atualização do Estoque

- A cada doação finalizada, o contador de doações do tipo sanguíneo é incrementado.
- Quando o contador atinge **5**, ele é **zerado** e **1 unidade** é adicionada ao estoque correspondente.
- Esse comportamento é automático e ocorre via evento consumido no contexto de Estoque.

---
## 🔁 Eventos Kafka (Mensageria Assíncrona)

A aplicação utiliza **Apache Kafka** para comunicação assíncrona entre contextos, garantindo desacoplamento e consistência eventual. Abaixo estão os eventos utilizados na solução:

| 🧩 Evento                        | 📤 Emitido Por       | 📥 Consumido Por     | 🎯 Propósito                                                                 |
|----------------------------------|----------------------|----------------------|------------------------------------------------------------------------------|
| `UsuarioCriado`                 | Usuario       | Notificacao          | Enviar e-mail de boas-vindas ao novo usuário                                |
| `DoacaoAgendada`               | Doacao               | Notificacao, Agenda  | Enviar e-mail confirmando agendamento e ocupar uma vaga no horário     |
| `DoacaoIniciada`               | Doacao               | Notificacao          | Enviar e-mail informando início da doação                                   |
| `DoacaoFinalizada`            | Doacao               | Estoque, Notificacao | Atualizar contador e estoque, e enviar e-mail de agradecimento              |
| `DoacaoComFalha`              | Doacao               | Notificacao          | Enviar e-mail informando falha no processo da doação                        |
| `DoacaoCancelada`             | Doacao               | Agenda               | Desocupar uma vaga no horário agendado                                |

> 🔔 A **Notificação** consome eventos passivamente, sem impactar a lógica de negócio.

---
## 🏁 Considerações Finais

Esta documentação apresenta uma visão geral da aplicação DoeSangue, detalhando seus contextos, fluxos, endpoints e eventos.  
Com esta base, é possível entender, manter e expandir o sistema de forma organizada e consistente.
