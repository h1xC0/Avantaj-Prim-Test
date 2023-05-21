using BaseInfrastructure;

namespace MainComponents.DraggableItems
{
    public interface IDraggableItemContainerView : IView
    {
        DraggableItemView CreateViewForGift(DraggableItemView draggableItemView);
    }
}