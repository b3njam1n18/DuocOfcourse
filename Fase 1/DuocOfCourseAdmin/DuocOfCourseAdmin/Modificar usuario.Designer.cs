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
            label1 = new Label();
            CareerText = new Label();
            StatusCheckBox = new CheckBox();
            AddPasswordLabel = new Label();
            AddRoleLabel = new Label();
            AddEmailLabel = new Label();
            button1 = new Button();
            AddNombreLabel = new Label();
            changeNameTextbox = new TextBox();
            changeEmailTextbox = new TextBox();
            RolCombobox = new ComboBox();
            CareerCombobox = new ComboBox();
            SchoolCombobox = new ComboBox();
            ChangePasswordTextbox = new TextBox();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(26, 200);
            label1.Name = "label1";
            label1.Size = new Size(46, 15);
            label1.TabIndex = 30;
            label1.Text = "Escuela";
            // 
            // CareerText
            // 
            CareerText.AutoSize = true;
            CareerText.Location = new Point(26, 156);
            CareerText.Name = "CareerText";
            CareerText.Size = new Size(45, 15);
            CareerText.TabIndex = 28;
            CareerText.Text = "Carrera";
            // 
            // StatusCheckBox
            // 
            StatusCheckBox.AutoSize = true;
            StatusCheckBox.Location = new Point(27, 297);
            StatusCheckBox.Name = "StatusCheckBox";
            StatusCheckBox.Size = new Size(81, 19);
            StatusCheckBox.TabIndex = 26;
            StatusCheckBox.Text = "Habilitado";
            StatusCheckBox.UseVisualStyleBackColor = true;
            // 
            // AddPasswordLabel
            // 
            AddPasswordLabel.AutoSize = true;
            AddPasswordLabel.Location = new Point(26, 244);
            AddPasswordLabel.Name = "AddPasswordLabel";
            AddPasswordLabel.Size = new Size(70, 15);
            AddPasswordLabel.TabIndex = 24;
            AddPasswordLabel.Text = "Contraseña:";
            // 
            // AddRoleLabel
            // 
            AddRoleLabel.AutoSize = true;
            AddRoleLabel.Location = new Point(27, 112);
            AddRoleLabel.Name = "AddRoleLabel";
            AddRoleLabel.Size = new Size(27, 15);
            AddRoleLabel.TabIndex = 23;
            AddRoleLabel.Text = "Rol:";
            // 
            // AddEmailLabel
            // 
            AddEmailLabel.AutoSize = true;
            AddEmailLabel.Location = new Point(27, 68);
            AddEmailLabel.Name = "AddEmailLabel";
            AddEmailLabel.Size = new Size(108, 15);
            AddEmailLabel.TabIndex = 21;
            AddEmailLabel.Text = "Correo electrónico:";
            // 
            // button1
            // 
            button1.BackColor = Color.FromArgb(43, 93, 162);
            button1.FlatStyle = FlatStyle.Popup;
            button1.Font = new Font("Segoe UI", 12F);
            button1.Location = new Point(327, 306);
            button1.Name = "button1";
            button1.Size = new Size(92, 37);
            button1.TabIndex = 20;
            button1.Text = "Guardar";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // AddNombreLabel
            // 
            AddNombreLabel.AutoSize = true;
            AddNombreLabel.Location = new Point(27, 24);
            AddNombreLabel.Name = "AddNombreLabel";
            AddNombreLabel.Size = new Size(54, 15);
            AddNombreLabel.TabIndex = 19;
            AddNombreLabel.Text = "Nombre:";
            // 
            // changeNameTextbox
            // 
            changeNameTextbox.Location = new Point(27, 42);
            changeNameTextbox.Name = "changeNameTextbox";
            changeNameTextbox.Size = new Size(391, 23);
            changeNameTextbox.TabIndex = 32;
            // 
            // changeEmailTextbox
            // 
            changeEmailTextbox.Location = new Point(27, 86);
            changeEmailTextbox.Name = "changeEmailTextbox";
            changeEmailTextbox.Size = new Size(391, 23);
            changeEmailTextbox.TabIndex = 33;
            // 
            // RolCombobox
            // 
            RolCombobox.FormattingEnabled = true;
            RolCombobox.Items.AddRange(new object[] { "Administrador", "Docente", "Estudiante" });
            RolCombobox.Location = new Point(27, 130);
            RolCombobox.Name = "RolCombobox";
            RolCombobox.Size = new Size(121, 23);
            RolCombobox.TabIndex = 34;
            // 
            // CareerCombobox
            // 
            CareerCombobox.FormattingEnabled = true;
            CareerCombobox.Location = new Point(27, 174);
            CareerCombobox.Name = "CareerCombobox";
            CareerCombobox.Size = new Size(121, 23);
            CareerCombobox.TabIndex = 35;
            // 
            // SchoolCombobox
            // 
            SchoolCombobox.FormattingEnabled = true;
            SchoolCombobox.Location = new Point(27, 218);
            SchoolCombobox.Name = "SchoolCombobox";
            SchoolCombobox.Size = new Size(121, 23);
            SchoolCombobox.TabIndex = 36;
            // 
            // ChangePasswordTextbox
            // 
            ChangePasswordTextbox.Location = new Point(27, 265);
            ChangePasswordTextbox.Name = "ChangePasswordTextbox";
            ChangePasswordTextbox.Size = new Size(391, 23);
            ChangePasswordTextbox.TabIndex = 37;
            // 
            // Modificar_usuario
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(459, 361);
            Controls.Add(ChangePasswordTextbox);
            Controls.Add(SchoolCombobox);
            Controls.Add(CareerCombobox);
            Controls.Add(RolCombobox);
            Controls.Add(changeEmailTextbox);
            Controls.Add(changeNameTextbox);
            Controls.Add(label1);
            Controls.Add(CareerText);
            Controls.Add(StatusCheckBox);
            Controls.Add(AddPasswordLabel);
            Controls.Add(AddRoleLabel);
            Controls.Add(AddEmailLabel);
            Controls.Add(button1);
            Controls.Add(AddNombreLabel);
            Name = "Modificar_usuario";
            Text = "Modificar_usuario";
            Load += Modificar_usuario_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label label1;
        private Label CareerText;
        private CheckBox StatusCheckBox;
        private Label AddPasswordLabel;
        private Label AddRoleLabel;
        private Label AddEmailLabel;
        private Button button1;
        private Label AddNombreLabel;
        private TextBox changeNameTextbox;
        private TextBox changeEmailTextbox;
        private ComboBox RolCombobox;
        private ComboBox CareerCombobox;
        private ComboBox SchoolCombobox;
        private TextBox ChangePasswordTextbox;
    }
}