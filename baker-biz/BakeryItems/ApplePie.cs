using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using baker_biz.BakeryItems;

namespace baker_biz
{
    public class ApplePie
    {
        private readonly Apple apples = new(3);
        private readonly Sugar sugar = new(2);
        private readonly Flour flour = new(1);
        private readonly Cinnamon cinnamon = new(1, true);
        private readonly Butter butter = new(0.75);
        private readonly Inventory availableInventory = new();
        private readonly List<Ingredient> recipeIngredients = new();
        private int maxAllIngredients;
        private int maxNoOptional;
        

        public ApplePie()
        {
            recipeIngredients = new List<Ingredient>()
            {
                apples, sugar, flour, cinnamon, butter
            };

            availableInventory.CreateEmptyInventory(recipeIngredients);
        }

        public int MaxAllIngredients
        {
            get
            {
                return maxAllIngredients;
            }            
        }

        public int MaxNoOptional
        {
            get
            {
                return maxNoOptional;
            }
        }

        public Inventory AvailableInventory
        {
            get
            {
                return availableInventory;
            }            
        }                

        public List<Ingredient> RecipeIngredients
        {
            get
            {
                return recipeIngredients;
            }
        }

         public void PrintApplePieMaxs()
        {
            Console.WriteLine("You can make " + maxAllIngredients + " cinnamon apple pie(s), ");
            Console.WriteLine("and " + maxNoOptional + " apple pie(s) without cinnamon.\n");
        }                

        public void RunApplePiesCalculations()
        {            
            bool withOptional = availableInventory.IngredientQuantities[cinnamon.Name] > 0;

            if (withOptional)
            {
                maxAllIngredients = MaxApplePies(withOptional);
                CalculateApplePieLeftovers(MaxAllIngredients, withOptional);
            }

            maxNoOptional = MaxApplePies();            
            CalculateApplePieLeftovers(MaxNoOptional);

            return;
        }

        public int MaxApplePies(bool withOptional = false)
        {
            List<double> maxsList = new List<double>();

            foreach (Ingredient ingredient in recipeIngredients)
            {
                if (!ingredient.IsOptional || withOptional)
                {
                    maxsList.Add(availableInventory.IngredientQuantities[ingredient.Name] / ingredient.RequiredAmount);
                } 
            };

            return (int)maxsList.Min();
        }

        public void CalculateApplePieLeftovers(int pies, bool withOptional = false)
        {
            foreach (Ingredient ingredient in recipeIngredients)
            {
                if (!ingredient.IsOptional || withOptional)
                availableInventory.IngredientQuantities[ingredient.Name] -= pies * ingredient.RequiredAmount;
            }                                       
        }
    }
}
