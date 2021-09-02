using CachingDecorator.Models;
using System.Collections.Generic;

namespace CachingDecorator.Repositories
{
    public interface ICarRepository
    {
        IEnumerable<Car> GetAll();
        Car GetById(int id);
    }
}
