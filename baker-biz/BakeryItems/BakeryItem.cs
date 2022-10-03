using baker_biz.IngredientsManager;
using baker_biz.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace baker_biz.BakeryItems
{
    public enum BakeryItems
    {
        ApplePie,
        BlueberryCobbler
    }

    public class BakeryItem
    {
        private readonly BakeryItems bakeryItem;
        private readonly string name;
        private List<QuantityIngredient> baseIngredients = new List<QuantityIngredient>();
        private List<QuantityIngredient> optionalIngredients = new List<QuantityIngredient>();
        private readonly IUserInputOutput user;
        //private int maxAllIngredients;
        //private int maxNoOptional;


        public BakeryItem(BakeryItems item, IUserInputOutput user)
        {
            bakeryItem = item;
            this.name = setName();
            setRecipeIngredients();
            this.user = user;            
        }

        public string Name
        {
            get => name;
        }

        private string setName()
        {
            switch (bakeryItem)
            {
                case BakeryItems.ApplePie:
                    {
                        return "apple pie";
                    }
                case BakeryItems.BlueberryCobbler:
                    {
                        return "blueberrie cobbler";
                    }                
                default:
                    {
                        //error
                        return "error";
                    }
            }
        }

        private void setRecipeIngredients()
        {
            switch (bakeryItem)
            {
                case BakeryItems.ApplePie:
                    {
                        createApplePieIngredients();
                        break;
                    }
                case BakeryItems.BlueberryCobbler:
                    {
                        createBlueberryCobblerIngredients();
                        break;
                    }
                default:
                    {
                        //error
                        break;
                    }
            }
        }

        private void createApplePieIngredients()
        {            
            baseIngredients = new List<QuantityIngredient>
            {
                new QuantityIngredient(Ingredients.Apple, null, quantity: 3),
                new QuantityIngredient(Ingredients.Flour, new MeasureUnit(MeasureUnits.Pound), quantity: 1),
                new QuantityIngredient(Ingredients.Sugar, new MeasureUnit(MeasureUnits.Pound), quantity: 2),
                new QuantityIngredient(Ingredients.Butter, new MeasureUnit(MeasureUnits.Tbs), quantity: 6),
            };

            optionalIngredients = new List<QuantityIngredient>
            {
                new QuantityIngredient(Ingredients.Cinnamon, new MeasureUnit(MeasureUnits.Tsp), quantity: 1)
            };
        }

        private void createBlueberryCobblerIngredients()
        {
            baseIngredients = new List<QuantityIngredient>
            {
                new QuantityIngredient(Ingredients.Blueberries, new MeasureUnit(MeasureUnits.Cup), modifier: "fresh or frozen", quantity: 4),
                new QuantityIngredient(Ingredients.LemonZest, null, quantity: 1),
                new QuantityIngredient(Ingredients.Sugar, new MeasureUnit(MeasureUnits.Pound), modifier: "granulated", quantity: 2),
                new QuantityIngredient(Ingredients.Butter, new MeasureUnit(MeasureUnits.Tbs), quantity: 5),
                new QuantityIngredient(Ingredients.Flour, new MeasureUnit(MeasureUnits.Cup), modifier: "all-purpose", quantity: 1),
                new QuantityIngredient(Ingredients.Milk, new MeasureUnit(MeasureUnits.Cup), quantity: 1),
                new QuantityIngredient(Ingredients.Cinnamon, new MeasureUnit(MeasureUnits.Tsp), quantity: 1),
            };            
        }        

        public List<QuantityIngredient> BaseIngredients
        {
            get => baseIngredients;
            set => baseIngredients = value;            
        }

        public List<QuantityIngredient> OptionalIngredients
        {
            get => optionalIngredients;
            set => optionalIngredients = value;
        }

        public List<QuantityIngredient> GetAllIngredients()
        {
            List<QuantityIngredient> allIngredients = new List<QuantityIngredient>();

            allIngredients.AddRange(BaseIngredients);
            allIngredients.AddRange(OptionalIngredients);

            return allIngredients;
        }

        public void OutputBakeryItemMaxs(int withOpt, int noOpt)
        {
            user.OutputBakeryItemMaxs(withOpt, noOpt, name);
        }

        public void OutputLeftoverQuantities(Inventory inventory)
        {
            user.OutputLeftoverQuantities(inventory);
        }        

        public int MaxBakeryItemsWithOptional(Inventory inventory)
        {
            List<double> maxsList = new List<double>();

            foreach (QuantityIngredient recipeIngredient in optionalIngredients)
            {
                if (recipeIngredient.Quantity != 0)
                {
                    foreach (QuantityIngredient inventoryIngredient in inventory.IngredientQuantities)
                    {
                        if (recipeIngredient.DeepEquals(inventoryIngredient))
                        {
                            maxsList.Add(inventoryIngredient.Quantity / recipeIngredient.Quantity);
                            break;
                        }
                    }
                }
            };

            maxsList.Add(MaxBakeryItemsBaseIngredients(inventory));

            return (int)maxsList.Min();
        }

        public int MaxBakeryItemsBaseIngredients(Inventory inventory)
        {
            List<double> maxsList = new List<double>();

            foreach (QuantityIngredient recipeIngredient in baseIngredients)
            {
                if (recipeIngredient.Quantity != 0)
                {
                    foreach (QuantityIngredient inventoryIngredient in inventory.IngredientQuantities)
                    {
                        if (recipeIngredient.DeepEquals(inventoryIngredient))
                        {
                            maxsList.Add(inventoryIngredient.Quantity / recipeIngredient.Quantity);
                            break;
                        }
                    }                    
                }
            };

            return (int)maxsList.Min();
        }

        public void CalculateBakeryItemLeftovers(ref Inventory inventory, int qtyBaked, bool withOptional = false)
        {
            foreach (QuantityIngredient recipeIngredient in baseIngredients)
            {
                foreach (QuantityIngredient inventoryIngredient in inventory.IngredientQuantities)
                {
                    if (recipeIngredient.DeepEquals(inventoryIngredient))
                    {
                        inventoryIngredient.Quantity -= qtyBaked * recipeIngredient.Quantity;
                        break;
                    }
                }                
            }

            if (withOptional)
            {
                foreach (QuantityIngredient recipeIngredient in optionalIngredients)
                {
                    foreach (QuantityIngredient inventoryIngredient in inventory.IngredientQuantities)
                    {
                        if (recipeIngredient.DeepEquals(inventoryIngredient))
                        {
                            inventoryIngredient.Quantity -= qtyBaked * recipeIngredient.Quantity;
                            break;
                        }
                    }
                }
            }
        }
    }
}
