using BaseInfrastructure;

namespace GameState
{
    public interface ILevelStateView : IView
    {
        void FadeBackground();
        ILevelEndView GetWinView(LevelEndView winView);
        ILevelEndView GetLoseView(LevelEndView loseView);
    }
}