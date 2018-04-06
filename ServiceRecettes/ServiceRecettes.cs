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
        private static Dictionary<int, List<Recette>> selections = new Dictionary<int, List<Recette>>();
        private static Dictionary<int, List<Recette>> lastSearches = new Dictionary<int, List<Recette>>();
        
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

        public void CloseConnexion(int clientID)
        {
            lastSearches.Remove(clientID);
            selections.Remove(clientID);
        }

        public List<Recette> getRecipeByIngredientName(string name, int clientId)
        {
            List<Recette> results = new List<Recette>();
            foreach (Recette r in Recettes)
            {
                if (r.ContainsIngredient(name))
                {
                    results.Add(r);
                }
            }

            lastSearches.Remove(clientId);
            lastSearches.Add(clientId, results);

            return results;
        }


        public Recette getRecipeByName(String name, int clientId)
        {
            foreach (Recette r in Recettes)
            {
                if (r.Nom.ToLower().Contains(name.ToLower()))
                {
                    lastSearches.Remove(clientId);
                    lastSearches.Add(clientId, new List<Recette>()
                    {
                        r
                    });
                    return r;
                }
            }
            return null;
        }

        private Recette getRecipeByName(String name)
        {
            foreach (Recette r in Recettes)
            {
                if (r.Nom.ToLower().Contains(name.ToLower()))
                {
                    return r;
                }
            }
            return null;
        }

        public List<Recette> getRecipes(int clientID)
        {
            lastSearches.Remove(clientID);
            lastSearches.Add(clientID, recettes);

            return Recettes;
        }


        public List<Recette> GetSelection(int clientId)
        {
            List<Recette> results = new List<Recette>();

            selections.TryGetValue(clientId, out results);

            return results;
            
        }

        public int OpenConnexion()
        {
            int clientsNb = selections.Count;
            selections.Add(clientsNb, new List<Recette>());
            lastSearches.Add(clientsNb, new List<Recette>());
            return clientsNb;
        }

        public bool RemoveFromCurrentSelection(string name, int clientId)
        {
            if (selections.TryGetValue(clientId, out List<Recette> results))
            {
                foreach (Recette r in results)
                {
                    if (name.Equals(r.Nom))
                    {
                        results.Remove(r);
                        return true;
                    }
                }
            }

            return false;
           
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

        public void SaveCurrentSelection(int clientId)
        {
            List<Recette> last = new List<Recette>();
            List<Recette> selected = new List<Recette>();

            lastSearches.TryGetValue(clientId, out last);

            selections.Remove(clientId);
            selections.Add(clientId, last);
        }
    }
}
