namespace DuocOfCourseAdmin
{
    partial class Modificar_usuario
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
            checkBox1 = new CheckBox();
            correoTextBox = new TextBox();
            correoLabel = new Label();
            label2 = new Label();
            secondLastNameTextbox = new TextBox();
            label1 = new Label();
            lastNameText = new TextBox();
            MidNameText = new TextBox();
            MiddleNameLabel = new Label();
            RegNameLabel = new Label();
            NombreTextbox = new TextBox();
            StatusCheckBox = new CheckBox();
            AddPasswordTextbox = new TextBox();
            AddPasswordLabel = new Label();
            RoleComboBox = new ComboBox();
            AddRoleLabel = new Label();
            SaveButton = new Button();
            SuspendLayout();
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Location = new Point(28, 288);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(128, 19);
            checkBox1.TabIndex = 48;
            checkBox1.Text = "Mostrar contraseña";
            checkBox1.UseVisualStyleBackColor = true;
            checkBox1.CheckedChanged += checkBox1_CheckedChanged;
            // 
            // correoTextBox
            // 
            correoTextBox.Location = new Point(26, 217);
            correoTextBox.Name = "correoTextBox";
            correoTextBox.Size = new Size(392, 23);
            correoTextBox.TabIndex = 46;
            correoTextBox.TextChanged += correoTextBox_TextChanged;
            // 
            // correoLabel
            // 
            correoLabel.AutoSize = true;
            correoLabel.Location = new Point(26, 199);
            correoLabel.Name = "correoLabel";
            correoLabel.Size = new Size(108, 15);
            correoLabel.TabIndex = 45;
            correoLabel.Text = "Correo electrónico:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(28, 155);
            label2.Name = "label2";
            label2.Size = new Size(102, 15);
            label2.TabIndex = 43;
            label2.Text = "Segundo apellido:";
            // 
            // secondLastNameTextbox
            // 
            secondLastNameTextbox.Location = new Point(26, 173);
            secondLastNameTextbox.Name = "secondLastNameTextbox";
            secondLastNameTextbox.Size = new Size(393, 23);
            secondLastNameTextbox.TabIndex = 44;
            secondLastNameTextbox.TextChanged += secondLastNameTextbox_TextChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(26, 111);
            label1.Name = "label1";
            label1.Size = new Size(90, 15);
            label1.TabIndex = 41;
            label1.Text = "Primer apellido:";
            // 
            // lastNameText
            // 
            lastNameText.Location = new Point(26, 129);
            lastNameText.Name = "lastNameText";
            lastNameText.Size = new Size(392, 23);
            lastNameText.TabIndex = 42;
            lastNameText.TextChanged += lastNameText_TextChanged;
            // 
            // MidNameText
            // 
            MidNameText.Location = new Point(28, 82);
            MidNameText.Name = "MidNameText";
            MidNameText.Size = new Size(391, 23);
            MidNameText.TabIndex = 40;
            MidNameText.TextChanged += MidNameText_TextChanged;
            // 
            // MiddleNameLabel
            // 
            MiddleNameLabel.AutoSize = true;
            MiddleNameLabel.Location = new Point(26, 64);
            MiddleNameLabel.Name = "MiddleNameLabel";
            MiddleNameLabel.Size = new Size(99, 15);
            MiddleNameLabel.TabIndex = 39;
            MiddleNameLabel.Text = "Segundo nombre";
            // 
            // RegNameLabel
            // 
            RegNameLabel.AutoSize = true;
            RegNameLabel.Location = new Point(26, 20);
            RegNameLabel.Name = "RegNameLabel";
            RegNameLabel.Size = new Size(90, 15);
            RegNameLabel.TabIndex = 37;
            RegNameLabel.Text = "Primer nombre:";
            // 
            // NombreTextbox
            // 
            NombreTextbox.Location = new Point(28, 38);
            NombreTextbox.Name = "NombreTextbox";
            NombreTextbox.Size = new Size(390, 23);
            NombreTextbox.TabIndex = 38;
            NombreTextbox.TextChanged += NombreTextbox_TextChanged;
            // 
            // StatusCheckBox
            // 
            StatusCheckBox.AutoSize = true;
            StatusCheckBox.Location = new Point(26, 364);
            StatusCheckBox.Name = "StatusCheckBox";
            StatusCheckBox.Size = new Size(81, 19);
            StatusCheckBox.TabIndex = 35;
            StatusCheckBox.Text = "Habilitado";
            StatusCheckBox.UseVisualStyleBackColor = true;
            StatusCheckBox.CheckedChanged += StatusCheckBox_CheckedChanged;
            // 
            // AddPasswordTextbox
            // 
            AddPasswordTextbox.Location = new Point(26, 259);
            AddPasswordTextbox.Name = "AddPasswordTextbox";
            AddPasswordTextbox.Size = new Size(393, 23);
            AddPasswordTextbox.TabIndex = 47;
            AddPasswordTextbox.TextChanged += AddPasswordTextbox_TextChanged;
            // 
            // AddPasswordLabel
            // 
            AddPasswordLabel.AutoSize = true;
            AddPasswordLabel.Location = new Point(26, 240);
            AddPasswordLabel.Name = "AddPasswordLabel";
            AddPasswordLabel.Size = new Size(70, 15);
            AddPasswordLabel.TabIndex = 34;
            AddPasswordLabel.Text = "Contraseña:";
            // 
            // RoleComboBox
            // 
            RoleComboBox.Items.AddRange(new object[] { "Administrador", "Docente", "Estudiante" });
            RoleComboBox.Location = new Point(25, 328);
            RoleComboBox.Name = "RoleComboBox";
            RoleComboBox.Size = new Size(177, 23);
            RoleComboBox.TabIndex = 36;
            RoleComboBox.SelectedIndexChanged += RoleComboBox_SelectedIndexChanged;
            // 
            // AddRoleLabel
            // 
            AddRoleLabel.AutoSize = true;
            AddRoleLabel.Location = new Point(25, 310);
            AddRoleLabel.Name = "AddRoleLabel";
            AddRoleLabel.Size = new Size(27, 15);
            AddRoleLabel.TabIndex = 33;
            AddRoleLabel.Text = "Rol:";
            // 
            // SaveButton
            // 
            SaveButton.BackColor = Color.FromArgb(43, 93, 162);
            SaveButton.FlatStyle = FlatStyle.Popup;
            SaveButton.Font = new Font("Segoe UI", 12F);
            SaveButton.Location = new Point(354, 363);
            SaveButton.Name = "SaveButton";
            SaveButton.Size = new Size(92, 37);
            SaveButton.TabIndex = 32;
            SaveButton.Text = "Guardar";
            SaveButton.UseVisualStyleBackColor = false;
            SaveButton.Click += button1_Click_1;
            // 
            // Modificar_usuario
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(470, 420);
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
            Controls.Add(SaveButton);
            Name = "Modificar_usuario";
            Text = "Modificar_usuario";
            Load += Modificar_usuario_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private CheckBox checkBox1;
        private TextBox correoTextBox;
        private Label correoLabel;
        private Label label2;
        private TextBox secondLastNameTextbox;
        private Label label1;
        private TextBox lastNameText;
        private TextBox MidNameText;
        private Label MiddleNameLabel;
        private Label RegNameLabel;
        private TextBox NombreTextbox;
        private CheckBox StatusCheckBox;
        private TextBox AddPasswordTextbox;
        private Label AddPasswordLabel;
        private ComboBox RoleComboBox;
        private Label AddRoleLabel;
        private Button SaveButton;
    }
}