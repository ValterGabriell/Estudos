using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Estudos.Specification
{
    internal class ProdutoPrecoMaximoSpecification : ISpecification<Produto>
    {
        private readonly decimal _precoMaximo;

        public ProdutoPrecoMaximoSpecification(decimal precoMaximo)
        {
            _precoMaximo = precoMaximo;
        }

        public bool IsSatisfiedBy(Produto produto)
        {
            return produto.Preco < _precoMaximo;
        }

        public Expression<Func<Produto, bool>> ToExpression()
        {
            return produto => produto.Preco < _precoMaximo;
        }


    }
}
