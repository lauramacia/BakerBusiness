using baker_biz.BakeryItems;
using baker_biz.IngredientsManager;
using baker_biz.User;
using System;
using System.Collections.Generic;

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
            ConsoleUser user = new();

            do
            {               
                BakeryItem applePie = new(BakeryItems.BakeryItems.ApplePie, user);
                List<QuantityIngredient> allIngredients = utils.DeepCopyQuantityIngredientList(applePie.GetAllIngredients());
                Inventory applePieInventory = new(allIngredients, user, true);

                applePieInventory.UpdateAvailableInventoryQuantitiesFromUser();

                int maxWithOptional = 0;
                if (applePie.OptionalIngredients.Count > 0 )
                {
                    maxWithOptional = applePie.MaxBakeryItemsWithOptional(applePieInventory);
                    applePie.CalculateBakeryItemLeftovers(ref applePieInventory, maxWithOptional, true);
                }

                int maxBasicOnly = applePie.MaxBakeryItemsBaseIngredients(applePieInventory);
                applePie.CalculateBakeryItemLeftovers(ref applePieInventory, maxBasicOnly);

                //applePie.RunBakeryItemCalculations(ref applePieInventory);

                applePie.OutputBakeryItemMaxs(maxWithOptional, maxBasicOnly);

                applePie.OutputLeftoverQuantities(applePieInventory);

                user.OutputCalculateOrQuit();
            } while (!user.UserIsQuitting());

        }       
    }

    


}
