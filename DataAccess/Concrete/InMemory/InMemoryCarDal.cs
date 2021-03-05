using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccess.Abstract;
using Entities.Concrete;

namespace DataAccess.Concrete.InMemory
{
    public class InMemoryCarDal : ICarDal
    {
        private List<Car> _cars;

        public InMemoryCarDal()
        {
            _cars = new List<Car>
            {
                new Car {CarId = 1, BrandId = 1, ColorId = 1, DailyPrice = 100, ModelYear = 2010, Description = "Sports car"},
                new Car {CarId = 2, BrandId = 1, ColorId = 1, DailyPrice = 200, ModelYear = 2011, Description = "Company Car"},
                new Car {CarId = 3, BrandId = 2, ColorId = 1, DailyPrice = 300, ModelYear = 2012, Description = "Convertible"},
                new Car {CarId = 4, BrandId = 3, ColorId = 2, DailyPrice = 400, ModelYear = 2013, Description = "Coupe"},
                new Car {CarId = 5, BrandId = 3, ColorId = 2, DailyPrice = 500, ModelYear = 2014, Description = "Sports Utility Vehicle"}
            };
        }

        public void Add(Car car)
        {
            _cars.Add(car);
        }

        public void Update(Car car)
        {
            Car carToUpdate = _cars.SingleOrDefault(c => c.CarId == car.CarId);

            carToUpdate.CarId = car.CarId;
            carToUpdate.BrandId = car.BrandId;
            carToUpdate.ColorId = car.ColorId;
            carToUpdate.DailyPrice = car.DailyPrice;
            carToUpdate.Description = car.Description;
            carToUpdate.ModelYear = car.ModelYear;
        }

        public void Delete(Car car)
        {
            Car carToDelete = _cars.SingleOrDefault(c => c.CarId == car.CarId);
            _cars.Remove(carToDelete);
        }

        public List<Car> GetAll()
        {
            return _cars;
        }

        public Car GetById(int carId)
        {
            throw new NotImplementedException();
        }
        
        public List<Car> GetAllBrand(int brandId)
        {
            return _cars.Where(c => c.BrandId == brandId).ToList();
        }
    }
}
