using Microsoft.Extensions.Configuration;

namespace Holmes_Services.Data_Access
{
    public static class DbConnector
    {
        private static readonly IConfiguration? _configuration;
        public static string Connection = _configuration.GetConnectionString("HolmesContext");

        public static string GetConnection() => Connection;
    }
}
