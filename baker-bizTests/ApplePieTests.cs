using Microsoft.VisualStudio.TestTools.UnitTesting;
using baker_biz;
using System;
using System.Collections.Generic;
using System.IO;

namespace baker_biz.Tests
{
    [TestClass()]
    public class ApplePieTests
    {
        private ApplePie testApplePie = new();               

        private Dictionary<string, double> getQuantitiesDictionary(int apple, double sugar, double flour, double butter, double cinnamon = 0)
        {
            Dictionary<string, double> quantities = new();
            
            foreach (Ingredient ingredient in testApplePie.RecipeIngredients) {
                switch (ingredient.Name)
                {
                    case Ingredient.Apple:
                        quantities[ingredient.Name] = apple;
                        break;
                    case Ingredient.Sugar:
                        quantities[ingredient.Name] = sugar;
                        break;
                    case Ingredient.Flour:
                        quantities[ingredient.Name] = flour;
                        break;
                    case Ingredient.Butter:
                        quantities[ingredient.Name] = butter;
                        break;
                    case Ingredient.Cinnamon:
                        quantities[ingredient.Name] = cinnamon;
                        break;
                }
            }

            return quantities;
        }   

        [TestMethod()]
        [DataTestMethod]
        [DataRow(true)]
        [DataRow(false)]
        public void MaxApplePies_WithCinnamon_HardCoded_Test(bool withCinnamon)
        {
            // assemble
            int apples = 10;
            int sugar = 9;
            int flour = 8;
            int cinnamon = 1;
            int butter = 13;

            Dictionary<string, double>  quantitiesWithCinnamon = getQuantitiesDictionary(apples, sugar, flour, butter, cinnamon);
            testApplePie.AvailableInventory.UpdateInventoryQuantitiesFromMap(quantitiesWithCinnamon);

            //act
            int result = testApplePie.MaxApplePies(withCinnamon);

            //assert
            if (withCinnamon)
            {
                Assert.AreEqual(1, result);
            }
            else
            {
                Assert.AreEqual(3, result);
            }
        }

        [TestMethod()]
        public void CalculateApplePieLeftovers_HardCoded_Test()
        {
            // assemble
            int apples = 10;
            int sugar = 9;
            int flour = 8;
            int cinnamon = 2;
            int butter = 2;
            Dictionary<string, double> quantitiesWithCinnamon = getQuantitiesDictionary(apples, sugar, flour, butter, cinnamon);
            testApplePie.AvailableInventory.UpdateInventoryQuantitiesFromMap(quantitiesWithCinnamon);

            int pies = 2; // from cinnamon

            double test = testApplePie.AvailableInventory.IngredientQuantities[Ingredient.Apple];

            //act
            testApplePie.CalculateApplePieLeftovers(pies, true);

            //assert
            Assert.AreEqual(4, testApplePie.AvailableInventory.IngredientQuantities[Ingredient.Apple]); // 10 - 3*2
            Assert.AreEqual(5, testApplePie.AvailableInventory.IngredientQuantities[Ingredient.Sugar]); // 9 - 2*2
            Assert.AreEqual(6, testApplePie.AvailableInventory.IngredientQuantities[Ingredient.Flour]); // 8 - 1*2
            Assert.AreEqual(0, testApplePie.AvailableInventory.IngredientQuantities[Ingredient.Cinnamon]); // 2-1*2
            Assert.AreEqual(0.5, testApplePie.AvailableInventory.IngredientQuantities[Ingredient.Butter]); // 12-6*2
        }

        [TestMethod()]        
        public void RunApplePiesCalculations_Test()
        {
            // assemble
            int apples = 10;
            int sugar = 9;
            int flour = 8;
            int cinnamon = 1;
            int butter = 4;

            Dictionary<string, double> quantitiesWithCinnamon = getQuantitiesDictionary(apples, sugar, flour, butter, cinnamon);
            testApplePie.AvailableInventory.UpdateInventoryQuantitiesFromMap(quantitiesWithCinnamon);

            //act
            testApplePie.RunApplePiesCalculations();

            //assert
            Assert.AreEqual(1, testApplePie.MaxAllIngredients); // from cinnamon
            Assert.AreEqual(2, testApplePie.MaxNoOptional); // from apples

            Assert.AreEqual(1, testApplePie.AvailableInventory.IngredientQuantities[Ingredient.Apple]); // 10 - 3*3
            Assert.AreEqual(3, testApplePie.AvailableInventory.IngredientQuantities[Ingredient.Sugar]); // 9 - 2*3
            Assert.AreEqual(5, testApplePie.AvailableInventory.IngredientQuantities[Ingredient.Flour]); // 8 - 1*3
            Assert.AreEqual(0, testApplePie.AvailableInventory.IngredientQuantities[Ingredient.Cinnamon]); // 1-1*1
            Assert.AreEqual(1.75, testApplePie.AvailableInventory.IngredientQuantities[Ingredient.Butter]); // 4-.75*3
        }

        [TestMethod()]
        public void PrintApplePieMaxs_Test()
        {
            // assemble
            RunApplePiesCalculations_Test();

            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            var expectedOutput1 = "You can make 1 cinnamon apple pie(s), ";
            var expectedOutput2 = "and 2 apple pie(s) without cinnamon.\n";

            //act
            testApplePie.PrintApplePieMaxs();

            //assert
            var outputLines = stringWriter.ToString().Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            Assert.AreEqual(expectedOutput1,outputLines[0]);
            Assert.AreEqual(expectedOutput2, outputLines[1]);
        }      

    }
}