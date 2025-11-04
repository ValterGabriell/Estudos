namespace Estudos.Factory;

/// <summary>
/// Interface que define o contrato para todos os veículos
/// </summary>
public interface IVeiculo
{
    string ObterTipo();
    decimal ObterPreco();
    void Acelerar();
    void Frear();
}
