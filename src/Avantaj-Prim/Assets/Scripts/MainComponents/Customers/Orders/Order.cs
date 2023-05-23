using System.Collections.Generic;
using MainComponents.Gifts.Models;

namespace MainComponents.Customers.Orders
{
    public class Order
    {
        public List<Gift> OrderList;

        public Order(List<Gift> orderList)
        {
            OrderList = orderList;
        }
    }
}