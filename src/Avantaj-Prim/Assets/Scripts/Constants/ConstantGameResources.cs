using UnityEngine;

namespace Constants
{
    [CreateAssetMenu(fileName = "ConstantGameResources", menuName = "Constants/ConstantGameResources")]
    public class ConstantGameResources : ScriptableObject
    {
        [System.Serializable]
        public class GiftSlotSprites
        {
            [SerializeField] private Sprite openSlotSprite;
            [SerializeField] private Sprite closedSlotSprite;

            public Sprite GetGiftSlotSprite(bool flag)
            {
                return flag ? openSlotSprite : closedSlotSprite;
            }
        }

        public GiftSlotSprites SlotSprites;
    }
}