using System;

namespace Estudos.ChainOfResponsability.Approvals;

public class VipDiscountApproval : BaseDiscountApproval
{
    private readonly HashSet<string> _vipClients = new() { "ClienteVIP1", "ClienteVIP2" };
    public override bool Handle(Order order, float proposedDiscount)
    {
        if(_vipClients.Contains(order.Client))
            return true;

        return base.Handle(order, proposedDiscount);
    }
}
