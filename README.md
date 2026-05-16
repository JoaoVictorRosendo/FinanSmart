# 💰 FinanSmart – Controle Financeiro Pessoal

> **🌐 Aplicação publicada:** https://finansmart.onrender.com
> **📁 Repositório:** https://github.com/JoaoVictorRosendo/finansmart

[![CI](https://github.com/JoaoVictorRosendo/finansmart/actions/workflows/ci.yml/badge.svg)](https://github.com/JoaoVictorRosendo/finansmart/actions/workflows/ci.yml)
![Versão](https://img.shields.io/badge/versão-1.2.0-blue)
![.NET](https://img.shields.io/badge/.NET-8.0-purple)

---

## 📌 Sobre o Projeto

**FinanSmart** é uma aplicação web de controle financeiro pessoal desenvolvida em **C# com ASP.NET Core MVC (.NET 8)**. Resolve uma dor real da sociedade brasileira: a dificuldade de acompanhar receitas, despesas e entender o impacto do câmbio no orçamento pessoal.

### Funcionalidades

| Funcionalidade | Descrição |
|---|---|
| 📊 Dashboard | Resumo de saldo, receitas, despesas e cotações do dia |
| 📋 Transações | Cadastro, categorização e exclusão de lançamentos |
| 💱 Câmbio | Cotações USD/BRL e EUR/BRL em tempo real via API |
| 💾 Persistência | Dados salvos em arquivo JSON local |

---

## 🏗️ Tecnologias

- **Linguagem:** C# 12 / .NET 8
- **Framework:** ASP.NET Core MVC
- **Armazenamento:** Arquivo JSON local
- **API Externa:** [AwesomeAPI](https://economia.awesomeapi.com.br/)
- **Testes:** xUnit + Moq
- **Linting:** .editorconfig + .NET Analyzers
- **CI/CD:** GitHub Actions
- **Deploy:** Render.com

---

## 🚀 Como Rodar Localmente

### Pré-requisitos
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)

### Passos

```bash
# 1. Clone o repositório
git clone https://github.com/JoaoVictorRosendo/finansmart.git
cd finansmart

# 2. Restaure as dependências
dotnet restore FinanSmart.sln

# 3. Execute a aplicação
dotnet run --project src/FinanSmart/FinanSmart.csproj

# 4. Acesse http://localhost:5000
```

---

## 🧪 Testes

```bash
dotnet test tests/FinanSmart.Tests/FinanSmart.Tests.csproj --verbosity normal
```

**9 testes:** 6 unitários (TransactionService) + 3 integração (ExchangeRateService com Moq).

---

## 📡 API Externa

```
GET https://economia.awesomeapi.com.br/json/last/USD-BRL,EUR-BRL
```

---

## 📦 Versionamento

| Versão | Descrição |
|---|---|
| v1.0.0 | CRUD de transações, dashboard, armazenamento JSON |
| v1.1.0 | Testes automatizados, CI com GitHub Actions, linting |
| v1.2.0 | Integração AwesomeAPI, testes de integração, deploy Render |

---

## 👤 Identificação

| Campo | Valor |
|---|---|
| **Aluno** | João Victor Rosendo Vilas Boas |
| **Matrícula** | 22553291 |
| **Instituição** | Centro Universitário de Brasília – CEUB |
| **Repositório** | https://github.com/JoaoVictorRosendo/finansmart |
| **Deploy** | https://finansmart.onrender.com |

---

## 📄 Licença

MIT License – veja [LICENSE](LICENSE).
