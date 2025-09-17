using System;
using System.Collections.Generic;
using System.Text;

namespace Estudos.Observer
{
    internal class Venda
    {
        private List<IObserver> observers = new List<IObserver>();  
        public void Attach(IObserver observer)
        {
            observers.Add(observer);
        }
        public void Detach(IObserver observer)
        {
            observers.Remove(observer);
        }

        public void RegistrarVenda(string produto, double valor)
        {
            Console.WriteLine($"Venda registrada: {produto} - R$ {valor}");
            NotifyObservers($"Nova venda: {produto} - R$ {valor}");
        }

        private void NotifyObservers(string message)
        {
            foreach (var observer in observers)
            {
                observer.Update(message);
            }
        }
    }
}
