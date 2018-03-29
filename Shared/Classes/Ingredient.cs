using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Classes
{
    [DataContract]
    public class Ingredient
    {

        private String nom;

        [DataMember]
        public string Nom
        {
            get { return nom; }
            set { nom = value; }
        }



        public Ingredient(String n)
        {
            nom = n;
        }

        public override String ToString()
        {
            return nom;
        }
    }
}
