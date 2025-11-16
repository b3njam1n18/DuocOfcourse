using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySqlConnector;
using DuocOfCourseAdmin.Infrastructure;
using BCryptNet = BCrypt.Net.BCrypt;

namespace DuocOfCourseAdmin
{
    public partial class Modificar_usuario : Form
    {
        private readonly long _userId;

        public Modificar_usuario(long userId)
        {
            InitializeComponent();
            _userId = userId;

            AddPasswordTextbox.UseSystemPasswordChar = true;

            if (RoleComboBox.Items.Count == 0)
                RoleComboBox.Items.AddRange(new[] { "Administrador", "Estudiante", "Docente" });

            this.Load += Modificar_usuario_Load;
        }

        // Helpers
        private bool controlEmpty(string s) => string.IsNullOrWhiteSpace(s);
        private void send(string cap, string msg) => MessageBox.Show(msg, cap, MessageBoxButtons.OK, MessageBoxIcon.Information);
        private static bool IsValidDuocEmail(string email) =>
            !string.IsNullOrWhiteSpace(email) &&
            email.Count(c => c == '@') == 1 &&
            email.EndsWith("@duocuc.cl", StringComparison.OrdinalIgnoreCase);

        private async void Modificar_usuario_Load(object sender, EventArgs e)
        {
            try { await CargarUsuarioAsync(_userId); }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar el usuario:\n" + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }
        }

