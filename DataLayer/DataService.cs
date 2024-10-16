using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DataLayer;

public class DataService
{   
    public IList<Category> GetCategories() {
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
    public bool DeleteCategory(int requested_ID)
    {
        var db = new NorthwindContext();
        var myCategory = db.Categories.FirstOrDefault(c => c.Id == requested_ID);
        if (myCategory == null) { return false; }
        else
        {
            db.Categories.Remove(myCategory);
            db.SaveChanges();

            return true;
        }
    }
    public bool UpdateCategory(int requested_ID, string categoryName, string categoryDescription)
        {
        var db = new NorthwindContext();
        var myCategory = db.Categories.FirstOrDefault(c => c.Id == requested_ID);
        if (myCategory == null) { return false; }
        else
        {
            myCategory.Id = requested_ID;
            myCategory.Name = categoryName;
            myCategory.Description = categoryDescription;
           
            db.SaveChanges();

            return true;
        }
    }


   

}
