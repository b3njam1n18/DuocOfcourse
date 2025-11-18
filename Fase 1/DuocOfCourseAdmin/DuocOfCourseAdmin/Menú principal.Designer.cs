namespace DuocOfCourseAdmin
{

    partial class Menú_principal
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
            MenuPanel = new Panel();
            ManageButton = new Button();
            LogoutButton = new Button();
            panel1 = new Panel();
            panel2 = new Panel();
            textBox1 = new TextBox();
            panel3 = new Panel();
            dataGridView1 = new DataGridView();
            Nombre = new DataGridViewTextBoxColumn();
            Correo = new DataGridViewTextBoxColumn();
            Rol = new DataGridViewTextBoxColumn();
            Activo = new DataGridViewTextBoxColumn();
            button1 = new Button();
            label1 = new Label();
            label2 = new Label();
            MenuPanel.SuspendLayout();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // MenuPanel
            // 
            MenuPanel.BackColor = Color.FromArgb(1, 44, 86);
            MenuPanel.Controls.Add(ManageButton);
            MenuPanel.Controls.Add(LogoutButton);
            MenuPanel.Dock = DockStyle.Left;
            MenuPanel.Location = new Point(0, 0);
            MenuPanel.Name = "MenuPanel";
            MenuPanel.Size = new Size(200, 640);
            MenuPanel.TabIndex = 0;
            // 
            // ManageButton
            // 
            ManageButton.Anchor = AnchorStyles.Top;
            ManageButton.BackColor = Color.FromArgb(253, 181, 34);
            ManageButton.Location = new Point(37, 38);
            ManageButton.Name = "ManageButton";
            ManageButton.Size = new Size(129, 41);
            ManageButton.TabIndex = 2;
            ManageButton.Text = "Modificar";
            ManageButton.UseVisualStyleBackColor = false;
            // 
            // LogoutButton
            // 
            LogoutButton.Anchor = AnchorStyles.Bottom;
            LogoutButton.BackColor = Color.FromArgb(253, 181, 34);
            LogoutButton.Location = new Point(37, 568);
            LogoutButton.Name = "LogoutButton";
            LogoutButton.Size = new Size(129, 41);
            LogoutButton.TabIndex = 1;
            LogoutButton.Text = "Cerrar sesión";
            LogoutButton.UseVisualStyleBackColor = false;
            LogoutButton.Click += LogoutButton_Click;
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Right;
            panel1.BackColor = SystemColors.ControlLight;
            panel1.Controls.Add(panel2);
            panel1.Location = new Point(245, 133);
            panel1.Name = "panel1";
            panel1.Size = new Size(880, 446);
            panel1.TabIndex = 1;
            // 
            // panel2
            // 
            panel2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panel2.BackColor = Color.FromArgb(43, 93, 162);
            panel2.Controls.Add(textBox1);
            panel2.Controls.Add(panel3);
            panel2.Location = new Point(18, 16);
            panel2.Name = "panel2";
            panel2.Size = new Size(848, 415);
            panel2.TabIndex = 0;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(21, 25);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(485, 23);
            textBox1.TabIndex = 1;
            textBox1.Text = "Buscador";
            textBox1.TextChanged += textBox1_TextChanged;
            // 
            // panel3
            // 
            panel3.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panel3.BackColor = Color.White;
            panel3.Controls.Add(dataGridView1);
            panel3.Controls.Add(button1);
            panel3.Location = new Point(21, 65);
            panel3.Name = "panel3";
            panel3.Size = new Size(809, 332);
            panel3.TabIndex = 0;
            panel3.Paint += panel3_Paint;
            // 
            // dataGridView1
            // 
            dataGridView1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { Nombre, Correo, Rol, Activo });
            dataGridView1.Location = new Point(27, 55);
            dataGridView1.MultiSelect = false;
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 70;
            dataGridView1.Size = new Size(760, 255);
            dataGridView1.TabIndex = 1;
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
            // 
            // Nombre
            // 
            Nombre.HeaderText = "Nombre";
            Nombre.Name = "Nombre";
            // 
            // Correo
            // 
            Correo.HeaderText = "Correo electrónico";
            Correo.Name = "Correo";
            // 
            // Rol
            // 
            Rol.HeaderText = "Rol";
            Rol.Name = "Rol";
            // 
            // Activo
            // 
            Activo.HeaderText = "Activo";
            Activo.Name = "Activo";
            // 
            // button1
            // 
            button1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            button1.BackColor = Color.FromArgb(43, 93, 162);
            button1.ForeColor = SystemColors.ButtonFace;
            button1.Location = new Point(655, 13);
            button1.Name = "button1";
            button1.Size = new Size(120, 36);
            button1.TabIndex = 0;
            button1.Text = "Agregar usuario";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(233, 38);
            label1.Name = "label1";
            label1.Size = new Size(0, 15);
            label1.TabIndex = 2;
            label1.Click += label1_Click;
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Right;
            label2.AutoSize = true;
            label2.Location = new Point(1001, 38);
            label2.Name = "label2";
            label2.Size = new Size(124, 15);
            label2.TabIndex = 3;
            label2.Text = "DuocOfCourse Admin";
            label2.TextAlign = ContentAlignment.TopRight;
            // 
            // Menú_principal
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1156, 640);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(panel1);
            Controls.Add(MenuPanel);
            Name = "Menú_principal";
            Text = "Menú_principal";
            Load += Menú_principal_Load_1;
            MenuPanel.ResumeLayout(false);
            panel1.ResumeLayout(false);
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel MenuPanel;
        private Button LogoutButton;
        private Panel panel1;
        private Panel panel2;
        private Panel panel3;
        private Button button1;
        private Label label1;
        private Label label2;
        private DataGridView dataGridView1;
        private TextBox textBox1;
        private DataGridViewTextBoxColumn Nombre;
        private DataGridViewTextBoxColumn Correo;
        private DataGridViewTextBoxColumn Rol;
        private DataGridViewTextBoxColumn Activo;
        private Button ManageButton;
    }
}