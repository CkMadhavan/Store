using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Supermarket.UnitTests
{
    [TestClass]
    public class SupermarketTest
    {
        [TestMethod]
        public void totalPrice_nodiscount_319()
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
            Assert.AreEqual(total, 319);
        }

    }
}
