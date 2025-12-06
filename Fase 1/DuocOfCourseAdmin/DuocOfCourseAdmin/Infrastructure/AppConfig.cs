using System;

namespace DuocOfCourseAdmin.Infrastructure
{
    public static class AppConfig
    {
        // Cadena de conexión fija dentro del ejecutable
        public static string MySqlConn =
            "Server=localhost;Port=3306;Database=appduocofcourse;User ID=admin;Password=nico12321!;SslMode=None;";
        
        // Por si acaso
        public static string FrontendBaseUrl = "http://localhost:3000";

        public static string? Get(string key) => null;
    }
}
