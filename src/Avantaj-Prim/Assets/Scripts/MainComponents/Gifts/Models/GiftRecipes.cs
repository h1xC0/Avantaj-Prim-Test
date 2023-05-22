using System.Collections.Generic;
using System.Linq;
using Services.ResourceProvider;
using UnityEngine;

namespace MainComponents.Gifts.Models
{
    [CreateAssetMenu(fileName = "GiftRecipes", menuName = "Constants/Gifts/GiftRecipes")]
    public class GiftRecipes : ScriptableObject, IResource
    {
        [SerializeField] private List<GiftCraftingRecipe> recipes;

        public Sprite GetGiftSpriteByRecipe(Gift giftConfig)
        {
            if (giftConfig.Box == null) return null;

            var recipesFound = GetRecipes(recipes, giftConfig.Box);
            
            if (giftConfig.Bow != null)
                recipesFound = GetRecipes(recipesFound, giftConfig.Bow);
            
            if (giftConfig.Ornament != null)
                recipesFound = GetRecipes(recipesFound, giftConfig.Ornament);

            return recipesFound.Any() ? recipesFound.FirstOrDefault()?.Result : null; // check if it doesnt gives error
        }

        public List<GiftCraftingRecipe> GetAvailableRecipes(GiftPartSO box, GiftPartSO[] availableParts)
        {
            var recipes = GetRecipes(box);
            var availableRecipes = FindAvailableRecipes(availableParts, recipes);
            
            return availableRecipes.Any() ? availableRecipes : null;
        }

        private List<GiftCraftingRecipe> FindAvailableRecipes(GiftPartSO[] availableParts, List<GiftCraftingRecipe> recipes)
        {
            var availableRecipes = new List<GiftCraftingRecipe>();
            var isRecipeAvailable = false;
            foreach (var recipe in recipes)
            {
                foreach (var part in recipe.GiftParts)
                {
                    isRecipeAvailable = availableParts.Contains(part);
                    if (isRecipeAvailable == false) break;
                }

                if (isRecipeAvailable) availableRecipes.Add(recipe);
            }

            return availableRecipes;
        }

        private List<GiftCraftingRecipe> GetRecipes(GiftPartSO part)
        {
            return recipes
                .Where(x => x.GiftParts.Contains(part) && x.GiftParts.Length > 1)
                .ToList();
        }

        private List<GiftCraftingRecipe> GetRecipes(List<GiftCraftingRecipe> from, GiftPartSO part)
        {
            return from
                .Where(x => x.GiftParts.Contains(part))
                .ToList();
        }
    }
}