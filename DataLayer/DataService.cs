using Microsoft.EntityFrameworkCore;

namespace DataLayer;

public class DataService
{
  public IList<Category> GetCategories()
  {
    var db = new NorthwindContext();
    return db.Categories.ToList();
  }

  public Category GetCategory(int id)
  {
    var db = new NorthwindContext();
    return db.Categories.Find(id);
  }

  public Category CreateCategory(string name, string description)
  {
    using (var db = new NorthwindContext())
    {
      // Find the current maximum category ID
      var maxId = db.Categories.Max(c => (int?)c.Id) ?? 0;

      // Create a new category
      var category = new Category
      {
        Id = maxId + 1,
        Name = name,
        Description = description
      };

      db.Categories.Add(category);
      db.SaveChanges();

      return category;
    }
  }
  // Method to delete a category by ID
  public bool DeleteCategory(int id)
  {
    var db = new NorthwindContext();
    var category = db.Categories.Find(id);
    if (category == null) return false;
    db.Categories.Remove(category);
    db.SaveChanges();
    return true;
  }

  // Method to update a category by ID
  public bool UpdateCategory(int id, string name, string description)
  {
    var db = new NorthwindContext();
    var category = db.Categories.Find(id);
    if (category == null) return false;
    category.Name = name;
    category.Description = description;
    db.SaveChanges();
    return true;
  }

  public IList<Product> GetProducts()
  {
    var db = new NorthwindContext();
    return db.Products.ToList();
  }

  public Product GetProduct(int id)
  {
    using (var db = new NorthwindContext())
    {
      var Product = db.Products
          .Where(p => p.Id == id)
          .Include(p => p.Category)
          .Select(p => new Product
          {
            Id = p.Id,
            Name = p.Name,
            CategoryName = p.Category.Name
          })
          .FirstOrDefault();

      return Product;
    }
  }

  public IList<Product> GetProductByCategory(int categoryId)
  {
    using var db = new NorthwindContext();

    var products = db.Products
        .Where(p => p.CategoryId == categoryId)
        .Include(p => p.Category)
        .Select(p => new Product
        {
          Id = p.Id,
          Name = p.Name,
          CategoryName = p.Category.Name,
          QuantityPerUnit = p.QuantityPerUnit,
          UnitPrice = p.UnitPrice,
          UnitsInStock = p.UnitsInStock
        })
        .ToList();

    return products;
  }

  public IList<Product> GetProductByName(string name)
  {
    using var db = new NorthwindContext();

    var products = db.Products
        .Where(p => p.Name.Contains(name))
        .Include(p => p.Category)
        .Select(p => new Product
        {
          Id = p.Id,
          Name = p.Name,
          CategoryName = p.Category.Name,
          QuantityPerUnit = p.QuantityPerUnit,
          UnitPrice = p.UnitPrice,
          UnitsInStock = p.UnitsInStock
        })
        .ToList();
    foreach (var product in products)
    {
      product.ProductName = product.Name;
    }

    return products;
  }

  public Order GetOrder(int id)
  {
    using var db = new NorthwindContext();

    var order = db.Orders
        .Where(o => o.Id == id)
        .Include(o => o.OrderDetails)
            .ThenInclude(od => od.Product)
                .ThenInclude(p => p.Category)
        .Select(o => new Order
        {
          Id = o.Id,
          Date = o.Date,
          Required = o.Required,
          OrderDetails = o.OrderDetails.Select(od => new OrderDetail
          {
            Product = new Product
            {
              Name = od.Product.Name,
              Category = new Category
              {
                Name = od.Product.Category.Name
              }
            }
          }).ToList()
        }).FirstOrDefault();

    return order;
  }

  public IList<Order> GetOrders()
  {
    using var db = new NorthwindContext();

    var orders = db.Orders
        .Include(o => o.OrderDetails)
            .ThenInclude(od => od.Product)
                .ThenInclude(p => p.Category)
        .Select(o => new Order
        {
          Id = o.Id,
          Date = o.Date,
          Required = o.Required,
          OrderDetails = o.OrderDetails.Select(od => new OrderDetail
          {
            Product = new Product
            {
              Name = od.Product.Name,
              Category = new Category
              {
                Name = od.Product.Category.Name
              }
            }
          }).ToList()
        }).ToList();
    return orders;
  }

  public IList<OrderDetail> GetOrderDetailsByOrderId(int orderId)
  {
    using var db = new NorthwindContext();

    var orderDetails = db.OrderDetails
        .Where(od => od.OrderId == orderId)
        .Include(od => od.Product)
            .ThenInclude(p => p.Category)
        .Select(od => new OrderDetail
        {
          Product = new Product
          {
            Name = od.Product.Name,
            Category = new Category
            {
              Name = od.Product.Category.Name
            }
          },
          UnitPrice = od.UnitPrice,
          Quantity = od.Quantity
        }).ToList();
    return orderDetails;
  }

  public IList<OrderDetail> GetOrderDetailsByProductId(int productId)
  {
    using var db = new NorthwindContext();

    var orderDetails = db.OrderDetails
        .Where(od => od.ProductId == productId)
        .Include(od => od.Order)
        .Select(od => new OrderDetail
        {
          OrderId = od.OrderId,
          Order = new Order
          {
            Date = od.Order.Date
          },
          UnitPrice = od.UnitPrice,
          Quantity = od.Quantity
        }).OrderBy(od => od.OrderId)
        .ToList();

    return orderDetails;
  }
}
