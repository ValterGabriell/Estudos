using Estudos.Factory.Veiculos;

namespace Estudos.Factory.AbstractFactory;

/// <summary>
/// Factory concreta para criar Motos
/// </summary>
public class MotoFactory : IVeiculoFactory
{
    public IVeiculo CriarVeiculo()
    {
        return new Moto();
    }
    
    public string ObterCategoria()
    {
        return "Veículo de Duas Rodas";
    }
}
