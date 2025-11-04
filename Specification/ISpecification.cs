using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Estudos.Specification
{
    public interface ISpecification<T>
    {
        // Método principal para verificação em memória
        bool IsSatisfiedBy(T item);

        // Método essencial para uso com Entity Framework/IQueryable
        // Permite que o filtro seja traduzido para SQL e executado no banco de dados.
        Expression<Func<T, bool>> ToExpression();

    }
    public class Produto
    {
        public string Nome { get; set; }
        public decimal Preco { get; set; }
        public bool EmEstoque { get; set; }
        public DateTime DataLancamento { get; set; }

        public void AplicarFiltros(IQueryable<Produto> produtos)
        {
            // 1. Crie as especificações base
            var emEstoqueSpec = new ProdutoEmEstoqueSpecification();
            var precoBaixoSpec = new ProdutoPrecoMaximoSpecification(99.99m);
       
            // Se quisesse uma OR, seria:
            // ISpecification<Produto> filtroOr = emEstoqueSpec.Or(novoSpec);

            // 3. Aplique o filtro à coleção (A beleza do LINQ)

            // Opção 1: Filtragem em memória (IEnumerable<T>)
            // IEnumerable<Produto> produtosFiltrados = produtos.Where(p => filtroFinal.IsSatisfiedBy(p));

            // Opção 2: Filtragem no Banco de Dados (IQueryable<T>)
            // Se o seu ISpecification tiver o método ToExpression() bem implementado:
            IQueryable<Produto> produtosFiltradosDB = produtos
                .Where(emEstoqueSpec.ToExpression())
                .Where(precoBaixoSpec.ToExpression());

            // ... continue o processamento com produtosFiltradosDB
        }
    }
}
