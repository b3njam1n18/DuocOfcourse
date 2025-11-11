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
            AddNameTextbox = new TextBox();
            AddNombreLabel = new Label();
            button1 = new Button();
            AddEmailLabel = new Label();
            AddEmailTextbox = new TextBox();
            AddRoleLabel = new Label();
            RoleComboBox = new ComboBox();
            AddPasswordLabel = new Label();
            AddPasswordTextbox = new TextBox();
            StatusCheckBox = new CheckBox();
            CareerText = new Label();
            comboBoxCarrera = new ComboBox();
            label1 = new Label();
            comboBoxEscuela = new ComboBox();
            SuspendLayout();
            // 
            // AddNameTextbox
            // 
            AddNameTextbox.Location = new Point(26, 41);
            AddNameTextbox.Name = "AddNameTextbox";
            AddNameTextbox.Size = new Size(389, 23);
            AddNameTextbox.TabIndex = 0;
            AddNameTextbox.TextChanged += AddNameTextbox_TextChanged;
            // 
            // AddNombreLabel
            // 
            AddNombreLabel.AutoSize = true;
            AddNombreLabel.Location = new Point(26, 23);
            AddNombreLabel.Name = "AddNombreLabel";
            AddNombreLabel.Size = new Size(54, 15);
            AddNombreLabel.TabIndex = 1;
            AddNombreLabel.Text = "Nombre:";
            // 
            // button1
            // 
            button1.BackColor = Color.FromArgb(43, 93, 162);
            button1.FlatStyle = FlatStyle.Popup;
            button1.Font = new Font("Segoe UI", 12F);
            button1.Location = new Point(326, 305);
            button1.Name = "button1";
            button1.Size = new Size(92, 37);
            button1.TabIndex = 2;
            button1.Text = "Guardar";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // AddEmailLabel
            // 
            AddEmailLabel.AutoSize = true;
            AddEmailLabel.Location = new Point(26, 67);
            AddEmailLabel.Name = "AddEmailLabel";
            AddEmailLabel.Size = new Size(108, 15);
            AddEmailLabel.TabIndex = 3;
            AddEmailLabel.Text = "Correo electrónico:";
            // 
            // AddEmailTextbox
            // 
            AddEmailTextbox.Location = new Point(26, 85);
            AddEmailTextbox.Name = "AddEmailTextbox";
            AddEmailTextbox.Size = new Size(389, 23);
            AddEmailTextbox.TabIndex = 4;
            AddEmailTextbox.TextChanged += AddEmailTextbox_TextChanged;
            // 
            // AddRoleLabel
            // 
            AddRoleLabel.AutoSize = true;
            AddRoleLabel.Location = new Point(26, 111);
            AddRoleLabel.Name = "AddRoleLabel";
            AddRoleLabel.Size = new Size(27, 15);
            AddRoleLabel.TabIndex = 5;
            AddRoleLabel.Text = "Rol:";
            AddRoleLabel.Click += label3_Click;
            // 
            // RoleComboBox
            // 
            RoleComboBox.Items.AddRange(new object[] { "Administrador", "Docente", "Estudiante" });
            RoleComboBox.Location = new Point(26, 129);
            RoleComboBox.Name = "RoleComboBox";
            RoleComboBox.Size = new Size(121, 23);
            RoleComboBox.TabIndex = 11;
            RoleComboBox.SelectedIndexChanged += RoleComboBox_SelectedIndexChanged;
            // 
            // AddPasswordLabel
            // 
            AddPasswordLabel.AutoSize = true;
            AddPasswordLabel.Location = new Point(25, 243);
            AddPasswordLabel.Name = "AddPasswordLabel";
            AddPasswordLabel.Size = new Size(70, 15);
            AddPasswordLabel.TabIndex = 7;
            AddPasswordLabel.Text = "Contraseña:";
            // 
            // AddPasswordTextbox
            // 
            AddPasswordTextbox.Location = new Point(25, 261);
            AddPasswordTextbox.Name = "AddPasswordTextbox";
            AddPasswordTextbox.PasswordChar = '*';
            AddPasswordTextbox.Size = new Size(392, 23);
            AddPasswordTextbox.TabIndex = 8;
            AddPasswordTextbox.TextChanged += AddPasswordTextbox_TextChanged;
            // 
            // StatusCheckBox
            // 
            StatusCheckBox.AutoSize = true;
            StatusCheckBox.Location = new Point(26, 296);
            StatusCheckBox.Name = "StatusCheckBox";
            StatusCheckBox.Size = new Size(81, 19);
            StatusCheckBox.TabIndex = 10;
            StatusCheckBox.Text = "Habilitado";
            StatusCheckBox.UseVisualStyleBackColor = true;
            // 
            // CareerText
            // 
            CareerText.AutoSize = true;
            CareerText.Location = new Point(25, 155);
            CareerText.Name = "CareerText";
            CareerText.Size = new Size(45, 15);
            CareerText.TabIndex = 14;
            CareerText.Text = "Carrera";
            // 
            // comboBoxCarrera
            // 
            comboBoxCarrera.FormattingEnabled = true;
            comboBoxCarrera.Location = new Point(26, 173);
            comboBoxCarrera.Name = "comboBoxCarrera";
            comboBoxCarrera.Size = new Size(121, 23);
            comboBoxCarrera.TabIndex = 15;
            comboBoxCarrera.SelectedIndexChanged += comboBoxCarrera_SelectedIndexChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(25, 199);
            label1.Name = "label1";
            label1.Size = new Size(46, 15);
            label1.TabIndex = 16;
            label1.Text = "Escuela";
            // 
            // comboBoxEscuela
            // 
            comboBoxEscuela.FormattingEnabled = true;
            comboBoxEscuela.Location = new Point(26, 217);
            comboBoxEscuela.Name = "comboBoxEscuela";
            comboBoxEscuela.Size = new Size(121, 23);
            comboBoxEscuela.TabIndex = 17;
            comboBoxEscuela.SelectedIndexChanged += comboBox1_SelectedIndexChanged_1;
            // 
            // AddUser
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(459, 361);
            Controls.Add(comboBoxEscuela);
            Controls.Add(label1);
            Controls.Add(comboBoxCarrera);
            Controls.Add(CareerText);
            Controls.Add(StatusCheckBox);
            Controls.Add(AddPasswordTextbox);
            Controls.Add(AddPasswordLabel);
            Controls.Add(RoleComboBox);
            Controls.Add(AddRoleLabel);
            Controls.Add(AddEmailTextbox);
            Controls.Add(AddEmailLabel);
            Controls.Add(button1);
            Controls.Add(AddNombreLabel);
            Controls.Add(AddNameTextbox);
            Name = "AddUser";
            Text = "AddUser";
            Load += AddUser_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox AddNameTextbox;
        private Label AddNombreLabel;
        private Button button1;
        private Label AddEmailLabel;
        private TextBox AddEmailTextbox;
        private Label AddRoleLabel;
        private ComboBox RoleComboBox;
        private Label AddPasswordLabel;
        private TextBox AddPasswordTextbox;
        private CheckBox StatusCheckBox;
        private Label CareerText;
        private ComboBox comboBoxCarrera;
        private Label label1;
        private ComboBox comboBoxEscuela;
    }
}