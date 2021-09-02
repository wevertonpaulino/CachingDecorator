using CachingDecorator.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CachingDecorator.Controllers
{
    [Route("api/cars")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly ICarRepository _carRepository;

        public CarsController(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        // GET api/cars
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_carRepository.GetAll());
        }

        // GET api/cars/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(_carRepository.GetById(id));
        }
    }
}
