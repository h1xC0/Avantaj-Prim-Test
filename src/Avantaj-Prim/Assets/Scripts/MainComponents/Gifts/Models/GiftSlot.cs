using Services.ResourceProvider;
using UnityEngine;

namespace MainComponents.Gifts.Models
{
    [CreateAssetMenu(fileName = "GiftSlot", menuName = "Constants/Gifts/GiftSlot")]
    public class GiftSlot : ScriptableObject, IResource
    {
        public string Title;
        public int Price;
        public bool OpenFromStart;
    }
}