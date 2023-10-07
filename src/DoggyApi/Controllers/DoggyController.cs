using DoggyApi.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DoggyApi.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class DoggyController : ControllerBase
    {
        private readonly ILogger<DoggyController> _log;
        private readonly Random _random;

        public DoggyController(ILogger<DoggyController> log)
        {
            _log = log;
            _random = new(Guid.NewGuid().GetHashCode());
        }

        [HttpGet]
        public IActionResult DogSpecies()
        {
            var possibleSpecies = new string[] {
                "sarcicha",
                "dog pulguento",
                "Mistura doida",
                "Serelepe pequeno",
                "Vira lata caramelo",
                "pug coitado",
                "Cachorro doido que pula corda",
                "Darmata",
                "Sarnento",
            };
            var adjetivos = new string[]
            {
                "fediddo",
                "do mato",
                "do mar",
                "serelepe",
                "bebe",
                "gato",
                "paraquedista",
                "anime",
                "otaku",
                "doido",
            };
            return Ok(possibleSpecies.GetRandomItem(_random) + " " + adjetivos.GetRandomItem(_random));
        }
    }
}
