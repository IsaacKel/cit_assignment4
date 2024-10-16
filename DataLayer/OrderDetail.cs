namespace DataLayer;

public class OrderDetail
{
  // Properties
  public int OrderId { get; set; }   // Foreign key to link to Order
  public int ProductId { get; set; } // Foreign key to link to Product
  public decimal UnitPrice { get; set; } = 0m;
  public int Quantity { get; set; } = 0;
  public decimal Discount { get; set; } = 0m;
  public Order? Order { get; set; } // Navigation property to the Order
  public Product? Product { get; set; } // Navigation property to the Product
}