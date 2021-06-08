using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookApi.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace BookApi.Services
{
    public class CategoryServices
    {
        private readonly IMongoCollection<CategoryBook> _category;
        public CategoryServices(ICategoryBookDatabaseSettings settings)
        {
            var client = new MongoClient(settings.CB_ConnectionString);
            var database = client.GetDatabase(settings.CB_DatabaseName);
            _category = database.GetCollection<CategoryBook>(settings.CategoryCollectionName);
        }
        public async Task<IEnumerable<CategoryBook>> Get() =>
            await _category.Find(c => true).ToListAsync();
    }
}
