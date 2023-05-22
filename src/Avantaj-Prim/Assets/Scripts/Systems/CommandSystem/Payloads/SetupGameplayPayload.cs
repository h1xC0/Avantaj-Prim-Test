using MainComponents.Gameplay;
using Services.Transitions;
using UnityEngine;
using Zenject;

namespace Systems.CommandSystem.Payloads
{
    public class SetupGameplayPayload : ICommandPayload
    {
        public ISceneTransitionService SceneTransitionService;

        public GameplayView GameplayView;
        
        // public WinLoseView WinLoseView;
        // public HudView HudView;
        
        public DiContainer Container;
        public Transform SpawnPoint;
        public Canvas Canvas;

        public SetupGameplayPayload(GameplayView gameplayView, ISceneTransitionService sceneTransitionService, Transform spawnPoint, Canvas canvas, DiContainer container)
        {
            GameplayView = gameplayView;
            SceneTransitionService = sceneTransitionService;
            Container = container;
            SpawnPoint = spawnPoint;
            Canvas = canvas;
        }
    }
}