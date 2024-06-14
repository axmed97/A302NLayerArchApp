using Serilog;
using System.Globalization;

namespace WebAPI.Middlewares
{
    public class LocalizationMiddleware : IMiddleware
    {
        private readonly ILogger<LocalizationMiddleware> _logger;

        public LocalizationMiddleware(ILogger<LocalizationMiddleware> logger)
        {
            _logger = logger;
        }

        public Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var lang = context.Request.Headers.AcceptLanguage.FirstOrDefault();

            if (lang == "az" || lang == "en-US" || lang == "ru-RU")
            {
                var culture = new CultureInfo(lang);
                //_logger.LogInformation($"Set culture name {culture.Name}");
                Log.Information($"Set culture name {culture.Name}");
                Thread.CurrentThread.CurrentCulture = culture;
                Thread.CurrentThread.CurrentUICulture = culture;
                return next(context);
            }
            else
            {
                var culture = new CultureInfo("az");
                //_logger.LogInformation($"Set culture name {culture.Name}");
                Log.Information($"Set culture name {culture.Name}");
                Thread.CurrentThread.CurrentCulture = culture;
                Thread.CurrentThread.CurrentUICulture = culture;
                return next(context);
            }
        }
    }
}
