using System;
using Constants;
using MainComponents.Gifts.Models;
using MainComponents.Level;

namespace Services.LevelConfigurationService
{
    public interface ILevelConfigurationService : IDisposable
    {
        int TotalLevels { get; }
        GiftSlot[] GiftSlots { get; }
        LevelConfiguration GetLevelConfiguration(int levelIndex);
        GiftRecipes GiftRecipes { get; }
        Rewards Rewards { get; }
    }
    
}