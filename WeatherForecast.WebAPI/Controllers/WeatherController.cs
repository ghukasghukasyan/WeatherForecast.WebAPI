using MediatR;
using Microsoft.AspNetCore.Mvc;
using WeatherForecast.Application.Commands.Weathers;
using WeatherForecast.Application.Queries.Weathers;
using WeatherForecast.DTO.Weathers;

namespace WeatherForecast.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherController : ControllerBase
    {
        private readonly IMediator _mediator;

        public WeatherController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]

        public async Task<IActionResult> CreateAsync([FromBody] WeatherDTO weather)
        {
            var result = await _mediator.Send(new CreateWeatherCommand(weather));

            return Ok(result);
        }

        [HttpPost("Bulk")]

        public async Task<IActionResult> CreateBulkAsync([FromBody] List<WeatherDTO> weathers)
        {
            var result = await _mediator.Send(new CreateBulkWeatherCommand(weathers));

            return Ok(result);
        }

        [HttpPut]

        public async Task<IActionResult> UpdateAsync([FromBody] WeatherDTO weather)
        {
            var result = await _mediator.Send(new UpdateWeatherCommand(weather));

            return Ok(result);
        }

        [HttpGet]

        public async Task<IActionResult> GetAsync()
        {
            var result = await _mediator.Send(new GetWeathersQuery());

            return Ok(result);
        }

        [HttpGet("Today")]

        public async Task<IActionResult> GetTodaysHourlyAsync(int hour)
        {
            var result = await _mediator.Send(new GetTodaysHourlyWeatherQuery(hour));

            return Ok(result);
        }

        [HttpGet("Hottest")]

        public async Task<IActionResult> GetHottestAsync(DateTime date)
        {
            var result = await _mediator.Send(new GetHottestWeatherQuery(date));

            return Ok(result);
        }
    }
}
