

## `SelectMany` no LINQ

O **`SelectMany`** é utilizado quando você tem **uma coleção que contém outras coleções** e deseja **“achatar” (flatten)** essas coleções internas em **uma única sequência**.

Em outras palavras:

> **`SelectMany` transforma uma lista de listas em uma lista simples.**

---

### 🔹 Cenário

Suponha a seguinte estrutura:

```csharp
class Serie
{
    public string Nome { get; set; }
    public List<int> Eps { get; set; }
}
```

E uma lista de séries:

```csharp
var lista = new List<Serie>
{
    new Serie 
    { 
        Nome = "For All Mankind", 
        Eps = new List<int> { 1, 2, 3, 4, 5 } 
    }
};
```

---

### 🔹 Usando `Select`

Se você usar apenas `Select`:

```csharp
var resultado = lista.Select(e => e.Eps);
```

📌 **Resultado:**

```csharp
IEnumerable<List<int>>
```

Ou seja, você terá **uma lista contendo outra lista**, não os episódios diretamente.

---

### 🔹 Usando `SelectMany`

Agora, usando `SelectMany`:

```csharp
var resultado = lista.SelectMany(e => e.Eps);
```

📌 **Resultado:**

```csharp
IEnumerable<int>
```

Conteúdo:

```text
1, 2, 3, 4, 5
```

👉 O `SelectMany` **percorre cada item da lista principal**, acessa a coleção interna (`Eps`) e **une todos os elementos em uma única sequência**.

---

### 🔹 O que o `SelectMany` faz internamente

De forma conceitual, é como se ele fizesse:

```csharp
foreach (var serie in lista)
{
    foreach (var ep in serie.Eps)
    {
        yield return ep;
    }
}
```

Ou seja, ele **elimina um nível de aninhamento** da estrutura.

---

### 🔹 Quando usar `SelectMany`

Use `SelectMany` quando:

* Você tem **coleções aninhadas**
* Precisa **trabalhar com os elementos internos diretamente**
* Quer **evitar listas de listas**
* Vai aplicar filtros, ordenações ou agregações sobre os itens internos

Exemplo:

```csharp
var epsPares = lista
    .SelectMany(s => s.Eps)
    .Where(ep => ep % 2 == 0);
```

---
```csharp
var resultado_achatado = resultados.SelectMany(
    agenda => agenda.Consultas,
    (agenda, consulta) => new 
    {
        agenda.AgendaId,
        agenda.EstabelecimentoId,
        agenda.EstabelecimentoNome,
        agenda.ProfissionalId,
        agenda.ProfissionalNome,
        consulta.ConsultaId,
        consulta.PacienteId,
        PacienteNome = consulta.Nome
    }
);
```
### 🔹 Diferença rápida: `Select` vs `SelectMany`

| Método       | Resultado                   |
| ------------ | --------------------------- |
| `Select`     | Mantém a estrutura aninhada |
| `SelectMany` | Achata a estrutura          |

---

### 🧠 Resumo

> **`SelectMany` é usado para projetar e achatar coleções internas em uma única sequência, facilitando operações sobre os elementos internos.**


