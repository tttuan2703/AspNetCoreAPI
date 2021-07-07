using _swagger.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace _swagger.Services
{
    public interface ICategoryServices
    {
        public Task<IEnumerable<CategoryBook>> GetAllCategory();
    }
}
