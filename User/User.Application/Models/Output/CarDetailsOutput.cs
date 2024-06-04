using User.Core.Enums;

namespace User.Application.Models.Output
{
    public class CarDetailsOutput
    {
        public CarDetailsOutput()
        {

        }

        public CarDetailsOutput(string id, int year, string model, string licensePlate, CarStatusEnum status)
        {
            Id = id;
            Year = year;
            Model = model;
            LicensePlate = licensePlate;
            Status = status;
        }

        public string Id { get; set; }
        public int Year { get; set; }
        public string Model { get; set; }
        public string LicensePlate { get; set; }
        public CarStatusEnum Status { get; set; }
    }
}
