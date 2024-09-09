namespace RentalCompany.Application.Models.Input
{
    public class CreateOrderInput
    {
        public string IdCar { get; set; } = string.Empty;
        public string IdCustomer { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime ExpectedReturnDate { get; set; }
    }
}
