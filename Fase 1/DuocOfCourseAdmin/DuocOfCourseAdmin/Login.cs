using DuocOfCourseAdmin.Infrastructure;
using MySqlConnector;
// Alias para BCrypt.Net-Next

namespace DuocOfCourseAdmin
{
    public partial class Login : Form
    {
        private const int AdminRoleId = 1;

        public Login()
        {
            InitializeComponent();
            LogoBox.Image = Properties.Resources.DuocOfCourse;
        }

        private bool controlLoginEmpty(string param) => string.IsNullOrWhiteSpace(param);

        private void sendMessage(string caption, string message, MessageBoxButtons buttons)
        {
            MessageBox.Show(message, caption, buttons);
        }

        // Los elementos de la UI
        private void Form1_Load(object sender, EventArgs e) { }
        private void textBox1_TextChanged(object sender, EventArgs e) { }
        private void textBox2_TextChanged(object sender, EventArgs e) { }
        private void label1_Click_1(object sender, EventArgs e) { }
        private void panelLogin_Paint(object sender, PaintEventArgs e) { }
        private void correoLabel_Click(object sender, EventArgs e) { }
        private void passwordLabel_Click(object sender, EventArgs e) { }

        // Link para redirigir a Recuperar contraseña.
        private void passwordButton_Click(object sender, EventArgs e)
        {
            var next = new RecuperarPassword();
            next.StartPosition = FormStartPosition.CenterScreen;
            next.FormClosed += (_, __) => this.Close();
            next.Show();
            this.Hide();
        }

        // Link para redirigir a Registro.
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var next = new Register();
            next.StartPosition = FormStartPosition.CenterScreen;
            next.FormClosed += (_, __) => this.Close();
            next.Show();
            this.Hide();
        }

        // Botón Login
        private async void loginButton_Click(object sender, EventArgs e)
        {
            if (controlLoginEmpty(correoText.Text))
            { sendMessage("Error", "Debe ingresar un correo electrónico", MessageBoxButtons.OK); return; }
            if (controlLoginEmpty(passwordText.Text))
            { sendMessage("Error", "Debe ingresar una contraseña", MessageBoxButtons.OK); return; }

            loginButton.Enabled = false;
            try
            {
                var (ok, msg, firstName, userId) = await TryLoginAsync(
                    correoText.Text.Trim(),
                    passwordText.Text
                );
                if (!ok)
                {
                    sendMessage("Acceso denegado", msg, MessageBoxButtons.OK);
                    return;
                }

                var next = new Menú_principal(firstName, userId);
                // Pasamos el primer nombre para el header
                next.StartPosition = FormStartPosition.CenterScreen;
                next.FormClosed += (_, __) => this.Close();
                next.Show();
                this.Hide();
            }
            finally { loginButton.Enabled = true; }
        }

        /// Compara email y contraseña dentro de la BD
        private async Task<(bool ok, string message, string firstName, long userId)> TryLoginAsync(string emailInput, string plainPassword)
        {
            try
            {
                using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(8));
                using var cn = new MySqlConnection(AppConfig.MySqlConn);
                await cn.OpenAsync(cts.Token);

                const string SQL = @"
                            SELECT
                                u.id,
                                u.first_name,
                                u.is_active,
                                u.role_id,
                                ac.password_hash
                            FROM users u
                            JOIN auth_credentials ac ON ac.user_id = u.id
                            WHERE u.deleted_at IS NULL
                              AND LOWER(u.email) = LOWER(@email)
                            LIMIT 1;";

                using var cmd = new MySqlCommand(SQL, cn);
                cmd.Parameters.AddWithValue("@email", emailInput);

                using var rd = await cmd.ExecuteReaderAsync(cts.Token);
                if (!await rd.ReadAsync(cts.Token))
                    return (false, "Credenciales inválidas.", "", 0);


                bool isActive = rd.GetBoolean(rd.GetOrdinal("is_active"));
                if (!isActive)
                    return (false, "El usuario no se encuentra habilitado.", "", 0);

                int roleId = rd.GetInt32(rd.GetOrdinal("role_id"));

                string hash = rd.GetString(rd.GetOrdinal("password_hash"));
                string firstName = rd["first_name"] as string ?? "Usuario";
                long userId = rd.GetInt64(rd.GetOrdinal("id"));

                if (!BCrypt.Net.BCrypt.Verify(plainPassword, hash))
                    return (false, "Credenciales inválidas.", "", 0);

                rd.Close();
                using (var upd = new MySqlCommand(
                    "UPDATE auth_credentials SET last_login_at = NOW() WHERE user_id = @uid;", cn))
                {
                    upd.Parameters.AddWithValue("@uid", userId);
                    await upd.ExecuteNonQueryAsync(cts.Token);
                }

                return (true, "OK", firstName, userId);
            }
            catch (Exception ex)
            {
                return (false, "Error de conexión: " + ex.Message, "", 0);
            }
        }

        private void LogoBox_Click(object sender, EventArgs e)
        {
        }
    }
}
