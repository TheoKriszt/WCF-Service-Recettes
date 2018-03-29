using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Classes
{
    [DataContract]
    public class Recette
    {


        private String nom;
        private List<Ingredient> ingredients;

        [DataMember]
        public string Nom
        {
            get { return nom; }
            set { nom = value; }
        }

        [DataMember]
        public List<Ingredient> Ingredients
        {
            get { return ingredients; }
            set { ingredients = value; }
        }

        public Recette(String n)
        {
            nom = n;
            ingredients = new List<Ingredient>();
        }
        public Recette(String n, List<Ingredient> ing)
        {
            nom = n;
            ingredients = ing;
        }

        public void AddIngredient(Ingredient i)
        {
            ingredients.Add(i);
        }

        public override String ToString()
        {
            String ret = "Recette : " + nom + ", ingredients : ";
            
            foreach(Ingredient i in ingredients)
            {
                ret += i + ", ";
            }
            
            return ret;
        }
    }
}
