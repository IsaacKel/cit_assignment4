using System.Collections.Generic;

namespace DataLayer;

public interface IDataService
{
  // Get all categories
  IList<Category> GetCategories();

  // Get a single category by ID
  Category? GetCategory(int id);

  // Create a new category and return the created category
  Category CreateCategory(string name, string description);

  // Delete a category by ID
  bool DeleteCategory(int id);

  //Update a category by id
  Category UpdateCategory(int id, string name, string description);

  // Get all products
  IList<Product> GetProducts();

  // Get a single product by ID
  Product? GetProduct(int id);

  // Get all products for a category
  IList<Product> GetProductByCategory(int categoryId);

  // Get products by name
  IList<Product> GetProductByName(string name);


}