using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalCompany.Application.Models.Input
{
    public class RentalPlanInput
    {
        public DateTime StartDate { get; set; }
        public DateTime ExpectedReturnDate { get; set; }
    }
}
