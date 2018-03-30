using System;
using System.Collections.Generic;
using Shared;
using Shared.Classes;

namespace ServiceRecettes
{

    public class ServiceRecettes : IService
    {
        private static List<Recette> recettes = new List<Recette>();
        private static List<int> sessionsIds = new List<int>();

        static ServiceRecettes()
        {
            
            Ingredient sucre = new Ingredient("Sucre");
            Ingredient farine = new Ingredient("Farine");
            Ingredient oeuf = new Ingredient("Oeuf");
            Ingredient chocolat = new Ingredient("Chocolat");
            Ingredient beurre = new Ingredient("Beurre");

            List<Ingredient> ingredientsCrepes = new List<Ingredient>
            {
                sucre,
                farine,
                oeuf
            };

            List<Ingredient> ingredientsGateau = new List<Ingredient>(ingredientsCrepes)
            {
                chocolat,
                beurre
            };

            Recette crepes = new Recette("Crepes", ingredientsCrepes);
            Recette gateau = new Recette("Gateau au chocolat", ingredientsGateau);

            Recettes.Add(crepes);
            Recettes.Add(gateau);

        }

        public static List<Recette> Recettes { get => recettes; set => recettes = value; }


        public bool AddIngredient(string recipeName, Ingredient ingredient)
        {
            Recette recipe = getRecipeByName(recipeName);

            if (recipe != null)
            {
                recipe.AddIngredient(ingredient);
                return true;
            }
            else return false;
        }

        public bool AddRecipe(Recette recette)
        {

;            if (getRecipeByName(recette.Nom) == null)
            {
                Recettes.Add(recette);
                return true;
            }
            else return false;
            
        }

        public Recette getRecipeByName(String name)
        {
            foreach (Recette r in Recettes)
            {
                if(r.Nom.ToLower().Contains(name.ToLower()))
                {
                    return r;
                }
            }
            return null;
        }

        public List<Recette> getRecipes()
        {
            return Recettes;
        }

        public bool RemoveRecipe(string recipeName)
        {
            Recette toRemove = getRecipeByName(recipeName);

            if (toRemove == null)
            {
                return false;
            }
            else
            {
                Recettes.Remove(toRemove);
                return true;
            }
            
        }

        public bool ReplaceIngredient(string recipeName, Ingredient ingredient, Ingredient replacement)
        {
            Recette recipe = getRecipeByName(recipeName);

            if (recipe != null 
                && recipe.Ingredients.Contains(ingredient) 
                && !recipe.Ingredients.Contains(replacement)
                )
            {
                recipe.Ingredients.Remove(ingredient);
                recipe.Ingredients.Add(replacement);
                return true;
            }
            else return false;
        }
    }
}
