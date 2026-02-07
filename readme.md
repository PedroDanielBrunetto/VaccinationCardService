# VaccinationCard API

## Visão Geral
Este projeto implementa uma **API de Cartão de Vacinação** utilizando **ASP.NET Core**, **Entity Framework Core**, **MediatR** e uma arquitetura inspirada em **Clean Architecture + CQRS**. O objetivo principal é demonstrar boas práticas de design, separação de responsabilidades e preparação para cenários reais de sistemas distribuídos, como integrações assíncronas via mensageria.

A API permite:
- Cadastro e consulta de pessoas
- Cadastro e consulta de vacinas
- Registro de vacinações
- Consulta do cartão de vacinação de uma pessoa
- Exclusão de pessoas, vacinas e vacinações

Além disso, o projeto simula a publicação de eventos de domínio em uma mensageria, preparando o sistema para processamento assíncrono e integração com outros serviços.

---

## Arquitetura

### Por que esta arquitetura?

A arquitetura foi pensada para:
- **Separar responsabilidades** (API, Application, Domain e Infrastructure)
- **Facilitar testes** (principalmente na camada Application)
- **Reduzir acoplamento** entre regras de negócio e infraestrutura
- **Permitir evolução futura**, como troca de banco de dados ou mensageria real

### Camadas

- **API**
  - Controllers
  - Middlewares
  - Configuração de DI, Swagger e pipeline HTTP

- **Application**
  - Commands e Queries (CQRS)
  - Handlers (MediatR)
  - DTOs
  - Eventos de domínio
  - Abstrações (interfaces)

- **Domain**
  - Entidades
  - Value Objects
  - Eventos de domínio
  - Regras de negócio

- **Infrastructure**
  - Implementação do DbContext (EF Core)
  - Implementações de mensageria (FakeMessageBus)
  - Persistência

---

## Por que usar DbContext + Entity Framework Core e não Repository Pattern?

### Decisão arquitetural

Neste projeto foi adotado o **DbContext diretamente**, ao invés do padrão Repository tradicional.

### Motivos:

1. **O DbContext já é um Unit of Work + Repository**
   - `DbSet<T>` já fornece operações de leitura e escrita
   - Evita repositórios genéricos que apenas "replicam" o EF

2. **Menos camadas desnecessárias**
   - Repositórios genéricos costumam adicionar abstração sem ganho real
   - Queries complexas acabam vazando para fora do repositório

3. **Melhor integração com CQRS**
   - Queries usam projeções diretas (`Select`)
   - Melhor performance e menor consumo de memória

4. **Mais controle sobre o EF Core**
   - Uso explícito de `AsNoTracking`
   - Controle fino de Includes, projeções e transações

5. **Abstração no nível correto**
   - A interface `IVaccinationDbContext` abstrai a infraestrutura
   - Permite mockar o contexto em testes

---

## CQRS (Command Query Responsibility Segregation)

O projeto separa claramente:

- **Commands**: alteram estado (POST, DELETE)
- **Queries**: apenas leitura (GET)

### Benefícios:
- Código mais simples e focado
- Queries otimizadas (sem tracking)
- Facilidade para evoluir para bancos distintos de leitura/escrita

---

## MediatR: Mediator + Observer

### Qual pattern foi aplicado?

👉 **Os dois**:

- **Mediator Pattern**
  - Controllers não conhecem handlers
  - Toda comunicação passa pelo `IMediator`

- **Observer Pattern (via eventos)**
  - `INotification`
  - Múltiplos handlers podem reagir ao mesmo evento

### Exemplo

Quando uma vacinação é criada:
1. O Command Handler persiste os dados
2. Um `VaccinationCreatedEvent` é disparado
3. O handler de evento publica a mensagem na mensageria

---

## Simulação de Mensageria

### Por que simular?

O projeto não depende de RabbitMQ, Kafka ou SQS, mas já está **preparado para integração real**.

### Implementação

- Interface `IMessageBus`
- Implementação `FakeMessageBus`
- Publicação via eventos de domínio

### Benefícios:
- Baixo acoplamento
- Facilidade para trocar por mensageria real
- Demonstra arquitetura orientada a eventos

---

## Middlewares

### Função dos middlewares

Middlewares centralizam comportamentos transversais:

- Tratamento de exceções
- Padronização de respostas
- Mapeamento de erros para HTTP status codes

### Middleware de erro

Responsável por:
- Capturar exceções não tratadas
- Traduzir exceções de domínio para HTTP
- Evitar `try/catch` espalhados pelo código

---

## HTTP Status Codes Utilizados

| Código | Quando é usado |
|------|---------------|
| 200 OK | Operação realizada com sucesso |
| 201 Created | Recurso criado com sucesso |
| 204 No Content | Exclusão bem-sucedida |
| 400 Bad Request | Erro de validação |
| 404 Not Found | Recurso não encontrado |
| 409 Conflict | Regra de negócio violada |
| 500 Internal Server Error | Erro inesperado |

---

## Benefícios de usar MediatR com EF Core

- Controllers extremamente simples
- Regras de negócio concentradas nos handlers
- Facilita testes unitários
- Elimina dependência direta entre camadas
- Integra naturalmente CQRS + eventos

---

## Endpoints da API

### People

#### POST /api/people
Cadastra uma nova pessoa.

**Schema – CreatePersonCommand**
- name: string
- document: string (apenas números)
- gender: enum (0,1,2)
- birth: datetime
- email: string

---

#### GET /api/people
Lista todas as pessoas cadastradas.

---

#### GET /api/people/{id}/vaccination-card
Retorna o cartão de vacinação de uma pessoa.

---

#### DELETE /api/people/{id}
Exclui uma pessoa e seus registros associados.

---

### Vaccines

#### POST /api/vaccines
Cadastra uma vacina.

**Schema – CreateVaccineCommand**
- name: string

---

#### GET /api/vaccines
Lista todas as vacinas.

---

#### DELETE /api/vaccines/{id}
Remove uma vacina.

---

### Vaccinations

#### POST /api/vaccinations
Registra uma vacinação.

**Schema – CreateVaccinationCommand**
- personId: guid
- vaccineId: guid
- dose: int

Ao concluir:
- Salva no banco
- Dispara evento de domínio
- Publica mensagem na mensageria

---

#### DELETE /api/vaccinations/{id}
Exclui um registro de vacinação.

---

## Swagger / OpenAPI

A API é totalmente documentada via **Swagger (OpenAPI 3.0)**:
- Visualização clara dos endpoints
- Schemas bem definidos
- Facilita testes e integração

---