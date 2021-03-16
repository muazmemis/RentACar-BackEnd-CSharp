using System;
using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class RentalValidator : AbstractValidator<Rental>
    {
        public RentalValidator()
        {
            RuleFor(r => r.CarId).NotEmpty();
            RuleFor(r => r.CustomerId).NotEmpty();
            RuleFor(r => r.RentDate).NotEmpty();
            RuleFor(r => r.ReturnDate).NotEmpty();
            RuleFor(r => r.RentDate).NotEmpty().GreaterThan(DateTime.Now).WithMessage("Geçmiş tarihte kiralama yapılamaz.");
            RuleFor(r => r.ReturnDate).GreaterThanOrEqualTo(r => r.RentDate).WithMessage("Aracın teslim tarihi kiralama tarihinden önce olamaz.");
        }
    }
}
