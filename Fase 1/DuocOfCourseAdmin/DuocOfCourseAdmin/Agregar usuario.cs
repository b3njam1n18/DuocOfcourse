using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySqlConnector;
using DuocOfCourseAdmin.Infrastructure;
using BCryptNet = BCrypt.Net.BCrypt;

namespace DuocOfCourseAdmin
{
    public partial class AddUser : Form
    {
        public AddUser()
        {
            InitializeComponent();
            AddPasswordTextbox.UseSystemPasswordChar = true;
        }

        private void AddUser_Load(object sender, EventArgs e)
        {
            RoleComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            RoleComboBox.Items.Clear();
            RoleComboBox.Items.AddRange(new object[]
            {
                "Administrador", // id = 1
                "Estudiante",    // id = 2
                "Docente"        // id = 3
            });
            RoleComboBox.SelectedIndex = 0;

            // Por defectoe está habilitado
            StatusCheckBox.Checked = true;
        }

        // Validación de correo electrónico
        private static bool IsValidDuocEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email)) return false;
            email = email.Trim();

            // Una sola arroba
            int at = email.IndexOf('@');
            if (at <= 0 || at != email.LastIndexOf('@')) return false;

            // Debe terminar en @duocuc.cl
            return email.EndsWith("@duocuc.cl", StringComparison.OrdinalIgnoreCase);
        }

        private bool controlEmpty(string s) => string.IsNullOrWhiteSpace(s);

        private void sendMessage(string caption, string message, MessageBoxButtons buttons)
            => MessageBox.Show(message, caption, buttons);

        private static async Task<long> CreateUserAsync(
            string firstName,
            string? middleName,
            string lastName,
            string? secondLastName,
            string email,
            string plainPassword,
            int roleId,
            bool isActive)
        {
            using var cn = new MySqlConnection(AppConfig.MySqlConn);
            await cn.OpenAsync();

            using var tx = await cn.BeginTransactionAsync();

            try
            {
                // Evita correos duplicados
                using (var chk = new MySqlCommand(
                    @"SELECT COUNT(*) FROM users 
                      WHERE deleted_at IS NULL AND LOWER(email)=LOWER(@e);",
                    cn, (MySqlTransaction)tx))
                {
                    chk.Parameters.AddWithValue("@e", email);
                    var count = Convert.ToInt32(await chk.ExecuteScalarAsync());
                    if (count > 0) throw new Exception("El correo ya existe.");
                }

                // Insert en users
                long newId;
                using (var ins = new MySqlCommand(
                    @"INSERT INTO users
                        (first_name, middle_name, last_name, second_last_name, email, role_id, is_active, created_at)
                      VALUES
                        (@fn, @mn, @ln, @sln, @e, @role, @active, NOW());
                      SELECT LAST_INSERT_ID();",
                    cn, (MySqlTransaction)tx))
                {
                    ins.Parameters.AddWithValue("@fn", firstName);
                    ins.Parameters.AddWithValue("@mn", (object?)middleName ?? DBNull.Value);
                    ins.Parameters.AddWithValue("@ln", lastName);
                    ins.Parameters.AddWithValue("@sln", (object?)secondLastName ?? DBNull.Value);
                    ins.Parameters.AddWithValue("@e", email);
                    ins.Parameters.AddWithValue("@role", roleId);
                    ins.Parameters.AddWithValue("@active", isActive);

                    var obj = await ins.ExecuteScalarAsync();
                    newId = Convert.ToInt64(obj);
                }

                // Hash de contraseña
                var hash = BCryptNet.HashPassword(plainPassword, workFactor: 11);

                // Insert credencial
                using (var cred = new MySqlCommand(
                    @"INSERT INTO auth_credentials(user_id, password_hash, created_at)
                      VALUES(@uid, @hash, NOW());",
                    cn, (MySqlTransaction)tx))
                {
                    cred.Parameters.AddWithValue("@uid", newId);
                    cred.Parameters.AddWithValue("@hash", hash);
                    await cred.ExecuteNonQueryAsync();
                }

                await tx.CommitAsync();
                return newId;
            }
            catch
            {
                await tx.RollbackAsync();
                throw;
            }
        }

        // Botón Guardar
        private async void button1_Click(object sender, EventArgs e)
        {
            // Validaciones mínimas
            if (controlEmpty(NombreTextbox.Text))
            { sendMessage("Error", "Debe ingresar un nombre.", MessageBoxButtons.OK); return; }

            if (controlEmpty(lastNameText.Text))
            { sendMessage("Error", "Debe ingresar su primer apellido.", MessageBoxButtons.OK); return; }

            if (controlEmpty(secondLastNameTextbox.Text))
            { sendMessage("Error", "Debe ingresar su segundo apellido.", MessageBoxButtons.OK); return; }

            if (controlEmpty(correoTextBox.Text))
            { sendMessage("Error", "Debe ingresar un correo electrónico.", MessageBoxButtons.OK); return; }

            if (!IsValidDuocEmail(correoTextBox.Text))
            { sendMessage("Error", "Correo electrónico inválido.", MessageBoxButtons.OK); return; }

            if (controlEmpty(AddPasswordTextbox.Text) || AddPasswordTextbox.Text.Length < 8)
            { sendMessage("Error", "Debe ingresar una clave de al menos 8 caracteres.", MessageBoxButtons.OK); return; }

            if (RoleComboBox.SelectedIndex < 0)
            { sendMessage("Error", "Debe seleccionar un rol.", MessageBoxButtons.OK); return; }

            // Mapear rol por orden: 1=Admin, 2=Estudiante, 3=Docente
            int roleId = RoleComboBox.SelectedIndex + 1;

            string firstName = NombreTextbox.Text.Trim();
            string? middleName = string.IsNullOrWhiteSpace(MidNameText.Text) ? null : MidNameText.Text.Trim();
            string lastName = lastNameText.Text.Trim();
            string? secondLastName = string.IsNullOrWhiteSpace(secondLastNameTextbox.Text) ? null : secondLastNameTextbox.Text.Trim();
            string email = correoTextBox.Text.Trim();
            string password = AddPasswordTextbox.Text; // ya validada longitud
            bool isActive = StatusCheckBox.Checked;

            // Guardar
            var btn = (sender as Control); if (btn != null) btn.Enabled = false;
            try
            {
                long id = await CreateUserAsync(
                    firstName, middleName, lastName, secondLastName,
                    email, password, roleId, isActive);

                MessageBox.Show($"Usuario creado correctamente (ID {id}).",
                    "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Cierra el diálogo
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo crear el usuario:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (btn != null) btn.Enabled = true;
            }
        }

        private void label3_Click(object sender, EventArgs e) { }
        private void comboBoxCarrera_SelectedIndexChanged(object sender, EventArgs e) { }
        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e) { }
        private void NombreTextbox_TextChanged(object sender, EventArgs e) { }
        private void MidNameText_TextChanged(object sender, EventArgs e) { }
        private void lastNameText_TextChanged(object sender, EventArgs e) { }
        private void secondLastNameTextbox_TextChanged(object sender, EventArgs e) { }
        private void correoTextBox_TextChanged(object sender, EventArgs e) { }
        private void AddPasswordTextbox_TextChanged(object sender, EventArgs e) { }
        private void RoleComboBox_SelectedIndexChanged(object sender, EventArgs e) { }
        private void StatusCheckBox_CheckedChanged(object sender, EventArgs e) { }
        private void label1_Click(object sender, EventArgs e) { }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            AddPasswordTextbox.UseSystemPasswordChar = !checkBox1.Checked;
        }
    }
}
