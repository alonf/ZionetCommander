using Zionet.Extensions.Command;

namespace Zionet.Extension.Command.Example.Controllers
{
    class CommandHandler : ICommandHandler
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        public CommandHandler(ILogger<WeatherForecastController> logger)
        {

            _logger = logger;
        }

        [Command("UpdateForecast")]
        private void OnUpdateForecast(string message, CommandWeatherResult weatherResult)
        {
            weatherResult.Result = Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [Command("DoSomething")]
        private void OnCommandB(string message)
        {
            _logger.LogInformation(message);
        }
    }
}