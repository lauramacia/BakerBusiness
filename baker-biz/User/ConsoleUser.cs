using baker_biz.IngredientsManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace baker_biz.User
{
    public class ConsoleUser : IUserInputOutput
    {
        public ConsoleUser() {}        

        public double GetAvailableIngredientQuantity(QuantityIngredient ingredient)
        {
            string request = getRequest(ingredient);
            ProcessOutMessage(request);
            return getIntFromUser();
            
        }

        public void OutputAvailableIngredientQuantity(QuantityIngredient ingredient)
        {
            string printMessage = getPrintQuantityIngredientMessage(ingredient);
            ProcessOutMessage(printMessage);

        }

        public void OutputBakeryItemMaxs(int withOpt, int noOpt, string name)
        {
            string message = "You can make " + withOpt + " " + name + "(s) with optional ingredients, " + Environment.NewLine +
                "and " + noOpt + " " + name + " without optional ingredients.\n";
            ProcessOutMessage(message);
        }

        public void OutputLeftoverQuantities(Inventory inventory)
        {
            ProcessOutMessage("Leftover quantities:\n");
            inventory.Print();
        }

        public void OutputCalculateOrQuit()
        {
            ProcessOutMessage("\n\nEnter to calculate, 'q' to quit!");
        }

        public void ProcessOutMessage(string message)
        {
            Console.WriteLine(message + Environment.NewLine);
        }

        public bool UserIsQuitting()
        {
            return string.Equals(Console.ReadLine()?.ToLower(), "q");
        }

        private string getRequest (QuantityIngredient ingredient)
        {
            string request;
                        
            if (ingredient.Unit.Unit != MeasureUnits.None)
            {
                request = "How many " + ingredient.GetUnitString() + " of " + ingredient.Name + " do you have?";
            } else
            { 
                request = "How many " + ingredient.Name + " do you have?";
            }

            return request;
        }

        private double getIntFromUser()
        {
            string readLine = Console.ReadLine() ?? "";
            double readDouble;

            while (!double.TryParse(readLine, out readDouble) || readDouble < 0)
            {
                Console.WriteLine("Amount must be zero or a positive number, please try again.");
                readLine = Console.ReadLine() ?? "";
            }

            return readDouble;
        }

        private string getPrintQuantityIngredientMessage(QuantityIngredient ingredient)
        {
            string msg;
            if (ingredient.Unit.Unit != MeasureUnits.None)
            {
                msg = ingredient.Quantity + " " + ingredient.GetUnitString() + " " + ingredient.Name;
            }
            else
            {
                msg = ingredient.Quantity + ingredient.GetUnitString() + " " + ingredient.GetUnitString() + " " + ingredient.Name;
            }

            return msg;
        }
    }
}
