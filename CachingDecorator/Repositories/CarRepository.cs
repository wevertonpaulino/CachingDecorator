using Bogus;
using CachingDecorator.Models;
using System.Linq;
using System.Collections.Generic;

namespace CachingDecorator.Repositories
{
    public class CarRepository : ICarRepository
    {
        public CarRepository()
        {
            int id = 0;

            var fakeCars = new Faker<Car>()
                .RuleFor(o => o.Id, f => ++id)
                .RuleFor(o => o.Year, f => f.Random.Int(1960, 2021))
                .RuleFor(o => o.Brand, f => f.Vehicle.Manufacturer())
                .RuleFor(o => o.Model, f => f.Vehicle.Model());

            Cars = fakeCars.Generate(10);
        }

        public IEnumerable<Car> Cars { get; set; }

        public IEnumerable<Car> GetAll()
        {
            return Cars;
        }

        public Car GetById(int id)
        {
            return Cars.FirstOrDefault(x => x.Id == id);
        }
    }
}
