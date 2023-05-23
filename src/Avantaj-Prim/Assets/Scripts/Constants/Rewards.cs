using System.Collections.Generic;
using System.Linq;
using Services.ResourceProvider;
using UnityEngine;

namespace Constants
{
    [CreateAssetMenu(fileName = "Rewards", menuName = "Constants/Customer/Rewards")]
    public class Rewards : ScriptableObject, IResource
    {
        public List<CustomerReward> rewardsList;
        
        public int GetRewardByOrderCount(int completedOrders)
        {
            // InitializeIfNull();
            if (rewardsList.Any() == false) return 0;
            
            var matchedPrice = rewardsList.FirstOrDefault(x => x.Orders == completedOrders);
            if (matchedPrice is null) return 0;

            return matchedPrice.Reward;
        }

        private void InitializeIfNull()
        {
            rewardsList ??= new List<CustomerReward>()
            {
                new(15, 1),
                new(40, 2),
                new(65, 3)
            };
        }
    }
}