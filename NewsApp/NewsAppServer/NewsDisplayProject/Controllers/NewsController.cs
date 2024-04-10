using Microsoft.AspNetCore.Mvc;
using NewsDisplay.Models;
using NewsDisplay.Services;
using System.Text.Json;

namespace YourProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NewsController : ControllerBase
    {
        private readonly INewsService _newsService;

        public NewsController(INewsService newService)
        {
            _newsService = newService;
        }

        [HttpGet]
        public async Task<ActionResult<List<NewsItem>>> GetNews()
        {
            var topics = await _newsService.GetNewsItemsAsync();
            return Ok(topics);
        }
    }
}
