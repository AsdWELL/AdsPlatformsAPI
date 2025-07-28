using AdsPlatformsAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace AdsPlatformsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdsPlatformsController(IAdsPlatformsService service) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> LoadPlatforms(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("Пустой файл");

            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                var content = await reader.ReadToEndAsync();

                await service.LoadLocations(content);

                return Ok("Данные загружены");
            }
        }
    }
}
