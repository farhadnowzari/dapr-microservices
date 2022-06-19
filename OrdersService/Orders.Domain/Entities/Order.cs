using Orders.Domain.Enums;

namespace Orders.Domain.Entities;

public class Order
{
    public int Id { get; set; }
    public int ProductId { get; set; }

    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }

    public OrderStatus Status { get; set; }
    public Product Product { get; set; }
}