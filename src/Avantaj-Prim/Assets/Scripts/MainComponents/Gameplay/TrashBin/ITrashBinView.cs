using BaseInfrastructure;
using UnityEngine.EventSystems;

namespace MainComponents.Gameplay
{
    public interface ITrashBinView : IView
    {
        void OnDrop(PointerEventData eventData);
    }
}