using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySqlConnector;
using DuocOfCourseAdmin.Infrastructure;

//Correo electrónico
using MailKit.Net.Smtp;
using MimeKit;
using MailKit.Security;


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

        private async Task EnviarCorreoAsync(string destino)
        {
            // Creación del mensaje
            var message = new MimeMessage();

            // Destinatario
            message.From.Add(new MailboxAddress("DuocOfCourseAdmin", "nicolas.canales.m.nc@gmail.com")); // Por ahora usaré el mío
            message.To.Add(new MailboxAddress("", destino));

            // Asunto
            message.Subject = "Recuperación de contraseña DuocOfCourseAdmin";

            // Cuerpo
            message.Body = new TextPart("html")
            {
                Text = $@"
                    <p>Hola,<p>
                    <p>Recibimos una solicitud para restablecer tu contraseña.<p>
                    <p>Si fuiste tú, haz click en el siguiente enlace:<p>
            <p><a href='https://www.youtube.com/watch?v=dQw4w9WgXcQ'>Restablecer contraseña</a></p>" // RECORATORIO: CAMBIAR EL LINK POR LA PÁGINA, NO EL RICKROLL !!!
            };
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
