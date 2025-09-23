using System;

namespace Estudos.ChainOfResponsability.Approvals;

public class BaseDiscountApproval
{
    private BaseDiscountApproval? _nextApproval;

    public BaseDiscountApproval SetNext(BaseDiscountApproval handler)
    {
        _nextApproval = handler;
        return handler;
    }

    public virtual bool Handle(Order order, float proposedDiscount)
    {
        return _nextApproval?.Handle(order, proposedDiscount) ?? false;
    }
    
}
