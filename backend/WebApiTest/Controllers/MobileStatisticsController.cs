using Microsoft.AspNetCore.Mvc;

namespace WebApiTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MobileStatisticsController : ControllerBase
    {
        private static readonly MobileStatistics[] statistics = new MobileStatistics[3] {
            new MobileStatistics {Id=1, Title="a123",LastStatistics=DateTime.Now, VersionClient="1",Type="windows"},
            new MobileStatistics {Id=2, Title="a",LastStatistics=DateTime.Now, VersionClient="1",Type="windows"},
            new MobileStatistics {Id=3, Title="a",LastStatistics=DateTime.Now, VersionClient="1",Type="windows"}};

        private readonly ILogger<MobileStatisticsController> _logger;

        public MobileStatisticsController(ILogger<MobileStatisticsController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "Tutorial")]
        public IEnumerable<MobileStatistics> Get()
        {
            _logger.LogInformation("--Log Information--");
            return statistics;
        }
    }
}