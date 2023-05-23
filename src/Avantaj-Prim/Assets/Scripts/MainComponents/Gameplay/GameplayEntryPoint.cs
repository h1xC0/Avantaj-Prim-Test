using GameState;
using MainComponents.GameplayUI;
using Services.Transitions;
using Signals;
using Systems.CommandSystem;
using Systems.CommandSystem.Payloads;
using UnityEngine;
using Zenject;

namespace MainComponents.Gameplay
{
    public class GameplayEntryPoint : MonoBehaviour
    {
        [SerializeField] private GameplayView gameplayView;
        [SerializeField] private LevelStateView levelStateView;
        [SerializeField] private LevelDataView levelDataView;
        
        [SerializeField] private Canvas canvas;

        [Inject]
        public void Construct(ICommandDispatcher commandDispatcher,
            ISceneTransitionService sceneTransitionService, DiContainer container)
        {
            commandDispatcher.Dispatch<SetupGameplaySignal>(new SetupGameplayPayload(gameplayView, levelStateView, levelDataView, canvas.transform, canvas, container));
        }
    }
}