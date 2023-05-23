using GameState;
using Services.LevelConfigurationService;
using Services.LevelProgressionService;
using Services.PresenterProvider;
using Systems.CommandSystem;
using Systems.CommandSystem.Payloads;

namespace Commands
{
    public class CreateLevelEndViewCommand : Command
    {
        private readonly IPresenterContainerService _presenterContainerService;
        private readonly ILevelProgressionService _levelProgressionService;

        public CreateLevelEndViewCommand(IPresenterContainerService presenterContainerService, ILevelProgressionService levelProgressionService)
        {
            _presenterContainerService = presenterContainerService;
            _levelProgressionService = levelProgressionService;
        }
        
        protected override void Execute(ICommandPayload payload)
        {
            Retain();

            var gameState = payload as EndLevelStatePayload;
            _levelProgressionService.SetLevelEnded(gameState.LevelEnded);
            _presenterContainerService.Resolve<LevelStatePresenter>().ConstructLevelEnd(gameState.LevelState);
            
            Release();
        }

    }
}