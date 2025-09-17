// See https://aka.ms/new-console-template for more information
using Estudos.Observer;
using Estudos.Observer.Observers;

Console.WriteLine("Hello, World!");


var venda = new Venda();
venda.Attach(new RelatorioObserver());
venda.Attach(new EstoqueObserver());

venda.RegistrarVenda("Notebook", 3500.00);