using Microsoft.Extensions.DependencyInjection;
using System.Text;
using User.Application.Interfaces;
using User.Application.Services;
using User.Application.Subscriber;

namespace User.Application
{
    public static class Extensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<ICarService, CarService>();

            return services;
        }

        public static IServiceCollection AddSubscriber(this IServiceCollection services)
        {
            services.AddHostedService<UpdateUsedCarSubscriber>();
            services.AddHostedService<UpdateReturnCarSubscriber>();

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
