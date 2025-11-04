namespace Estudos.Strategy.Estrategias;

/// <summary>
/// Estratégia concreta: Pagamento com Cartão de Crédito
/// </summary>
public class PagamentoCartaoCredito : IEstrategiaPagamento
{
    private readonly string _numeroCartao;
    private readonly string _cvv;
    private readonly DateTime _dataValidade;

    public PagamentoCartaoCredito(string numeroCartao, string cvv, DateTime dataValidade)
    {
        _numeroCartao = numeroCartao;
        _cvv = cvv;
        _dataValidade = dataValidade;
    }

    public string ObterNome() => "Cartão de Crédito";

    public bool ProcessarPagamento(decimal valor)
    {
        if (!ValidarPagamento(valor))
        {
            Console.WriteLine("? Pagamento com cartão de crédito inválido!");
            return false;
        }

        var taxa = CalcularTaxa(valor);
        var valorTotal = valor + taxa;

        Console.WriteLine($"?? Processando pagamento com Cartão de Crédito...");
        Console.WriteLine($"   Cartão: **** **** **** {_numeroCartao[^4..]}");
        Console.WriteLine($"   Valor: R$ {valor:N2}");
        Console.WriteLine($"   Taxa (2.5%): R$ {taxa:N2}");
        Console.WriteLine($"   Total: R$ {valorTotal:N2}");
        Console.WriteLine("? Pagamento aprovado! Parcelamento em até 12x sem juros.");
        
        return true;
    }

    public decimal CalcularTaxa(decimal valor)
    {
        // Taxa de 2.5% para cartão de crédito
        return valor * 0.025m;
    }

    public bool ValidarPagamento(decimal valor)
    {
        if (string.IsNullOrWhiteSpace(_numeroCartao) || _numeroCartao.Length != 16)
        {
            Console.WriteLine("Número do cartão inválido!");
            return false;
        }

        if (string.IsNullOrWhiteSpace(_cvv) || _cvv.Length != 3)
        {
            Console.WriteLine("CVV inválido!");
            return false;
        }

        if (_dataValidade < DateTime.Now)
        {
            Console.WriteLine("Cartão vencido!");
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
