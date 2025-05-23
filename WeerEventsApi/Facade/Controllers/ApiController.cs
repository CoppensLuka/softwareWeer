using Microsoft.AspNetCore.Mvc;

namespace WeerEventsApi.Facade.Controllers
{
    [ApiController]
    [Route("")]
    public class ApiController : ControllerBase
    {
        private readonly DomeinController _controller;

        public ApiController(DomeinController controller)
        {
            _controller = controller;
        }

        [HttpGet]
        public IActionResult Root() => Ok("Server is running");

        [HttpGet("steden")]
        public IActionResult GetSteden() => Ok(_controller.GeefSteden());

        [HttpGet("weerstations")]
        public IActionResult GetWeerstations() => Ok(_controller.GeefWeerstations());

        [HttpGet("metingen")]
        public IActionResult GetMetingen() => Ok(_controller.GeefMetingen());

        [HttpGet("weerbericht")]
        public IActionResult GetWeerbericht() => Ok(_controller.GeefWeerbericht());

        [HttpPost("commands/meting-command")]
        public IActionResult DoeMeting()
        {
            _controller.MaakMetingen();
            return Ok("Metingen uitgevoerd");
        }
    }
}
