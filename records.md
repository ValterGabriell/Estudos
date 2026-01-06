
**Um record, na teoria, é um tipo de dado que representa um valor composto, definido apenas pelos seus campos.**

Ele:

* Não tem identidade
* Não tem ciclo de vida
* Não é mutável
* É comparado pelo conteúdo

Se algum valor muda:

* Não é o mesmo record
* É criado outro

Em termos de modelo:

* **Entidades** → têm identidade e mudam
* **Records** → representam valores e são substituídos

Por isso `record` é a base ideal para:

* Value Objects
* DTOs
* Mensagens
* Snapshots de dados


## **Por que para comparar tenho que ter `Equals`, `GetHashCode` e `ToString`?**

**R –**

* **`Equals`** responde: *“Dois objetos representam o mesmo valor?”* → igualdade lógica.
* **`GetHashCode`** transforma o objeto em um número usado por estruturas de espalhamento (hash table).

  * Cada objeto vai para uma “gaveta” baseada no hash.
  * Na comparação, o runtime só compara objetos da mesma gaveta → **performance**.
* **`Equals` confirma**, o **hashcode aproxima**.
* **`ToString`** não é usado pelo runtime para comparar, mas é essencial para **debug, logs e diagnóstico humano**.

👉 **Regra-chave:** se `Equals` retorna `true`, o `GetHashCode` **deve ser igual**.

---

## **Onde usar corretamente `record`? Por quê?**

**R –**

* **DTOs e Value Objects**, porque representam **valores imutáveis**.
* A **imutabilidade é o carro-chefe do `record`**:

  * Evita efeitos colaterais
  * Garante igualdade consistente
  * Torna o código previsível
  * Facilita concorrência e testes

---

## **Quais bugs alterar um DTO pode trazer?**

**R –**

* Controller recebe o DTO
* Service altera o DTO
* Outro service reutiliza o mesmo DTO

👉 O comportamento passa a depender da **ordem de execução**.

**Efeitos comuns:**

* Funciona local, quebra em produção
* Logs inconsistentes: *“O log diz que veio X, mas o cliente jura que mandou Y”*
* Validação burlada
* Bugs intermitentes difíceis de reproduzir

---

## **Por que DTOs e Value Objects têm que ser imutáveis?**

**R –**

* Um **Value Object** é definido **exclusivamente pelos seus valores**, não por identidade.

  * Ex.: dinheiro, email, nome, intervalo de datas, coordenadas, documentos
  * **Se o valor muda, NÃO é o mesmo objeto**

* DTO **não é domínio**, mas **também não deve mudar**, pois representa:

  * Snapshot de dados
  * Contrato entre camadas
  * Mensagem entre sistemas

👉 DTO não é algo que você *modifica*, é algo que você *consome*.

---

## **Quando e por que alterar um Value Object?**

**R –**

* Um **Value Object nunca é alterado**, ele é **substituído**.
* Quando o valor muda:

  * Um novo Value Object é criado
  * O antigo é trocado dentro da **Entidade**

👉 O Value Object **não se altera por conta própria**.
👉 Quem coordena mudanças é a **Entidade / Aggregate Root**.

---

## **Qual a diferença entre `record` posicional e nominal?**

**R –**

### **Record posicional**

* Propriedades vêm do **construtor**
* **Ordem dos parâmetros importa**
* `Equals`, `GetHashCode` e `Deconstruct` dependem da ordem
* Menos código
* Mais frágil
* Difícil de evoluir
* Fácil de errar ao instanciar

**Quando usar:**
→ DTOs simples, dados efêmeros, uso local.

---

### **Record nominal**

* Propriedades explícitas
* Inicialização por nome
* Ordem **não** importa
* Intenção de domínio clara
* Mais seguro para evolução
* Melhor para arquitetura limpa

**Quando usar:**
→ Value Objects, Commands, Events, contratos entre camadas (DTOs complexos).

---

## **Quando e por que usar `record struct`?**

**R –**

* `record struct` é para **valores imutáveis, pequenos e muito usados**, onde **performance importa**.
* Ele combina:

  * Semântica de valor (`record`)
  * Alocação como tipo valor (`struct`)
* É alocado diretamente na **stack / inline**, não no heap.
* **Não gera GC**.

### **Quando usar:**

* Representa um valor puro (coordenadas, intervalos, medidas)
* É pequeno:

  * **~16 bytes → excelente**
  * **até ~32 bytes → limite aceitável**
* É criado muitas vezes:

  * Loops
  * Processamento intensivo
  * Cálculos
  * Hot paths

---

## **OBS — Records (resumo geral)**

* Ao “alterar” um record:

  * Não alteramos a instância
  * Criamos **uma nova referência** com o valor atualizado (`with`)
* **Não usar** `record` para:

  * Entidades com identidade
  * Objetos com ciclo de vida
* Records são ideais para:

  * Valores
  * Mensagens
  * Contratos
  * Snapshots

---

## **Struct — como passar**

| Como passa | Copia memória? | Pode alterar?          |
| ---------- | -------------- | ---------------------- |
| Normal     | ✅ Sim          | ❌ Não afeta o original |
| `ref`      | ❌ Não          | ✅ Sim                  |
| `in`       | ❌ Não          | ❌ Não                  |
| `out`      | ❌ Inicializa   | ✅ Sim                  |

---

## **Struct — modelo mental**

```
Pessoa struct -> [ endereço_da_string ]
Heap -> "Valter"

Pessoa A -> [ endereço_da_string ]
Pessoa B -> [ endereço_da_string ]
Heap -> "Valter"
```

👉 O struct é copiado
👉 O objeto referenciado **não**

---

## **Tamanho ideal de `record struct`**

* **16 bytes → excelente**
* **Até 32 bytes → máximo aceitável**

| Tipo de campo                  | Tamanho             |
| ------------------------------ | ------------------- |
| `int`                          | 4 bytes             |
| `float`                        | 4 bytes             |
| `long`                         | 8 bytes             |
| `double`                       | 8 bytes             |
| `bool`                         | 1 byte (há padding) |
| `Guid`                         | 16 bytes            |
| Referência (`string`, `class`) | 8 bytes (x64)       |

Record Struct é usado em coordenadas, por exemplo, porque uma coordenada é um dado imutavel, nunca alteramos ela, sempre criamos uma nova.
Alem disso, é algo pequeno, sao 2 long. oq dao 16 bytes o que é exelente, entao tira pressao da heap e do GC e aloca na stack memoria.

---

## **Checklist antes de criar um objeto**

* Tem identidade? → `class`
* Representa um valor? → `record`
* Muda ao longo do tempo? → `class`
* É uma mensagem? → `record`
* Cruza camadas? → `record`



