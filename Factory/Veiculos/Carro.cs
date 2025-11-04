namespace Estudos.Factory.Veiculos;

/// <summary>
/// Produto concreto: Carro
/// </summary>
public class Carro : IVeiculo
{
    public string ObterTipo() => "Carro";
    
    public decimal ObterPreco() => 50000m;
    
    public void Acelerar()
    {
        Console.WriteLine("Carro acelerando suavemente...");
    }
    
    public void Frear()
    {
        Console.WriteLine("Carro freando com ABS...");
    }
}
