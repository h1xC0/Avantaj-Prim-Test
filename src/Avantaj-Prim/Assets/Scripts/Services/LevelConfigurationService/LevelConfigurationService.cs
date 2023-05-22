using System;
using System.Collections.Generic;
using System.Linq;
using Constants;
using MainComponents.Gifts.Models;
using MainComponents.Level;
using Services.ResourceProvider;
using UnityEngine;

namespace Services.LevelConfigurationService
{
    public class LevelConfigurationService : ILevelConfigurationService
    {
        public int TotalLevels => _levelConfigurations.Count;
        public GiftSlot[] GiftSlots => _giftSlots;
        public GiftRecipes GiftRecipes => _giftRecipes;
        public Rewards Rewards => _rewards;
        
        private List<LevelConfiguration> _levelConfigurations = new();
        
        private GiftRecipes _giftRecipes;
        private Rewards _rewards;
        private GiftSlot[] _giftSlots;

        private readonly IResourceProviderService _resourcesProvider;

        public LevelConfigurationService(IResourceProviderService resourcesProvider)
        {
            _resourcesProvider = resourcesProvider;

            LoadLevelConfigurations();
        }
        
        public LevelConfiguration GetLevelConfiguration(int levelIndex)
        {
            var levelConfig = _levelConfigurations
                .FirstOrDefault(x => x.LevelNumber == levelIndex);

            if (levelConfig is not null) return levelConfig;
            
            Debug.LogError($"You chosen a wrong level config");
            
            return null;
        }

        private bool LoadLevelConfigurations()
        {
            _levelConfigurations = _resourcesProvider
                .LoadResources<LevelConfiguration>(ResourceNames.Levels)
                .ToList();
            
            if (_levelConfigurations.Count == 0) return false;

            _giftRecipes = _resourcesProvider
                .LoadResource<GiftRecipes>(ResourceNames.GiftRecipes);

            _rewards = _resourcesProvider
                .LoadResources<Rewards>(ResourceNames.Rewards)
                .FirstOrDefault();

            _giftSlots = _resourcesProvider
                .LoadResources<GiftSlot>(ResourceNames.GiftSlots)
                .ToArray();

            if (_giftSlots != null && _rewards != null && _giftRecipes != null)
            {
                throw new NullReferenceException("Resources wasn't load!");
            }
            
            return true;
        }

        public void Dispose()
        {
            _levelConfigurations.Clear();
            
        }
    }
}