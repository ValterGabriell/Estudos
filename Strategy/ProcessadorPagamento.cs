namespace Estudos.Strategy;

/// <summary>
/// Contexto que usa a estratégia de pagamento
/// Responsável por manter uma referência à estratégia e delegar o trabalho a ela
/// 
/// O contexto não conhece as classes concretas das estratégias,
/// trabalha apenas com a interface
/// </summary>
public class ProcessadorPagamento
{
    private IEstrategiaPagamento? _estrategia;

    /// <summary>
    /// Define a estratégia de pagamento a ser utilizada
    /// Permite trocar a estratégia em tempo de execução
    /// </summary>
    public void DefinirEstrategia(IEstrategiaPagamento estrategia)
    {
        _estrategia = estrategia;
        Console.WriteLine($"?? Estratégia de pagamento alterada para: {_estrategia.ObterNome()}\n");
    }

    /// <summary>
    /// Processa o pagamento usando a estratégia atual
    /// </summary>
    public bool ProcessarPagamento(decimal valor)
    {
        if (_estrategia == null)
        {
            Console.WriteLine("? Nenhuma estratégia de pagamento foi definida!");
            return false;
        }

        Console.WriteLine($"???????????????????????????????????????");
        Console.WriteLine($"Método de Pagamento: {_estrategia.ObterNome()}");
        Console.WriteLine($"???????????????????????????????????????");
        
        var resultado = _estrategia.ProcessarPagamento(valor);
        
        Console.WriteLine($"???????????????????????????????????????\n");
        
        return resultado;
    }

    /// <summary>
    /// Calcula a taxa usando a estratégia atual
    /// </summary>
    public decimal CalcularTaxa(decimal valor)
    {
        if (_estrategia == null)
        {
            throw new InvalidOperationException("Nenhuma estratégia de pagamento foi definida!");
        }

        return _estrategia.CalcularTaxa(valor);
    }

    /// <summary>
    /// Obtém o nome da estratégia atual
    /// </summary>
    public string? ObterEstrategiaAtual()
    {
        return _estrategia?.ObterNome();
    }
}
