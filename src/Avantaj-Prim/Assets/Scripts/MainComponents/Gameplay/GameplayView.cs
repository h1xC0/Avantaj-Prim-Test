using BaseInfrastructure;
using MainComponents.Crafting;
using MainComponents.DraggableItems;
using MainComponents.Gameplay.TrashBin;
using UnityEngine;

namespace MainComponents.Gameplay
{
    public class GameplayView : BaseView, IGameplayView
    {
        public CraftingSlotView[] CraftingSlots => _craftingSlots;
        public DraggableItemContainerView[] DesignContainers => _ornamentContainers;
        public DraggableItemContainerView[] BoxContainers => _boxContainers;
        public DraggableItemContainerView[] BowContainers => _bowContainers;
        public TrashBinView TrashBinView => trashBinView;
        public Canvas Canvas => _canvas;

        [Header("Gift Crafting")]
        [SerializeField] private CraftingSlotView[] _craftingSlots;
        [SerializeField] private DraggableItemContainerView[] _boxContainers;
        [SerializeField] private DraggableItemContainerView[] _bowContainers;
        [SerializeField] private DraggableItemContainerView[] _ornamentContainers;
        [SerializeField] private TrashBinView trashBinView;


        private Canvas _canvas;

        public override void Construct()
        {
            _canvas = GetComponent<Canvas>();
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }
        
        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}