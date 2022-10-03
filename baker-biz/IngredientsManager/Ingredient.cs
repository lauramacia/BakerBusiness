namespace baker_biz.IngredientsManager
{
    public enum Ingredients
    {
        Apple,
        Sugar,
        Flour,
        Cinnamon,
        Butter,
        Blueberries,
        Milk,
        LemonZest
    }

    public class Ingredient
    {
        private Ingredients baseIngredient;
        private string name;
        private string modifier = "";
        private MeasureUnit defaultUnit;
       
        public Ingredient(Ingredients ingredient)
        {
            this.baseIngredient = ingredient;
            this.name = setName();
            this.defaultUnit = setDefaultUnit();            
        }

        public Ingredient(Ingredients ingredient, string modifier)
        {
            this.baseIngredient = ingredient;
            this.modifier = modifier;
            this.name = setNameWithModifier(modifier);
            this.defaultUnit = setDefaultUnit();
        }


        private string setName()
        {
            switch (baseIngredient)
            {
                case Ingredients.Apple:
                    {
                        return "apple(s)";
                    }
                case Ingredients.Blueberries:
                    {
                        return "blueberries";
                    }
                case Ingredients.Sugar:
                    {
                        return "sugar";
                    }
                case Ingredients.Flour:
                    {
                        return "flour";
                    }
                case Ingredients.Cinnamon:
                    {
                        return "cinnamon";
                    }
                case Ingredients.Butter:
                    {
                        return "butter";
                    }
                case Ingredients.Milk:
                    {
                        return "milk";
                    }
                case Ingredients.LemonZest:
                    {
                        return "lemon zest";
                    }
                default:
                    {
                        //error
                        return "error";                        
                    }
            }
        }

        public string setNameWithModifier(string modifier)
        {            
            if (string.IsNullOrEmpty(modifier)) {
                return setName();                
            }

            switch (baseIngredient)
            {
                case Ingredients.Apple:
                    {
                        return modifier + " apple(s)";
                    }
                case Ingredients.Blueberries:
                    {
                        return modifier + " blueberries";
                    }
                case Ingredients.Sugar:
                    {
                        return modifier + " sugar";
                    }
                case Ingredients.Flour:
                    {
                        return modifier + " flour";
                    }
                case Ingredients.Cinnamon:
                    {
                        return modifier + " cinnamon";
                    }
                case Ingredients.Butter:
                    {
                        return modifier + " butter";
                    }
                default:
                    {
                        //error
                        return "error";
                    }
            }
        }

        private MeasureUnit setDefaultUnit()
        {
            switch (baseIngredient)
            {
                case Ingredients.Apple:
                    {
                        return new MeasureUnit(MeasureUnits.None);
                    }
                case Ingredients.Blueberries:
                    {
                        return new MeasureUnit(MeasureUnits.Cup);
                    }
                case Ingredients.Sugar:
                    {
                        return new MeasureUnit(MeasureUnits.Cup);
                    }
                case Ingredients.Flour:
                    {
                        return new MeasureUnit(MeasureUnits.Cup);
                    }
                case Ingredients.Cinnamon:
                    {
                        return new MeasureUnit(MeasureUnits.Tsp);
                    }
                case Ingredients.Butter:
                    {
                        return new MeasureUnit(MeasureUnits.Tbs);
                    }
                case Ingredients.Milk:
                    {
                        return new MeasureUnit(MeasureUnits.Cup);
                    }
                case Ingredients.LemonZest:
                    {
                        return new MeasureUnit(MeasureUnits.None);
                    }
                default:
                    {
                        //error
                        return new MeasureUnit(MeasureUnits.None);
                    }
            }
        }


        public string Name
        {
            get => name;
            set => name = value;
        }

        public string Modifier
        {
            get => modifier;
            set => modifier = value;
        }

        public Ingredients BaseIngredient
        {
            get => baseIngredient;
            set => baseIngredient = value;
        }

        public MeasureUnit DefaultUnit
        {
            get => defaultUnit;
            set => defaultUnit = value;
        }

        public bool DeepEquals(Ingredient other)
        {
            return name == other.Name;
        }

        public bool Equals(Ingredient other)
        {
            return baseIngredient == other.baseIngredient;
        }

        public bool IsSameIngredientAndModifier(Ingredients otherIngredient, string otherModifier = "")
        {
            return baseIngredient == otherIngredient && modifier.Equals(otherModifier);
        }

        public bool IsSameIngredient(Ingredients ing)
        {
            return baseIngredient == ing;
        }
    }

    public class QuantityIngredient : Ingredient
    {
        private MeasureUnit unit;
        private double quantity;

       public QuantityIngredient(Ingredients ingredient, MeasureUnit? unit, string modifier = "", double quantity = 0)
            : base(ingredient, modifier)
        {
            if (unit != null)
            {
                this.unit = unit;
            } else
            {
                this.unit = base.DefaultUnit;
            }
            
            this.quantity = quantity;
        }                

        public MeasureUnit Unit
        {
            get => unit;
            set => unit = value;
        }

        public double Quantity
        {
            get => quantity;
            set => quantity = value;
        }        

        public string GetUnitString()
        {
            return unit.ToString();
        }

        public bool IsSameIngredientAndUnit(QuantityIngredient ing)
        {
            return base.DeepEquals(ing) && unit == ing.Unit;
        }


        public void UpdateIngredientQuantity(double qty)
        {
            quantity = qty;
        }

        public void UpdateIngredientQuantity(MeasureUnit newUnit, double qty)
        {
            unit = newUnit;
            quantity = qty;
        }
    }
}

