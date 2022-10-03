using System;

namespace baker_biz.IngredientsManager
{
    public enum MeasureUnits
    {
        Tbs,
        Tsp,
        Stick,
        Pound,
        Cup,
        None
    }

    public class MeasureUnit
    {
        private MeasureUnits? unit;

        public MeasureUnit(MeasureUnits unit)
        {
            this.unit = unit;
        }

        public override string ToString()
        {
            switch (unit)
            {
                case MeasureUnits.Tbs:
                    {
                        return "tablespoon(s)";
                    }
                case MeasureUnits.Tsp:
                    {
                        return "teaspoon(s)";
                    }
                case MeasureUnits.Stick:
                    {
                        return "stick(s)";
                    }
                case MeasureUnits.Pound:
                    {
                        return "pound(s)";
                    }
                case MeasureUnits.Cup:
                    {
                        return "cup(s)";
                    }
                case MeasureUnits.None:
                    {
                        return "";
                    }
                default:
                    {
                        return "";
                    }
            }
        }


        public MeasureUnits? Unit
        {
            get => unit;
            set => unit = value;
        }

        public double SticksToTbs(double sticks)
        {
            return sticks * 8;
        }

        public double TbsToSticks(double tbs)
        {            
            return tbs / 8;
        }


    }
}
