using System;
using System.Collections.Generic;

namespace Supermarket
{
    public class SupermarketCheckout
    {
        static List<string> skus = new List<string>();
        static Dictionary<string, int> prices = new Dictionary<string, int>();

        // Using a tuple of number of items eligible for discount and the new amount as value
        static Dictionary<string, Tuple<int, int>> discounts = new Dictionary<string, Tuple<int, int>>();

        static List<string> skus_to_buy = new List<string>();

        public static void Main(string[] args)
        {
            // Main Function

            Console.WriteLine("SUPERMARKET CHECKOUT\n");
        not_m_or_h_label:
            Console.WriteLine("Do you wish to enter items Manually or use Hard-coded values? (m/h)?");
            string m_or_h = Console.ReadLine();  // Get user input

            Console.WriteLine('\n');

            switch (m_or_h)
            {  // Switch-case to handle different inputs

                case "m":
                    getInputs();
                    break;

                case "h":
                    hardInputs();
                    break;

                default:
                    Console.WriteLine("\nPlease Enter m or h");
                    goto not_m_or_h_label;  // Get user input again

            }

            toCart();

            totalPrice();

        }

        public static void toCart()
        {
            // Add items to cart

            string current_sku_buy = "";

            Console.WriteLine("\n\n------------------------------------------------------------------------------------------------");
            Console.WriteLine("Proceeding to checkout. Scan items you want to buy by typing their SKU one by one. Press enter without typing anything if you're done");

            current_sku_buy = Console.ReadLine();
            while (current_sku_buy != "")
            {

                if (skus.Contains(current_sku_buy))
                {  // Add only if item is part of items list
                    skus_to_buy.Add(current_sku_buy);
                }
                else
                {
                    Console.WriteLine("Item not found");
                }

                current_sku_buy = Console.ReadLine();

            }

        }

        public static void totalPrice()
        {
            // Calculate total price

            // Dictionary to keep track of quantity of each item
            Dictionary<string, int> sku_counter = new Dictionary<string, int>();

            for (int i = 0; i < skus_to_buy.Count; i++)
            {

                // Incrementing value by 1 if key exits, else set value to 1
                if (sku_counter.ContainsKey(skus_to_buy[i]))
                {
                    sku_counter[skus_to_buy[i]] += 1;
                }
                else
                {
                    sku_counter[skus_to_buy[i]] = 1;
                }

            }

            int total = 0;

            Console.WriteLine("\nThe following items have been added to the cart");
            Console.WriteLine("\nSKU - Quantity - Amount");
            foreach (KeyValuePair<string, int> entry in sku_counter)  // Iterating through the dictionary
            {

                // Normal amount without discount
                int amount = prices[entry.Key] * entry.Value;

                if (discounts[entry.Key].Item1 != 0)
                {

                    // Calculate difference in price between normal and special offer for the mentioned number of items
                    int difference = (prices[entry.Key] * discounts[entry.Key].Item1) - discounts[entry.Key].Item2;

                    // Subtract the difference as many times as the mentioned number of items appears
                    amount -= (entry.Value / discounts[entry.Key].Item1) * difference;

                }

                total += amount;
                Console.WriteLine("{0} - {1} - {2}", entry.Key, entry.Value, amount);

            }

            Console.WriteLine("\nTotal Price : {0}", total);
            Console.Read();  // Wait to see output

        }

        public static void getInputs()
        {
            // Get manual inputs

            string current_sku = "";
            string current_discount = "";


            // Get SKUs
            Console.WriteLine("------------------------------------------------------------------------------------------------");
            Console.WriteLine("Enter Stock Keeping Unit (SKU) for each element one by one. Press enter without typing anything if you're done");

            current_sku = Console.ReadLine();
            while (current_sku != "")
            {
                if (skus.Contains(current_sku))
                {
                    Console.WriteLine("Item already added");
                }
                else
                {
                    skus.Add(current_sku);
                }

                current_sku = Console.ReadLine();
            }

            Console.WriteLine("\nThe following items have been added to the SKU");
            for (int i = 0; i < skus.Count; i++)
            {
                Console.WriteLine(skus[i]);
            }


            // Get Prices
            Console.WriteLine("------------------------------------------------------------------------------------------------");
            Console.WriteLine("Enter Price for each element one by one");

            int ctr1 = 0;
            while (skus.Count != prices.Count)
            {  // len(sku) should be same as len(prices)
                try
                {
                    prices.Add(skus[ctr1], Convert.ToInt32(Console.ReadLine()));
                    ctr1 += 1;
                }
                catch
                {
                    Console.WriteLine("Please enter integer values for prices");
                }

            }

            Console.WriteLine("\nThe following prices have been recorded for the items");
            for (int i = 0; i < prices.Count; i++)
            {
                Console.WriteLine("{0} - {1}", skus[i], prices[skus[i]]);
            }


            // Get Discounts
            Console.WriteLine("------------------------------------------------------------------------------------------------");
            Console.WriteLine("Enter Discount for each element one by one in the following format :");
            Console.WriteLine("Number of items eligible for discount-New amount for those many items");
            Console.WriteLine("For example, if the weekly discount is 3 Apples for 130, enter '3-130' in the corresponding row");
            Console.WriteLine("Enter '0-0' if no discount is applicable for that particular item");

            int ctr = 0;
            while (skus.Count != discounts.Count)
            {  // len(sku) should be same as len(discounts)
                try
                {
                    current_discount = Console.ReadLine();
                    Tuple<int, int> value = Tuple.Create(Convert.ToInt32(current_discount.Split('-')[0]), Convert.ToInt32(current_discount.Split('-')[1]));
                    discounts.Add(skus[ctr], value);
                    ctr += 1;
                }
                catch
                {
                    Console.WriteLine("Please enter discounts in the above mentioned format");
                }
            }

            Console.WriteLine("\nThe following prices and discounts have been recorded for the items");
            for (int i = 0; i < discounts.Count; i++)
            {
                Tuple<int, int> discount = discounts[skus[i]];
                if (discount.Item1 == 0)
                {
                    Console.WriteLine("{0} - {1}", skus[i], prices[skus[i]]);
                }
                else
                {
                    Console.WriteLine("{0} - {1} - {2} for {3}", skus[i], prices[skus[i]], discount.Item1, discount.Item2);
                }

            }

        }

        public static void hardInputs()
        {
            // Hard-code inputs

            skus = new List<string>() { "A99", "B15", "C40", "T34" };

            prices.Add(skus[0], 50);
            prices.Add(skus[1], 30);
            prices.Add(skus[2], 60);
            prices.Add(skus[3], 99);

            discounts.Add(skus[0], Tuple.Create(3, 130));
            discounts.Add(skus[1], Tuple.Create(2, 45));
            discounts.Add(skus[2], Tuple.Create(0, 0));
            discounts.Add(skus[3], Tuple.Create(0, 0));

            Console.WriteLine("\nThe following prices and discounts have been recorded for the items");
            for (int i = 0; i < discounts.Count; i++)
            {
                Tuple<int, int> discount = discounts[skus[i]];
                if (discount.Item1 == 0)
                {
                    Console.WriteLine("{0} - {1}", skus[i], prices[skus[i]]);
                }
                else
                {
                    Console.WriteLine("{0} - {1} - {2} for {3}", skus[i], prices[skus[i]], discount.Item1, discount.Item2);
                }

            }

        }

    }
}
