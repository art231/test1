using Microsoft.AspNetCore.Mvc;

namespace WebApiTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TutorialController : ControllerBase
    {
        private static readonly Tutorial[] statistics = new Tutorial[3] {
            new Tutorial {Title="a123",LastStatistics=DateTime.Now, VersionClient="1",Type="windows"},
            new Tutorial {Title="a",LastStatistics=DateTime.Now, VersionClient="1",Type="windows"},
            new Tutorial {Title="a",LastStatistics=DateTime.Now, VersionClient="1",Type="windows"}};

        private readonly ILogger<TutorialController> _logger;

        public TutorialController(ILogger<TutorialController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "Tutorial")]
        public IEnumerable<Tutorial> Get()
        {
            _logger.LogInformation("--Log Information--");
            return statistics;
        }
    }
}