# SearchNarutoAPI

Aplicação **Blazor WebAssembly (.NET 10)** que consome a [Dattebayo API](https://dattebayo-api.onrender.com) (universo Naruto) para listar e buscar personagens.

Atividade da disciplina de **Desenvolvimento Web** — Usabilidade, Dev. Web, Mobile e Jogos.

## Funcionalidades

- Lista paginada de personagens (`GET /characters`)
- Busca por **nome** (`GET /characters?name=Naruto`)
- Busca por **ID** (`GET /characters/{id}`), digitando o número na caixa de busca
- Exibição, para cada personagem, de:
  - **Nome** (`name`)
  - **Imagem** (primeira URL da lista `images`)
  - **Clã ou afiliação** (`personal.clan` ou `personal.affiliation`)
  - **Jutsus** (`jutsu`)
  - **Patente ninja** (`rank.ninjaRank`)

## Tecnologias

- C# / .NET 10
- Blazor WebAssembly
- `HttpClient` + `System.Text.Json`

## Pré-requisitos

- [.NET SDK 10](https://dotnet.microsoft.com/download)

Para conferir a instalação:

```bash
dotnet --version
```

## Como executar

```bash
git clone https://github.com/SEU_USUARIO/SearchNarutoAPI.git
cd SearchNarutoAPI
dotnet run
```

Abra no navegador o endereço exibido no terminal (por exemplo, `http://localhost:5000`).

> A API está hospedada em um plano gratuito, então a primeira requisição pode levar alguns segundos para responder.

## Como pesquisar

1. Digite o nome do personagem (ex.: `Naruto`, `Sasuke`, `Sakura`) ou o ID (ex.: `1`).
2. Clique em **Buscar** ou pressione Enter.
3. Clique em **Limpar** para voltar à lista completa.

## Estrutura do projeto

| Caminho | Descrição |
|---|---|
| `Program.cs` | Configura o `HttpClient` (base URL da API) e registra o serviço |
| `Models/Character.cs` | Modelos do JSON retornado pela API |
| `Services/NarutoApiService.cs` | Chamadas aos endpoints de personagens |
| `Pages/Home.razor` | Tela de busca, listagem e paginação |
| `wwwroot/css/app.css` | Estilos da aplicação |

## Endpoints utilizados

- Base URL: `https://dattebayo-api.onrender.com`
- Lista / busca: `/characters`
- Por ID: `/characters/{id}`

## Autor

Seu Nome — Seu RA / Turma
