using System;

namespace baker_biz
{
    class Program
    {
        static void Main()
        {
            // want to maximize the number of apple pies we can make.
            // it takes 3 apples, 2 lbs of sugar and 1 pound of flour to make 1 apple pie
            // Requirement 1: add cinnamon(optional), 1 tsp per pie.Once cinnamon is exhausted, you just make pies without it
            // this is intended to run on .NET Core                       

            do
            {
                ApplePie applePie = new();  

                applePie.AvailableInventory.UpdateInventoryQuantitiesFromConsole(applePie.RecipeIngredients);

                applePie.RunApplePiesCalculations();

                applePie.PrintApplePieMaxs();

                Console.WriteLine("Leftover quantities:\n");
                applePie.AvailableInventory.Print(applePie.RecipeIngredients);

                Console.WriteLine("\n\nEnter to calculate, 'q' to quit!");
            } while (!string.Equals(Console.ReadLine()?.ToLower(), "q"));

        }       
    }

    


}
