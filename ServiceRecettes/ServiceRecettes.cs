//using Shared;
using System;
using System.Collections.Generic;
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



        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }
    }
}
