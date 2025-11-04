using Estudos.Factory.Veiculos;

namespace Estudos.Factory.AbstractFactory;

/// <summary>
/// Factory concreta para criar Caminhões
/// </summary>
public class CaminhaoFactory : IVeiculoFactory
{
    public IVeiculo CriarVeiculo()
    {
        return new Caminhao();
    }
    
    public string ObterCategoria()
    {
        return "Veículo de Carga";
    }
}
