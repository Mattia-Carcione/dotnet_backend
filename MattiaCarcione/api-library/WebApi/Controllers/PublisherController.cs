using Microsoft.AspNetCore.Mvc;
using LibraryServices.Services.Read.ReadPublisher;

namespace WebApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PublisherController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var publisher = await ReadPublishers.GetAllPublishers();

            return Ok(publisher);
        }
    }
}