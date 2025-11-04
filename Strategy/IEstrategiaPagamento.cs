namespace Estudos.Strategy;

/// <summary>
/// Interface que define o contrato para todas as estratégias de pagamento
/// </summary>
public interface IEstrategiaPagamento
{
    string ObterNome();
    bool ProcessarPagamento(decimal valor);
    decimal CalcularTaxa(decimal valor);
    bool ValidarPagamento(decimal valor);
}
