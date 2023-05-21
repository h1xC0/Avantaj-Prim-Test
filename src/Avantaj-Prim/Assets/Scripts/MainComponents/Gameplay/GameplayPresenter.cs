using BaseInfrastructure;
using MainComponents.DraggableItems;
using MainComponents.Gameplay.TrashBin;
using MainComponents.Level;
using Services.AnimationService;
using Services.InputService;
using Services.ResourceProvider;

namespace MainComponents.Gameplay
{
    public class GameplayPresenter : BasePresenter<IGameplayView>
    {
        private readonly IInputService _inputService;
        private readonly IResourceProviderService _resourceProviderService;
        private readonly IAnimationService _animationService;

        public GameplayPresenter(
            IGameplayView viewContract,
            IInputService inputService,
            IResourceProviderService resourceProviderService, IAnimationService animationService) 
            : base(viewContract)
        {
            _inputService = inputService;
            _resourceProviderService = resourceProviderService;
            _animationService = animationService;
        }

        public void ConstructGameplay(LevelConfiguration levelConfiguration)
        {
            View.Hide();

            ConstructBoxVariants(levelConfiguration);
            ConstructBowVariants(levelConfiguration);
            ConstructsOrnamentVariants(levelConfiguration);
            ConstructTrashBin();
            
            View.Show();
        }

        private void ConstructTrashBin() => 
            AddDisposable(new TrashBinPresenter(View.TrashBinView, _inputService));

        private void ConstructsOrnamentVariants(LevelConfiguration levelConfiguration)
        {
            for (int i = 0; i < levelConfiguration.AvailableOrnaments.Length; i++)
            {
                if (i >= View.DesignContainers.Length) break;
                var giftPart = levelConfiguration.AvailableOrnaments[i];
                var view = View.DesignContainers[i];

                AddDisposable(new DraggableItemContainerPresenter(view, _inputService, _resourceProviderService, View.Canvas, giftPart, _animationService));
            }
        }

        private void ConstructBowVariants(LevelConfiguration levelConfiguration)
        {
            for (int i = 0; i < levelConfiguration.AvailableBows.Length; i++)
            {
                if (i >= View.BowContainers.Length) break;
                var giftPart = levelConfiguration.AvailableBows[i];
                var view = View.BowContainers[i];

                AddDisposable(new DraggableItemContainerPresenter(view, _inputService, _resourceProviderService,  View.Canvas, giftPart, _animationService));
            }
        }

        private void ConstructBoxVariants(LevelConfiguration levelConfiguration)
        {
            for (int i = 0; i < levelConfiguration.AvailableBoxes.Length; i++)
            {
                if (i >= View.BoxContainers.Length) break;
                var giftPart = levelConfiguration.AvailableBoxes[i];
                var view = View.BoxContainers[i];

                AddDisposable(new DraggableItemContainerPresenter(view, _inputService, _resourceProviderService, View.Canvas, giftPart, _animationService));
            }
        }
    }
}