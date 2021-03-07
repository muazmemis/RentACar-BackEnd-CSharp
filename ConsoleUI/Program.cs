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

            //foreach(var car in carManager.GetCarsByBrandId(2))
            //{
            //    Console.WriteLine(car.Description + " BrandId: " + car.BrandId);
            //}

            //foreach(var car in carManager.GetCarsByColorId(1))
            //{
            //    Console.WriteLine(car.Description + " ColorId: " + car.ColorId);
            //}

            //foreach(var car in carManager.GetByDailyPrice(2, 400))
            //{
            //    Console.WriteLine(car.Description + " DailyPrice: " + car.DailyPrice);
            //}

            carManager.Add(new Car() { CarName = "Mondeo", DailyPrice = 550, BrandId = 8, Description = "Dizel", 
                ColorId = 5, ModelYear = 2018 });

            foreach(var car in carManager.GetAll())
            {
                Console.WriteLine("Car Name: {0}",car.CarName);
            }
        }
    }
}
