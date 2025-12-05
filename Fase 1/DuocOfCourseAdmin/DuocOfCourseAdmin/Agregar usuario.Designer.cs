namespace DuocOfCourseAdmin
{
    partial class AddUser
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
            button1 = new Button();
            AddRoleLabel = new Label();
            RoleComboBox = new ComboBox();
            AddPasswordLabel = new Label();
            AddPasswordTextbox = new TextBox();
            StatusCheckBox = new CheckBox();
            label2 = new Label();
            secondLastNameTextbox = new TextBox();
            label1 = new Label();
            lastNameText = new TextBox();
            MidNameText = new TextBox();
            MiddleNameLabel = new Label();
            RegNameLabel = new Label();
            NombreTextbox = new TextBox();
            correoTextBox = new TextBox();
            correoLabel = new Label();
            checkBox1 = new CheckBox();
            label3 = new Label();
            ComboBoxCarrera = new ComboBox();
            SuspendLayout();
            // 
            // button1
            // 
            button1.BackColor = Color.FromArgb(43, 93, 162);
            button1.FlatStyle = FlatStyle.Popup;
            button1.Font = new Font("Segoe UI", 12F);
            button1.Location = new Point(352, 415);
            button1.Name = "button1";
            button1.Size = new Size(92, 37);
            button1.TabIndex = 2;
            button1.Text = "Guardar";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // AddRoleLabel
            // 
            AddRoleLabel.AutoSize = true;
            AddRoleLabel.Location = new Point(30, 350);
            AddRoleLabel.Name = "AddRoleLabel";
            AddRoleLabel.Size = new Size(27, 15);
            AddRoleLabel.TabIndex = 5;
            AddRoleLabel.Text = "Rol:";
            AddRoleLabel.Click += label3_Click;
            // 
            // RoleComboBox
            // 
            RoleComboBox.Items.AddRange(new object[] { "Administrador", "Docente", "Estudiante" });
            RoleComboBox.Location = new Point(30, 368);
            RoleComboBox.Name = "RoleComboBox";
            RoleComboBox.Size = new Size(177, 23);
            RoleComboBox.TabIndex = 7;
            RoleComboBox.SelectedIndexChanged += RoleComboBox_SelectedIndexChanged;
            // 
            // AddPasswordLabel
            // 
            AddPasswordLabel.AutoSize = true;
            AddPasswordLabel.Location = new Point(30, 250);
            AddPasswordLabel.Name = "AddPasswordLabel";
            AddPasswordLabel.Size = new Size(70, 15);
            AddPasswordLabel.TabIndex = 7;
            AddPasswordLabel.Text = "Contraseña:";
            // 
            // AddPasswordTextbox
            // 
            AddPasswordTextbox.Location = new Point(30, 269);
            AddPasswordTextbox.Name = "AddPasswordTextbox";
            AddPasswordTextbox.Size = new Size(393, 23);
            AddPasswordTextbox.TabIndex = 6;
            AddPasswordTextbox.TextChanged += AddPasswordTextbox_TextChanged;
            // 
            // StatusCheckBox
            // 
            StatusCheckBox.AutoSize = true;
            StatusCheckBox.Location = new Point(33, 403);
            StatusCheckBox.Name = "StatusCheckBox";
            StatusCheckBox.Size = new Size(81, 19);
            StatusCheckBox.TabIndex = 10;
            StatusCheckBox.Text = "Habilitado";
            StatusCheckBox.UseVisualStyleBackColor = true;
            StatusCheckBox.CheckedChanged += StatusCheckBox_CheckedChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(30, 162);
            label2.Name = "label2";
            label2.Size = new Size(102, 15);
            label2.TabIndex = 26;
            label2.Text = "Segundo apellido:";
            // 
            // secondLastNameTextbox
            // 
            secondLastNameTextbox.Location = new Point(30, 180);
            secondLastNameTextbox.Name = "secondLastNameTextbox";
            secondLastNameTextbox.Size = new Size(393, 23);
            secondLastNameTextbox.TabIndex = 4;
            secondLastNameTextbox.TextChanged += secondLastNameTextbox_TextChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(30, 118);
            label1.Name = "label1";
            label1.Size = new Size(90, 15);
            label1.TabIndex = 24;
            label1.Text = "Primer apellido:";
            // 
            // lastNameText
            // 
            lastNameText.Location = new Point(30, 136);
            lastNameText.Name = "lastNameText";
            lastNameText.Size = new Size(392, 23);
            lastNameText.TabIndex = 3;
            lastNameText.TextChanged += lastNameText_TextChanged;
            // 
            // MidNameText
            // 
            MidNameText.Location = new Point(30, 92);
            MidNameText.Name = "MidNameText";
            MidNameText.Size = new Size(391, 23);
            MidNameText.TabIndex = 2;
            MidNameText.TextChanged += MidNameText_TextChanged;
            // 
            // MiddleNameLabel
            // 
            MiddleNameLabel.AutoSize = true;
            MiddleNameLabel.Location = new Point(30, 71);
            MiddleNameLabel.Name = "MiddleNameLabel";
            MiddleNameLabel.Size = new Size(99, 15);
            MiddleNameLabel.TabIndex = 22;
            MiddleNameLabel.Text = "Segundo nombre";
            // 
            // RegNameLabel
            // 
            RegNameLabel.AutoSize = true;
            RegNameLabel.Location = new Point(30, 27);
            RegNameLabel.Name = "RegNameLabel";
            RegNameLabel.Size = new Size(90, 15);
            RegNameLabel.TabIndex = 20;
            RegNameLabel.Text = "Primer nombre:";
            // 
            // NombreTextbox
            // 
            NombreTextbox.Location = new Point(30, 45);
            NombreTextbox.Name = "NombreTextbox";
            NombreTextbox.Size = new Size(393, 23);
            NombreTextbox.TabIndex = 1;
            NombreTextbox.TextChanged += NombreTextbox_TextChanged;
            // 
            // correoTextBox
            // 
            correoTextBox.Location = new Point(30, 224);
            correoTextBox.Name = "correoTextBox";
            correoTextBox.Size = new Size(392, 23);
            correoTextBox.TabIndex = 5;
            correoTextBox.TextChanged += correoTextBox_TextChanged;
            // 
            // correoLabel
            // 
            correoLabel.AutoSize = true;
            correoLabel.Location = new Point(30, 206);
            correoLabel.Name = "correoLabel";
            correoLabel.Size = new Size(108, 15);
            correoLabel.TabIndex = 28;
            correoLabel.Text = "Correo electrónico:";
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Location = new Point(295, 298);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(128, 19);
            checkBox1.TabIndex = 31;
            checkBox1.Text = "Mostrar contraseña";
            checkBox1.UseVisualStyleBackColor = true;
            checkBox1.CheckedChanged += checkBox1_CheckedChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(30, 302);
            label3.Name = "label3";
            label3.Size = new Size(45, 15);
            label3.TabIndex = 32;
            label3.Text = "Carrera";
            label3.Click += label3_Click_1;
            // 
            // ComboBoxCarrera
            // 
            ComboBoxCarrera.FormattingEnabled = true;
            ComboBoxCarrera.Location = new Point(30, 324);
            ComboBoxCarrera.Name = "ComboBoxCarrera";
            ComboBoxCarrera.Size = new Size(177, 23);
            ComboBoxCarrera.TabIndex = 33;
            ComboBoxCarrera.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // AddUser
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(458, 468);
            Controls.Add(ComboBoxCarrera);
            Controls.Add(label3);
            Controls.Add(checkBox1);
            Controls.Add(correoTextBox);
            Controls.Add(correoLabel);
            Controls.Add(label2);
            Controls.Add(secondLastNameTextbox);
            Controls.Add(label1);
            Controls.Add(lastNameText);
            Controls.Add(MidNameText);
            Controls.Add(MiddleNameLabel);
            Controls.Add(RegNameLabel);
            Controls.Add(NombreTextbox);
            Controls.Add(StatusCheckBox);
            Controls.Add(AddPasswordTextbox);
            Controls.Add(AddPasswordLabel);
            Controls.Add(RoleComboBox);
            Controls.Add(AddRoleLabel);
            Controls.Add(button1);
            MaximumSize = new Size(474, 507);
            MinimumSize = new Size(474, 458);
            Name = "AddUser";
            Text = "Agregar usuario";
            Load += AddUser_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private TextBox NombreTextbox;
        private TextBox MidNameText;
        private TextBox lastNameText;
        private TextBox secondLastNameTextbox;
        private Button button1;
        private Label AddRoleLabel;
        private ComboBox RoleComboBox;
        private Label AddPasswordLabel;
        private TextBox AddPasswordTextbox;
        private CheckBox StatusCheckBox;
        private Label label2;
        private Label label1;
        private Label MiddleNameLabel;
        private Label RegNameLabel;
        private TextBox correoTextBox;
        private Label correoLabel;
        private CheckBox checkBox1;
        private Label label3;
        private ComboBox ComboBoxCarrera;
    }
}