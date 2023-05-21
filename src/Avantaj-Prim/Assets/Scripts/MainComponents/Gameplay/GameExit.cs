using BaseInfrastructure;
using UnityEngine.Device;
using UnityEngine.EventSystems;

namespace MainComponents.Gameplay
{
    public class GameExit : BaseView, IGameExit, IPointerClickHandler
    {
        public void OnPointerClick(PointerEventData eventData)
        {
            Application.Quit();
        }
    }
}