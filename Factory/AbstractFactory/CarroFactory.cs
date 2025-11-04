using Estudos.Factory.Veiculos;

namespace Estudos.Factory.AbstractFactory;

/// <summary>
/// Factory concreta para criar Carros
/// </summary>
public class CarroFactory : IVeiculoFactory
{
    public IVeiculo CriarVeiculo()
    {
        return new Carro();
    }
    
    public string ObterCategoria()
    {
        return "Veículo de Passeio";
    }
}
