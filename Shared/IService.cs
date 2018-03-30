using Shared.Classes;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace Shared

{
    [ServiceContract]
    public interface IService
    {
        [OperationContract]
        List<Recette> getRecipes();

        [OperationContract]
        Recette getRecipeByName(String name);

        [OperationContract]
        bool AddRecipe(Recette recette);

        [OperationContract]
        bool AddIngredient(String recipeName, Ingredient ingredient);

        [OperationContract]
        bool ReplaceIngredient(String recipeName, Ingredient ingredient, Ingredient replacement);

        [OperationContract]
        bool RemoveRecipe(String recipeName);

    }

}
