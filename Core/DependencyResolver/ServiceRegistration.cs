using Core.Utilities.Message.Abstract;
using Core.Utilities.Message.Concrete;
using Core.Utilities.Security.Abstract;
using Core.Utilities.Security.Concrete;
using Microsoft.Extensions.DependencyInjection;

namespace Core.DependencyResolver
{
    public static class ServiceRegistration
    {
        public static void AddCoreService(this IServiceCollection services)
        {
            services.AddScoped<ITokenService, TokenManager>();
            services.AddScoped<IMessageService, MessageService>();
        }
    }
}
