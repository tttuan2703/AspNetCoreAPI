using _swagger.DataMongoDB;
using _swagger.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace _swagger.Services
{
    public class CategoryServices : ICategoryServices
    {
        protected readonly IMongoCollection<CategoryBook> _category;
        public CategoryServices(MyDB db)
        {
            _category = db.GetCategoryBookCollection();
        }
        public async Task<IEnumerable<CategoryBook>> GetAllCategory() =>
            await _category.Find(c => true).ToListAsync();
    }
}
