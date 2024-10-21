using Microsoft.Extensions.Configuration;

namespace ac_insertdocs_inbase.UnitTests
{
    public static class UtToolkit
    {
        public static void BuildConfig(IConfigurationBuilder builder)
        {
            builder.SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
                .AddEnvironmentVariables();
        }
    }
}