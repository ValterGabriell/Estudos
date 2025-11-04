# Factory Pattern - Padrão de Projeto Criacional

## ?? O que é?

O **Factory Pattern** é um padrão de projeto criacional que fornece uma interface para criar objetos, mas permite que as subclasses decidam qual classe instanciar. Ele encapsula a lógica de criação de objetos.

## ?? Tipos de Factory

### 1. Simple Factory (Factory Method Estático)
- Método estático que retorna instâncias baseado em parâmetros
- Implementado em: `VeiculoFactory.cs`
- Mais simples e direto

### 2. Factory Method Pattern
- Define uma interface para criar objetos, mas deixa subclasses decidirem qual classe instanciar
- Usa herança: delega a criação para subclasses

### 3. Abstract Factory Pattern
- Fornece uma interface para criar famílias de objetos relacionados sem especificar suas classes concretas
- Implementado em: pasta `AbstractFactory/`
- Mais complexo, mas mais flexível

## ? Vantagens

1. **Encapsulamento**: Lógica de criação centralizada
2. **Baixo Acoplamento**: Cliente não conhece classes concretas
3. **Fácil Manutenção**: Adicionar novos tipos é simples
4. **Princípio Open/Closed**: Aberto para extensão, fechado para modificação
5. **Single Responsibility**: Separa criação de uso

## ? Desvantagens

1. Pode aumentar a complexidade do código
2. Mais classes para gerenciar
3. Overhead para casos simples

## ?? Quando Usar?

- Quando você não sabe de antemão os tipos exatos de objetos
- Quando quer fornecer uma biblioteca de objetos sem expor implementação
- Quando quer delegar a lógica de instanciação
- Quando tem lógica complexa de criação

## ?? Estrutura do Exemplo

```
Factory/
??? IVeiculo.cs                    # Interface do produto
??? TipoVeiculo.cs                 # Enum para tipos
??? VeiculoFactory.cs              # Simple Factory
??? Veiculos/
?   ??? Carro.cs                   # Produto concreto
?   ??? Moto.cs                    # Produto concreto
?   ??? Caminhao.cs                # Produto concreto
??? AbstractFactory/
    ??? IVeiculoFactory.cs         # Interface da factory abstrata
    ??? CarroFactory.cs            # Factory concreta
    ??? MotoFactory.cs             # Factory concreta
    ??? CaminhaoFactory.cs         # Factory concreta
```

## ?? Exemplo de Uso

### Simple Factory
```csharp
// Cliente não precisa conhecer as classes concretas
var carro = VeiculoFactory.CriarVeiculo(TipoVeiculo.Carro);
carro.Acelerar(); // "Carro acelerando suavemente..."

var moto = VeiculoFactory.CriarVeiculo("moto");
Console.WriteLine(moto.ObterPreco()); // 15000
```

### Abstract Factory
```csharp
// Usando factory abstrata
IVeiculoFactory factory = new CarroFactory();
var veiculo = factory.CriarVeiculo();
Console.WriteLine(factory.ObterCategoria()); // "Veículo de Passeio"

// Fácil trocar a família de produtos
factory = new CaminhaoFactory();
veiculo = factory.CriarVeiculo();
```

## ?? Relação com Outros Padrões

- **Abstract Factory**: Geralmente usa Factory Methods
- **Prototype**: Pode usar factories para clonar objetos
- **Singleton**: Factory pode retornar sempre a mesma instância
- **Template Method**: Factory Method é frequentemente chamado por Template Methods

## ?? Referências

- Gang of Four (GoF) Design Patterns
- SOLID Principles (especialmente Open/Closed e Dependency Inversion)
