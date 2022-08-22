using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace baker_biz
{    
    public class Ingredient
    {
        public const string Apple = "apple";
        public const string Sugar = "sugar";
        public const string Flour = "flour";
        public const string Cinnamon = "cinnamon";
        public const string Butter = "butter";

        private readonly String name;
        private readonly String measure;
        private double requiredAmount;
        private bool isOptional;

        public Ingredient(String name, String measure) 
        {
            this.name = name;
            this.measure = measure;
        }        

        public String Name
        {
            get => name;
        }

        public double RequiredAmount
        {
            get => requiredAmount;
            set => requiredAmount = value;
        }

        public bool IsOptional
        {
            get => isOptional;
            set => isOptional = value;
        }

        public virtual int ConsoleRequestQuantity()
        {
            Console.WriteLine("How many " + measure + " of " + name + " do you have?");
            return utils.GetValidatedPositiveInt();
        }        

        public virtual void PrintIngredientQuantity(double q)
        {
            Console.WriteLine(q + " " + name + " " + measure);
        }
    }
    
    public class Apple : Ingredient
    {
        public Apple(double requiredAmount, bool isOptional = false)
            : base(Apple, "")
        {
            this.RequiredAmount = requiredAmount;
            this.IsOptional = isOptional;
        }

        public override int ConsoleRequestQuantity()
        {
            Console.WriteLine("How many apples do you have?");
            return utils.GetValidatedPositiveInt();
        }

        public override void PrintIngredientQuantity(double q)
        {
            Console.WriteLine(q + " apple(s)");
        }
    }

    public class Sugar : Ingredient
    {
        public Sugar(double requiredAmount, bool isOptional = false)
            : base(Sugar, "pound(s) (lbs)")
        {
            this.RequiredAmount = requiredAmount;
            this.IsOptional = isOptional;
        }
    }

    public class Flour : Ingredient
    {
        public Flour(double requiredAmount, bool isOptional = false)
            : base(Flour, "pound(s) (lbs)")
        {
            this.RequiredAmount = requiredAmount;
            this.IsOptional = isOptional;
        }
    }

    public class Cinnamon : Ingredient
    {
        public Cinnamon(double requiredAmount, bool isOptional = false)
            : base(Cinnamon, "teaspoon(s) (tsp)")
        {
            this.RequiredAmount = requiredAmount;
            this.IsOptional = isOptional;
        }
    }

    public class Butter : Ingredient
    {
        public Butter(double requiredAmount, bool isOptional = false)
            : base(Butter, "stick(s)")
        {
            this.RequiredAmount = requiredAmount;
            this.IsOptional = isOptional;
        }
    }   
}

