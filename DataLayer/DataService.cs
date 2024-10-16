using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

}
