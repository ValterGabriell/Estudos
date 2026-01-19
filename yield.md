## `yield return` em C#

O **`yield return`** é usado para criar **iterações sob demanda (lazy evaluation)**, permitindo que um método produza valores **um por vez**, sem precisar construir toda a coleção em memória antes.

Em vez de retornar todos os dados de uma vez, o método **“pausa” sua execução** no ponto do `yield` e **retoma exatamente dali** na próxima iteração.

---

### 🔹 Conceito fundamental

Quando um método contém `yield return`:

* O método **não executa tudo de uma vez**
* O **estado da execução é preservado** (variáveis locais, posição no `for`, etc.)
* A cada chamada do iterador (`foreach`, `MoveNext`), o método **continua a execução a partir do último `yield`**

Ou seja:

> O `yield` salva o ponto onde ocorreu e, quando o loop continua, o método volta exatamente daquele ponto.

---

### 🔹 Exemplo simples

```csharp
IEnumerable<int> GerarNumeros()
{
    for (int i = 0; i < 5; i++)
    {
        yield return i;
    }
}
```

Uso:

```csharp
foreach (var n in GerarNumeros())
{
    Console.WriteLine(n);
}
```

📌 O método:

* Executa até o primeiro `yield return`
* Retorna `0`
* Pausa
* Retoma do `for` na próxima iteração
* Repete até o fim

---

### 🔹 O que acontece por baixo dos panos

O compilador transforma esse método em uma **máquina de estados**, algo conceitualmente parecido com:

```csharp
while (estado != Finalizado)
{
    MoveNext();
    Current = valorAtual;
}
```

Isso permite:

* Retomar exatamente de onde parou
* Manter variáveis locais vivas
* Evitar a criação de listas intermediárias

---

### 🔹 Exemplo com processamento pesado

Sem `yield`:

```csharp
List<int> Processar()
{
    var lista = new List<int>();

    for (int i = 0; i < 10000; i++)
    {
        lista.Add(i * 2);
    }

    return lista;
}
```

🔴 Cria **10.000 objetos em memória** antes de retornar qualquer coisa.

---

Com `yield return`:

```csharp
IEnumerable<int> Processar()
{
    for (int i = 0; i < 10000; i++)
    {
        yield return i * 2;
    }
}
```

🟢 Apenas **um elemento por vez** é materializado, conforme o consumo.

---

### 🔹 Interrupção antecipada

```csharp
foreach (var n in Processar())
{
    if (n > 10)
        break;

    Console.WriteLine(n);
}
```

📌 O método **para de executar imediatamente**, sem gerar os demais valores.

---

### 🔹 Relação direta com LINQ

Muitos métodos do LINQ (`Where`, `Select`, `SelectMany`) usam `yield return` internamente.

Exemplo conceitual de `Where`:

```csharp
IEnumerable<T> Where<T>(IEnumerable<T> source, Func<T, bool> predicate)
{
    foreach (var item in source)
    {
        if (predicate(item))
            yield return item;
    }
}
```

Isso explica por que LINQ:

* É lazy
* Não executa até ser enumerado
* Evita consumo excessivo de memória

---

### 🔹 Quando usar `yield return`

Use `yield return` quando:

* Os dados podem ser consumidos gradualmente
* A coleção pode ser grande
* Nem todos os elementos serão necessariamente usados
* Você quer evitar criar milhares de objetos em memória

---

### 🧠 Resumo

* `yield return` pausa o método e salva seu estado
* A execução continua exatamente do ponto do `yield`
* Os dados são gerados sob demanda
* Reduz uso de memória
* É a base do comportamento lazy do LINQ

> **`yield return` permite criar iterações eficientes, escaláveis e com baixo consumo de memória, retornando apenas o necessário no momento certo.**
