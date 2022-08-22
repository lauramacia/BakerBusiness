using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace baker_biz.BakeryItems
{
    public class Inventory
    {
        Dictionary<String, double> ingredientQuantities;

        public Dictionary<String, double> IngredientQuantities
        {
            get
            {
                return ingredientQuantities;
            }
        }

        public Inventory()
        {
            ingredientQuantities = new Dictionary<String, double>();
        }

        public void CreateEmptyInventory(List<Ingredient> ingredients)
        {
            foreach (Ingredient ingredient in ingredients)
            {
                ingredientQuantities[ingredient.Name] = 0;
            }            
        }

        public void UpdateInventoryQuantitiesFromConsole(List<Ingredient> ingredients)
        {
            foreach (Ingredient ingredient in ingredients)
            {
                ingredientQuantities[ingredient.Name] = ingredient.ConsoleRequestQuantity();
            }
        }

        public void UpdateInventoryQuantitiesFromMap(Dictionary<string, double> quants)
        {
            foreach (string ingredient in quants.Keys)
            {
                ingredientQuantities[ingredient] = quants[ingredient];
            }
        }

        public void Print(List<Ingredient> ingredients)
        {
            foreach (Ingredient ingredient in ingredients)
            {
                ingredient.PrintIngredientQuantity((double)ingredientQuantities[ingredient.Name]);
            }
        }
    }
}
