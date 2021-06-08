using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookApi.Models
{
    public class CategoryBookDatabaseSettings : ICategoryBookDatabaseSettings
    {
        public string CategoryCollectionName { get; set; }
        public string CB_ConnectionString { get; set; }
        public string CB_DatabaseName { get; set; }
    }
    public interface ICategoryBookDatabaseSettings
    {
        string CategoryCollectionName { get; set; }
        string CB_ConnectionString { get; set; }
        string CB_DatabaseName { get; set; }
    }
}
