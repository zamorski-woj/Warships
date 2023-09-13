using Microsoft.AspNetCore.Mvc;

namespace Warships.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class PlayerController : ControllerBase
    {

        private readonly ILogger<PlayerController> _logger;

        public PlayerController(ILogger<PlayerController> logger)
        {
            _logger = logger;
        }


        [HttpGet]
        public string Get()
        {
            return "a1";
        }
    }
}
