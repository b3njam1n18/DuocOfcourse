using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySqlConnector;
using DuocOfCourseAdmin.Infrastructure;
// Alias para BCrypt.Net-Next
using BCryptNet = BCrypt.Net.BCrypt;

namespace DuocOfCourseAdmin
{
    public partial class Login : Form
    {
        private const int AdminRoleId = 1;

        public Login()
        {
            InitializeComponent();
        }

        private bool controlLoginEmpty(string param) => string.IsNullOrWhiteSpace(param);

        private void sendMessage(string caption, string message, MessageBoxButtons buttons)
        {
            MessageBox.Show(message, caption, buttons);
        }

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

        // Login
        private async void loginButton_Click(object sender, EventArgs e)
        {
            // Validaciones por si están vacíos los campos
            if (controlLoginEmpty(correoText.Text))
            {
                sendMessage("Error", "Debe ingresar un correo electrónico", MessageBoxButtons.OK);
                return;
            }
            if (controlLoginEmpty(passwordText.Text))
            {
                sendMessage("Error", "Debe ingresar una contraseña", MessageBoxButtons.OK);
                return;
            }

            loginButton.Enabled = false;
            try
            {
                var (ok, msg) = await TryLoginAsync(
                    correoText.Text.Trim(),
                    passwordText.Text
                );

                if (!ok)
                {
                    sendMessage("Acceso denegado", msg, MessageBoxButtons.OK);
                    return;
                }

                var next = new Menú_principal();
                next.StartPosition = FormStartPosition.CenterScreen;
                next.FormClosed += (_, __) => this.Close();
                next.Show();
                this.Hide();
            }
            finally
            {
                loginButton.Enabled = true;
            }
        }

        /// Compara email y contraseña dentro de la BD
        private async Task<(bool ok, string message)> TryLoginAsync(string emailInput, string plainPassword)
        {
            try
            {
                using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(8));
                using var cn = new MySqlConnection(AppConfig.MySqlConn);
                await cn.OpenAsync(cts.Token);

                const string SQL = @"
                SELECT
                    u.id,
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
                    return (false, "Credenciales inválidas.");

                bool isActive = rd.GetBoolean(rd.GetOrdinal("is_active"));
                if (!isActive)
                    return (false, "Usuario inactivo.");

                string hash = rd.GetString(rd.GetOrdinal("password_hash"));
                long userId = rd.GetInt64(rd.GetOrdinal("id"));
                int roleId = rd.GetInt32(rd.GetOrdinal("role_id"));

                // Verificar contraseña
                if (!BCryptNet.Verify(plainPassword, hash))
                    return (false, "Credenciales inválidas.");

                // Restringir a Administrador (role_id = 1)
                if (roleId != AdminRoleId)
                    return (false, "Acceso no permitido.");

                rd.Close();
                using (var upd = new MySqlCommand(
                    "UPDATE auth_credentials SET last_login_at = NOW() WHERE user_id = @uid;", cn))
                {
                    upd.Parameters.AddWithValue("@uid", userId);
                    await upd.ExecuteNonQueryAsync(cts.Token);
                }

                return (true, "OK");
            }
            catch (Exception ex)
            {
                return (false, "Error de conexión: " + ex.Message);
            }
        }


        private async Task SetTempPasswordAsync(string email, string newPlainPassword)
        {
            using var cn = new MySqlConnection(AppConfig.MySqlConn);
            await cn.OpenAsync();

            // Busca el user_id por email
            long? userId = null;
            using (var find = new MySqlCommand(
                "SELECT u.id FROM users u WHERE u.deleted_at IS NULL AND LOWER(u.email)=LOWER(@e) LIMIT 1;", cn))
            {
                find.Parameters.AddWithValue("@e", email);
                var obj = await find.ExecuteScalarAsync();
                if (obj == null)
                    throw new Exception("Usuario no existe.");
                userId = Convert.ToInt64(obj);
            }

            // Generar hash bcrypt
            var hash = BCryptNet.HashPassword(newPlainPassword, workFactor: 11);

            // Actualizar o insertar credencial
            using var tx = await cn.BeginTransactionAsync();
            try
            {
                using (var upd = new MySqlCommand(@"
            UPDATE auth_credentials SET password_hash=@h, updated_at=NOW()
            WHERE user_id=@uid;", cn, (MySqlTransaction)tx))
                {
                    upd.Parameters.AddWithValue("@h", hash);
                    upd.Parameters.AddWithValue("@uid", userId);
                    var rows = await upd.ExecuteNonQueryAsync();

                    if (rows == 0)
                    {
                        using var ins = new MySqlCommand(@"
                    INSERT INTO auth_credentials(user_id,password_hash,created_at)
                    VALUES(@uid,@h,NOW());", cn, (MySqlTransaction)tx);
                        ins.Parameters.AddWithValue("@uid", userId);
                        ins.Parameters.AddWithValue("@h", hash);
                        await ins.ExecuteNonQueryAsync();
                    }
                }

                await tx.CommitAsync();
            }
            catch
            {
                await tx.RollbackAsync();
                throw;
            }
        }



        // TESTEO DE CAMBIO DE CONTRASEÑA PARA EL LOGIN, EL INICIO DE RECUPERAR LA CONTRASEÑA
        private async void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // Esto debería ser un parámetro: correoText y passwordText ***
                await SetTempPasswordAsync("ni.canalesm@duocuc.cl", "duoc123");
                MessageBox.Show("Contraseña reseteada.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

        }
    }
}
