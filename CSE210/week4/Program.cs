using System;

class Program
{
    static void Main()
    {

        var addr1 = new Address("123 Main St", "Anytown", "NY", "USA");
        var addr2 = new Address("456 Elm Rd", "Otherville", "ON", "Canada");

        var cust1 = new Customer("John Doe", addr1);
        var cust2 = new Customer("Jane Doe", addr2);

        var order1 = new Order(cust1);
        order1.AddProduct(new Product("DingleHopper", "DH123", 3.50, 4));
        order1.AddProduct(new Product("ShinyThingy", "ST456", 15.00, 2));

        var order2 = new Order(cust2);
        order2.AddProduct(new Product("Thingamajig", "T789", 7.25, 3));
        order2.AddProduct(new Product("Doodad", "D012", 12.00, 1));

        foreach (var order in new [] { order1, order2 })
        {
            Console.WriteLine(order.GetPackingLabel());
            Console.WriteLine(order.GetShippingLabel());
            Console.WriteLine($"Total Cost: ${order.CalculateTotalCost():0.00}\n");
            Console.WriteLine(new string('-', 40));
        }
    }
}