using POSTSLibrary.DataAccess;
using POSTSLibrary.Internal.DataAccess;
using POSTSLibrary.Services;
using System;
using System.Collections.Generic;

namespace POSConsole
{
    class Program
    {
        static void Main()
        {
            // ABCDABA Verify that the total price is $13.25
            // CCCCCCC Verify that the total price is $6.00
            // ABCD Verify that the total price is $7.25

            // Product Code Unit Price Bulk Price
            // A $1.25  3 for $3.00
            // B $4.25
            // C $1.00  $5 for a six-pack
            // D $0.75

            List<string> orders = new List<string>() { "ABCDABA", "CCCCCCC", "ABCD" };
            var pos = new PointOfSaleTerminal();

            orders.ForEach(order => {
                foreach (char productId in order)
                {
                    pos.ScanItem(productId);
                };
                double TotalPrice = pos.CalculateCartTotal();
                Console.WriteLine($"Order => {order} has a total cost of ${TotalPrice}");
                pos.EmptyCart();
            });

            Console.ReadLine();
        }
    }
}
