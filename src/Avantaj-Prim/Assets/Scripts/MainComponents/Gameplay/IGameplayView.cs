using BaseInfrastructure;
using MainComponents.Crafting;
using MainComponents.DraggableItems;
using MainComponents.Gameplay.TrashBin;
using UnityEngine;

namespace MainComponents.Gameplay
{
    public interface IGameplayView : IView
    {
        CraftingSlotView[] CraftingSlots { get; }
        DraggableItemContainerView[] BoxContainers { get; }
        DraggableItemContainerView[] BowContainers { get; }
        DraggableItemContainerView[] DesignContainers { get; }
        TrashBinView TrashBinView { get; }
        
        Canvas Canvas { get; }
        void Show();
        void Hide();
    }
}