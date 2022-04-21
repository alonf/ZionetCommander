# ZionetCommander

An elementary command dispatcher library that works with .NET Microsoft Dependency Injection

## A simple example:

Add the following code to the services setup code:

    builder.Services.ConfigureCommander();
    builder.Services.AddSingleton<ICommandHandler, Zionet.Extension.Command.Example.Controllers.CommandHandler>();

You may register multiple ICommandHandler types.

Create a handler class:

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


Use the handler:

    public class CommandWeatherResult
    {
        public WeatherForecast[] Result { get; set; } = Array.Empty<WeatherForecast>();
    }

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
