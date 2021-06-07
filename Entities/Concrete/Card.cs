using Core.Entities;

namespace Entities.Concrete
{
    public class Card : IEntity
    {
        public int Id { get; set; }
        public string CardName { get; set; }
        public string CardNumber { get; set; }
        public string ExpirationDate { get; set; }
        public int CardCvv { get; set; }
        public decimal CardMoney { get; set; }
    }
}