        private async Task CargarUsuarioAsync(long id)
        {
            const string SQL = @"
                SELECT u.id,u.first_name,u.middle_name,u.last_name,u.second_last_name,
                       u.email,u.role_id,u.is_active
                FROM users u
                WHERE u.deleted_at IS NULL AND u.id=@id
                LIMIT 1;";

            using var cn = new MySqlConnection(AppConfig.MySqlConn);
            await cn.OpenAsync();

            using var cmd = new MySqlCommand(SQL, cn);
            cmd.Parameters.AddWithValue("@id", id);

            using var rd = await cmd.ExecuteReaderAsync();
            if (!await rd.ReadAsync())
            {
                MessageBox.Show("Usuario no encontrado o eliminado.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                Close();
                return;
            }

            NombreTextbox.Text = rd["first_name"] as string ?? "";
            MidNameText.Text = rd["middle_name"] as string ?? "";
            lastNameText.Text = rd["last_name"] as string ?? "";
            secondLastNameTextbox.Text = rd["second_last_name"] as string ?? "";
            correoTextBox.Text = rd["email"] as string ?? "";

            int roleId = Convert.ToInt32(rd["role_id"]);
            RoleComboBox.SelectedIndex = Math.Max(0, Math.Min(2, roleId - 1));

            StatusCheckBox.Checked = Convert.ToBoolean(rd["is_active"]);
            AddPasswordTextbox.Text = string.Empty; // No ve la contraseña, pero puede modificarla
        }

        private void NombreTextbox_TextChanged(object sender, EventArgs e) { }
        private void MidNameText_TextChanged(object sender, EventArgs e) { }
        private void lastNameText_TextChanged(object sender, EventArgs e) { }
        private void secondLastNameTextbox_TextChanged(object sender, EventArgs e) { }
        private void correoTextBox_TextChanged(object sender, EventArgs e) { }
        private void AddPasswordTextbox_TextChanged(object sender, EventArgs e) { }
        private void RoleComboBox_SelectedIndexChanged(object sender, EventArgs e) { }
        private void StatusCheckBox_CheckedChanged(object sender, EventArgs e) { }

        // Botón Guardar
        private async void button1_Click_1(object sender, EventArgs e)
        {
            this.SaveButton.Click += new System.EventHandler(this.button1_Click_1);
            // Lectura de inputs
            var firstName = NombreTextbox.Text.Trim();
            var middleName = string.IsNullOrWhiteSpace(MidNameText.Text) ? null : MidNameText.Text.Trim();
            var lastName = lastNameText.Text.Trim();
            var secondLastName = string.IsNullOrWhiteSpace(secondLastNameTextbox.Text) ? null : secondLastNameTextbox.Text.Trim();
            var email = correoTextBox.Text.Trim();
            var newPassword = AddPasswordTextbox.Text;
            var isActive = StatusCheckBox.Checked;
            var roleId = (RoleComboBox.SelectedIndex >= 0) ? RoleComboBox.SelectedIndex + 1 : 0;

            // Validaciones
            if (controlEmpty(firstName)) { send("Error", "Debe ingresar un nombre."); return; }
            if (controlEmpty(lastName)) { send("Error", "Debe ingresar un apellido."); return; }
            if (!IsValidDuocEmail(email)) { send("Error", "El correo debe terminar en @duocuc.cl y contener solo una @."); return; }
            if (roleId == 0) { send("Error", "Debe seleccionar un rol."); return; }
            if (!string.IsNullOrEmpty(newPassword) && newPassword.Length < 8)
            { send("Error", "La nueva contraseña debe tener al menos 8 caracteres."); return; }

            var btn = sender as Control; if (btn != null) btn.Enabled = false;

            try
            {
                using var cn = new MySqlConnection(AppConfig.MySqlConn);
                await cn.OpenAsync();

                using var tx = await cn.BeginTransactionAsync();

                // Verificar que el email no exista (Aún no estoy seguro de si debería cambiar su correo o no, pero estará la opción)
                // Quizás en el futuro quitemos esta función*
                using (var chk = new MySqlCommand(@"
                    SELECT COUNT(1)
                    FROM users
                    WHERE deleted_at IS NULL
                      AND LOWER(email)=LOWER(@em) AND id<>@id;", cn, (MySqlTransaction)tx))
                {
                    chk.Parameters.AddWithValue("@em", email);
                    chk.Parameters.AddWithValue("@id", _userId);
                    var exists = Convert.ToInt32(await chk.ExecuteScalarAsync()) > 0;
                    if (exists)
                    {
                        await tx.RollbackAsync();
                        MessageBox.Show("El correo ya está registrado por otro usuario.", "Duplicado",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                // Actualiza la tabla users
                using (var upUser = new MySqlCommand(@"
                    UPDATE users
                    SET first_name=@fn,
                        middle_name=@mn,
                        last_name=@ln,
                        second_last_name=@sln,
                        email=@em,
                        role_id=@rid,
                        is_active=@act,
                        updated_at=NOW()
                    WHERE id=@id;", cn, (MySqlTransaction)tx))
                {
                    upUser.Parameters.AddWithValue("@fn", firstName);
                    upUser.Parameters.AddWithValue("@mn", (object?)middleName ?? DBNull.Value);
                    upUser.Parameters.AddWithValue("@ln", lastName);
                    upUser.Parameters.AddWithValue("@sln", (object?)secondLastName ?? DBNull.Value);
                    upUser.Parameters.AddWithValue("@em", email);
                    upUser.Parameters.AddWithValue("@rid", roleId);
                    upUser.Parameters.AddWithValue("@act", isActive);
                    upUser.Parameters.AddWithValue("@id", _userId);

                    await upUser.ExecuteNonQueryAsync();
                }

                // Si ingresó nueva contraseña, actualizar/insertar auth_credentials
                if (!string.IsNullOrEmpty(newPassword))
                {
                    var hash = BCryptNet.HashPassword(newPassword, workFactor: 11);

                    using var upd = new MySqlCommand(@"
                        UPDATE auth_credentials
                           SET password_hash=@h, updated_at=NOW()
                         WHERE user_id=@uid;", cn, (MySqlTransaction)tx);
                    upd.Parameters.AddWithValue("@h", hash);
                    upd.Parameters.AddWithValue("@uid", _userId);
                    var rows = await upd.ExecuteNonQueryAsync();

                    if (rows == 0)
                    {
                        using var ins = new MySqlCommand(@"
                            INSERT INTO auth_credentials (user_id,password_hash,created_at)
                            VALUES (@uid,@h,NOW());", cn, (MySqlTransaction)tx);
                        ins.Parameters.AddWithValue("@uid", _userId);
                        ins.Parameters.AddWithValue("@h", hash);
                        await ins.ExecuteNonQueryAsync();
                    }
                }

                await tx.CommitAsync();

                MessageBox.Show("Usuario actualizado correctamente.", "OK",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo guardar los cambios:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (btn != null) btn.Enabled = true;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            AddPasswordTextbox.UseSystemPasswordChar = !checkBox1.Checked;
        }
    }
}
