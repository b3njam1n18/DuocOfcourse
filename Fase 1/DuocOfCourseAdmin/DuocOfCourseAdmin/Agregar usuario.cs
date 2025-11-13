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
    public partial class AddUser : Form
    {
        public AddUser()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void AddUser_Load(object sender, EventArgs e)
        {

        }

        // Nota: Al cerrarlo, cierra todo el programa, debe cerrarse solo o no tener X roja arriba.
        private void button1_Click(object sender, EventArgs e)
        {
            if (controlEmpty(AddNameTextbox.Text))
            {
                sendMessage("Error", "Debe ingresar un nombre.", MessageBoxButtons.OK);
                return;
            }
            if (controlEmpty(AddEmailTextbox.Text))
            {
                sendMessage("Error", "Debe ingresar un correo electrónico.", MessageBoxButtons.OK);
                return;
            }
            if (controlEmpty(AddPasswordTextbox.Text))
            {
                sendMessage("Error", "Debe ingresar una clave.", MessageBoxButtons.OK);
                return;
            }
            else
            {
                // *********************************** Agregar al usuario si es válido. *********************************** 
                this.Hide();
            }
        }

        // Validación de campo vacío.
        private bool controlEmpty(string param) => string.IsNullOrWhiteSpace(param);

        // Notificación.
        private void sendMessage(string caption, string message, MessageBoxButtons buttons)
        {
            MessageBox.Show(message, caption, buttons);
        }

        private void AddNameTextbox_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void AddEmailTextbox_TextChanged(object sender, EventArgs e)
        {
            
        }


        private void AddPasswordTextbox_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

                                        // Validar selección.
        private void RoleComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }
        private void comboBoxCarrera_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            
        }
    }
}
