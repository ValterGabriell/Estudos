using System;

namespace Estudos.ChainOfResponsability.Approvals;

public class GlobalDiscountApproval : BaseDiscountApproval
{
    public override bool Handle(Order order, float proposedDiscount)
    {
        if (proposedDiscount <= 0.1f)
            return true;

        return base.Handle(order, proposedDiscount);
    }
}
