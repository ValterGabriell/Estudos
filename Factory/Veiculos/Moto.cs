namespace Estudos.Factory.Veiculos;

/// <summary>
/// Produto concreto: Moto
/// </summary>
public class Moto : IVeiculo
{
    public string ObterTipo() => "Moto";
    
    public decimal ObterPreco() => 15000m;
    
    public void Acelerar()
    {
        Console.WriteLine("Moto acelerando rapidamente...");
    }
    
    public void Frear()
    {
        Console.WriteLine("Moto freando bruscamente...");
    }
}
