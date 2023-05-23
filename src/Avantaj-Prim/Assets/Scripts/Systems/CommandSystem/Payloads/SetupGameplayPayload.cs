using GameState;
using MainComponents.Gameplay;
using MainComponents.GameplayUI;
using UnityEngine;
using Zenject;

namespace Systems.CommandSystem.Payloads
{
    public class SetupGameplayPayload : ICommandPayload
    {
        public GameplayView GameplayView;
        public LevelStateView LevelStateView;
        public LevelDataView LevelDataView;
        
        public DiContainer Container;
        public Transform SpawnPoint;
        public Canvas Canvas;

        public SetupGameplayPayload(GameplayView gameplayView, LevelStateView levelStateView, LevelDataView levelDataView, Transform spawnPoint, Canvas canvas, DiContainer container)
        {
            GameplayView = gameplayView;
            LevelStateView = levelStateView;
            LevelDataView = levelDataView;
            Container = container;
            SpawnPoint = spawnPoint;
            Canvas = canvas;
        }
    }
}