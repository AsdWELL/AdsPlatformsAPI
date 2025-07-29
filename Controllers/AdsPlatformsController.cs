using AdsPlatformsAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AdsPlatformsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdsPlatformsController(IAdsPlatformsService platformsService) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> LoadPlatforms(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("Пустой файл");

            await platformsService.LoadLocations(file);

            return Ok("Данные загружены");
        }

        [HttpGet]
        public async Task<IActionResult> GetPlatformsByLocation(string location)
        {
            return Ok(await platformsService.GetPlatformsByLocation(location));
        }
    }
}
