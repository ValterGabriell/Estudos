using Estudos.Factory.AbstractFactory;

namespace Estudos.Factory;

/// <summary>
/// Classe com exemplos práticos de uso do Factory Pattern
/// </summary>
public class ExemplosUso
{
    /// <summary>
    /// Demonstra o uso do Simple Factory
    /// </summary>
    public static void ExemploSimpleFactory()
    {
        Console.WriteLine("=== SIMPLE FACTORY PATTERN ===\n");
        
        // Criando veículos usando enum
        var carro = VeiculoFactory.CriarVeiculo(TipoVeiculo.Carro);
        var moto = VeiculoFactory.CriarVeiculo(TipoVeiculo.Moto);
        var caminhao = VeiculoFactory.CriarVeiculo(TipoVeiculo.Caminhao);
        
        // Cliente usa a interface, não a classe concreta
        ImprimirDetalhesVeiculo(carro);
        ImprimirDetalhesVeiculo(moto);
        ImprimirDetalhesVeiculo(caminhao);
        
        Console.WriteLine("\n--- Criando por string ---\n");
        
        // Criando usando string
        var veiculo = VeiculoFactory.CriarVeiculo("carro");
        veiculo.Acelerar();
        veiculo.Frear();
    }
    
    /// <summary>
    /// Demonstra o uso do Abstract Factory
    /// </summary>
    public static void ExemploAbstractFactory()
    {
        Console.WriteLine("\n\n=== ABSTRACT FACTORY PATTERN ===\n");
        
        // Array de factories - fácil trocar a implementação
        IVeiculoFactory[] factories = 
        [
            new CarroFactory(),
            new MotoFactory(),
            new CaminhaoFactory()
        ];
        
        foreach (var factory in factories)
        {
            Console.WriteLine($"Categoria: {factory.ObterCategoria()}");
            var veiculo = factory.CriarVeiculo();
            ImprimirDetalhesVeiculo(veiculo);
            Console.WriteLine();
        }
    }
    
    /// <summary>
    /// Exemplo de uso em um cenário real: Sistema de Aluguel
    /// </summary>
    public static void ExemploSistemaAluguel()
    {
        Console.WriteLine("\n\n=== SISTEMA DE ALUGUEL ===\n");
        
        // Simulando escolha do usuário
        var escolhaUsuario = "moto";
        
        try
        {
            var veiculoAlugado = VeiculoFactory.CriarVeiculo(escolhaUsuario);
            
            Console.WriteLine($"Você alugou um {veiculoAlugado.ObterTipo()}");
            Console.WriteLine($"Preço: R$ {veiculoAlugado.ObterPreco():N2}");
            Console.WriteLine("\nTestando o veículo:");
            veiculoAlugado.Acelerar();
            veiculoAlugado.Frear();
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Erro: {ex.Message}");
        }
    }
    
    /// <summary>
    /// Exemplo mostrando extensibilidade: adicionar novo veículo é fácil
    /// </summary>
    public static void ExemploExtensibilidade()
    {
        Console.WriteLine("\n\n=== EXTENSIBILIDADE ===\n");
        Console.WriteLine("Para adicionar um novo veículo:");
        Console.WriteLine("1. Criar classe que implementa IVeiculo");
        Console.WriteLine("2. Adicionar novo case no switch da Factory");
        Console.WriteLine("3. Criar nova Factory concreta (se usar Abstract Factory)");
        Console.WriteLine("\nO código cliente NÃO precisa ser alterado!");
    }
    
    /// <summary>
    /// Método auxiliar para imprimir detalhes
    /// </summary>
    private static void ImprimirDetalhesVeiculo(IVeiculo veiculo)
    {
        Console.WriteLine($"Veículo: {veiculo.ObterTipo()}");
        Console.WriteLine($"Preço: R$ {veiculo.ObterPreco():N2}");
        veiculo.Acelerar();
        veiculo.Frear();
        Console.WriteLine();
    }
    
    /// <summary>
    /// Executa todos os exemplos
    /// </summary>
    public static void ExecutarTodosExemplos()
    {
        ExemploSimpleFactory();
        ExemploAbstractFactory();
        ExemploSistemaAluguel();
        ExemploExtensibilidade();
    }
}
