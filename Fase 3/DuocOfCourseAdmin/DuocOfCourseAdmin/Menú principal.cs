using System;
using System.Data;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySqlConnector;
using DuocOfCourseAdmin.Infrastructure;

namespace DuocOfCourseAdmin
{
    public partial class Menú_principal : Form
    {
        private readonly string _firstName;
        private bool _editDialogOpen = false;
        private long LoggedUserId { get; }

        private readonly BindingSource _bsUsers = new BindingSource();
        private DataTable _tblUsers = new DataTable();
        private const string SQL_LIST_USERS = @"
            SELECT
                u.id,
                TRIM(CONCAT_WS(' ', u.first_name, u.middle_name, u.last_name, u.second_last_name)) AS nombre,
                u.email,
                r.name AS rol,
                u.is_active AS activo,
                u.created_at,
                cc.name AS Carrera,
                s.name AS Escuela
            FROM users u
            JOIN roles r ON r.id = u.role_id
            JOIN course_categories cc ON cc.id = u.category_id
            JOIN schools s ON s.id = cc.school_id
            WHERE u.deleted_at IS NULL
            ORDER BY u.id DESC;";


        public Menú_principal(string firstName, long userId)
        {
            InitializeComponent();
            _firstName = string.IsNullOrWhiteSpace(firstName) ? "Usuario" : firstName.Trim();
            LoggedUserId = userId;
            this.Load += Menú_principal_Load;
            textBox1.TextChanged += textBox1_TextChanged;
        }

                            // UI 

        // Cargar la tabla de usuarios con los datos
        private void ConfigurarGrid()
        {
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            dataGridView1.Columns.Clear();

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colId",
                DataPropertyName = "id",
                HeaderText = "ID",
                Visible = false
            });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colNombre",
                DataPropertyName = "nombre",
                HeaderText = "Nombre",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                FillWeight = 40
            });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colEmail",
                DataPropertyName = "email",
                HeaderText = "Email",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                FillWeight = 35
            });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colRol",
                DataPropertyName = "rol",
                HeaderText = "Rol",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colCarrera",
                DataPropertyName = "Carrera",
                HeaderText = "Carrera",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            });
            dataGridView1.Columns.Add(new DataGridViewCheckBoxColumn
            {
                Name = "colActivo",
                DataPropertyName = "activo",
                HeaderText = "Activo",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colCreado",
                DataPropertyName = "created_at",
                HeaderText = "Creado",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            });
            dataGridView1.Columns.Add(new DataGridViewButtonColumn
            {
                Name = "colModificar",
                HeaderText = "",
                Text = "Modificar",
                UseColumnTextForButtonValue = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
            });

            dataGridView1.DataSource = _bsUsers;
        }


        // DATA
        private async Task CargarUsuariosAsync()
        {
            using var cn = new MySqlConnection(AppConfig.MySqlConn);
            await cn.OpenAsync();

            using var cmd = new MySqlCommand(SQL_LIST_USERS, cn);
            using var rd = await cmd.ExecuteReaderAsync();

            var tabla = new DataTable();
            tabla.Load(rd);

            _tblUsers = tabla;
            _bsUsers.DataSource = _tblUsers;   // Se refresca sola
        }

        // LOAD
        // Carga inicial del listado
        private async void Menú_principal_Load(object sender, EventArgs e)
        {
            try
            {
                ConfigurarGrid();
                await CargarUsuariosAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error cargando usuarios:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // Carga el mensaje de bienvenida
            label1.Text = $"Bienvenido/a, {_firstName}";

        }

        // Para poder interactuar con el grid y modificar los usuarios
        private async void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (dataGridView1.Columns[e.ColumnIndex].Name != "colModificar") return;

            if (dataGridView1.Rows[e.RowIndex].DataBoundItem is DataRowView drv)
            {
                long id = Convert.ToInt64(drv["id"]);
                using (var frm = new Modificar_usuario(id)
                {
                    StartPosition = FormStartPosition.CenterParent,
                    LoggedUserId = this.LoggedUserId
                })
                {
                    if (frm.ShowDialog(this) == DialogResult.OK)
                        await CargarUsuariosAsync();
                }

            }
        }

        // Buscador
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (_tblUsers == null || _tblUsers.Rows.Count == 0) return;

            string q = (textBox1.Text ?? string.Empty).Replace("'", "''");
            _tblUsers.DefaultView.RowFilter = string.IsNullOrWhiteSpace(q)
                ? string.Empty
                : $"nombre LIKE '%{q}%' OR email LIKE '%{q}%' OR rol LIKE '%{q}%'";
        }

                                // Navegación
        // Botón Cerrar sesión
        private void LogoutButton_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show(this, "¿Desea cerrar sesión?", "Confirmación",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign);

            if (result == DialogResult.Yes)
            {
                var next = new Login { StartPosition = FormStartPosition.CenterScreen };
                next.FormClosed += (_, __) => this.Close();
                next.Show();
                this.Hide();
            }
        }

        // Botón Agregar usuario
        private async void button1_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            try
            {
                using var frm = new AddUser { StartPosition = FormStartPosition.CenterParent };

                if (frm.ShowDialog(this) == DialogResult.OK)
                {
                    await CargarUsuariosAsync();
                }
            }
            finally
            {
                this.Enabled = true;
                this.Activate();
            }

        }

        private void Menú_principal_Load_1(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void ManagementButton_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {
            
        }
    }
}