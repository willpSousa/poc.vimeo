using Microsoft.AspNetCore.Mvc;
using Poc.Vimeo.DTO;
using Poc.Vimeo.Repository;
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


        [HttpGet("{id:int}/status")]
        public async Task<IActionResult> GetVideoStatus(int id) =>
            Ok(await new VimeoVideoRepository().GetById(id));
    }
}
