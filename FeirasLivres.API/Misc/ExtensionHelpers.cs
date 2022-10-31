using Serilog;

namespace FeirasLivres.Api.Misc
{
    public static class ExtensionHelpers
    {
        public static ILoggingBuilder SetSerilog(this ILoggingBuilder loggingBuilder, WebApplicationBuilder builder)
        {
            // Enum.TryParse(builder.Configuration["SeriLog:RollingInterval"], out RollingInterval serlogRollingInterval);
            var logger = new LoggerConfiguration()
                .ReadFrom.Configuration(builder.Configuration).Enrich.FromLogContext()
                // .MinimumLevel.Error()
                // .WriteTo.File(builder.Configuration["SeriLog:OutputFile"], rollingInterval: serlogRollingInterval)
                .CreateLogger();

            builder.Services.AddScoped<LogEndpointFilter>();

            return loggingBuilder.AddSerilog(logger);
        }
    }
}
