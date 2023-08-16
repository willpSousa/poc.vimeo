using Microsoft.AspNetCore.Mvc;
using Poc.Vimeo.DTO;
using Poc.Vimeo.Services;

namespace Poc.Vimeo.Controllers
{
    [Route("api/[controller]")]
    public class VimeoController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Upload()
        {
            if (!Request.Form.Files.Any())
            {
                return BadRequest();
            }

            var file = Request.Form.Files[0];

            return Ok(await new VimeoRegistrationService().Register(file));
        }

        [HttpGet("complete")]
        public IActionResult Complete([FromQuery] string video_uri) =>
            Ok(new VimeoUploadResultDTO { VideoUri = video_uri });
    }
}
