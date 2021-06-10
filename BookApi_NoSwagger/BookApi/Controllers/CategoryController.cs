using BookApi.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace BookApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly CategoryServices _CategoryService;
        public CategoryController(CategoryServices CategoryService)
        {
            _CategoryService = CategoryService;
        }
        public Task<string> Get()
        {
            return this.GetCategory();
        }
        [HttpGet("{id:length(24)}", Name = "GetCategory")]
        public async Task<string> GetCategory()
        {
            var books = await _CategoryService.Get();
            return JsonConvert.SerializeObject(books);
        }
    }
}
