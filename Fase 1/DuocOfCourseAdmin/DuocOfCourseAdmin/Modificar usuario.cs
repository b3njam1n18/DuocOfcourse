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
    public partial class Modificar_usuario : Form
    {
        public Modificar_usuario()
        {
            InitializeComponent();
        }

        // Validación de campo vacío.
        private bool controlEmpty(string param) => string.IsNullOrWhiteSpace(param);

        // Notificación.
        private void sendMessage(string caption, string message, MessageBoxButtons buttons)
        {
            MessageBox.Show(message, caption, buttons);
        }

        private void Modificar_usuario_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (controlEmpty(changeNameTextbox.Text))
            {
                sendMessage("Error", "Debe ingresar un nombre.", MessageBoxButtons.OK);
                return;
            }
            if (controlEmpty(changeEmailTextbox.Text))
            {
                sendMessage("Error", "Debe ingresar un correo electrónico.", MessageBoxButtons.OK);
                return;
            }
            if (controlEmpty(ChangePasswordTextbox.Text))
            {
                sendMessage("Error", "Debe ingresar una contraseña.", MessageBoxButtons.OK);
                return;
            }
            else
            {
                // *********************************** Modificar al usuario si es válido. *********************************** 
                this.Hide();
            }
        }
    }
}
