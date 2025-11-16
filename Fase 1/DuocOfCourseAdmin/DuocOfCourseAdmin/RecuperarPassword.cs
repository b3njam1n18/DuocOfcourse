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
    public partial class RecuperarPassword : Form
    {
        public RecuperarPassword()
        {
            InitializeComponent();
        }

        // Sólo probar con nuestros correos, no vaya a existir el correo y se lo mande a alguien más ***************************************
        private void button1_Click(object sender, EventArgs e)
        {
            // Para redirigir al Form destino.
            var next = new CorreoEnviado();
            next.StartPosition = FormStartPosition.CenterScreen;
            next.FormClosed += (_, __) => this.Close();
            next.Show();
            this.Hide();
        }
    }
}
