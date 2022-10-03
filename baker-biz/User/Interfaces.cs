using baker_biz.IngredientsManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace baker_biz.User
{
    public interface IUserInputOutput
    {
       public double GetAvailableIngredientQuantity(QuantityIngredient ingredient);
       public void OutputAvailableIngredientQuantity(QuantityIngredient ingredient);
       public void OutputBakeryItemMaxs(int withOpt, int noOpt, string name);
       public void OutputLeftoverQuantities(Inventory inventory);
       public void OutputCalculateOrQuit();
       public bool UserIsQuitting();
       public void ProcessOutMessage(string message);
    }      
}
