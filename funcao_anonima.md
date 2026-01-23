Funcao anonima é uma funcao que nao tem nome, só parametro, corpo e retorno.
Ela nunca é executada sozinha, sempre precisa de alguem pra executar ela junto.
Ou seja, nao é uma funcao que a gente chama ela, a gente entrega pra alguem

Exemplo
Func<int, int, int> operacao = (a, b) => a + b;

a operacao guarda a funcao, a gente entrega entao a funcao pra alguem

ex

int ExecutarOperacao(Func<int, int, int> op)
{
    return op(2, 3);
}

var resultado = ExecutarOperacao(operacao);

a gente entregou pra funcao executar operacao
