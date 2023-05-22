using BaseInfrastructure;
using MainComponents.DraggableItems;
using MainComponents.Gifts;
using MainComponents.Gifts.TrashBin;
using UnityEngine;

namespace MainComponents.Gameplay
{
    public interface IGameplayView : IView
    {
        GiftSlotView[] GiftSlots { get; }
        DraggableItemContainerView[] BoxContainers { get; }
        DraggableItemContainerView[] BowContainers { get; }
        DraggableItemContainerView[] DesignContainers { get; }
        TrashBinView TrashBinView { get; }

        void Construct(Canvas canvas);
        void Show();
        void Hide();
    }
}