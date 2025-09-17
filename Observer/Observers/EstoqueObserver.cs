using System;
using System.Collections.Generic;
using System.Text;

namespace Estudos.Observer.Observers
{
    internal class EstoqueObserver : IObserver
    {
        public void Update(string message)
        {
            Console.WriteLine($"[Estoque] {message} - Verificando estoque...");
        }
    }
}
