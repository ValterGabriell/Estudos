// See https://aka.ms/new-console-template for more information
using CommandDesignPattern.Command;
using CommandDesignPattern.Invoker;
using Estudos.ChainOfResponsability.Approvals;
using Estudos.Observer;
using Estudos.Observer.Observers;
using System.Windows.Input;

Console.WriteLine("Hello, World!");

/*OBERSVER*/
var venda = new Venda();
venda.Attach(new RelatorioObserver());
venda.Attach(new EstoqueObserver());
venda.RegistrarVenda("Notebook", 3500.00);
/*OBERSVER*/


/*COR*/
var baseDiscount = new BaseDiscountApproval();
baseDiscount.SetNext(new VipDiscountApproval())
    .SetNext(new StockClearenceDiscountApproval())
    .SetNext(new GlobalDiscountApproval());
/*COR*/



//COMAND
var modifyPrice = new ModifyPrice();
var product = new Product("Phone", 500);

Execute(modifyPrice, new ProductCommand(product, PriceAction.Increase, 100));
Execute(modifyPrice, new ProductCommand(product, PriceAction.Increase, 50));
Execute(modifyPrice, new ProductCommand(product, PriceAction.Decrease, 25));

Console.WriteLine(product);

void Execute(ModifyPrice modifyPrice, ICommandMine productCommand)
{
    modifyPrice.SetCommand(productCommand);
    modifyPrice.Execute();
}