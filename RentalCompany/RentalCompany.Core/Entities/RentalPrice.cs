using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalCompany.Core.Entities
{
    public static class RentalPrices
    {
        public const double SevenDays = 30.00;
        public const double FifteenDays = 28.00;
        public const double ThirtyDays = 22.00;
        public const double FortyFiveDays = 20.00;
        public const double FiftyDays = 18.00;
        public const double AdditionalDaily = 50.00;
        public const double PenaltySevenDays = SevenDays + (SevenDays * 0.2);
        public const double PenaltyGreaterThanSevenDays = 0.4;


        public static double CalculateTotalAmountWithoutPenalty(DateTime startDate, DateTime returnDate)
        {
            int totalDays = (int)(returnDate - startDate).TotalDays;

            totalDays = totalDays == 0 ? 1 : totalDays;

            if (totalDays >= 0 && totalDays < 7)
                return AdditionalDaily * totalDays;

            if (totalDays >= 7 && totalDays < 15)
                return (7 * SevenDays) + ((totalDays - 7) * AdditionalDaily);

            if (totalDays >= 15 && totalDays < 30)
                return (15 * FifteenDays) + ((totalDays - 15) * AdditionalDaily);

            if (totalDays >= 30 && totalDays < 45)
                return (30 * ThirtyDays) + ((totalDays - 30) * AdditionalDaily);

            if (totalDays >= 50)
                return (50 * FiftyDays) + ((totalDays - 50) * AdditionalDaily);

            return 0.00;
        }

        public static double CalculateTotalAmountWithPenalty(DateTime startDate, DateTime expectedReturnDate, DateTime returnDate)
        {

            if (returnDate <= expectedReturnDate)
            {
                return 0.00;
            }

            int totalRentalDuration = (int)(expectedReturnDate - startDate).TotalDays;
            int lateDays = (int)(expectedReturnDate - returnDate).TotalDays;

            var priceWithoutPenalty = CalculateTotalAmountWithoutPenalty(startDate, expectedReturnDate);

            if (totalRentalDuration >= 7 && totalRentalDuration < 15)
            {
                return priceWithoutPenalty + (lateDays * PenaltySevenDays);
            }
            else
            {
                return priceWithoutPenalty + (lateDays * (PenaltyGreaterThanSevenDays + (PenaltyGreaterThanSevenDays * 0.4)));
            }
        }
    }
}
