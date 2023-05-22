using System.Collections.Generic;
using System.Linq;
using Services.ResourceProvider;
using UnityEngine;

namespace Constants
{
    [CreateAssetMenu(fileName = "Rewards", menuName = "Constants/Customer/Rewards")]
    public class Rewards : ScriptableObject, IResource
    {
        public List<CustomerReward> RewardsList;
        
        public int GetRewardByOrderCount(int completedOrders)
        {
            if (RewardsList.Any() == false) return 0;
            
            var matchedPrice = RewardsList.FirstOrDefault(x => x.Orders == completedOrders);
            if (matchedPrice is null) return 0;

            return matchedPrice.Reward;
        }
    }
}