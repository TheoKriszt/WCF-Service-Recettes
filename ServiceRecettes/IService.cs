//using System;
//using System.Collections.Generic;
//using System.Linq;
using Shared.Classes;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace ServiceRecettes

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

        /*
        [OperationContract]
        string GetData(int value);

        [OperationContract]
        CompositeType GetDataUsingDataContract(CompositeType composite);
        */

        // TODO: ajoutez vos opérations de service ici
    }
    /*
    // Todo : supprimer la classe CompositeType, deprecated, unused
    // Utilisez un contrat de données comme indiqué dans l'exemple ci-après pour ajouter les types composites aux opérations de service.
    // Vous pouvez ajouter des fichiers XSD au projet. Une fois le projet généré, vous pouvez utiliser directement les types de données qui y sont définis, avec l'espace de noms "ServiceRecettes.ContractType".
    [DataContract]
    public class CompositeType
    {
        bool boolValue = true;
        string stringValue = "Hello ";

        [DataMember]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }

    
    }*/
}
