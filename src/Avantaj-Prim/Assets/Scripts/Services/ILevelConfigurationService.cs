using System;
using MainComponents.Level;

namespace Services
{
    public interface ILevelConfigurationService : IDisposable
    {
        LevelConfiguration GetLevelConfiguration(int levelIndex);
    }
}