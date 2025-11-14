using DuocOfCourseAdmin.Infrastructure;
using MySqlConnector;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
// alias para BCrypt.Net-Next
using BCryptNet = BCrypt.Net.BCrypt;

namespace DuocOfCourseAdmin
{
    public partial class Register : Form
    {
        // Layout raíz: [Header(logo)] + [Contenido(panel1)]
        private TableLayoutPanel _formRoot;
        private Panel _header;
        private Panel _contentHost;
        private PictureBox _logo;

        private int _panelNaturalHeight;

        public Register()
        {
            InitializeComponent();
        }

        // ================== UTIL: obtener texto por nombre de control ==================
        private string GetTextOf(params string[] names)
        {
            foreach (var name in names)
            {
                var ctrl = this.Controls.Find(name, true).FirstOrDefault() as TextBoxBase;
                if (ctrl != null) return (ctrl.Text ?? string.Empty).Trim();
            }
            return string.Empty;
        }

        private TextBoxBase GetTextBox(params string[] names)
        {
            foreach (var name in names)
            {
                var ctrl = this.Controls.Find(name, true).FirstOrDefault() as TextBoxBase;
                if (ctrl != null) return ctrl;
            }
            return null;
        }

        // Validaciones
        private (bool ok, string msg) ValidarCampos(string firstName, string lastName, string email, string password)
        {
            if (string.IsNullOrWhiteSpace(firstName)) return (false, "Debes ingresar el primer nombre.");
            if (string.IsNullOrWhiteSpace(lastName)) return (false, "Debes ingresar el primer apellido.");
            if (string.IsNullOrWhiteSpace(email)) return (false, "Debes ingresar el correo.");
            if (string.IsNullOrWhiteSpace(password)) return (false, "Debes ingresar la contraseña.");
            if (password.Length < 8) return (false, "La contraseña debe tener al menos 8 caracteres.");
            return (true, "OK");
        }

        private static bool EmailVerification(string email)
        {
            if(string.IsNullOrWhiteSpace(email)) return false;
            email = email.Trim();

            // Si no tiene una arroba
            if (email.Count(c => c == '@') != 1) return false;

            // El dominio debe terminar el @duocuc.cl
            if (!email.EndsWith("@duocuc.cl", StringComparison.OrdinalIgnoreCase)) return false;

            return true;
        }

