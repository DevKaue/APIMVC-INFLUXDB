# 📊 API MVC com InfluxDB

Este projeto é uma API desenvolvida em ASP.NET MVC que integra com o banco de dados de séries temporais InfluxDB. Ele permite o registro e a consulta de dados temporais, sendo ideal para aplicações que necessitam de monitoramento em tempo real, como sistemas de IoT, métricas de desempenho e análise de sensores.

## 🚀 Tecnologias Utilizadas

- **ASP.NET MVC** – Framework para construção da API RESTful.
- **InfluxDB** – Banco de dados otimizado para séries temporais.
- **C#** – Linguagem principal do projeto.
- **HTML/CSS/JavaScript** – Para a interface web (se aplicável).

## ⚙️ Funcionalidades

- ✅ Criação de registros com timestamp no InfluxDB  
- ✅ Consulta de dados com filtros por tempo  
- ✅ Integração direta com InfluxDB utilizando bibliotecas oficiais  
- ✅ Visualização dos dados (se aplicável via front-end)  

## 📁 Estrutura do Projeto

```
APIMVC-INFLUXDB/
├── app/
│   ├── Controllers/
│   ├── Models/
│   ├── Views/
│   └── ...
├── .vscode/
├── TesteAPI.sln
└── README.md
```

## 🔧 Como Executar o Projeto

### Pré-requisitos

- [.NET SDK](https://dotnet.microsoft.com/download) instalado
- [InfluxDB](https://www.influxdata.com/get-influxdb/) rodando localmente ou em nuvem

### Passos

1. Clone este repositório:

```bash
git clone https://github.com/DevKaue/APIMVC-INFLUXDB.git
```

2. Acesse a pasta do projeto:

```bash
cd APIMVC-INFLUXDB
```

3. Restaure as dependências:

```bash
dotnet restore
```

4. Configure a conexão com o InfluxDB no arquivo `appsettings.json` ou conforme definido no projeto.

5. Execute o projeto:

```bash
dotnet run
```

6. A API estará acessível em:

```
http://localhost:5000
```

## 📌 Observações
- Certifique-se de que o serviço do InfluxDB esteja ativo e acessível.  
- A API pode ser utilizada como base para projetos de monitoramento em tempo real.  


🔗 Projeto por [@DevKaue](https://github.com/DevKaue)
