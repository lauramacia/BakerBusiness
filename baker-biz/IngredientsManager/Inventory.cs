using baker_biz.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace baker_biz.IngredientsManager
{
    public class Inventory
    {
        private List<QuantityIngredient> ingredientQuantities;
        private IUserInputOutput user;

        public List<QuantityIngredient> IngredientQuantities
        {
            get
            {
                return ingredientQuantities;
            }
        }

        public Inventory(List<QuantityIngredient> ingredients, IUserInputOutput user, bool setEmpty = false)
        {
            this.user = user;
            ingredientQuantities = ingredients;
            if (setEmpty)
            {
                foreach (QuantityIngredient ingredient in ingredients)
                {
                    ingredient.Quantity = 0;
                }
            }
        }        

        public void UpdateOrAddInventoryItem(Ingredients ingredient, MeasureUnits newUnit, string modifier = "", double newQuantity = 0)
        {
            MeasureUnit newMeasureUnit = new MeasureUnit(newUnit);
            //if exists, update
            foreach (QuantityIngredient ing in ingredientQuantities)
            {
                if (ing.BaseIngredient == ingredient && ing.Modifier.Equals(modifier))
                {
                    ing.Unit = newMeasureUnit;
                    ing.Quantity = newQuantity;
                    return;
                }
            }

            //if not exists, add
            ingredientQuantities.Add(new QuantityIngredient(ingredient, newMeasureUnit, modifier, newQuantity));            
        }

        public void AddInventoryItem(QuantityIngredient ingredient)
        {
            //if exists, return already exists
            foreach (QuantityIngredient ing in ingredientQuantities)
            {
                if (ing.DeepEquals(ingredient))
                {
                    // error  : not added already exists          
                    return;
                }
            }

            //if not exists, add with provided unit / qty
            ingredientQuantities.Add(ingredient);
        }

        //public void UpdateInventoryQuantity(QuantityIngredient ingredient, double newQty, MeasureUnits? newUnit)
        //{
        //    foreach (QuantityIngredient ing in ingredientQuantities)
        //    {
        //        if (ing.DeepEquals(ingredient))
        //        {
        //            ing.Quantity = newQty;
        //            if (newUnit != null)
        //            {
        //                ing.Unit = newUnit.Value;
        //            }
        //            return;
        //        }
        //    }


        //}

        //public void AddIngredientWithDefaultUnit(Ingredients ingredient, string modifier = "")
        //{
        //    if (!ingredientAlreadyInInventory(ingredient))
        //    {
        //        QuantityIngredient ing = new(ingredient, modifier, );
        //        QuantityIngredient qIng = new(ing, ing.DefaultUnit);
        //        ingredientQuantities[qIng] = 0;
        //    }
        //    else
        //    {
        //        //throw error to user: ingredient already exists in inventory, change quantity?
        //    }
        //}

        //public void SetIngredientQuanity(Ingredients ingredient, double qty, MeasureUnits? units = null)
        //{
        //    if (!ingredientAlreadyInInventory(ingredient))
        //    {
        //        Ingredient ing = new(ingredient);
        //        MeasureIngredient mIng;
        //        if (units == null)
        //        {
        //            mIng = new(ing, ing.DefaultUnit);
        //        }
        //        else
        //        {
        //            mIng = new(ing, units.Value);
        //        }
        //        ingredientQuantities[mIng] = qty;
        //    }
        //    else
        //    {
        //        //throw error to user: ingredient already exists in inventory, change quantity?
        //    }
        //}

        public void Print()
        {
           foreach (QuantityIngredient ing in ingredientQuantities)
            {
                user.OutputAvailableIngredientQuantity(ing);
            }
        }

        private bool ingredientAlreadyInInventory(Ingredients ingredient, string modifier = "")
        {
            foreach (QuantityIngredient ing in ingredientQuantities)
            {
                if (ing.IsSameIngredientAndModifier(ingredient))
                {
                    return true;
                }
            }

            return false;
        }

        public void UpdateAvailableInventoryQuantitiesFromUser()
        {
            foreach (QuantityIngredient ingredient in ingredientQuantities)
            {
                ingredient.Quantity = user.GetAvailableIngredientQuantity(ingredient);                                                
            }
        }        
    }
}
