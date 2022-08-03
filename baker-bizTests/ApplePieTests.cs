using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace baker_biz.Tests
{
    [TestClass()]
    public class ApplePieTests
    {

        [TestMethod()]
        public void MaxApplePiesCinnamon_SoftCoded_Test()
        {
            // assemble
            int apples = 10;
            int sugar = 9;
            int flour = 8;
            int cinnamon = 1;
            ApplePie.ApplePieInventory testInventory = new(apples, sugar, flour, cinnamon);

            int expectedApples = apples / ApplePie.APPLES_PER_PIE; // 10/3 = 3.x
            int expectedPies = expectedApples; // 3

            int expectedSugar = sugar / ApplePie.SUGAR_POUNDS_PER_PIE; // 9/2 = 4.x
            expectedPies = Math.Min(expectedPies, expectedSugar); // 3

            int expectedFlour = flour / ApplePie.FLOUR_POUNDS_PER_PIE; // 8/1 = 8
            expectedPies = Math.Min(expectedPies, expectedFlour); // 3

            int expectedCinnamon = cinnamon / ApplePie.CINNAMON_TSP_PER_PIE; // 1/1 = 1
            expectedPies = Math.Min(expectedPies, expectedCinnamon); // 1

            //act
            int result = ApplePie.MaxApplePiesCinnamon(testInventory);

            //assert
            Assert.AreEqual(expectedPies, result);
        }

        [TestMethod()]
        public void MaxApplePiesCinnamon_HardCoded_Test()
        {
            // assemble
            int apples = 10;
            int sugar = 9;
            int flour = 8;
            int cinnamon = 2;
            ApplePie.ApplePieInventory testInventory = new(apples, sugar, flour, cinnamon);

            int expectedPies = 2; // from cinnamon as lower

            //act
            int result = ApplePie.MaxApplePiesCinnamon(testInventory);

            //assert
            Assert.AreEqual(expectedPies, result);
        }

        [TestMethod()]
        public void MaxApplePiesNoCinnamon_SoftCoded_Test()
        {
            // assemble
            int apples = 10;
            int sugar = 9;
            int flour = 1;
            ApplePie.ApplePieInventory testInventory = new(apples, sugar, flour, 0);

            int expectedApples = apples / ApplePie.APPLES_PER_PIE; // 10/3 = 3.x
            int expectedPies = expectedApples; // 3

            int expectedSugar = sugar / ApplePie.SUGAR_POUNDS_PER_PIE; // 9/2 = 4.x
            expectedPies = Math.Min(expectedPies, expectedSugar); // 3

            int expectedFlour = flour / ApplePie.FLOUR_POUNDS_PER_PIE; // 1/1 = 1
            expectedPies = Math.Min(expectedPies, expectedFlour); // 1


            //act
            int result = ApplePie.MaxApplePiesNoCinnamon(testInventory);

            //assert
            Assert.AreEqual(expectedPies, result);
        }

        [TestMethod()]
        public void MaxApplePiesNoCinnamon_HardCoded_Test()
        {
            // assemble
            int apples = 6;
            int sugar = 9;
            int flour = 5;
            ApplePie.ApplePieInventory testInventory = new(apples, sugar, flour, 0);

            int expectedPies = 2; // from apples as lower

            //act
            int result = ApplePie.MaxApplePiesNoCinnamon(testInventory);

            //assert
            Assert.AreEqual(expectedPies, result);
        }


        [TestMethod()]
        public void CalculateApplePieLeftovers_SoftCoded_Test()
        {
            // assemble
            int apples = 10;
            int sugar = 9;
            int flour = 8;
            int cinnamon = 2;
            ApplePie.ApplePieInventory testInventory = new(apples, sugar, flour, cinnamon);

            int pies = ApplePie.MaxApplePiesCinnamon(testInventory);

            int expApples = apples - pies * ApplePie.APPLES_PER_PIE;
            int expSugar = sugar - pies * ApplePie.SUGAR_POUNDS_PER_PIE;
            int expFlour = flour - pies * ApplePie.FLOUR_POUNDS_PER_PIE;
            int expCinnamon = cinnamon - pies * ApplePie.CINNAMON_TSP_PER_PIE;



            //act
            ApplePie.CalculateApplePieLeftovers(pies, ref testInventory);

            //assert
            Assert.AreEqual(expApples, testInventory.Apples);
            Assert.AreEqual(expSugar, testInventory.Sugar);
            Assert.AreEqual(expFlour, testInventory.Flour);
            Assert.AreEqual(expCinnamon, testInventory.Cinnamon);
        }

        [TestMethod()]
        public void CalculateApplePieLeftovers_HardCoded_Test()
        {
            // assemble
            int apples = 10;
            int sugar = 9;
            int flour = 8;
            int cinnamon = 2;
            ApplePie.ApplePieInventory testInventory = new(apples, sugar, flour, cinnamon);

            int pies = 2; // from cinnamon

            //act
            ApplePie.CalculateApplePieLeftovers(pies, ref testInventory);

            //assert
            Assert.AreEqual(4, testInventory.Apples); // 10 - 3*2
            Assert.AreEqual(5, testInventory.Sugar); // 9 - 2*2
            Assert.AreEqual(6, testInventory.Flour); // 8 - 1*2
            Assert.AreEqual(0, testInventory.Cinnamon); // 2-1*2
        }
    }
}