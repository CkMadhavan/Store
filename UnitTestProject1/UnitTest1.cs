using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Supermarket.UnitTests
{
    [TestClass]
    public class SupermarketTest
    {
        [TestMethod]
        public void totalPrice_NoDiscount_319()
        {
            // Arrange
            List<string> skus = new List<string>();
            Dictionary<string, int> prices = new Dictionary<string, int>();
            Dictionary<string, Tuple<int, int>> discounts = new Dictionary<string, Tuple<int, int>>();
            List<string> skus_to_buy = new List<string>();

            skus = new List<string>() { "A99", "B15", "C40", "T34" };

            prices.Add(skus[0], 50);
            prices.Add(skus[1], 30);
            prices.Add(skus[2], 60);
            prices.Add(skus[3], 99);

            discounts.Add(skus[0], Tuple.Create(0, 0));
            discounts.Add(skus[1], Tuple.Create(0, 0));
            discounts.Add(skus[2], Tuple.Create(0, 0));
            discounts.Add(skus[3], Tuple.Create(0, 0));

            skus_to_buy = new List<string>() { "A99", "B15", "A99", "T34" , "C40", "B15" };

            // Act
            int total = SupermarketCheckout.totalPrice(skus, prices, discounts, skus_to_buy);
            Console.WriteLine(total);

            // Assert
            Assert.AreEqual(total, 319);  // 2*50 (A99) + 2*30 (B15) + 1*60 (C40) + 1*99 (T34) = 319
        }

        [TestMethod]
        public void totalPrice_EmptySkus_0()
        {
            // Arrange
            List<string> skus = new List<string>();
            Dictionary<string, int> prices = new Dictionary<string, int>();
            Dictionary<string, Tuple<int, int>> discounts = new Dictionary<string, Tuple<int, int>>();
            List<string> skus_to_buy = new List<string>();

            // Act
            int total = SupermarketCheckout.totalPrice(skus, prices, discounts, skus_to_buy);
            Console.WriteLine(total);

            // Assert
            Assert.AreEqual(total, 0);
        }

        [TestMethod]
        public void totalPrice_EmptySkuToBuy_0()
        {
            // Arrange
            List<string> skus = new List<string>();
            Dictionary<string, int> prices = new Dictionary<string, int>();
            Dictionary<string, Tuple<int, int>> discounts = new Dictionary<string, Tuple<int, int>>();
            List<string> skus_to_buy = new List<string>();

            skus = new List<string>() { "A99", "B15", "C40", "T34" };

            prices.Add(skus[0], 50);
            prices.Add(skus[1], 30);
            prices.Add(skus[2], 60);
            prices.Add(skus[3], 99);

            discounts.Add(skus[0], Tuple.Create(3, 130));
            discounts.Add(skus[1], Tuple.Create(2, 45));
            discounts.Add(skus[2], Tuple.Create(0, 0));
            discounts.Add(skus[3], Tuple.Create(0, 0));

            // Act
            int total = SupermarketCheckout.totalPrice(skus, prices, discounts, skus_to_buy);
            Console.WriteLine(total);

            // Assert
            Assert.AreEqual(total, 0);
        }

        [TestMethod]
        public void totalPrice_DiscountAAndB_364()
        {
            // Arrange
            List<string> skus = new List<string>();
            Dictionary<string, int> prices = new Dictionary<string, int>();
            Dictionary<string, Tuple<int, int>> discounts = new Dictionary<string, Tuple<int, int>>();
            List<string> skus_to_buy = new List<string>();

            skus = new List<string>() { "A99", "B15", "C40", "T34" };

            prices.Add(skus[0], 50);
            prices.Add(skus[1], 30);
            prices.Add(skus[2], 60);
            prices.Add(skus[3], 99);

            discounts.Add(skus[0], Tuple.Create(3, 130));
            discounts.Add(skus[1], Tuple.Create(2, 45));
            discounts.Add(skus[2], Tuple.Create(0, 0));
            discounts.Add(skus[3], Tuple.Create(0, 0));

            skus_to_buy = new List<string>() { "A99", "B15", "A99", "A99", "T34", "C40", "B15", "B15" };

            // Act
            int total = SupermarketCheckout.totalPrice(skus, prices, discounts, skus_to_buy);
            Console.WriteLine(total);

            // Assert
            Assert.AreEqual(total, 364);  // (1*130) (A99) + (1*45 + 1*30) (B15) + 1*60 (C40) + 1*99 (T34) = 364
        }

    }
}
