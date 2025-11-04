namespace Estudos.Factory.Veiculos;

/// <summary>
/// Produto concreto: Caminhão
/// </summary>
public class Caminhao : IVeiculo
{
    public string ObterTipo() => "Caminhão";
    
    public decimal ObterPreco() => 150000m;
    
    public void Acelerar()
    {
        Console.WriteLine("Caminhão acelerando lentamente...");
    }
    
    public void Frear()
    {
        Console.WriteLine("Caminhão freando com freio a ar...");
    }
}
