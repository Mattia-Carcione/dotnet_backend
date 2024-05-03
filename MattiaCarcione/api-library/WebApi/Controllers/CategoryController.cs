using Microsoft.AspNetCore.Mvc;
using LibraryServices.Services.Read.ReadCategory;

namespace WebApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories = await ReadCategories.GetAllCategory();

            return Ok(categories);
        }
    }
}
