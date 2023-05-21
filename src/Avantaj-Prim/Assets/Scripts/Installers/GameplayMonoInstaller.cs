using MainComponents.Gameplay;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class GameplayMonoInstaller : MonoInstaller
    {
        [SerializeField] private GameplayEntryPoint _gameplayEntryPoint;
        public override void InstallBindings()
        {
            Container.Install<GameplayContextInstaller>();
        }
   
        // private void BindEntryPoint()
        // {
        //     Container.BindInterfacesTo<GameplayEntryPoint>()
        //     .FromInstance(_gameplayEntryPoint)
        //     .AsSingle()
        //     .NonLazy();
        // }
    }
}