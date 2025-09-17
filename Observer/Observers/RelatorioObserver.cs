using System;
using System.Collections.Generic;
using System.Text;

namespace Estudos.Observer.Observers
{
    internal class RelatorioObserver : IObserver
    {
        public void Update(string message)
        {
            Console.WriteLine($"[Relatório] {message}");
        }
    }
}
