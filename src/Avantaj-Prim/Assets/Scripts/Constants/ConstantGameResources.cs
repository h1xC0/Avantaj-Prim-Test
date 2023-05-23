using System.Collections.Generic;
using UnityEngine;

namespace Constants
{
    [CreateAssetMenu(fileName = "ConstantGameResources", menuName = "Constants/ConstantGameResources")]
    public class ConstantGameResources : ScriptableObject
    {
        [System.Serializable]
        public class GiftSlotSprites
        {
            public Sprite openSlotSprite;
            public Sprite closedSlotSprite;
        }

        [System.Serializable]
        public class CustomerSprites
        {
            public Sprite satisfiedCustomer;
            public Sprite unsatisfiedCustomer;
        }

        [SerializeField] private GiftSlotSprites slotSprites;
        [SerializeField] private List<CustomerSprites> customerSpritesList;
        
        public Sprite GetGiftSlotSprite(bool flag)
        {
            return flag ? slotSprites.openSlotSprite : slotSprites.closedSlotSprite;
        }
        public CustomerSprites GetRandomCustomer()
        {
            var index = Random.Range(0, customerSpritesList.Count);
            return customerSpritesList[index];
        }
    }
}