// See https://aka.ms/new-console-template for more information
using Estudos.ChainOfResponsability.Approvals;
using Estudos.Observer;
using Estudos.Observer.Observers;

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