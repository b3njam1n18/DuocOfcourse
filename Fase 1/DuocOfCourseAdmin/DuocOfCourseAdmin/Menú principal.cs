using MySqlX.XDevAPI.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DuocOfCourseAdmin
{
    public partial class Menú_principal : Form
    {
        public Menú_principal()
        {
            InitializeComponent();
        }

        private void Menú_principal_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }


        private void ManagementButton_Click(object sender, EventArgs e)
        {

        }

        private void LogoutButton_Click(object sender, EventArgs e)
        {
            // Parámetros del mensaje.
            string message = "¿Desea cerrar sesión?";
            string caption = "Confirmación";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;

            // Mensaje.
            result = MessageBox.Show(this, message, caption, buttons,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button1,
            MessageBoxOptions.RightAlign);

            // Resultado de la opción que elige.
            if (result == DialogResult.Yes)
            {
                // Para redirigir al Form destino.
                var next = new Login();
                next.StartPosition = FormStartPosition.CenterScreen;
                next.FormClosed += (_, __) => this.Close();
                next.Show();
                this.Hide();
            }
        }

            // A diferencia de las otras funciones de redirección, estas dos se abren como hijos de este Form,
            // por lo que si se cierran no se cierra el programa entero.
        // BOTÓN AGREGAR.
        private void button1_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            using (var frm = new AddUser())
            {
                frm.StartPosition = FormStartPosition.CenterParent;
                frm.ShowDialog(this);
            }
            this.Enabled = true;
            this.Activate();
        }

        // BOTÓN MODIFICAR.
        private void button2_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            using (var frm = new Modificar_usuario())
            {
                frm.StartPosition = FormStartPosition.CenterParent;
                frm.ShowDialog(this);
            }
            this.Enabled = true;
            this.Activate();
        }
    }
}
