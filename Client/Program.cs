//using ServiceRecettes;
using Shared;
using ServiceRecettes;
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
        private static int id;
        private static ServiceRecettesReference.IService serviceProxy = new ChannelFactory<ServiceRecettesReference.IService>("BasicHttpBinding_IService").CreateChannel();
        static void Main(string[] args)
        {

            id = serviceProxy.OpenConnexion();
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
                        serviceProxy.CloseConnexion(id);
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
                    case 7: // recipes by ingredient
                        GetRecipeByIngredientName();
                        break;
                    case 8: //save in current selection
                        SaveCurrentSelection();
                        break;
                    case 9: // print current selection
                        PrintCurrentSelection();
                        break;
                    case 10:    // remove name from current selection
                        RemoveFromCurrentSelection();
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

        private static void RemoveFromCurrentSelection()
        {
            Console.WriteLine("Supprimer quelle recettte de la selection courante ? ");
            String name = Console.ReadLine();

            bool status = serviceProxy.RemoveFromCurrentSelection(name, id);
            if( status )
            {
                Console.WriteLine("Element supprime");
            }else Console.WriteLine("Erreur : l'element n'existe pas dans la selection");
        }

        private static void PrintCurrentSelection()
        {
            Recette[] selection = serviceProxy.GetSelection(id);

            if(selection.Length > 0){
                Console.WriteLine("Recettes selectionnees : ");
                foreach(Recette r in selection)
                {
                    Console.WriteLine(r);
                }
            }else
            {
                Console.WriteLine("La selection est vide");
            }                
        }

        private static void SaveCurrentSelection()
        {
            serviceProxy.SaveCurrentSelection(id);
        }

        private static void GetRecipeByIngredientName()
        {
            Console.WriteLine("Le nom de l'ingrédient doit être ? ");
            String ingredientName = Console.ReadLine();

            Recette[] recettes = serviceProxy.getRecipeByIngredientName(ingredientName, id);
            if (recettes != null)
            {
                foreach(Recette r in recettes)
                    Console.WriteLine(r);
            }
            else
            {
                Console.WriteLine("Aucune recette correspondante");
            }
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

            Recette r = serviceProxy.getRecipeByName(recipeName, id);
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
            foreach(Recette r in serviceProxy.getRecipes(id))
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
            Console.WriteLine("[3] - Ajouter une recette dans la base");
            Console.WriteLine("[4] - Supprimer une recette de la base");
            Console.WriteLine("[5] - Ajouter un ingrédient à une recette");
            Console.WriteLine("[6] - Remplacer un ingrédient dans une recette");
            Console.WriteLine("[7] - Trouver les recettes comportant un certain ingredient");
            Console.WriteLine("\n--------------  Selection courante  --------------");
            Console.WriteLine("[ 8] - Sauver la dernière recherche dans la selection courante");
            Console.WriteLine("[ 9] - Afficher la selection courante");
            Console.WriteLine("[10] - Supprimer une recette de la selection courante");
            Console.WriteLine("\n--------------------------------------------------");
            Console.WriteLine("[0] - Fermer ce programme");
            Console.WriteLine();
            Console.Write("Choix : ");
        }
    }
}
