namespace DuocOfCourseAdmin
{
    partial class Register
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            panel1 = new Panel();
            backButton = new Button();
            label2 = new Label();
            secondLastNameTextbox = new TextBox();
            label1 = new Label();
            lastNameText = new TextBox();
            MidNameText = new TextBox();
            MiddleNameLabel = new Label();
            RegNameLabel = new Label();
            RegisterButton = new Button();
            checkBox1 = new CheckBox();
            RegNombreTextbox = new TextBox();
            RegPasswordTextbox = new TextBox();
            passwordLabel = new Label();
            RegCorreoTextbox = new TextBox();
            RegCorreoLabel = new Label();
            LogoBox = new PictureBox();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)LogoBox).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.None;
            panel1.BackColor = Color.White;
            panel1.Controls.Add(backButton);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(secondLastNameTextbox);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(lastNameText);
            panel1.Controls.Add(MidNameText);
            panel1.Controls.Add(MiddleNameLabel);
            panel1.Controls.Add(RegNameLabel);
            panel1.Controls.Add(RegisterButton);
            panel1.Controls.Add(checkBox1);
            panel1.Controls.Add(RegNombreTextbox);
            panel1.Controls.Add(RegPasswordTextbox);
            panel1.Controls.Add(passwordLabel);
            panel1.Controls.Add(RegCorreoTextbox);
            panel1.Controls.Add(RegCorreoLabel);
            panel1.Location = new Point(255, 118);
            panel1.Name = "panel1";
            panel1.Size = new Size(379, 310);
            panel1.TabIndex = 0;
            // 
            // backButton
            // 
            backButton.BackColor = Color.FromArgb(1, 44, 86);
            backButton.ForeColor = SystemColors.ButtonHighlight;
            backButton.Location = new Point(13, 13);
            backButton.Name = "backButton";
            backButton.Size = new Size(64, 33);
            backButton.TabIndex = 20;
            backButton.Text = "Volver";
            backButton.UseVisualStyleBackColor = false;
            backButton.Click += backButton_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(191, 106);
            label2.Name = "label2";
            label2.Size = new Size(102, 15);
            label2.TabIndex = 18;
            label2.Text = "Segundo apellido:";
            // 
            // secondLastNameTextbox
            // 
            secondLastNameTextbox.Location = new Point(191, 124);
            secondLastNameTextbox.Name = "secondLastNameTextbox";
            secondLastNameTextbox.Size = new Size(144, 23);
            secondLastNameTextbox.TabIndex = 19;
            secondLastNameTextbox.TextChanged += textBox1_TextChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(37, 106);
            label1.Name = "label1";
            label1.Size = new Size(90, 15);
            label1.TabIndex = 16;
            label1.Text = "Primer apellido:";
            // 
            // lastNameText
            // 
            lastNameText.Location = new Point(37, 124);
            lastNameText.Name = "lastNameText";
            lastNameText.Size = new Size(144, 23);
            lastNameText.TabIndex = 17;
            lastNameText.TextChanged += lastNameText_TextChanged;
            // 
            // MidNameText
            // 
            MidNameText.Location = new Point(191, 80);
            MidNameText.Name = "MidNameText";
            MidNameText.Size = new Size(144, 23);
            MidNameText.TabIndex = 15;
            MidNameText.TextChanged += MidNameText_TextChanged;
            // 
            // MiddleNameLabel
            // 
            MiddleNameLabel.AutoSize = true;
            MiddleNameLabel.Location = new Point(188, 62);
            MiddleNameLabel.Name = "MiddleNameLabel";
            MiddleNameLabel.Size = new Size(99, 15);
            MiddleNameLabel.TabIndex = 14;
            MiddleNameLabel.Text = "Segundo nombre";
            // 
            // RegNameLabel
            // 
            RegNameLabel.AutoSize = true;
            RegNameLabel.Location = new Point(39, 62);
            RegNameLabel.Name = "RegNameLabel";
            RegNameLabel.Size = new Size(90, 15);
            RegNameLabel.TabIndex = 11;
            RegNameLabel.Text = "Primer nombre:";
            RegNameLabel.Click += RegNameLabel_Click;
            // 
            // RegisterButton
            // 
            RegisterButton.BackColor = Color.FromArgb(1, 44, 86);
            RegisterButton.ForeColor = SystemColors.ButtonHighlight;
            RegisterButton.Location = new Point(241, 254);
            RegisterButton.Name = "RegisterButton";
            RegisterButton.Size = new Size(93, 33);
            RegisterButton.TabIndex = 10;
            RegisterButton.Text = "Registrarse";
            RegisterButton.UseVisualStyleBackColor = false;
            RegisterButton.Click += RegisterButton_Click;
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Location = new Point(37, 254);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(128, 19);
            checkBox1.TabIndex = 9;
            checkBox1.Text = "Mostrar contraseña";
            checkBox1.UseVisualStyleBackColor = true;
            checkBox1.CheckedChanged += checkBox1_CheckedChanged;
            // 
            // RegNombreTextbox
            // 
            RegNombreTextbox.Location = new Point(37, 80);
            RegNombreTextbox.Name = "RegNombreTextbox";
            RegNombreTextbox.Size = new Size(144, 23);
            RegNombreTextbox.TabIndex = 12;
            RegNombreTextbox.TextChanged += RegNombreTextbox_TextChanged;
            // 
            // RegPasswordTextbox
            // 
            RegPasswordTextbox.Location = new Point(38, 218);
            RegPasswordTextbox.Name = "RegPasswordTextbox";
            RegPasswordTextbox.Size = new Size(296, 23);
            RegPasswordTextbox.TabIndex = 12;
            // 
            // passwordLabel
            // 
            passwordLabel.AutoSize = true;
            passwordLabel.Location = new Point(38, 200);
            passwordLabel.Name = "passwordLabel";
            passwordLabel.Size = new Size(70, 15);
            passwordLabel.TabIndex = 2;
            passwordLabel.Text = "Contraseña:";
            passwordLabel.Click += label2_Click;
            // 
            // RegCorreoTextbox
            // 
            RegCorreoTextbox.Location = new Point(37, 173);
            RegCorreoTextbox.Name = "RegCorreoTextbox";
            RegCorreoTextbox.Size = new Size(296, 23);
            RegCorreoTextbox.TabIndex = 13;
            // 
            // RegCorreoLabel
            // 
            RegCorreoLabel.AutoSize = true;
            RegCorreoLabel.Location = new Point(39, 155);
            RegCorreoLabel.Name = "RegCorreoLabel";
            RegCorreoLabel.Size = new Size(108, 15);
            RegCorreoLabel.TabIndex = 0;
            RegCorreoLabel.Text = "Correo electrónico:";
            RegCorreoLabel.Click += RegCorreoLabel_Click;
            // 
            // LogoBox
            // 
            LogoBox.Anchor = AnchorStyles.None;
            LogoBox.BackColor = Color.Transparent;
            LogoBox.BackgroundImageLayout = ImageLayout.Center;
            LogoBox.ErrorImage = null;
            LogoBox.Image = Properties.Resources.Group_10;
            LogoBox.Location = new Point(316, 26);
            LogoBox.Name = "LogoBox";
            LogoBox.Size = new Size(248, 75);
            LogoBox.SizeMode = PictureBoxSizeMode.Zoom;
            LogoBox.TabIndex = 13;
            LogoBox.TabStop = false;
            LogoBox.Click += LogoBox_Click;
            // 
            // Register
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(1, 44, 86);
            ClientSize = new Size(903, 554);
            Controls.Add(LogoBox);
            Controls.Add(panel1);
            MinimumSize = new Size(501, 593);
            Name = "Register";
            Text = "Registro";
            Load += Register_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)LogoBox).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Label RegCorreoLabel;
        private TextBox RegCorreoTextbox;
        private Label passwordLabel;
        private TextBox RegPasswordTextbox;
        private TextBox RegNombreTextbox;
        private CheckBox checkBox1;
        private Button RegisterButton;
        private Label RegNameLabel;
        private PictureBox LogoBox;
        private TextBox MidNameText;
        private Label MiddleNameLabel;
        private Label label1;
        private TextBox lastNameText;
        private Label label2;
        private TextBox secondLastNameTextbox;
        private Button backButton;
    }
}