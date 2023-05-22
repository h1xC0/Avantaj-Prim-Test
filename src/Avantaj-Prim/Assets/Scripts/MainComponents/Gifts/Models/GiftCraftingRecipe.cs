using Services.ResourceProvider;
using UnityEngine;

namespace MainComponents.Gifts.Models
{
    [CreateAssetMenu(fileName = "GiftCraftingRecipe", menuName = "Constants/Gifts/GiftCraftingRecipe")]
    public class GiftCraftingRecipe : ScriptableObject, IResource
    {
        public GiftPartSO[] GiftParts;
        public Sprite Result;
    }
}