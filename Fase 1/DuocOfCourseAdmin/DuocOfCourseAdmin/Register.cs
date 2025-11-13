using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

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


        // --- Handlers generados por el diseñador (pueden quedar vacíos) ---
        private void panel1_Paint(object sender, PaintEventArgs e) { }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) { }
        private void label2_Click(object sender, EventArgs e) { }
        private void SchoolLabel_Click(object sender, EventArgs e) { }
        private void Register_Load(object sender, EventArgs e) { }
        private void LogoBox_Click(object sender, EventArgs e) { }

        private void RegNameLabel_Click(object sender, EventArgs e)
        {

        }

        private void RegCorreoLabel_Click(object sender, EventArgs e)
        {

        }

        private void CarreraLabel_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void RegisterButton_Click(object sender, EventArgs e)
        {
            // Para redirigir al Form destino.
            var next = new Login();
            next.StartPosition = FormStartPosition.CenterScreen;
            next.FormClosed += (_, __) => this.Close();
            next.Show();
            this.Hide();
        }
    }
}
