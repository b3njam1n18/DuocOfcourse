using Microsoft.Extensions.Configuration;
using System;

namespace DuocOfCourseAdmin.Infrastructure
{
    public static class AppConfig
    {
        private static readonly IConfigurationRoot _cfg =
            new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

        // Ej: AppConfig.MySqlConn te devuelve la cadena de conexión
        public static string MySqlConn =>
            _cfg.GetConnectionString("MySql")
            ?? throw new InvalidOperationException("Falta ConnectionStrings:MySql en appsettings.json");

        // Si quieres leer otras claves: AppConfig.Get("Ui:Theme")
        public static string? Get(string key) => _cfg[key];


        // URL del frontend de la página web
        public static string FrontendBaseUrl = "http://localhost:3000";
    }
}
