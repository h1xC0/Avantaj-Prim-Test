using MainComponents.Customers.Orders;

namespace MainComponents.Customers
{
    public class CustomerModel
    {
        public Order Order;
        public float OrderWaitingTime;

        public CustomerModel(Order order, float orderWaitingTime)
        {
            Order = order;
            OrderWaitingTime = orderWaitingTime;
        }

    }
}