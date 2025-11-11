using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace DuocOfCourseAdmin
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private bool controlLoginEmpty(string param) => string.IsNullOrWhiteSpace(param);

        private void sendMessage(string caption, string message, MessageBoxButtons buttons)
        {
            MessageBox.Show(message, caption, buttons);
        }

        private void Form1_Load(object sender, EventArgs e) 
        {
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e) 
        {
            
        }

        private void textBox2_TextChanged(object sender, EventArgs e) 
        {
            
        }

        private void label1_Click_1(object sender, EventArgs e) 
        {
            
        }

        private void panelLogin_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void correoLabel_Click(object sender, EventArgs e)
        {

        }

        private void passwordLabel_Click(object sender, EventArgs e)
        {

        }

        // Botón para Recuperar contraseña.
        private void passwordButton_Click(object sender, EventArgs e)
        {
            // Para redirigir al Form destino.
            var next = new RecuperarPassword();
            next.StartPosition = FormStartPosition.CenterScreen;
            next.FormClosed += (_, __) => this.Close();
            next.Show();
            this.Hide();
        }

        // Link de Registro.
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Para redirigir al Form destino.
            var next = new Register();
            next.StartPosition = FormStartPosition.CenterScreen;
            next.FormClosed += (_, __) => this.Close();
            next.Show();
            this.Hide();
        }

        private void loginButton_Click(object sender, EventArgs e)
        {
            // Para redirigir al Form destino.
            var next = new Menú_principal();
            next.StartPosition = FormStartPosition.CenterScreen;
            next.FormClosed += (_, __) => this.Close();
            next.Show();
            this.Hide();

            //// Validación para que los campos no estén vacíos.
            //if (controlLoginEmpty(correoText.Text))
            //{
            //    sendMessage("Error", "Debe ingresar un correo electrónico", MessageBoxButtons.OK);
            //    return;
            //}
            //if (controlLoginEmpty(passwordText.Text))
            //{
            //    sendMessage("Error", "Debe ingresar una contraseña", MessageBoxButtons.OK);
            //    return;
            //}
        }

    }
}
