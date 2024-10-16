using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DataLayer;

public class DataService
{   
    public IList<Category> GetCategories  () {
        var db = new NorthwindContext();
        return db.Categories.ToList();
    }
    public Category GetCategory(int requested_ID)
    {
        var db = new NorthwindContext();

        var myCategory = db.Categories.FirstOrDefault(c => c.Id == requested_ID);
        return myCategory;
    }
    public Category CreateCategory(string categoryName, string categoryDescription)
    {
        var db = new NorthwindContext();
        int id = db.Categories.Max(x => x.Id) + 1;
        var myCategory = new Category
        {
            Id = id,
            Name = categoryName,
            Description = categoryDescription
        };

        db.Categories.Add(myCategory);

        db.SaveChanges();

        return myCategory;
    }

}
