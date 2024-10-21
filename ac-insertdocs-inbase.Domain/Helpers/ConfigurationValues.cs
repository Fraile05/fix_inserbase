using Microsoft.Extensions.Configuration;

namespace ac_insertdocs_inbase.Domain.Helpers
{
    public class ConfigurationValues
    {
        private readonly IConfiguration _configuration;

        public string db_host { get; set; } = string.Empty;
        public string db_name { get; set; } = string.Empty;
        public string db_user { get; set; } = string.Empty;
        public string db_password { get; set; } = string.Empty;
        public string connection_string { get; set; } = string.Empty;

        public ConfigurationValues(IConfiguration configuration)
        {
            _configuration = configuration;
            SetValues();
            connection_string = $"Host={db_host};Database={db_name};Username={db_user};Password={db_password}; CommandTimeout=1024; Timeout=300;";
        }

        private void SetValues()
        {
            if (Environment.GetEnvironmentVariable("DB_HOST") != null)
            {
                db_host = Environment.GetEnvironmentVariable("DB_HOST")!;
                db_name = Environment.GetEnvironmentVariable("DB_DATABASE")!;
                db_user = Environment.GetEnvironmentVariable("DB_USER")!;
                db_password = Environment.GetEnvironmentVariable("DB_PASSWORD")!;
            }
            else
            {
                string ambiente = _configuration.GetValue<string>("ambiente")!;
                var section = _configuration.GetSection(ambiente);

                db_host = section.GetValue<string>("DB_HOST")!;
                db_name = section.GetValue<string>("DB_DATABASE")!;
                db_user = section.GetValue<string>("DB_USER")!;
                db_password = section.GetValue<string>("DB_PASSWORD")!;
            }
        }
    }
}
