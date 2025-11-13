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
        private readonly BindingSource _bsUsers = new BindingSource();
        private DataTable _tblUsers = new DataTable();
        private const string SQL_LIST_USERS = @"
            SELECT
                u.id,
                TRIM(CONCAT_WS(' ', u.first_name, u.middle_name, u.last_name, u.second_last_name)) AS nombre,
                u.email,
                r.name AS rol,
                u.is_active AS activo,
                u.created_at
            FROM users u
            JOIN roles r   ON r.id = u.role_id
            WHERE u.deleted_at IS NULL
            ORDER BY u.id DESC;";

        public Menú_principal()
        {
            InitializeComponent();
            // Engancho eventos (por si el diseñador no los dejó conectados)
            this.Load += Menú_principal_Load;
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
            textBox1.TextChanged += textBox1_TextChanged;
        }


        // =====================  UI / GRID  =====================

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


        // =====================  DATA  =====================

        private async Task CargarUsuariosAsync()
        {
            using var cn = new MySqlConnection(AppConfig.MySqlConn);
            await cn.OpenAsync();

            using var cmd = new MySqlCommand(SQL_LIST_USERS, cn);
            using var rd = await cmd.ExecuteReaderAsync();

            var tabla = new DataTable();
            tabla.Load(rd);

            _tblUsers = tabla;
            _bsUsers.DataSource = _tblUsers;   // el grid se refresca solo
        }

        // =====================  LOAD / EVENTS  =====================

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
        }


        // Click del botón dentro del grid
        // Click del botón dentro del grid (lee SIEMPRE de la fila clickeada)
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (dataGridView1.Columns[e.ColumnIndex].Name != "colModificar") return;

            // Toma el ítem enlazado a ESA fila (sirve con DataTable o BindingSource)
            if (dataGridView1.Rows[e.RowIndex].DataBoundItem is DataRowView drv)
            {
                string nombre = drv["nombre"]?.ToString() ?? "";
                // Solo el nombre, como pediste
                MessageBox.Show(nombre, "Usuario", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        // Buscador
        // Buscador: filtra por columnas reales del SELECT
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (_tblUsers == null || _tblUsers.Rows.Count == 0) return;

            string q = (textBox1.Text ?? string.Empty).Replace("'", "''");
            _tblUsers.DefaultView.RowFilter = string.IsNullOrWhiteSpace(q)
                ? string.Empty
                : $"nombre LIKE '%{q}%' OR email LIKE '%{q}%' OR rol LIKE '%{q}%'";
        }
        private void MostrarPrimerUsuario()
        {
            if (dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("Sin datos en el grid.");
                return;
            }

            if (dataGridView1.Rows[0].DataBoundItem is DataRowView drv)
            {
                MessageBox.Show(drv["nombre"]?.ToString() ?? "", "Primer usuario");
            }
        }

        // =====================  CONEXIÓN (test)  =====================

        private async Task<bool> ProbarConexionAsync()
        {
            try
            {
                using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(5));
                using var conn = new MySqlConnection(AppConfig.MySqlConn);
                await conn.OpenAsync(cts.Token);

                using var cmd = new MySqlCommand("SELECT VERSION();", conn);
                var ver = (string?)await cmd.ExecuteScalarAsync(cts.Token);

                MessageBox.Show($"Conectado a MySQL\nVersión: {ver}",
                    "Conexión OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo conectar.\n\n" + ex.Message,
                    "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
        private async Task ProbarDatosAsync()
        {
            try
            {
                using var cn = new MySqlConnector.MySqlConnection(AppConfig.MySqlConn);
                await cn.OpenAsync();

                // ¿A qué BD estoy conectado?
                var db = (string?)await new MySqlConnector.MySqlCommand("SELECT DATABASE();", cn)
                    .ExecuteScalarAsync() ?? "(desconocida)";

                // ¿Hay usuarios?
                var count = Convert.ToInt32(
                    await new MySqlConnector.MySqlCommand(
                        "SELECT COUNT(*) FROM users WHERE deleted_at IS NULL;", cn
                    ).ExecuteScalarAsync()
                );

                if (count == 0)
                {
                    MessageBox.Show($"Conexión OK a '{db}', pero 'users' está vacía.",
                        "Prueba de datos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Nombre completo del primer usuario (ajusta si cambiaste columnas)
                var sqlPrimer = @"
            SELECT TRIM(CONCAT_WS(' ', first_name, middle_name, last_name, second_last_name))
            FROM users
            WHERE deleted_at IS NULL
            ORDER BY id DESC
            LIMIT 1;";

                var primerNombre = (string?)await new MySqlConnector.MySqlCommand(sqlPrimer, cn)
                    .ExecuteScalarAsync();

                MessageBox.Show(
                    $"Conexión OK a '{db}'.\n" +
                    $"Usuarios: {count}\n" +
                    $"Primer usuario: {primerNombre}",
                    "Prueba de datos",
                    MessageBoxButtons.OK, MessageBoxIcon.Information
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fallo en la prueba de datos:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void ManagementButton_Click(object sender, EventArgs e)
        {
            ManagementButton.Enabled = false;
            try { await ProbarDatosAsync(); }
            finally { ManagementButton.Enabled = true; }
        }


        // =====================  NAVEGACIÓN / OTROS  =====================

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

        // Botón "Agregar usuario" (abre como hijo modal)
        private void button1_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            try
            {
                using var frm = new AddUser { StartPosition = FormStartPosition.CenterParent };
                frm.ShowDialog(this);
            }
            finally
            {
                this.Enabled = true;
                this.Activate();
            }
        }

        // Botón "Modificar" externo (si lo mantienes además del botón en grid)
        private void button2_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            try
            {
                using var frm = new Modificar_usuario { StartPosition = FormStartPosition.CenterParent };
                frm.ShowDialog(this);
            }
            finally
            {
                this.Enabled = true;
                this.Activate();
            }
        }
    }
}
