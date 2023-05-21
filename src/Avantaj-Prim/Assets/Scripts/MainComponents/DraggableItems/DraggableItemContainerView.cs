using BaseInfrastructure;
using UnityEngine;

namespace MainComponents.DraggableItems
{
    public class DraggableItemContainerView : BaseView, IDraggableItemContainerView
    {
        public DraggableItemView CreateViewForGift(DraggableItemView draggableItemView)
        {
            return Instantiate(draggableItemView, transform);
        }        
    }
}