﻿using Domain.Models.OrderModels;

namespace Services.Specifications;

internal class OrderSpecifications : BaseSpecifications<Order>
{
    public OrderSpecifications(Guid id) : base(order=>order.Id==id)
    {
        AddInclude(x => x.DeliveryMethod);
        AddInclude(x => x.Items);
    }
    public OrderSpecifications(string email) : base(order=>order.UserEmail==email)
    {
        AddInclude(x => x.DeliveryMethod);
        AddInclude(x => x.Items);

        AddOrderByDescending(x => x.OrderTime);
    }
}
