
using System.Collections.Generic;
using System.Text;

public class Order
{
    private List<Product> _products = new List<Product>();
    private Customer _customer;

    public Order(Customer customer)
    {
        _customer = customer;
    }

    public void AddProduct(Product product)
    {
        _products.Add(product);
    }

    public double CalculateTotalCost()
    {
        double total = 0;
        foreach (var p in _products)
            total += p.GetTotalCost();

        total += _customer.LivesInUSA() ? 5 : 35;
        return total;
    }

    public string GetPackingLabel()
    {
        var sb = new StringBuilder();
        sb.AppendLine("Packing Label:");
        foreach (var p in _products)
            sb.AppendLine($" - {p.Name} (ID: {p.ProductId})");
        return sb.ToString();
    }

    public string GetShippingLabel()
    {
        var sb = new StringBuilder();
        sb.AppendLine("Shipping To:");
        sb.AppendLine(_customer.Name);
        sb.AppendLine(_customer.Address.ToString());
        return sb.ToString();
    }
}
