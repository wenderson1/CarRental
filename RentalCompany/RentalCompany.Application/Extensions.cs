using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalCompany.Application
{
    public static class Extensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IDeliveryManService, DeliveryManService>();
            services.AddScoped<IOrderService, OrderService>();
            wwwwwwwf
            return services;
        }
        public static IServiceCollection AddSubscriber(this IServiceCollection services)
        {
            services.AddHostedService<MotorcyclesAvailablesSubscriber>();
            return services;
        }

        public static string ToDashCase(this string text)
        {
            if (text == null)
                throw new ArgumentNullException(nameof(text));

            if (text.Length < 2)
                return text;

            var sb = new StringBuilder();
            sb.Append(char.ToLowerInvariant(text[0]));
            for (int i = 1; i < text.Length; i++)
            {
                char c = text[i];
                if (char.IsUpper(c))
                {
                    sb.Append('-');
                    sb.Append(char.ToLowerInvariant(c));
                }
                else
                {
                    sb.Append(c);
                }
            }

            Console.WriteLine($"ToDashCase" + sb.ToString());

            return sb.ToString();
        }
    }
}
