using CachingDecorator.Models;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace CachingDecorator.Repositories.Caching
{
    public class CarCachingDecorator : ICarRepository
    {
        private readonly IMemoryCache _memoryCache;
        private readonly ILogger<CarCachingDecorator> _logger;
        private readonly ICarRepository _repository;

        public CarCachingDecorator(IMemoryCache memoryCache, ILogger<CarCachingDecorator> logger, ICarRepository repository)
        {
            _memoryCache = memoryCache;
            _logger = logger;
            _repository = repository;
        }

        public IEnumerable<Car> GetAll()
        {
            var key = "Cars";
            var cars = _memoryCache.Get<IEnumerable<Car>>(key);

            if (cars == null)
            {
                cars = _repository.GetAll();

                if (cars != null)
                {
                    _logger.LogTrace("Setting cars in cache for {CacheKey}", key);
                    _memoryCache.Set(key, cars, TimeSpan.FromMinutes(1));
                }
            }
            else
            {
                _logger.LogTrace("Cache hit for {CacheKey}", key);
            }

            return cars;
        }

        public Car GetById(int id)
        {
            var key = $"Car:{id}";
            var car = _memoryCache.Get<Car>(key);

            if (car == null)
            {
                car = _repository.GetById(id);

                if (car != null)
                {
                    _logger.LogTrace("Setting car in cache for {CacheKey}", key);
                    _memoryCache.Set(key, car, TimeSpan.FromMinutes(1));
                }
            }
            else
            {
                _logger.LogTrace("Cache hit for {CacheKey}", key);
            }

            return car;
        }
    }
}
