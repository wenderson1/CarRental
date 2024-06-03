using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalCompany.Core.Entities
{
    public class RentalPlan
    {
        public string Id { get; set; }
        public string IdCustomer { get; set; }
        public string IdCar { get; set; }
        public double ExpectedPrice { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime ExpectedReturnDate { get; set; }

        public RentalPlan(string idCustomer, string idCar, double expectedPrice, DateTime startDate, DateTime expectedReturnDate)
        {
            Id = Guid.NewGuid().ToString();
            IdCustomer = idCustomer;
            IdCar = idCar;
            ExpectedPrice = expectedPrice;
            StartDate = startDate;
            ExpectedReturnDate = expectedReturnDate;
        }
    }
}
