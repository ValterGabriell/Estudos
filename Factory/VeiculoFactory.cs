using Estudos.Factory.Veiculos;

namespace Estudos.Factory;

/// <summary>
/// Factory Method Pattern (Simple Factory)
/// Responsável por criar instâncias de veículos baseado no tipo solicitado
/// 
/// VANTAGENS:
/// - Encapsula a lógica de criação de objetos
/// - Facilita manutenção e extensão (adicionar novos tipos)
/// - Cliente não precisa conhecer as classes concretas
/// - Reduz acoplamento entre código cliente e implementações
/// 
/// QUANDO USAR:
/// - Quando a classe não pode antecipar o tipo de objetos que deve criar
/// - Quando você quer localizar o conhecimento de qual classe criar
/// - Quando você tem lógica complexa de criação de objetos
/// </summary>
public class VeiculoFactory
{
    /// <summary>
    /// Cria um veículo baseado no tipo especificado
    /// </summary>
    public static IVeiculo CriarVeiculo(TipoVeiculo tipo)
    {
        return tipo switch
        {
            TipoVeiculo.Carro => new Carro(),
            TipoVeiculo.Moto => new Moto(),
            TipoVeiculo.Caminhao => new Caminhao(),
            _ => throw new ArgumentException($"Tipo de veículo '{tipo}' não suportado")
        };
    }
    
    /// <summary>
    /// Cria um veículo baseado em uma string (exemplo de sobrecarga)
    /// </summary>
    public static IVeiculo CriarVeiculo(string tipoString)
    {
        return tipoString.ToLower() switch
        {
            "carro" => new Carro(),
            "moto" => new Moto(),
            "caminhao" or "caminhão" => new Caminhao(),
            _ => throw new ArgumentException($"Tipo de veículo '{tipoString}' não reconhecido")
        };
    }
}
