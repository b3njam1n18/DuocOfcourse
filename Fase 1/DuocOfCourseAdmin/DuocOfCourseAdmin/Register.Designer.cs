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
            label1 = new Label();
            lastNameText = new TextBox();
            MidNameText = new TextBox();
            MiddleNameLabel = new Label();
            RegNameLabel = new Label();
            RegisterButton = new Button();
            checkBox1 = new CheckBox();
            RegNombreTextbox = new TextBox();
            comboBoxCarrera = new ComboBox();
            CarreraLabel = new Label();
            comboBoxSchool = new ComboBox();
            SchoolLabel = new Label();
            RegPasswordTextbox = new TextBox();
            passwordLabel = new Label();
            RegCorreoTextbox = new TextBox();
            RegCorreoLabel = new Label();
            LogoBox = new PictureBox();
            label2 = new Label();
            secondLastNameTextbox = new TextBox();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)LogoBox).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top;
            panel1.BackColor = Color.White;
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
            panel1.Controls.Add(comboBoxCarrera);
            panel1.Controls.Add(CarreraLabel);
            panel1.Controls.Add(comboBoxSchool);
            panel1.Controls.Add(SchoolLabel);
            panel1.Controls.Add(RegPasswordTextbox);
            panel1.Controls.Add(passwordLabel);
            panel1.Controls.Add(RegCorreoTextbox);
            panel1.Controls.Add(RegCorreoLabel);
            panel1.Location = new Point(214, 107);
            panel1.Name = "panel1";
            panel1.Size = new Size(379, 386);
            panel1.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(37, 83);
            label1.Name = "label1";
            label1.Size = new Size(90, 15);
            label1.TabIndex = 16;
            label1.Text = "Primer apellido:";
            // 
            // lastNameText
            // 
            lastNameText.Location = new Point(38, 101);
            lastNameText.Name = "lastNameText";
            lastNameText.Size = new Size(144, 23);
            lastNameText.TabIndex = 17;
            lastNameText.TextChanged += lastNameText_TextChanged;
            // 
            // MidNameText
            // 
            MidNameText.Location = new Point(190, 49);
            MidNameText.Name = "MidNameText";
            MidNameText.Size = new Size(144, 23);
            MidNameText.TabIndex = 15;
            MidNameText.TextChanged += MidNameText_TextChanged;
            // 
            // MiddleNameLabel
            // 
            MiddleNameLabel.AutoSize = true;
            MiddleNameLabel.Location = new Point(187, 31);
            MiddleNameLabel.Name = "MiddleNameLabel";
            MiddleNameLabel.Size = new Size(99, 15);
            MiddleNameLabel.TabIndex = 14;
            MiddleNameLabel.Text = "Segundo nombre";
            // 
            // RegNameLabel
            // 
            RegNameLabel.AutoSize = true;
            RegNameLabel.Location = new Point(38, 31);
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
            RegisterButton.Location = new Point(241, 335);
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
            checkBox1.Location = new Point(37, 335);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(128, 19);
            checkBox1.TabIndex = 9;
            checkBox1.Text = "Mostrar contraseña";
            checkBox1.UseVisualStyleBackColor = true;
            checkBox1.CheckedChanged += checkBox1_CheckedChanged;
            // 
            // RegNombreTextbox
            // 
            RegNombreTextbox.Location = new Point(37, 49);
            RegNombreTextbox.Name = "RegNombreTextbox";
            RegNombreTextbox.Size = new Size(144, 23);
            RegNombreTextbox.TabIndex = 12;
            RegNombreTextbox.TextChanged += RegNombreTextbox_TextChanged;
            // 
            // comboBoxCarrera
            // 
            comboBoxCarrera.AllowDrop = true;
            comboBoxCarrera.FormattingEnabled = true;
            comboBoxCarrera.Items.AddRange(new object[] { "a", "b", "c", "d", "e" });
            comboBoxCarrera.Location = new Point(38, 244);
            comboBoxCarrera.Name = "comboBoxCarrera";
            comboBoxCarrera.Size = new Size(121, 23);
            comboBoxCarrera.TabIndex = 7;
            comboBoxCarrera.Text = "Carrera";
            // 
            // CarreraLabel
            // 
            CarreraLabel.AutoSize = true;
            CarreraLabel.Location = new Point(38, 226);
            CarreraLabel.Name = "CarreraLabel";
            CarreraLabel.Size = new Size(105, 15);
            CarreraLabel.TabIndex = 6;
            CarreraLabel.Text = "Seleccione carrera:";
            CarreraLabel.Click += CarreraLabel_Click;
            // 
            // comboBoxSchool
            // 
            comboBoxSchool.AllowDrop = true;
            comboBoxSchool.FormattingEnabled = true;
            comboBoxSchool.Items.AddRange(new object[] { "a", "b", "c", "d" });
            comboBoxSchool.Location = new Point(38, 194);
            comboBoxSchool.Name = "comboBoxSchool";
            comboBoxSchool.Size = new Size(121, 23);
            comboBoxSchool.TabIndex = 5;
            comboBoxSchool.Text = "Escuela";
            comboBoxSchool.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // SchoolLabel
            // 
            SchoolLabel.AutoSize = true;
            SchoolLabel.Location = new Point(38, 176);
            SchoolLabel.Name = "SchoolLabel";
            SchoolLabel.Size = new Size(108, 15);
            SchoolLabel.TabIndex = 4;
            SchoolLabel.Text = "Seleccione escuela:";
            SchoolLabel.Click += SchoolLabel_Click;
            // 
            // RegPasswordTextbox
            // 
            RegPasswordTextbox.Location = new Point(38, 299);
            RegPasswordTextbox.Name = "RegPasswordTextbox";
            RegPasswordTextbox.PasswordChar = '*';
            RegPasswordTextbox.Size = new Size(296, 23);
            RegPasswordTextbox.TabIndex = 12;
            // 
            // passwordLabel
            // 
            passwordLabel.AutoSize = true;
            passwordLabel.Location = new Point(38, 281);
            passwordLabel.Name = "passwordLabel";
            passwordLabel.Size = new Size(70, 15);
            passwordLabel.TabIndex = 2;
            passwordLabel.Text = "Contraseña:";
            passwordLabel.Click += label2_Click;
            // 
            // RegCorreoTextbox
            // 
            RegCorreoTextbox.Location = new Point(38, 142);
            RegCorreoTextbox.Name = "RegCorreoTextbox";
            RegCorreoTextbox.Size = new Size(296, 23);
            RegCorreoTextbox.TabIndex = 13;
            // 
            // RegCorreoLabel
            // 
            RegCorreoLabel.AutoSize = true;
            RegCorreoLabel.Location = new Point(38, 124);
            RegCorreoLabel.Name = "RegCorreoLabel";
            RegCorreoLabel.Size = new Size(108, 15);
            RegCorreoLabel.TabIndex = 0;
            RegCorreoLabel.Text = "Correo electrónico:";
            RegCorreoLabel.Click += RegCorreoLabel_Click;
            // 
            // LogoBox
            // 
            LogoBox.Anchor = AnchorStyles.Top;
            LogoBox.BackColor = Color.Transparent;
            LogoBox.BackgroundImageLayout = ImageLayout.Center;
            LogoBox.ErrorImage = null;
            LogoBox.Image = Properties.Resources.Group_10;
            LogoBox.Location = new Point(292, 26);
            LogoBox.Name = "LogoBox";
            LogoBox.Size = new Size(248, 75);
            LogoBox.SizeMode = PictureBoxSizeMode.Zoom;
            LogoBox.TabIndex = 13;
            LogoBox.TabStop = false;
            LogoBox.Click += LogoBox_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(187, 83);
            label2.Name = "label2";
            label2.Size = new Size(102, 15);
            label2.TabIndex = 18;
            label2.Text = "Segundo apellido:";
            // 
            // secondLastNameTextbox
            // 
            secondLastNameTextbox.Location = new Point(190, 101);
            secondLastNameTextbox.Name = "secondLastNameTextbox";
            secondLastNameTextbox.Size = new Size(144, 23);
            secondLastNameTextbox.TabIndex = 19;
            secondLastNameTextbox.TextChanged += textBox1_TextChanged;
            // 
            // Register
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(1, 44, 86);
            ClientSize = new Size(903, 554);
            Controls.Add(LogoBox);
            Controls.Add(panel1);
            Name = "Register";
            Text = "7";
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
        private ComboBox comboBoxSchool;
        private Label SchoolLabel;
        private Label CarreraLabel;
        private ComboBox comboBoxCarrera;
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
    }
}