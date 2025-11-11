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
    public partial class CorreoEnviado : Form
    {
        public CorreoEnviado()
        {
            InitializeComponent();
            string correo = "test@duocuc.cl";
            textLabel.Text = $"Se ha enviado un correo a {correo} \npara restablecer la contraseña.";

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void labelText(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
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
