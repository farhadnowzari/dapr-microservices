namespace Orders.Domain.Entities;

public class Product
{
    public Product()
    {
        Orders = new List<Order>();
    }

    public int Id { get; set; }
    public string Name { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }

    public List<Order> Orders { get; private set; }
}