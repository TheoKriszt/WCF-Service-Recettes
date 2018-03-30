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

        public override bool Equals(object obj)
        {
            return Nom.Equals((obj as Ingredient).Nom);
        }

        public override int GetHashCode()
        {
            var hashCode = 142165330;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(nom);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Nom);
            return hashCode;
        }
    }
}
