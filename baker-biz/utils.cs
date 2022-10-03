using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using baker_biz.BakeryItems;
using baker_biz.IngredientsManager;

namespace baker_biz
{
    internal class utils
    {
        

        //public static int RequestAvailableQuantity(String request)
        //{
        //    Console.WriteLine(request);
        //    return utils.GetValidatedPositiveInt();
        //}

        public static List<QuantityIngredient> DeepCopyQuantityIngredientList(List<QuantityIngredient> originalList)
        {
            List<QuantityIngredient> returnList = new List<QuantityIngredient>();
            foreach (QuantityIngredient ing in originalList)
            {
                QuantityIngredient newIng = new QuantityIngredient(ing.BaseIngredient, ing.Unit, ing.Modifier);
                returnList.Add(newIng);
            }

            return returnList;
        }
    }
}
