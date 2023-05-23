namespace Constants
{
    [System.Serializable]
    public class CustomerReward
    {
        public int Reward;
        public int Orders;

        public CustomerReward(int reward, int orders)
        {
            Reward = reward;
            Orders = orders;
        }
    }
}