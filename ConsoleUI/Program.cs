using System;
using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            CarManager carManager = new CarManager(new EfCarDal());
            BrandManager brandManager = new BrandManager(new EfBrandDal());
            ColorManager colorManager = new ColorManager(new EfColorDal());

            //CarsBrandId(carManager);
            //CarsColorId(carManager);
            //CarsDailyPrice(carManager);
            //CarsGetAll(carManager);

            //BrandGetAll(brandManager);

            foreach (var car in carManager.GetCarDetails())
            {
                Console.WriteLine($"CarName: {car.CarName}, BrandName: {car.BrandName}, ColorName: {car.ColorName}");
            }
        }

        private static void BrandGetAll(BrandManager brandManager)
        {
            foreach (var brand in brandManager.GetAll())
            {
                Console.WriteLine(brand.BrandName);
            }
        }

        private static void CarsGetAll(CarManager carManager)
        {
            carManager.Add(new Car()
            {
                CarName = "Mondeo", DailyPrice = 550, BrandId = 8, Description = "Dizel",
                ColorId = 5, ModelYear = 2018
            });

            foreach (var car in carManager.GetAll())
            {
                Console.WriteLine("Car Name: {0}", car.CarName);
            }
        }

        private static void CarsDailyPrice(CarManager carManager)
        {
            foreach (var car in carManager.GetByDailyPrice(2, 400))
            {
                Console.WriteLine(car.Description + " DailyPrice: " + car.DailyPrice);
            }
        }

        private static void CarsColorId(CarManager carManager)
        {
            foreach (var car in carManager.GetCarsByColorId(1))
            {
                Console.WriteLine(car.Description + " ColorId: " + car.ColorId);
            }
        }

        private static void CarsBrandId(CarManager carManager)
        {
            foreach (var car in carManager.GetCarsByBrandId(2))
            {
                Console.WriteLine(car.Description + " BrandId: " + car.BrandId);
            }
        }
    }
}
