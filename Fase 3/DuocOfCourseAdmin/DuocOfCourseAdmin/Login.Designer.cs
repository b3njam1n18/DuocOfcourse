namespace DuocOfCourseAdmin
{
    partial class Login
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            LogoBox = new PictureBox();
            passwordText = new TextBox();
            correoLabel = new Label();
            loginButton = new Button();
            correoText = new TextBox();
            passwordLabel = new Label();
            passwordButton = new Button();
            panelLogin = new Panel();
            ((System.ComponentModel.ISupportInitialize)LogoBox).BeginInit();
            panelLogin.SuspendLayout();
            SuspendLayout();
            // 
            // LogoBox
            // 
            LogoBox.Anchor = AnchorStyles.Top;
            LogoBox.BackColor = Color.Transparent;
            LogoBox.BackgroundImageLayout = ImageLayout.Center;
            LogoBox.ErrorImage = null;
            LogoBox.Location = new Point(479, 51);
            LogoBox.Name = "LogoBox";
            LogoBox.Size = new Size(248, 75);
            LogoBox.SizeMode = PictureBoxSizeMode.Zoom;
            LogoBox.TabIndex = 1;
            LogoBox.TabStop = false;
            LogoBox.Click += LogoBox_Click;
            // 
            // passwordText
            // 
            passwordText.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            passwordText.Location = new Point(86, 102);
            passwordText.Name = "passwordText";
            passwordText.PasswordChar = '*';
            passwordText.Size = new Size(208, 23);
            passwordText.TabIndex = 1;
            passwordText.TextChanged += textBox2_TextChanged;
            // 
            // correoLabel
            // 
            correoLabel.Anchor = AnchorStyles.Left;
            correoLabel.AutoSize = true;
            correoLabel.Location = new Point(86, 32);
            correoLabel.Name = "correoLabel";
            correoLabel.Size = new Size(105, 15);
            correoLabel.TabIndex = 6;
            correoLabel.Text = "Correo electrónico";
            correoLabel.Click += correoLabel_Click;
            // 
            // loginButton
            // 
            loginButton.Anchor = AnchorStyles.None;
            loginButton.BackColor = Color.FromArgb(253, 181, 34);
            loginButton.Location = new Point(86, 145);
            loginButton.Name = "loginButton";
            loginButton.Size = new Size(207, 41);
            loginButton.TabIndex = 5;
            loginButton.Text = "Iniciar sesión";
            loginButton.UseVisualStyleBackColor = false;
            loginButton.Click += loginButton_Click;
            // 
            // correoText
            // 
            correoText.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            correoText.Location = new Point(86, 50);
            correoText.Name = "correoText";
            correoText.Size = new Size(208, 23);
            correoText.TabIndex = 0;
            correoText.TextChanged += textBox1_TextChanged;
            // 
            // passwordLabel
            // 
            passwordLabel.Anchor = AnchorStyles.Left;
            passwordLabel.AutoSize = true;
            passwordLabel.Location = new Point(86, 84);
            passwordLabel.Name = "passwordLabel";
            passwordLabel.Size = new Size(67, 15);
            passwordLabel.TabIndex = 7;
            passwordLabel.Text = "Contraseña";
            passwordLabel.Click += passwordLabel_Click;
            // 
            // passwordButton
            // 
            passwordButton.Anchor = AnchorStyles.None;
            passwordButton.Location = new Point(86, 192);
            passwordButton.Name = "passwordButton";
            passwordButton.Size = new Size(207, 39);
            passwordButton.TabIndex = 8;
            passwordButton.Text = "Olvidé mi contraseña";
            passwordButton.UseVisualStyleBackColor = true;
            passwordButton.Click += passwordButton_Click;
            // 
            // panelLogin
            // 
            panelLogin.Anchor = AnchorStyles.None;
            panelLogin.AutoScroll = true;
            panelLogin.BackColor = Color.White;
            panelLogin.BackgroundImageLayout = ImageLayout.None;
            panelLogin.Controls.Add(passwordButton);
            panelLogin.Controls.Add(passwordLabel);
            panelLogin.Controls.Add(correoText);
            panelLogin.Controls.Add(loginButton);
            panelLogin.Controls.Add(correoLabel);
            panelLogin.Controls.Add(passwordText);
            panelLogin.Location = new Point(409, 172);
            panelLogin.MaximumSize = new Size(700, 560);
            panelLogin.MinimumSize = new Size(385, 308);
            panelLogin.Name = "panelLogin";
            panelLogin.Size = new Size(385, 308);
            panelLogin.TabIndex = 0;
            panelLogin.Paint += panelLogin_Paint;
            // 
            // Login
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(1, 44, 86);
            ClientSize = new Size(1187, 665);
            Controls.Add(LogoBox);
            Controls.Add(panelLogin);
            MinimumSize = new Size(1203, 704);
            Name = "Login";
            Text = "Inicio de sesión";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)LogoBox).EndInit();
            panelLogin.ResumeLayout(false);
            panelLogin.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private PictureBox LogoBox;
        private TextBox passwordText;
        private Label correoLabel;
        private Button loginButton;
        private TextBox correoText;
        private Label passwordLabel;
        private Button passwordButton;
        private Panel panelLogin;
    }
}
