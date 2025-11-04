namespace Estudos.Strategy.Estrategias;

/// <summary>
/// Estratégia concreta: Pagamento com Carteira Digital (PayPal, PicPay, etc)
/// </summary>
public class PagamentoCarteiraDigital : IEstrategiaPagamento
{
    private readonly string _email;
    private readonly string _senha;
    private readonly string _nomeCarteira;

    public PagamentoCarteiraDigital(string email, string senha, string nomeCarteira = "PayPal")
    {
        _email = email;
        _senha = senha;
        _nomeCarteira = nomeCarteira;
    }

    public string ObterNome() => $"Carteira Digital - {_nomeCarteira}";

    public bool ProcessarPagamento(decimal valor)
    {
        if (!ValidarPagamento(valor))
        {
            Console.WriteLine("? Pagamento com carteira digital inválido!");
            return false;
        }

        var taxa = CalcularTaxa(valor);
        var valorTotal = valor + taxa;

        Console.WriteLine($"?? Processando pagamento via {_nomeCarteira}...");
        Console.WriteLine($"   Email: {_email}");
        Console.WriteLine($"   Valor: R$ {valor:N2}");
        Console.WriteLine($"   Taxa (1.5%): R$ {taxa:N2}");
        Console.WriteLine($"   Total: R$ {valorTotal:N2}");
        Console.WriteLine($"? Pagamento processado via {_nomeCarteira}!");
        
        return true;
    }

    public decimal CalcularTaxa(decimal valor)
    {
        // Taxa de 1.5% para carteira digital
        return valor * 0.015m;
    }

    public bool ValidarPagamento(decimal valor)
    {
        if (string.IsNullOrWhiteSpace(_email) || !_email.Contains('@'))
        {
            Console.WriteLine("Email inválido!");
            return false;
        }

        if (string.IsNullOrWhiteSpace(_senha) || _senha.Length < 6)
        {
            Console.WriteLine("Senha inválida!");
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
