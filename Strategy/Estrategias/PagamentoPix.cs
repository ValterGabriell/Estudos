namespace Estudos.Strategy.Estrategias;

/// <summary>
/// Estratégia concreta: Pagamento com PIX
/// </summary>
public class PagamentoPix : IEstrategiaPagamento
{
    private readonly string _chavePix;

    public PagamentoPix(string chavePix)
    {
        _chavePix = chavePix;
    }

    public string ObterNome() => "PIX";

    public bool ProcessarPagamento(decimal valor)
    {
        if (!ValidarPagamento(valor))
        {
            Console.WriteLine("❌ Pagamento PIX inválido!");
            return false;
        }

        var taxa = CalcularTaxa(valor);
        var valorTotal = valor + taxa;

        Console.WriteLine($"📱 Processando pagamento via PIX...");
        Console.WriteLine($"   Chave PIX: {_chavePix}");
        Console.WriteLine($"   Valor: R$ {valor:N2}");
        Console.WriteLine($"   Taxa: R$ {taxa:N2}");
        Console.WriteLine($"   Total: R$ {valorTotal:N2}");
        Console.WriteLine("✅ Pagamento confirmado instantaneamente!");
        
        return true;
    }

    public decimal CalcularTaxa(decimal valor)
    {
        // PIX sem taxa
        return 0m;
    }

    public bool ValidarPagamento(decimal valor)
    {
        if (string.IsNullOrWhiteSpace(_chavePix))
        {
            Console.WriteLine("Chave PIX inválida!");
            return false;
        }

        if (valor <= 0)
        {
            Console.WriteLine("Valor inválido!");
            return false;
        }

        return true;
    }
}