        // Registro
        private async Task<long> CreateUserAsync(
            string firstName, string middleName, string lastName, string secondLastName,
            string email, string plainPassword, int roleId = 1)
        {
            using var cn = new MySqlConnection(AppConfig.MySqlConn);
            await cn.OpenAsync();

            // Comprueba si el correo ya existe
            using (var chk = new MySqlCommand(
                "SELECT 1 FROM users WHERE deleted_at IS NULL AND LOWER(email)=LOWER(@e) LIMIT 1;", cn))
            {
                chk.Parameters.AddWithValue("@e", email);
                var exists = await chk.ExecuteScalarAsync();
                if (exists != null) throw new Exception("Ya existe un usuario con ese correo.");
            }

            // Hashear
            var hash = BCryptNet.HashPassword(plainPassword, workFactor: 11);

            // Insertar en users y auth_credentials
            using var tx = await cn.BeginTransactionAsync();
            try
            {
                // Insert en users
                using (var insUser = new MySqlCommand(@"
                    INSERT INTO users
                        (first_name, middle_name, last_name, second_last_name, email, is_active, role_id, created_at)
                    VALUES
                        (@fn, @mn, @ln, @sl, @e, 1, @rid, NOW());", cn, (MySqlTransaction)tx))
                {
                    insUser.Parameters.AddWithValue("@fn", firstName);
                    insUser.Parameters.AddWithValue("@mn", (object?)middleName ?? DBNull.Value);
                    insUser.Parameters.AddWithValue("@ln", lastName);
                    insUser.Parameters.AddWithValue("@sl", (object?)secondLastName ?? DBNull.Value);
                    insUser.Parameters.AddWithValue("@e", email);
                    insUser.Parameters.AddWithValue("@rid", roleId);
                    await insUser.ExecuteNonQueryAsync();
                }

                long userId;
                using (var getId = new MySqlCommand("SELECT LAST_INSERT_ID();", cn, (MySqlTransaction)tx))
                    userId = Convert.ToInt64(await getId.ExecuteScalarAsync());

                // Insert en auth_credentials
                using (var insCred = new MySqlCommand(@"
                    INSERT INTO auth_credentials (user_id, password_hash, created_at)
                    VALUES (@uid, @hash, NOW());", cn, (MySqlTransaction)tx))
                {
                    insCred.Parameters.AddWithValue("@uid", userId);
                    insCred.Parameters.AddWithValue("@hash", hash);
                    await insCred.ExecuteNonQueryAsync();
                }

                await tx.CommitAsync();
                return userId;
            }
            catch
            {
                await tx.RollbackAsync();
                throw;
            }
        }

        // Botón para registrar el usuario
        private async void RegisterButton_Click(object sender, EventArgs e)
        {
            // Lee directo de los controles
            var firstName = RegNombreTextbox?.Text?.Trim() ?? "";
            var middleName = string.IsNullOrWhiteSpace(MidNameText?.Text) ? null : MidNameText.Text.Trim();
            var lastName = lastNameText?.Text?.Trim() ?? "";
            var secondLastName = string.IsNullOrWhiteSpace(secondLastNameTextbox?.Text) ? null : secondLastNameTextbox.Text.Trim();
            var email = RegCorreoTextbox?.Text?.Trim() ?? "";
            var password = RegPasswordTextbox?.Text?.Trim() ?? "";

            // Validación mínima
            if (string.IsNullOrWhiteSpace(firstName)) { MessageBox.Show("Debes ingresar el primer nombre."); return; }
            if (string.IsNullOrWhiteSpace(lastName)) { MessageBox.Show("Debes ingresar el primer apellido."); return; }
            if (string.IsNullOrWhiteSpace(email)) { MessageBox.Show("Debes ingresar el correo."); return; }

            // Validaciones del correo
            if (!(email.EndsWith("@duocuc.cl", StringComparison.OrdinalIgnoreCase)
                  && email.Count(c => c == '@') == 1))
            {
                MessageBox.Show("El correo electrónico ingresado no es válido.",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(password) || password.Length < 8)
            {
                MessageBox.Show("La contraseña debe tener al menos 8 caracteres.");
                return;
            }

            var btn = (sender as Control); if (btn != null) btn.Enabled = false;
            try
            {
                long newId = await CreateUserAsync(
                    firstName: firstName,
                    middleName: middleName,
                    lastName: lastName,
                    secondLastName: secondLastName,
                    email: email,
                    plainPassword: password,
                    roleId: 1
                );

                MessageBox.Show($"Usuario registrado con éxito.");
                var next = new Login();
                next.StartPosition = FormStartPosition.CenterScreen;
                next.FormClosed += (_, __) => this.Close();
                next.Show();
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo registrar el usuario:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally { if (btn != null) btn.Enabled = true; }
        }


        private void panel1_Paint(object sender, PaintEventArgs e) { }
        private void label2_Click(object sender, EventArgs e) { }
        private void SchoolLabel_Click(object sender, EventArgs e) { }
        private void Register_Load(object sender, EventArgs e) { }
        private void LogoBox_Click(object sender, EventArgs e) { }
        private void RegNameLabel_Click(object sender, EventArgs e) { }
        private void RegCorreoLabel_Click(object sender, EventArgs e) { }
        private void CarreraLabel_Click(object sender, EventArgs e) { }

        // Estos no se si los usaremos al final, probablemente los borremos.
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) { }
        private void checkBox1_CheckedChanged(object sender, EventArgs e) { }

        // Inputs
        private void RegNombreTextbox_TextChanged(object sender, EventArgs e) { }
        private void MidNameText_TextChanged(object sender, EventArgs e) { }
        private void lastNameText_TextChanged(object sender, EventArgs e) { }
        private void textBox1_TextChanged(object sender, EventArgs e) { }
    }
}
