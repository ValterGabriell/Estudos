
## Struct

Assim como `record`, **struct representa um valor**.
Ele **não possui identidade**, apenas significado pelo seu conteúdo.

* É um **value type**
* É alocado na **stack** ou **inline**
* **Não pode ser `null`**
* A igualdade é **campo a campo** (manual)
* É **copiado por valor**

---

## O que significa “alocado inline”

Quando um `struct` é **alocado inline**, significa que **seus dados ficam armazenados diretamente dentro do local que o contém**, e não em um endereço separado da memória.

Ou seja:

> O valor do struct fica **embutido**, não referenciado.

### Exemplo

```csharp
class Pedido
{
    public Money Total;
}
```

Se `Money` for um `struct`:

* Os campos de `Money` ficam **dentro da memória do objeto `Pedido`**
* Não existe ponteiro para outro objeto
* Tudo é um único bloco de memória

Se `Money` fosse uma `class`:

* `Pedido` armazenaria apenas uma **referência**
* O objeto `Money` estaria em outro lugar da memória (heap)

---

## Semântica de valor

Por ser um tipo de valor, a cópia funciona assim:

```csharp
var a = algumStruct;
var b = a;
```

* `a` e `b` são **valores diferentes na memória**
* Alterar `b` **não altera `a`**
* Se fosse `class`, ambos apontariam para o **mesmo endereço**

---

## Quando um struct faz sentido

Um struct deve representar:

* Um **valor atômico**
* Pequeno
* Preferencialmente **imutável**
* Sem ciclo de vida
* Sem identidade própria

### Exemplos clássicos

* Dinheiro
* Coordenadas
* Identificadores tipados
* Datas customizadas
* Resultados matemáticos
* Value Objects de domínio

---

## Passagem por parâmetro

Structs são **copiados sempre**, a menos que você controle isso explicitamente.

```csharp
void Processar(in Email email)
```

* `in` → evita a cópia do valor
* Garante **melhor performance**
* Mantém **segurança**
* Muito usado em código crítico

---

## Tamanho ideal de `record struct`

* **16 bytes** → excelente
* **Até 32 bytes** → máximo aceitável
* Acima disso → reavaliar uso de struct

### Referência de tamanho dos campos

| Tipo                       | Tamanho             |
| -------------------------- | ------------------- |
| int                        | 4 bytes             |
| float                      | 4 bytes             |
| long                       | 8 bytes             |
| double                     | 8 bytes             |
| bool                       | 1 byte (há padding) |
| Guid                       | 16 bytes            |
| Referência (string, class) | 8 bytes (x64)       |

---

## Quando usar struct “sozinho”

O uso de `struct` puro é **exceção**, não regra.

Casos válidos:

* Código de infraestrutura / baixo nível
* Interop / APIs nativas
* Tipos matemáticos
* Código altamente sensível à performance

---

## Resumo mental

* **class** → “Tenho identidade e ciclo de vida”
* **struct** → “Sou um valor técnico, de baixo nível”
* **record** → “Sou um valor de negócio, comparado por conteúdo”

---

## Tabela direta

| Tipo          | É o quê        | Alocação       | Igualdade     | Imutável       | Quando usar                 |
| ------------- | -------------- | -------------- | ------------- | -------------- | --------------------------- |
| class         | Reference Type | Heap           | Referência    | ❌              | Entidades, serviços, ORM    |
| struct        | Value Type     | Stack / inline | Campo a campo | ❌              | Infra, interop, performance |
| record class  | Reference Type | Heap           | Por valor     | ✔ (`init`)     | DTOs, eventos, snapshots    |
| record struct | Value Type     | Stack / inline | Por valor     | ✔ (`readonly`) | Value Objects               |

---

## O que realmente muda

### Identidade

* `class` → **quem eu sou**
* `struct / record` → **o que eu valho**

---

### Igualdade

```csharp
class A { int X; }
record B(int X);
```

* `new A(1) == new A(1)` → ❌
* `new B(1) == new B(1)` → ✔

---

### Imutabilidade

* `class` → mutável por padrão
* `record` → imutável por design
* `struct` → perigoso se mutável

---

### Performance

* `class` → pressão no GC
* `struct / record struct` → menos GC
* `struct` grande → pode piorar performance

---

## Regra de ouro

* Entidade → `class`
* Valor de negócio → `readonly record struct`
* Snapshot / DTO → `record class`
* Infra / performance → `struct`

