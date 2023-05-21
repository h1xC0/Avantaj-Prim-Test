using MainComponents.Customers;
using MainComponents.DraggableItems;
using MainComponents.Level;
using Services.ResourceProvider;

namespace Constants
{
    public static class ResourceNames
    {
        public static ResourceInfo DraggableItem = new(typeof(DraggableItemView), "Prefab/DraggableItem");
        public static ResourceInfo Levels = new(typeof(LevelConfiguration), "ScriptableObjects/LevelConfigurations");
    }
}