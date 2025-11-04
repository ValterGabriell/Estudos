# Strategy Pattern - Padrão de Projeto Comportamental

## ?? O que é?

O **Strategy Pattern** é um padrão de projeto comportamental que permite definir uma família de algoritmos, encapsular cada um deles e torná-los intercambiáveis. O Strategy permite que o algoritmo varie independentemente dos clientes que o utilizam.

## ?? Problema que Resolve

Imagine que você tem uma classe que precisa fazer algo de várias maneiras diferentes. Por exemplo:
- Diferentes formas de pagamento (cartão, PIX, boleto)
- Diferentes algoritmos de ordenação
- Diferentes estratégias de desconto
- Diferentes formas de validação

Sem o Strategy, você teria um código cheio de `if-else` ou `switch-case`, difícil de manter e violar o **Open/Closed Principle**.

## ??? Estrutura

1. **Strategy (Interface)**: `IEstrategiaPagamento`
   - Define o contrato que todas as estratégias concretas devem seguir

2. **Concrete Strategies (Implementações)**: 
   - `PagamentoCartaoCredito`
   - `PagamentoPix`
   - `PagamentoBoleto`
   - `PagamentoCarteiraDigital`

3. **Context (Contexto)**: `ProcessadorPagamento`
   - Mantém uma referência à estratégia
   - Delega o trabalho para a estratégia
   - Não conhece detalhes das implementações

## ? Vantagens

1. **Open/Closed Principle**: Aberto para extensão, fechado para modificação
2. **Single Responsibility**: Cada estratégia tem uma única responsabilidade
3. **Substitui condicionais**: Elimina `if-else` ou `switch-case` complexos
4. **Flexibilidade**: Troca algoritmos em tempo de execução
5. **Testabilidade**: Fácil testar cada estratégia isoladamente
6. **Manutenibilidade**: Mudanças em uma estratégia não afetam outras

## ? Desvantagens

1. Aumenta o número de classes no projeto
2. Cliente precisa conhecer as diferenças entre estratégias
3. Pode ser overkill para casos simples
4. Overhead de comunicação entre contexto e estratégia

## ?? Quando Usar?

? Use quando:
- Você tem muitas classes relacionadas que diferem apenas no comportamento
- Você precisa de variantes diferentes de um algoritmo
- Um algoritmo usa dados que o cliente não deveria conhecer
- Uma classe define muitos comportamentos condicionais

? Não use quando:
- Você tem apenas alguns algoritmos que raramente mudam
- As diferenças entre algoritmos são triviais
- Você não precisa trocar comportamentos em tempo de execução

## ?? Estrutura do Exemplo

```
Strategy/
??? IEstrategiaPagamento.cs           # Interface da estratégia
??? ProcessadorPagamento.cs           # Contexto
??? Estrategias/
?   ??? PagamentoCartaoCredito.cs    # Estratégia concreta
?   ??? PagamentoPix.cs              # Estratégia concreta
?   ??? PagamentoBoleto.cs           # Estratégia concreta
?   ??? PagamentoCarteiraDigital.cs  # Estratégia concreta
??? ExemplosUso.cs                    # Demonstrações práticas
??? README.md                         # Este arquivo
```

## ?? Exemplo de Uso

```csharp
// Criar o processador (contexto)
var processador = new ProcessadorPagamento();

// Escolher estratégia em tempo de execução
processador.DefinirEstrategia(new PagamentoPix("meuemail@example.com"));
processador.ProcessarPagamento(100.00m);

// Trocar estratégia facilmente
processador.DefinirEstrategia(new PagamentoCartaoCredito("1234567890123456", "123", DateTime.Now.AddYears(3)));
processador.ProcessarPagamento(100.00m);

// Cada estratégia tem seu próprio comportamento
processador.DefinirEstrategia(new PagamentoBoleto("12345678901", DateTime.Now.AddDays(3)));
processador.ProcessarPagamento(100.00m);
```

## ?? Comparação com Outros Padrões

### Strategy vs State
- **Strategy**: Escolha do cliente qual estratégia usar
- **State**: Mudança de estado automática baseada no contexto

### Strategy vs Template Method
- **Strategy**: Usa composição (has-a)
- **Template Method**: Usa herança (is-a)

### Strategy vs Factory
- **Factory**: Cria objetos
- **Strategy**: Define comportamentos

## ?? Princípios SOLID Aplicados

- ? **Single Responsibility**: Cada estratégia tem uma responsabilidade
- ? **Open/Closed**: Aberto para novas estratégias, fechado para modificação
- ? **Liskov Substitution**: Estratégias são intercambiáveis
- ? **Interface Segregation**: Interface coesa e específica
- ? **Dependency Inversion**: Contexto depende de abstração, não de implementação

## ?? Casos de Uso Reais

1. **Sistemas de Pagamento**: Diferentes métodos de pagamento
2. **Ordenação**: Diferentes algoritmos (QuickSort, MergeSort, BubbleSort)
3. **Compressão**: ZIP, RAR, 7Z
4. **Roteamento**: Diferentes estratégias de navegação (carro, bicicleta, a pé)
5. **Validação**: Diferentes regras de validação
6. **Cálculo de Frete**: Por peso, por distância, por volume
7. **Notificações**: Email, SMS, Push

## ?? Referências

- Gang of Four (GoF) Design Patterns
- SOLID Principles
- Clean Code - Robert C. Martin
- Refactoring - Martin Fowler
