using Microsoft.AspNetCore.Mvc;
using WizStore.Services;
using WizStore.Auth;
using WizStore.Entities;
using WizStore.Models.Users;
using WizStore.Models;

namespace WizStore.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class WeatherForecastController(IUserService userService) : ControllerBase
    {
        private IUserService _userService = userService;

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };


        [AllowAnonymous]
        [HttpGet("GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [Authorize(Role.Admin)]
        [HttpGet("AdministratorsCorner")]
        public IActionResult AdminnistratorsCorner()
        {
            var users = _userService.GetAll();
            return Ok(users);
        }
    }
}
