namespace Estudos.Factory.AbstractFactory;

/// <summary>
/// Abstract Factory Pattern
/// Interface que define o contrato para factories de veículos
/// Permite criar famílias de objetos relacionados
/// </summary>
public interface IVeiculoFactory
{
    IVeiculo CriarVeiculo();
    string ObterCategoria();
}
