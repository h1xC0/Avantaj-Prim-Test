using BaseInfrastructure;
using MainComponents.Customers;
using MainComponents.DraggableItems;
using MainComponents.Gifts;
using MainComponents.Gifts.TrashBin;
using UnityEngine;

namespace MainComponents.Gameplay
{
    public class GameplayView : BaseView, IGameplayView
    {
        public GiftSlotView[] GiftSlots => _giftSlots;
        public DraggableItemContainerView[] DesignContainers => _ornamentContainers;
        public DraggableItemContainerView[] BoxContainers => _boxContainers;
        public DraggableItemContainerView[] BowContainers => _bowContainers;
        public TrashBinView TrashBinView => trashBinView;
        public CustomerView Customer => customer;
        public CustomerSpawnPoint[] CustomerSpawnPoints => spawnPoints;


        [Header("Gift Crafting")]
        [SerializeField] private GiftSlotView[] _giftSlots;
        [SerializeField] private DraggableItemContainerView[] _boxContainers;
        [SerializeField] private DraggableItemContainerView[] _bowContainers;
        [SerializeField] private DraggableItemContainerView[] _ornamentContainers;
        [SerializeField] private TrashBinView trashBinView;
        
        [Header("Customers")]
        [SerializeField] private CustomerView customer;
        [SerializeField] private CustomerSpawnPoint[] spawnPoints;

        private Canvas _canvas;

        public void Construct(Canvas canvas)
        {
            _canvas = canvas;
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