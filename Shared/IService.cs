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
        List<Recette> getRecipes(int clientId);

        [OperationContract]
        Recette getRecipeByName(String name, int clientId);

        [OperationContract]
        List<Recette> getRecipeByIngredientName(String name, int clientId);

        [OperationContract]
        bool AddRecipe(Recette recette);

        [OperationContract]
        bool AddIngredient(String recipeName, Ingredient ingredient);

        [OperationContract]
        bool ReplaceIngredient(String recipeName, Ingredient ingredient, Ingredient replacement);

        [OperationContract]
        bool RemoveRecipe(String recipeName);

        [OperationContract]
        bool RemoveFromCurrentSelection(String name, int clientId);

        [OperationContract]
        List<Recette> GetSelection(int clientId);

        [OperationContract]
        void SaveCurrentSelection(int clientId);

        [OperationContract]
        int OpenConnexion();

        [OperationContract]
        void CloseConnexion(int clientId);

    }

}
