using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySqlConnector;
using DuocOfCourseAdmin.Infrastructure;

//Correo electrónico
using MailKit.Net.Smtp;
using MimeKit;
using MailKit.Security;
using System.Security.Cryptography;

namespace DuocOfCourseAdmin
{
    public partial class RecuperarPassword : Form
    {
        public RecuperarPassword()
        {
            InitializeComponent();
        }

        // Sólo probar con nuestros correos personales (nicolas.canales.m.nc@gmail.com),
        // Duoc no permite por reglamento que les mandemos correos así
        // No vaya a existir el correo y se lo mande a alguien más ***************************************

        private async void button1_Click(object sender, EventArgs e)
        {
            string email = textBox1.Text.Trim();

            // Validar correo vacío
            if (string.IsNullOrWhiteSpace(email))
            {
                MessageBox.Show("Ingrese un correo válido.");
                return;
            }

            // Validar si existe en BD
            bool existe = await ExisteUsuarioAsync(email);
            if (!existe)
            {
                MessageBox.Show("El correo electrónico ingresado no existe.");
                return;
            }

            // Enviar correo
            try
            {
                await EnviarCorreoAsync(email);

                // 4. Abrir siguiente pantalla
                var next = new CorreoEnviado(email);
                next.StartPosition = FormStartPosition.CenterScreen;
                next.FormClosed += (_, __) => this.Close();
                next.Show();
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error enviando correo:\n" + ex.Message);
            }
        }

        // Validar si el usuario existe
        private async Task<bool> ExisteUsuarioAsync(string email)
        {
            using var cn = new MySqlConnection(AppConfig.MySqlConn);
            await cn.OpenAsync();

            const string SQL = @"SELECT COUNT(*) FROM users WHERE deleted_at IS NULL AND LOWER(email)=LOWER(@em);";

            using var cmd = new MySqlCommand(SQL, cn);
            cmd.Parameters.AddWithValue("@em", email);

            var result = Convert.ToInt32(await cmd.ExecuteScalarAsync());
            return result > 0;
        }

        // Validación de correo
        private async Task<bool> VerificarCorreoAsync(string email)
        {
            using var cn = new MySqlConnection(AppConfig.MySqlConn);
            await cn.OpenAsync();

            const string SQL = @"
                SELECT COUNT(*)
                    FROM users
                    WHERE deleted_at IS NULL
                    AND LOWER (email) = LOWER(@email)
                    LIMIT 1;";
            using var cmd = new MySqlCommand(SQL, cn);
            cmd.Parameters.AddWithValue("@email", email);

            int count = Convert.ToInt32(await cmd.ExecuteScalarAsync());
            return count > 0;
        }

        private void RecuperarPassword_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        // Helper pq aspnet no funciona en escritorio, solo web
        private string Base64UrlEncode(byte[] bytes)
        {
            return Convert.ToBase64String(bytes)
                .Replace("+", "-")
                .Replace("/", "_")
                .Replace("=", "");
        }

        private async Task EnviarCorreoAsync(string destino)
        {
            // 1. Obtener usuario desde BD
            int userId;
            string firstName;

            using (var cn = new MySqlConnection(AppConfig.MySqlConn))
            {
                await cn.OpenAsync();

                var cmd = new MySqlCommand(
                    "SELECT id, first_name FROM users WHERE deleted_at IS NULL AND LOWER(email)=LOWER(@em) LIMIT 1;",
                    cn
                );
                cmd.Parameters.AddWithValue("@em", destino);

                using var rd = await cmd.ExecuteReaderAsync();
                if (!rd.Read())
                    throw new Exception("El correo no existe.");

                userId = rd.GetInt32("id");
                firstName = rd.GetString("first_name");
            }

            // 2. Invalidar token previo
            using (var cn = new MySqlConnection(AppConfig.MySqlConn))
            {
                await cn.OpenAsync();
                var cmd = new MySqlCommand(
                    "UPDATE password_reset_tokens SET is_used = 1 WHERE user_id=@u AND is_used=0;",
                    cn
                );
                cmd.Parameters.AddWithValue("@u", userId);
                await cmd.ExecuteNonQueryAsync();
            }

            // 3. Crear token nuevo (versión compatible WinForms)
            var raw = RandomNumberGenerator.GetBytes(64);
            var token = Base64UrlEncode(raw);
            var expires = DateTime.UtcNow.AddHours(1);

            // 4. Guardar token nuevo en la BD
            using (var cn = new MySqlConnection(AppConfig.MySqlConn))
            {
                await cn.OpenAsync();
                var cmd = new MySqlCommand(
                    @"INSERT INTO password_reset_tokens (user_id, token, expires_at, is_used)
              VALUES (@u, @t, @e, 0);",
                    cn
                );
                cmd.Parameters.AddWithValue("@u", userId);
                cmd.Parameters.AddWithValue("@t", token);
                cmd.Parameters.AddWithValue("@e", expires);
                await cmd.ExecuteNonQueryAsync();
            }

            // 5. URL real desde config (igual que la web)
            string frontendUrl = AppConfig.FrontendBaseUrl;
            string resetLink = $"{frontendUrl}/reset-password?token={token}";

            // 6. Crear correo con el mismo formato que la página
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("DuocOfCourse", "nicolas.canales.m.nc@gmail.com"));
            message.To.Add(new MailboxAddress("", destino));
            message.Subject = "Restablecer contraseña";

            message.Body = new TextPart("html")
            {
                Text = $@"
            Hola {firstName},<br/><br/>
            Haz clic en el siguiente enlace para restablecer tu contraseña:<br/>
            <a href='{resetLink}'>Restablecer contraseña</a><br/><br/>
            Este enlace expirará en 1 hora."
            };

            // 7. Envío SMTP
            using var client = new SmtpClient();
            try
            {
                await client.ConnectAsync("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
                await client.AuthenticateAsync("nicolas.canales.m.nc@gmail.com", "gpnw wgdb syaf xufd");
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo enviar el correo: " + ex.Message);
            }
        }


    }
}
