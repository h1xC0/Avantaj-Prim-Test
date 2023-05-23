using BaseInfrastructure;
using Constants;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace GameState
{
    public class LevelStateView : BaseView, ILevelStateView
    {
        [SerializeField] private Image backgroundBlock;
        [SerializeField] private Transform viewSpawnPoint;
        
        public void FadeBackground()
        {
            backgroundBlock.gameObject.SetActive(true);
            backgroundBlock.DOFade(0.8f, AnimationConstants.AnimationSpeed);
        }

        public ILevelEndView GetWinView(LevelEndView winView)
        {
            return Instantiate(winView, viewSpawnPoint);
        }

        public ILevelEndView GetLoseView(LevelEndView loseView)
        {
            return Instantiate(loseView, viewSpawnPoint);
        }        
    }
}