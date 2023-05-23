using MainComponents.Customers.Orders;

namespace MainComponents.Customers
{
    public class Customer
    {
        public Order Order;
        public float OrderWaitingTime;

        public Customer(Order order, float orderWaitingTime)
        {
            Order = order;
            OrderWaitingTime = orderWaitingTime;
        }

    }
}