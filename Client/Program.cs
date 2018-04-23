//using ServiceRecettes;
using Shared;
//using ServiceRecettes;
using System;
//using System.Collections.Generic;
//using System.Linq;
using System.ServiceModel;
using System.Collections.Generic;
using Shared.Classes;
//using System.Text;
//using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        private static IService serviceProxy = new ChannelFactory<IService>("ServiceRecettes.ServiceRecettes").CreateChannel();
        //private static ServiceRecettesReference.IService serviceProxy = new ChannelFactory<ServiceRecettesReference.IService>("ServiceRecettes.ServiceRecettes").CreateChannel();
        static void Main(string[] args)
        {
            

            Console.WriteLine("[Client WCF  - Service Recettes]");
            

            bool process = true;

            while(process){
                PrintMenu();
                int choice = GetChoice();

                Console.WriteLine("\n");

                switch (choice)
                {
                    case 0:
                        process = false;
                        break;
                    case 1: // all recipes
                        GetRecipes();
                        break;
                    case 2: // recipe by name
                        GetRecipeByName();
                        break;
                    case 3: // add recipe
                        AddRecipe();
                        break;
                    case 4: // del recipe
                        RemoveRecipe();
                        break;
                    case 5: // add ingredient
                        AddIngredientToRecipe();
                        break;
                    case 6: // alter ingredient
                        ReplaceIngredientInRecipe();
                        break;
                    default:
                        Console.WriteLine("Choix invalide : " + choice);
                        break;
                }

                Console.WriteLine("\n");
                
            }



            Console.WriteLine("Appuyez sur une touche pour fermer le client");
            Console.ReadLine();
        }

        private static void ReplaceIngredientInRecipe()
        {
            Console.WriteLine("Le nom de la recette doit être ? ");
            String recipeName = Console.ReadLine();
            Console.WriteLine("Le nom de l'ingrédient à remplacer est ? ");
            String ingredientName = Console.ReadLine();
            Console.WriteLine("Le nom de l'ingrédient de remplacement est ? ");
            String replacementName = Console.ReadLine();

            if (serviceProxy.ReplaceIngredient(recipeName, new Ingredient(ingredientName), new Ingredient(replacementName)))
            {
                Console.WriteLine("Ingrédient remplacé");
            }else
            {
                Console.WriteLine("Erreur : la recette n'existe peut-être pas, ne contient pas de ce premier ingrédient, ou contient déjà du second ingrédient");
            }

        }

        private static void AddIngredientToRecipe()
        {
            Console.WriteLine("Le nom de la recette doit être ? ");
            String recipeName = Console.ReadLine();
            Console.WriteLine("Le nom de l'ingrédient est ? ");
            String ingredientName = Console.ReadLine();

            if (serviceProxy.AddIngredient(recipeName, new Ingredient(ingredientName)))
            {
                Console.WriteLine("Ingrédient ajouté avec succès");
            }
            else
            {
                Console.WriteLine("Erreur : la recette n'existe peut-être pas ou elle possède déjà cet ingrédient");
            }
        }

        private static void RemoveRecipe()
        {
            Console.WriteLine("Le nom de la recette doit être ? ");
            String recipeName = Console.ReadLine();

            if (serviceProxy.RemoveRecipe(recipeName))
            {
                Console.WriteLine("La recette a bien été supprimée");
            }else
            {
                Console.WriteLine("La recette n'a pas pu être supprimée (peut-être n'existait-t-elle pas ?)");
            }
        }

        private static void AddRecipe()
        {
            Console.WriteLine("Nom de la nouvelle recette : ");
            String recipeName = Console.ReadLine();
            Recette r = new Recette(recipeName);

            String ingredientName = "aucun";

            while (!ingredientName.Equals(""))
            {
                Console.WriteLine("Ajoutez un ingrédient (laisser vide pour terminer)");
                Console.WriteLine("Nom de l'ingredient : ");
                ingredientName = Console.ReadLine();
                r.AddIngredient(new Ingredient(ingredientName));
            }

            if (serviceProxy.AddRecipe(r))
            {
                Console.WriteLine("La recette suivante a été ajoutée aves succès : ");
                Console.WriteLine(r);
            }
            else
            {
                Console.WriteLine("Erreur : la recette n'a pas pu être ajoutée (peut-être existe-t-elle déjà ?)");
            }
        }

        private static void GetRecipeByName()
        {
            Console.WriteLine("Le nom de la recette doit être ? ");
            String recipeName = Console.ReadLine();

            Recette r = serviceProxy.getRecipeByName(recipeName);
            if (r != null)
            {
                Console.WriteLine(r);
            }
            else
            {
                Console.WriteLine("Aucune recette correspondante");
            }
            
        }

        private static void GetRecipes()
        {
            foreach(Recette r in serviceProxy.getRecipes())
            {
                Console.WriteLine(r);
            }
        }

        private static int GetChoice()
        {
            int choice = -1;

            while (choice < 0)
            {
                try
                {
                    choice = int.Parse(Console.ReadLine());
                }
                catch (FormatException)
                {
                    Console.WriteLine("Cette option n'est pas correcte");
                }
            }
            return choice;
        }

        static void PrintMenu()
        {
            Console.WriteLine("[1] - Lister toutes les recettes");
            Console.WriteLine("[2] - Trouver une recette par nom");
            Console.WriteLine("[3] - Ajouter une recette");
            Console.WriteLine("[4] - Supprimer une recette");
            Console.WriteLine("[5] - Ajouter un ingrédient à une recette");
            Console.WriteLine("[6] - Remplacer un ingrédient dans une recette");
            Console.WriteLine("[0] - Fermer ce programme");
            Console.WriteLine();
            Console.Write("Choix : ");
        }
    }
}
