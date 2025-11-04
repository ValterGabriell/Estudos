using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Estudos.Specification
{
    internal class ProdutoEmEstoqueSpecification : ISpecification<Produto>
    {
        public bool IsSatisfiedBy(Produto produto)
        {
            return produto.EmEstoque;
        }

        public Expression<Func<Produto, bool>> ToExpression()
        {
            return produto => produto.EmEstoque;
        }
    }
}
