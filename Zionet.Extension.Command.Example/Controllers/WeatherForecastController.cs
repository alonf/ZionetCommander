using Microsoft.AspNetCore.Mvc;
using Zionet.Extensions.Command;

namespace Zionet.Extension.Command.Example.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase, ICommandHandler
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly ICommandManager _commandManager;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, ICommandManager commandManager)
        {
            _logger = logger;
            _commandManager = commandManager;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            CommandWeatherResult forecast = new CommandWeatherResult();
            _commandManager.Execute("UpdateForecast", forecast);

            return forecast.Result;
        }

        [HttpPost(Name = "DoSomething")]
        public void Post()
        {
            _commandManager.Execute("DoSomething");
        }
    }
}