namespace Estudos.Strategy.Estrategias;

/// <summary>
/// Estratégia concreta: Pagamento com Boleto Bancário
/// </summary>
public class PagamentoBoleto : IEstrategiaPagamento
{
    private readonly string _cpfPagador;
    private readonly DateTime _dataVencimento;

    public PagamentoBoleto(string cpfPagador, DateTime dataVencimento)
    {
        _cpfPagador = cpfPagador;
        _dataVencimento = dataVencimento;
    }

    public string ObterNome() => "Boleto Bancário";

    public bool ProcessarPagamento(decimal valor)
    {
        if (!ValidarPagamento(valor))
        {
            Console.WriteLine("? Pagamento com boleto inválido!");
            return false;
        }

        var taxa = CalcularTaxa(valor);
        var valorTotal = valor + taxa;
        var codigoBarras = GerarCodigoBarras();

        Console.WriteLine($"?? Gerando Boleto Bancário...");
        Console.WriteLine($"   CPF: {_cpfPagador}");
        Console.WriteLine($"   Vencimento: {_dataVencimento:dd/MM/yyyy}");
        Console.WriteLine($"   Valor: R$ {valor:N2}");
        Console.WriteLine($"   Taxa: R$ {taxa:N2}");
        Console.WriteLine($"   Total: R$ {valorTotal:N2}");
        Console.WriteLine($"   Código de Barras: {codigoBarras}");
        Console.WriteLine("? Boleto gerado com sucesso! Pague até o vencimento.");
        
        return true;
    }

    public decimal CalcularTaxa(decimal valor)
    {
        // Taxa fixa de R$ 3,50 para boleto
        return 3.50m;
    }

    public bool ValidarPagamento(decimal valor)
    {
        if (string.IsNullOrWhiteSpace(_cpfPagador) || _cpfPagador.Length != 11)
        {
            Console.WriteLine("CPF inválido!");
            return false;
        }

        if (_dataVencimento < DateTime.Now.Date)
        {
            Console.WriteLine("Data de vencimento inválida!");
            return false;
        }

        if (valor <= 0)
        {
            Console.WriteLine("Valor inválido!");
            return false;
        }

        return true;
    }

    private string GerarCodigoBarras()
    {
        // Simulação de código de barras
        var random = new Random();
        return $"23793.38128 60000.012345 67890.101234 5 {DateTime.Now.Ticks}";
    }
}
