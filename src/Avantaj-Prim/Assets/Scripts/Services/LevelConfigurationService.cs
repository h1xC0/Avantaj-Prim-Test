using System;
using System.Collections.Generic;
using System.Linq;
using Constants;
using MainComponents.Level;
using Services.ResourceProvider;
using UnityEngine;

namespace Services
{
    public class LevelConfigurationService : ILevelConfigurationService
    {
        private readonly IResourceProviderService _resourcesProvider;

        private List<LevelConfiguration> _levelConfigurations = new();

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

        private void LoadLevelConfigurations()
        {
            _levelConfigurations = _resourcesProvider
                .LoadResources<LevelConfiguration>(ResourceNames.Levels)
                .ToList();
        }

        public void Dispose()
        {
            _levelConfigurations.Clear();
        }
    }
}