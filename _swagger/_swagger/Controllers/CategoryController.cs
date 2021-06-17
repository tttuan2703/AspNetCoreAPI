using _swagger.Services;
using Microsoft.AspNetCore.Mvc;

namespace _swagger.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : Controller
    {
        private readonly ICategoryServices _categoryServices;
        public CategoryController(ICategoryServices categoryServices)
        {
            _categoryServices = categoryServices;
        }
        [HttpGet]
        public IActionResult GetListCategorys()
        {
            return Ok(_categoryServices.GetAllCategory().Result);
        }
    }
}
