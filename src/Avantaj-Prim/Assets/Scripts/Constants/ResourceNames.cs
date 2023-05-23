using GameState;
using MainComponents.Customers;
using MainComponents.DraggableItems;
using MainComponents.Gifts.Models;
using MainComponents.Level;

namespace Constants
{
    public static class ResourceNames
    {
        public static ResourceInfo DraggableItem = new(typeof(DraggableItemView), "Prefab/DraggableItem");
        public static ResourceInfo Levels = new(typeof(LevelConfiguration), "ScriptableObjects/LevelConfigurations");
        public static ResourceInfo GiftSlots = new(typeof(GiftSlot), "ScriptableObjects/GiftSlots");
        public static ResourceInfo GiftRecipes = new(typeof(GiftRecipes), "ScriptableObjects/GiftRecipes");
        public static ResourceInfo Rewards = new(typeof(Rewards), "ScriptableObjects/Rewards");
        public static ResourceInfo WinView = new(typeof(LevelEndView), "Prefab/WinView");
        public static ResourceInfo LoseView = new(typeof(LevelEndView), "Prefab/LoseView");
        public static ResourceInfo CustomerView = new(typeof(CustomerView), "Prefab/CustomerView");
    }
}