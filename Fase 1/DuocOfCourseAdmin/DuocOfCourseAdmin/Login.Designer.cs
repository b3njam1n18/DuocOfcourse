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
            panelLogin = new Panel();
            button1 = new Button();
            registerLink = new LinkLabel();
            passwordButton = new Button();
            passwordLabel = new Label();
            correoText = new TextBox();
            loginButton = new Button();
            correoLabel = new Label();
            passwordText = new TextBox();
            LogoBox = new PictureBox();
            panelLogin.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)LogoBox).BeginInit();
            SuspendLayout();
            // 
            // panelLogin
            // 
            panelLogin.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panelLogin.AutoSize = true;
            panelLogin.BackColor = Color.White;
            panelLogin.Controls.Add(button1);
            panelLogin.Controls.Add(registerLink);
            panelLogin.Controls.Add(passwordButton);
            panelLogin.Controls.Add(passwordLabel);
            panelLogin.Controls.Add(correoText);
            panelLogin.Controls.Add(loginButton);
            panelLogin.Controls.Add(correoLabel);
            panelLogin.Controls.Add(passwordText);
            panelLogin.Location = new Point(440, 158);
            panelLogin.MaximumSize = new Size(700, 560);
            panelLogin.MinimumSize = new Size(334, 308);
            panelLogin.Name = "panelLogin";
            panelLogin.Size = new Size(334, 308);
            panelLogin.TabIndex = 0;
            panelLogin.Paint += panelLogin_Paint;
            // 
            // button1
            // 
            button1.Location = new Point(30, 262);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 9;
            button1.Text = "button1";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // registerLink
            // 
            registerLink.Anchor = AnchorStyles.None;
            registerLink.AutoSize = true;
            registerLink.LinkColor = Color.FromArgb(0, 60, 173);
            registerLink.Location = new Point(159, 250);
            registerLink.Name = "registerLink";
            registerLink.Size = new Size(128, 15);
            registerLink.TabIndex = 0;
            registerLink.TabStop = true;
            registerLink.Text = "Registro nuevo usuario";
            registerLink.LinkClicked += linkLabel1_LinkClicked;
            // 
            // passwordButton
            // 
            passwordButton.Anchor = AnchorStyles.None;
            passwordButton.Location = new Point(57, 192);
            passwordButton.Name = "passwordButton";
            passwordButton.Size = new Size(204, 39);
            passwordButton.TabIndex = 8;
            passwordButton.Text = "Olvidé mi contraseña";
            passwordButton.UseVisualStyleBackColor = true;
            passwordButton.Click += passwordButton_Click;
            // 
            // passwordLabel
            // 
            passwordLabel.Anchor = AnchorStyles.None;
            passwordLabel.AutoSize = true;
            passwordLabel.Location = new Point(57, 84);
            passwordLabel.Name = "passwordLabel";
            passwordLabel.Size = new Size(67, 15);
            passwordLabel.TabIndex = 7;
            passwordLabel.Text = "Contraseña";
            passwordLabel.Click += passwordLabel_Click;
            // 
            // correoText
            // 
            correoText.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            correoText.Location = new Point(54, 53);
            correoText.Name = "correoText";
            correoText.Size = new Size(207, 23);
            correoText.TabIndex = 0;
            correoText.TextChanged += textBox1_TextChanged;
            // 
            // loginButton
            // 
            loginButton.Anchor = AnchorStyles.None;
            loginButton.BackColor = Color.FromArgb(253, 181, 34);
            loginButton.Location = new Point(54, 145);
            loginButton.Name = "loginButton";
            loginButton.Size = new Size(207, 41);
            loginButton.TabIndex = 5;
            loginButton.Text = "Iniciar sesión";
            loginButton.UseVisualStyleBackColor = false;
            loginButton.Click += loginButton_Click;
            // 
            // correoLabel
            // 
            correoLabel.Anchor = AnchorStyles.None;
            correoLabel.AutoSize = true;
            correoLabel.Location = new Point(57, 32);
            correoLabel.Name = "correoLabel";
            correoLabel.Size = new Size(105, 15);
            correoLabel.TabIndex = 6;
            correoLabel.Text = "Correo electrónico";
            correoLabel.Click += correoLabel_Click;
            // 
            // passwordText
            // 
            passwordText.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            passwordText.Location = new Point(54, 102);
            passwordText.Name = "passwordText";
            passwordText.PasswordChar = '*';
            passwordText.Size = new Size(207, 23);
            passwordText.TabIndex = 1;
            passwordText.TextChanged += textBox2_TextChanged;
            // 
            // LogoBox
            // 
            LogoBox.Anchor = AnchorStyles.Top;
            LogoBox.BackColor = Color.Transparent;
            LogoBox.BackgroundImageLayout = ImageLayout.Center;
            LogoBox.ErrorImage = null;
            LogoBox.Image = Properties.Resources.Group_10;
            LogoBox.Location = new Point(479, 51);
            LogoBox.Name = "LogoBox";
            LogoBox.Size = new Size(248, 75);
            LogoBox.SizeMode = PictureBoxSizeMode.Zoom;
            LogoBox.TabIndex = 1;
            LogoBox.TabStop = false;
            // 
            // Login
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(1, 44, 86);
            ClientSize = new Size(1187, 665);
            Controls.Add(LogoBox);
            Controls.Add(panelLogin);
            Name = "Login";
            Text = "Form1";
            Load += Form1_Load;
            panelLogin.ResumeLayout(false);
            panelLogin.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)LogoBox).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panelLogin;
        private TextBox passwordText;
        private TextBox correoText;
        private Button loginButton;
        private Label correoLabel;
        private Label passwordLabel;
        private Button passwordButton;
        private LinkLabel registerLink;
        private PictureBox LogoBox;
        private Button button1;
    }
}
