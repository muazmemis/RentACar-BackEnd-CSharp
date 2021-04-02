using System;
using System.Collections.Generic;
using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Business;
using Core.Utilities.Helpers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace Business.Concrete
{
    public class CarImageManager : ICarImageService
    {
        private readonly ICarImageDal _carImageDal;

        public CarImageManager(ICarImageDal carImageDal)
        {
            _carImageDal = carImageDal;
        }

        [ValidationAspect(typeof(CarImageValidator))]
        public IResult Add(IFormFile file, CarImage carImage)
        {
            IResult result = BusinessRules.Run(CheckIfCarImageLimitExceded(carImage.CarId));

            if(result != null)
            {
                return result;
            }

            carImage.ImagePath = FileHelper.Add(file);
            carImage.CreationTime = DateTime.Now;
            _carImageDal.Add(carImage);

            return new SuccessResult(Messages.CarImageAdded);
        }

        public IResult Update(IFormFile file, CarImage carImage)
        {
            carImage.ImagePath = FileHelper.Update(Environment.CurrentDirectory + @"\wwwroot\"
                + _carImageDal.Get(c => c.Id == carImage.Id).ImagePath, file);
            carImage.CreationTime = DateTime.Now;
            _carImageDal.Update(carImage);

            return new SuccessResult();
        }

        public IResult Delete(CarImage carImage)
        {
            FileHelper.Delete(Environment.CurrentDirectory + @"\wwwroot\" + carImage.ImagePath);
            _carImageDal.Delete(carImage);

            return new SuccessResult();
        }

        public IDataResult<List<CarImage>> GetAll()
        {
            return new SuccessDataResult<List<CarImage>>(_carImageDal.GetAll());
        }

        public IDataResult<CarImage> GetById(int carImageId)
        {
            return new SuccessDataResult<CarImage>(_carImageDal.Get(c => c.Id == carImageId));
        }

        public IDataResult<List<CarImage>> GetByCarId(int carId)
        {
            IResult result = BusinessRules.Run(CheckIfAnyCarImageExists(carId));

            if(result != null)
            {
                return new ErrorDataResult<List<CarImage>>(result.Message);
            }

            return new SuccessDataResult<List<CarImage>>(CheckIfAnyCarImageExists(carId).Data);
        }

        private IResult CheckIfCarImageLimitExceded(int carId)
        {
            var carImageCount = _carImageDal.GetAll(c => c.CarId == carId).Count;
            if(carImageCount > 5)
            {
                return new ErrorResult(Messages.CarImageLimitExceded);
            }

            return new SuccessResult();
        }

        private IDataResult<List<CarImage>> CheckIfAnyCarImageExists(int carId)
        {
            string path = @"\CarImages\DefaultCarImage.jpg";
            var result = _carImageDal.GetAll(c => c.CarId == carId).Any();

            if(result)
            {
                return new SuccessDataResult<List<CarImage>>(_carImageDal.GetAll(c => c.CarId == carId));
            }

            List<CarImage> carImage = new List<CarImage>();
            carImage.Add(new CarImage { CarId = carId, ImagePath = path, CreationTime = DateTime.Now });

            return new SuccessDataResult<List<CarImage>>(carImage);
        }
    }
}
