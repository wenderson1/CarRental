using RentalCompany.Core.Enums;

namespace RentalCompany.Application.Models.Output
{
    public class OrderOutput
    {
        public string Id { get; set; }
        public string IdCustomer { get; set; }
        public string IdCar{ get; set; }
        public OrderStatusEnum Status { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime ExpectedReturnDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public double Price { get; set; }

        public OrderOutput(string id, string idCar, string idCustomer, OrderStatusEnum status, DateTime startDate, DateTime expectedReturnDate, DateTime? returnDate, double price)
        {
            Id = id;
            IdCustomer = idCustomer;
            IdCar = idCar;
            Status = status;
            StartDate = startDate;
            ExpectedReturnDate = expectedReturnDate;
            ReturnDate = returnDate;
            Price = price;
        }

    }
}
