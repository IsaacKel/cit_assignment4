namespace DataLayer;
public class Product
{
  // Properties
  public int Id { get; set; } = 0;
  public string? Name { get; set; } = null;
  public string? SupplierId { get; set; }
  public int CategoryId { get; set; }
  public decimal UnitPrice { get; set; } = 0m;
  public string? QuantityPerUnit { get; set; } = null;
  public int UnitsInStock { get; set; } = 0;
  public Category? Category { get; set; }
  public string CategoryName { get; set; }

  public string? ProductName
  {
    get => Name;
    set => Name = value;
  }

}
