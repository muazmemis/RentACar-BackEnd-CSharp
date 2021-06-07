using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfRentalDal : EfEntityRepositoryBase<Rental, MyDatabaseContext>, IRentalDal
    {
        public List<RentalDetailDto> GetRentalDetails(Expression<Func<RentalDetailDto, bool>> filter = null)
        {
            using (MyDatabaseContext context = new MyDatabaseContext())
            {
                var result = from r in context.Rentals
                             join car in context.Cars on r.CarId equals car.CarId
                             join brand in context.Brands on car.BrandId equals brand.BrandId
                             join color in context.Colors on car.ColorId equals color.ColorId
                             join customer in context.Customers on r.CustomerId equals customer.Id
                             join user in context.Users on customer.UserId equals user.Id

                             select new RentalDetailDto
                             {
                                 Id = r.Id,
                                 CarId = car.CarId,
                                 CustomerId = customer.Id,
                                 CarName = car.CarName,
                                 ReturnDate = r.ReturnDate,
                                 RentDate = r.RentDate,
                                 DailyPrice = car.DailyPrice,
                                 BrandName = brand.BrandName,
                                 ColorName = color.ColorName,
                                 CompanyName = customer.CompanyName,
                                 CustomerName = user.FirstName + " " + user.LastName,
                             };

                return filter == null ? result.ToList() : result.Where(filter).ToList();
            }
        }
    }
}