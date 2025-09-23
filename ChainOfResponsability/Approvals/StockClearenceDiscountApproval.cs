using System;

namespace Estudos.ChainOfResponsability.Approvals;

public class StockClearenceDiscountApproval : BaseDiscountApproval
{
   private readonly HashSet<string> _stockClearenceProducts = new() { "P99"};
    override public bool Handle(Order order, float proposedDiscount)
    {

        if (_stockClearenceProducts.Contains(order.Client))
            return true;

        return base.Handle(order, proposedDiscount);
    }

}
